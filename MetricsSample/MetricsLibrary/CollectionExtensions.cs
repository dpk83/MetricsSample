// © Microsoft Corporation. All rights reserved.

using System.Collections.Generic;

namespace MetricsLibrary
{
    internal static class CollectionsExtensions
    {
        public static (string key, string value)[] MergeWith(this (string key, string value)[] valuesToStartWith,
            ICollection<(string key, string value)> valuesToOverrideWith, IDictionary<string, int> keyIndexMap,
            (string key, string value)[] buffer)
        {
            valuesToStartWith.CopyTo(buffer, 0);

            foreach (var dimension in valuesToOverrideWith)
            {
                buffer[keyIndexMap[dimension.key]] = dimension;
            }

            return buffer;
        }

        public static (string key, string value)[] AddMissingValuesFromKeys(this ICollection<(string key, string value)>? fixedDimensions,
            ICollection<string> allowedKeys, bool fixedDimensionsAreNullOrEmpty)
        {
            var container = new (string key, string value)[allowedKeys.Count];
            var index = 0;

            if (fixedDimensionsAreNullOrEmpty)
            {
                foreach (var key in allowedKeys)
                {
                    container[index] = (key, string.Empty);
                    index++;
                }

                return container;
            }

            var found = false;
            foreach (var key in allowedKeys)
            {
                foreach (var dimension in fixedDimensions!)
                {
                    if (dimension.key == key)
                    {
                        container[index] = dimension;
                        index++;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    container[index] = (key, string.Empty);
                    index++;
                }

                found = false;
            }

            return container;
        }
    }
}