namespace Eu4ng.Manager.Singleton
{
    /// <summary>
    /// 제네릭 싱글톤 클래스
    /// </summary>
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        static T s_Instance;

        public static T Instance => s_Instance ?? CreateInstance();

        static T CreateInstance()
        {
            s_Instance = new T();
            s_Instance.Initialize();

            return s_Instance;
        }

        bool IsInitialized { get; set; }

        void Initialize()
        {
            if (IsInitialized) return;

            OnInitialize();
        }

        protected abstract void OnInitialize();
    }
}
