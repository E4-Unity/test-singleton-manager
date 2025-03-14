using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
    /// <summary>
    /// MonoBehaviour 기반의 제네릭 싱글톤 클래스입니다.
    /// Instance 프로퍼티 호출 시 초기화 완료 상태를 보장합니다.
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T s_Instance;

        public static T Instance
        {
            get
            {
                var instance = s_Instance ?? FindInstance() ?? CreateInstance();
                instance.Initialize();

                return instance;
            }
        }

        public bool IsInitialized { get; private set; }

        /* MonoSingleton */

        private static T FindInstance()
        {
            s_Instance = FindFirstObjectByType<T>();

            return s_Instance;
        }

        private static T CreateInstance()
        {
            var instance = new GameObject(typeof(T).Name);
            s_Instance = instance.AddComponent<T>();

            return s_Instance;
        }

        private void Initialize()
        {
            if (IsInitialized) return;

            Debug.Log(typeof(T).Name + " is initialized.");
            IsInitialized = true;
            OnInitialize();
        }

        protected virtual void OnInitialize() {}

        /* MonoBehaviour */

        protected virtual void Awake()
        {
            var instance = GetComponent<T>();

            if (s_Instance is null)
            {
                s_Instance = instance;
            }
            else if (s_Instance != instance)
            {
                Destroy(gameObject);
                Debug.LogWarning(gameObject.name + " is destroyed.\n" + s_Instance.gameObject.name + " (" + typeof(T).Name + ")" + " is already exist.");
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
            if (s_Instance == GetComponent<T>()) s_Instance = null;
        }
    }
}
