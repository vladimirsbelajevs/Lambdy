using System;
using System.Collections.Generic;

namespace Lambdy.Parameters
{
    internal class ParameterTracker
    {
        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        private int _paramIndex;

        private const string _paramPrefix = "@";

        public void AddParameter(
            string key,
            object value)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(value);

            if (key.IndexOf(_paramPrefix, StringComparison.Ordinal) < 0)
            {
                key = $"{_paramPrefix}{key}";
            }
            
            Parameters.Add(key, value);
        }
        
        public string AddParameter(object value)
        {
            ArgumentNullException.ThrowIfNull(value);
            
            var param = GetNextParameterName();
            Parameters.Add(param, value);
            return param;
        }

        private string GetNextParameterName()
        {
            return  $"{_paramPrefix}p{_paramIndex++}";
        }
    }
}
