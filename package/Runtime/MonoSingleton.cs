using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
    /// <summary>
    /// MonoBehaviour를 상속받은 제네릭 싱글톤 클래스
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        static T s_Instance;

        public static T Instance => s_Instance ?? FindInstance() ?? CreateInstance();

        bool IsInitialized { get; set; }

        /* MonoSingleton */

        static T FindInstance()
        {
            s_Instance = FindFirstObjectByType<T>();
            if (s_Instance != null)
            {
                LogSingletonManager.Log(typeof(T).Name + " is found.");

                s_Instance.Initialize();
            }

            return s_Instance;
        }

        static T CreateInstance()
        {
            var instance = new GameObject(typeof(T).Name);
            s_Instance = instance.AddComponent<T>();

            LogSingletonManager.Log(typeof(T).Name + " is created.");

            s_Instance.Initialize();

            return s_Instance;
        }

        void Initialize()
        {
            if (IsInitialized) return;

            LogSingletonManager.Log(typeof(T).Name + " is initialized.");
            IsInitialized = true;
            OnInitialize();
        }

        protected abstract void OnInitialize();

        /* MonoBehaviour */

        protected virtual void Awake()
        {
            var instance = GetComponent<T>();

            if (s_Instance is null)
            {
                LogSingletonManager.Log(typeof(T).Name + " is awoken.");

                s_Instance = instance;
                Initialize();
            }
            else if (s_Instance != instance)
            {
                LogSingletonManager.Log(typeof(T).Name + " is already exists.");

                Destroy(gameObject);
            }
        }

        protected virtual void OnEnable() {}

        protected virtual void Start() {}

        protected virtual void FixedUpdate() {}

        protected virtual void Update() {}

        protected virtual void LateUpdate() {}

        protected virtual void OnDisable() {}

        protected virtual void OnDestroy()
        {
            if (s_Instance == GetComponent<T>())
            {
                s_Instance = null;

                LogSingletonManager.Log(typeof(T).Name + " is destroyed.");
            }
        }
    }
}
