using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
    public abstract class MonoSingleton : MonoBehaviour
    {
        bool IsInitialized { get; set; }

        /* MonoSingleton */

        public void Initialize()
        {
            if (IsInitialized) return;

            LogSingletonManager.Log(gameObject.name + " is initialized.");
            IsInitialized = true;
            OnInitialize();
        }

        protected abstract void OnInitialize();

        public virtual bool IsSubsystem => false;

        /* MonoBehaviour */

        protected virtual void Awake() {}

        protected virtual void OnEnable() {}

        protected virtual void Start() {}

        protected virtual void FixedUpdate() {}

        protected virtual void Update() {}

        protected virtual void LateUpdate() {}

        protected virtual void OnDisable() {}

        protected virtual void OnDestroy() {}
    }

    /// <summary>
    /// MonoBehaviour를 상속받은 제네릭 싱글톤 클래스
    /// </summary>
    public abstract class MonoSingleton<T> : MonoSingleton where T : MonoSingleton
    {
        static T s_Instance;

        public static T Instance => s_Instance ?? FindInstance() ?? CreateInstance();

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

        /* MonoBehaviour */

        protected override void Awake()
        {
            base.Awake();

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

        protected override void OnDestroy()
        {
            if (s_Instance == GetComponent<T>())
            {
                s_Instance = null;

                LogSingletonManager.Log(typeof(T).Name + " is destroyed.");
            }

            base.OnDestroy();
        }
    }
}
