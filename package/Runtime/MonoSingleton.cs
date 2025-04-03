using Eu4ng.Utilities;
using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
    public abstract class MonoSingleton : MonoBehaviour
    {
        [field: SerializeField, ReadOnly] protected bool IsInitialized { get; set; }

        /* MonoSingleton */

        public abstract void Initialize();

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
            // Find
            var instance = FindFirstObjectByType<T>();
            if (instance is null) return null;

            // Initialize
            LogSingletonManager.Log(typeof(T).Name + " is found.");
            instance.Initialize();

            return instance;
        }

        static T CreateInstance()
        {
            // Create
            var gameObject = new GameObject(typeof(T).Name);
            var instance = gameObject.AddComponent<T>();

            // Initialize
            LogSingletonManager.Log(typeof(T).Name + " is created.");
            instance.Initialize();

            return instance;
        }

        public override void Initialize()
        {
            if (IsInitialized) return;

            IsInitialized = true;
            s_Instance = this as T;
            OnInitialize();

            LogSingletonManager.Log(gameObject.name + " is initialized.");
        }

        /* MonoBehaviour */

        protected override void Awake()
        {
            base.Awake();

            // Subsystem should be initialized manually
            if (IsSubsystem) return;

            var instance = GetComponent<T>();

            if (s_Instance is null)
            {
                LogSingletonManager.Log(typeof(T).Name + " is awoken.");

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
