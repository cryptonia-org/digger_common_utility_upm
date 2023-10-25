using System;
using DisposableSubscriptions;

namespace CommonUtility.TypedPlayerPrefs
{
    public class PlayerPrefsData<T> : IData<T> where T : struct
    {
        private readonly string _key;
        private readonly Event<T> _event = new Event<T>();

        private bool _loaded;
        private T _value;

        public T Value
        {
            get
            {
                if (_loaded == false)
                {
                    _value = PlayerPrefsExtra.GetObject<T>(_key);
                    _loaded = true;
                }

                return _value;
            }
        }

        public bool HasValue => PlayerPrefsExtra.HasKey<T>(_key);

        public PlayerPrefsData(string key, T defaultValue = default)
        {
            _key = key;
            if (HasValue == false)
                SetValue(defaultValue);
        }

        public void SetValue(T value)
        {
            PlayerPrefsExtra.SetObject<T>(_key, Validate(value));
            _value = value;
            _event.Update(_value);
        }

        public IDisposable AddListener(Action<T> onChanged) => _event.Subscribe(onChanged);

        protected virtual T Validate(T value) => value;
    }
}