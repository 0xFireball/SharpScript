using System.Collections.Generic;

namespace SharpScript
{
    public class GlobalVariableCollection
    {
        private Dictionary<string, dynamic> _internalDict = new Dictionary<string, dynamic>();

        public dynamic this[string key]
        {
            get
            {
                return _internalDict[key];
            }

            set
            {
                _internalDict[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return _internalDict.Count;
            }
        }

        public bool IsReadOnly => false;

        public ICollection<string> Keys
        {
            get
            {
                return _internalDict.Keys;
            }
        }

        public ICollection<dynamic> Values
        {
            get
            {
                return _internalDict.Values;
            }
        }

        public void Add(KeyValuePair<string, dynamic> item)
        {
            _internalDict.Add(item.Key, item.Value);
        }

        public void Add(string key, dynamic value)
        {
            _internalDict.Add(key, value);
        }

        public void Clear()
        {
            _internalDict.Clear();
        }

        public bool Contains(KeyValuePair<string, dynamic> item)
        {
            return ContainsKey(item.Key);
        }

        public bool ContainsKey(string key)
        {
            return _internalDict.ContainsKey(key);
        }

        public bool Remove(KeyValuePair<string, dynamic> item)
        {
            return Remove(item.Key);
        }

        public bool Remove(string key)
        {
            _internalDict.Remove(key);
            return true;
        }
    }
}