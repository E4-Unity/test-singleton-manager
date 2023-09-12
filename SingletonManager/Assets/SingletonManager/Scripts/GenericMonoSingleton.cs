using UnityEngine;

namespace E4.SingletonManager
{
    public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
    {
        static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance is not null) return m_Instance;

                m_Instance = FindObjectOfType<T>();
                if (m_Instance is not null) return m_Instance;

                var instance = new GameObject(typeof(T).Name);
                m_Instance = instance.AddComponent<T>();

                return m_Instance;
            }
        }

        protected virtual void Awake()
        {
            var instance = GetComponent<T>();

            if (m_Instance is null)
                m_Instance = instance;
            else if (m_Instance != instance)
            {
                Destroy(gameObject);
                Debug.LogWarning(gameObject.name + " is destroyed.\n" + m_Instance.gameObject.name + " (" + typeof(T).Name + ")" + " is already exist.");
            }
        }

        protected virtual void OnDestroy()
        {
            if (m_Instance == GetComponent<T>())
                m_Instance = null;
        }
    }
}
