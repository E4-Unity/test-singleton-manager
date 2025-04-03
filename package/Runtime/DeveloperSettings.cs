using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using Eu4ng.Utilities.Editor;
#endif

namespace Eu4ng.Manager.Singleton
{
    public abstract class DeveloperSettings : ScriptableObject
    {
        protected const string RESOURCES_PATH = "Assets/Resources";
        protected const string SETTINGS_PATH = "DeveloperSettings";

        protected bool IsInitialized { get; set; }

        protected void Initialize()
        {
            if (IsInitialized) return;

            OnInitialize();
        }

        protected abstract void OnInitialize();

#if UNITY_EDITOR
        public static void CreateDeveloperSettings(Type type)
        {
            // 유효성 검사
            if (type is null) return;

            // 이미 생성된 경우 무시
            var loadedDeveloperSettings = Resources.Load(Path.Combine(SETTINGS_PATH, type.Name));
            if (loadedDeveloperSettings is not null) return;

            // 폴더 생성
            string directory = DirectoryManager.CreateDirectory(RESOURCES_PATH + "/" + SETTINGS_PATH);

            // 스크립터블 오브젝트 생성
            var instance = CreateInstance(type);
            AssetDatabase.CreateAsset(instance, Path.Combine(directory, type.Name + ".asset"));
            AssetDatabase.SaveAssets();

            LogSingletonManager.Log(type.Name + " is created.");
        }
#endif
    }

    public abstract class DeveloperSettings<T> : DeveloperSettings where T : DeveloperSettings<T>
    {
        static T s_Instance;

        public static T Instance => s_Instance ?? LoadScriptableObject();

        static T LoadScriptableObject()
        {
            s_Instance = Resources.Load<T>(Path.Combine(SETTINGS_PATH, typeof(T).Name));

#if UNITY_EDITOR
            if (s_Instance is null) CreateDeveloperSettings(typeof(T));
            s_Instance = Resources.Load<T>(Path.Combine(SETTINGS_PATH, typeof(T).Name));
#endif

            return s_Instance;
        }
    }
}
