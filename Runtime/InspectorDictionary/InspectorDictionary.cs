using System;
using System.Collections.Generic;
using UnityEngine;

namespace CommonUtility
{
    [Serializable]
    public class InspectorDictionary<TKey, TValue>
    {
        [Serializable]
        private class Pair<TPairKey, TPairValue>
        {
            public TPairKey Key;
            public TPairValue Value;
        }

        [SerializeField, InspectorDictionaryPair]
        private List<Pair<TKey, TValue>> _values;

        private Dictionary<TKey, TValue> _dictionary;

        public TValue this[TKey key] => AsDictionary()[key];

        public IReadOnlyDictionary<TKey, TValue> AsDictionary()
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<TKey, TValue>();
                for (int i = 0; i < _values.Count; i++)
                    _dictionary.Add(_values[i].Key, _values[i].Value);
            }

            return _dictionary;
        }
    }
}