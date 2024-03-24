using System.Collections.Generic;
using UnityEngine;

namespace CommonUtility.Catalogs
{
    public abstract class Catalog<TUnit, TInetrfaceUnit, TKey> : ScriptableObject where TUnit : ScriptableObject, TInetrfaceUnit
    {
        [SerializeField]
        private List<TUnit> _units;
        [Space]
        [SerializeField]
        private TUnit _default;

        public IReadOnlyList<TInetrfaceUnit> Collection => _units;

        protected abstract bool Predicate(TUnit unit, TKey key);

        public TInetrfaceUnit this[TKey key] => Get(key);

        public TInetrfaceUnit Get(TKey key)
        {
            TUnit unit = _units.Find((unit) => Predicate(unit, key));
            if (unit == null)
                return _default;

            return unit;
        }
    }
}