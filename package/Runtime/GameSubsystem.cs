using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
    public abstract class GameSubsystem : MonoSingleton
    {

    }

    public abstract class GameSubsystem<T> : GameSubsystem where T : GameSubsystem<T>
    {
        static T s_Instance;
        public static T Instance => s_Instance;

        /* GameSubsystem */

        public override void Initialize()
        {
            if (IsInitialized) return;

            IsInitialized = true;
            s_Instance = this as T;
            OnInitialize();

            LogSingletonManager.Log(typeof(T).Name + " is initialized.");
        }

        /* MonoBehaviour */

        protected override void Awake()
        {
            base.Awake();

            if (s_Instance is null)
            {
                LogSingletonManager.Log(typeof(T).Name + " is awoken.");

                Initialize();
            }
            else if (s_Instance != this as T)
            {
                LogSingletonManager.Log(typeof(T).Name + " is already exists.");

                Destroy(gameObject);
            }
        }

        protected override void OnDestroy()
        {
            if (s_Instance == this as T)
            {
                s_Instance = null;

                LogSingletonManager.Log(typeof(T).Name + " is destroyed.");
            }

            base.OnDestroy();
        }
    }
}
