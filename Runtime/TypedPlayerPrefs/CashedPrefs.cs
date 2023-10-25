namespace CommonUtility.TypedPlayerPrefs
{
    public class CashedPrefs<T>
    {
        private string _key = null;
        private bool _loaded = false;
        private T _value = default;
        private T _defaultValue = default;

        public CashedPrefs(string key, T defaultValue)
        {
            _key = key;
            _defaultValue = defaultValue;
        }

        public T Value
        {
            get
            {
                if (_loaded == false)
                    _value = PlayerPrefsExtra.GetObject(_key, _defaultValue);

                return _value;
            }
            set
            {
                if (value != null && value.Equals(_value))
                    return;

                PlayerPrefsExtra.SetObject(_key, value);
                _value = value;
            }
        }
    }
}