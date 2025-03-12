using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T s_Instance;

        public static T Instance
        {
            get
            {
                // 이미 등록된 경우
                if (s_Instance != null) return s_Instance;

                // 씬에 존재하지만 등록되지 않은 경우
                s_Instance = FindFirstObjectByType<T>();
                if (s_Instance != null) return s_Instance;

                // 씬에 존재하지도 않으며 등록되지도 않은 경우
                var instance = new GameObject(typeof(T).Name);
                s_Instance = instance.AddComponent<T>();

                return s_Instance;
            }
        }

        protected virtual void Awake()
        {
            var instance = GetComponent<T>();

            if (s_Instance == null)
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
