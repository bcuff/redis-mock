using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisMock
{
    public class RedisValue
    {
        public int IntegerValue
        {
            get
            {
                if (Value is int)
                {
                    return (int)Value;
                }

                int result;
                int.TryParse(TextValue, out result);
                return result;
            }
            set
            {
                Value = value;
            }
        }

        public string TextValue
        {
            get
            {
                return Value.ToString();
            }
            set
            {
                Value = value;
            }
        }

        public object Value { get; set; }

        public SortedDictionary<string, string> SetValue
        {
            get
            {
                return Value as SortedDictionary<string, string>;
            }
            set
            {
                Value = value;
            }
        }

        public SortedDictionary<string, string> ToSet()
        {
            var result = Value as SortedDictionary<string, string>;
            if (result != null) return result;
            result = new SortedDictionary<string, string>();
            Value = result;
            return result;
        }

        public DateTime? ExpirationDate { get; set;  }
    }
}
