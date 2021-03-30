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

    public class SortedDimensions
    {
        private ValueTuple<string, string>[] _array;
        private StringBuilder _stringBuilder;
        private bool _isDirty = true;
        private string _valueHashStr = string.Empty;

        public SortedDimensions(List<ValueTuple<string, string>> valueTuples)
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
        }
    }
}
