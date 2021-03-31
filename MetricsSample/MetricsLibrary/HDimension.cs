using System;
using System.Collections.Generic;

namespace MetricsLibrary
{
    public class HDimensions
    {
        private const int HashSalt = 17;
        private ValueTuple<string, string>[] _array;
        private int[] _hashCodeArray;
        private int _hashCode;

        public HDimensions(List<ValueTuple<string, string>> valueTuples)
        {
            _array = new (string, string)[valueTuples.Count];
            _hashCodeArray = new int[valueTuples.Count];

            int index = 0;
            foreach (var tuple in valueTuples)
            {
                _array[index++] = tuple;
            }

            var tupleKeyComparer = new TupleKeyComparer();
            Array.Sort(_array, tupleKeyComparer);

            // Hash codes aren't unique and can't be used for our purpose
            KeyHashCode = GetKeysHash();

            // Now that the array is sorted, store their hash value
            for (int i = 0; i < _array.Length; i++)
            {
                int tupleHash = getHash(_array[i]);
                _hashCodeArray[i] = tupleHash;
                _hashCode += tupleHash;
            }
        }


        // If we want to avoid List, we can also take
        // public HDimensions(ValueTuple<string, string> valueTuple1)
        // public HDimensions(ValueTuple<string, string> valueTuple1, ValueTuple<string, string> valueTuple2)
        // public HDimensions(ValueTuple<string, string> valueTuple1, ValueTuple<string, string> valueTuple2, ValueTuple<string, string> valueTuple3)
        // ... upto max supported dimensions

        // OR we can even break tuples down into
        // public HDimensions(key1, value1, key2, value2, key3, value3 ...)

        // Make it private and expose the GetHashCode override instead?
        public int HashCode
        {
            get 
            {
                return _hashCode;
            }
        }

        public int KeyHashCode { get; private set; }

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

                        int newHashCode = getHash(_array[i]);
                        _hashCode = _hashCode - _hashCodeArray[i] + newHashCode;
                        _hashCodeArray[i] = newHashCode;
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

        private int GetKeysHash()
        {
            int hashCode = 0;
            for (int i = 0; i < _array.Length; i++)
            {

                var keyHashCode = _array[i].Item1.GetHashCode(StringComparison.OrdinalIgnoreCase);
                hashCode = hashCode + (keyHashCode + HashSalt) * keyHashCode;
            }

            return hashCode;
        }

        private int getHash(ValueTuple<string, string> tuple)
        {
            var keyHashCode = tuple.Item1.GetHashCode(StringComparison.OrdinalIgnoreCase);
            var valueHashCode = tuple.Item2.GetHashCode(StringComparison.OrdinalIgnoreCase);
            return (keyHashCode + HashSalt) * (keyHashCode + valueHashCode);

            // return index * (_array[index].Item1.GetHashCode(StringComparison.OrdinalIgnoreCase) + _array[index].Item2.GetHashCode(StringComparison.OrdinalIgnoreCase));
        }
    }
}