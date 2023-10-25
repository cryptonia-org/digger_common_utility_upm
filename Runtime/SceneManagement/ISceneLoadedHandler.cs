namespace CommonUtility.SceneManagement
{
    public interface ISceneLoadedHandler<T> : ISceneLoadedHandler where T : ISceneArgs
    {
        public void OnSceneLoaded(T args);
    }

    public interface ISceneLoadedHandler { }
}