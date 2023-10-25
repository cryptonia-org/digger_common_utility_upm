namespace CommonUtility.TypedPlayerPrefs
{
    public interface IData<T> : IStorage<T>
    {
        public void SetValue(T value);
    }
}