using System;
using UnityEngine;

namespace CommonUtility.InspectorField
{
    [Serializable]
    public class Field<T> : Field where T : class
    {
        [SerializeField]
        private UnityEngine.Object _value;

        public T Value => _value as T;

        public override Type ValueType => typeof(T);

        /// <summary>
        /// Calls automatic from editor
        /// </summary>
        public override void Validate()
        {
            if (_value == null)
                return;

            if (typeof(T).IsAssignableFrom(_value.GetType()))
                return;

            if (_value is GameObject obj && obj.TryGetComponent(typeof(T), out Component component))
                _value = component;
            else
                _value = null;
        }
    }

    public abstract class Field
    {
        public abstract Type ValueType { get; }

        public abstract void Validate();
    }
}