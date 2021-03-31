using System;
using System.Collections.Generic;
using System.Text;

namespace MetricsLibrary
{
    public class TupleKeyComparer : IComparer<ValueTuple<string, string>>
    {
        public int Compare((string, string) x, (string, string) y)
        {
            return String.Compare(x.Item1, y.Item1, true /* ignoreCase */);
        }
    }

    public class Dimensions
    {
        private ValueTuple<string, string>[] _array;
        private StringBuilder _stringBuilder;
        private bool _isDirty = true;
        private string _valueHashStr = string.Empty;

        // We can avoid List and instead have overloaded constructor
        // public Dimensions(ValueTuple<string, string> valueTuple1)
        // public Dimensions(ValueTuple<string, string> valueTuple1, ValueTuple<string, string> valueTuple2)
        // public Dimensions(ValueTuple<string, string> valueTuple1, ValueTuple<string, string> valueTuple2, ValueTuple<string, string> valueTuple3)
        // ... upto max supported dimensions

        // OR we can even break tuples down into
        // public Dimensions(key1, value1, key2, value2, key3, value3 ...)

        // Constructor takes a list of tuples
        public Dimensions(List<ValueTuple<string, string>> valueTuples)
        {
            _array = new (string, string)[valueTuples.Count];

            int index = 0;
            int capacity = valueTuples.Count * 2;
            foreach (var tuple in valueTuples)
            {
                _array[index++] = tuple;
                capacity += tuple.Item1.Length + tuple.Item2.Length;
            }

            var tupleKeyComparer = new TupleKeyComparer();
            Array.Sort(_array, tupleKeyComparer);

            _stringBuilder = new StringBuilder(capacity);

            // Calculate and update unique hashes for keys and values
            foreach (var entry in _array)
            {
                _stringBuilder.Append(entry.Item1);
                _stringBuilder.Append(":");
            }

            KeyHashStr = _stringBuilder.ToString();

            // Reuse the string builder now for valueHash
            UpdateValueHash();
        }

        public Dimensions(Dimensions sortedDimensions)
        {
            _array = new (string, string)[sortedDimensions.Count];
            for (int i = 0; i < sortedDimensions.Count; i++)
            {
                _array[i] = sortedDimensions._array[i];
            }

            KeyHashStr = sortedDimensions.KeyHashStr;
            _valueHashStr = sortedDimensions._valueHashStr;
            _isDirty = sortedDimensions._isDirty;

            _stringBuilder = new StringBuilder(sortedDimensions._stringBuilder.Capacity);
        }

        public string KeyHashStr { get; }

        public string ValueHashStr
        {
            get 
            {
                if (_isDirty)
                {
                    // _isDirty flag is set, recalculate the hash string
                    UpdateValueHash();

                    _isDirty = false;
                }

                return _valueHashStr;
            }
        }

        public int Count => _array.Length;

        // To allow updating dimension value. dimensions["key"] = "newValue"
        public string this[string key]
        {
            get
            {
                for (int i = 0; i < _array.Length; i++)
                {
                    if (key == _array[i].Item1)
                    {
                        return _array[i].Item2;
                    }
                }

                throw new ArgumentOutOfRangeException(nameof(key));
            }

            set
            {
                for (int i = 0; i < _array.Length; i++)
                {
                    if (key == _array[i].Item1)
                    {
                        // Assign the new value
                        _array[i].Item2 = value;

                        // Mark it dirty so hash is re-calculated on next request
                        _isDirty = true;
                        return;
                    }
                }

                throw new ArgumentOutOfRangeException(nameof(key));
            }
        }

        // Getter to return tuple at given index
        public (string, string) this[int index]
        {
            get
            {
                if (index >= _array.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return _array[index];
            }
        }

        private void UpdateValueHash()
        {
            _stringBuilder.Clear();
            foreach (var entry in _array)
            {
                _stringBuilder.Append(entry.Item2);
                _stringBuilder.Append(":");
            }

            _valueHashStr = _stringBuilder.ToString();
            
            // Hash value is updated, reset isDirty flag
            _isDirty = false;
        }
    }
}
