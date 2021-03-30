using System;
using System.Collections.Generic;

namespace MetricsLibrary
{
    public class Dimensions
    {
        private const int HashSalt = 17;
        private ValueTuple<string, string>[] _array;

        public Dimensions(List<ValueTuple<string, string>> valueTuples)
        {
            _array = new (string, string)[valueTuples.Count];
            
            int index = 0;
            foreach (var tuple in valueTuples)
            {
                _array[index++] = tuple;
            }

            // Sort array and use binary search to find an element
            // Then use the strings to build the hashes instead of using HashCodes.

            // Hash codes aren't unique and can't be used for our purpose
            HashCode = GetHash();
            KeysHashCode = GetKeysHash();
        }


        // If we want to avoid List, we can also take
        // public Dimensions(ValueTuple<string, string> valueTuple1)
        // public Dimensions(ValueTuple<string, string> valueTuple1, ValueTuple<string, string> valueTuple2)
        // public Dimensions(ValueTuple<string, string> valueTuple1, ValueTuple<string, string> valueTuple2, ValueTuple<string, string> valueTuple3)
        // ... upto max supported dimensions

        // OR we can even break tuples down into
        // public Dimensions(key1, value1, key2, value2, key3, value3 ...)

        // Make it private and expose the GetHashCode override instead?
        public int HashCode { get; private set; }

        public int KeysHashCode { get; private set; }

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
                        // Update the hash code 
                        int hashCode = HashCode - getHashForElementAt(i);
                        
                        // Assign the new value
                        _array[i].Item2 = value;
                        
                        // Update the hash code to include the new value 
                        HashCode = hashCode + getHashForElementAt(i);
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

        private int GetHash()
        {
            int hashCode = 0;
            for (int i = 0; i < _array.Length; i++)
            {
                hashCode = hashCode + getHashForElementAt(i);
            }

            return hashCode;
        }

        private int getHashForElementAt(int index)
        {
            var keyHashCode = _array[index].Item1.GetHashCode(StringComparison.OrdinalIgnoreCase);
            var valueHashCode = _array[index].Item2.GetHashCode(StringComparison.OrdinalIgnoreCase);
            return (keyHashCode + HashSalt) * (keyHashCode + valueHashCode);

            // return index * (_array[index].Item1.GetHashCode(StringComparison.OrdinalIgnoreCase) + _array[index].Item2.GetHashCode(StringComparison.OrdinalIgnoreCase));
        }
    }
}
