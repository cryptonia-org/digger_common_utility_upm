using System;

namespace CommonUtility.TypedPlayerPrefs
{
    public interface IStorage<out T>
    {
        public bool HasValue { get; }

        public T Value { get; }

        public IDisposable AddListener(Action<T> onChanged);
    }
}