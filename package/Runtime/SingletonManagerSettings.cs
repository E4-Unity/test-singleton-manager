using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Eu4ng.Manager.Singleton
{
    public class SingletonManagerSettings : ScriptableObject
    {
        private const string Path = "Assets/Resources/SingletonManagerSettings.asset";
        private static SingletonManagerSettings Settings;

#if UNITY_EDITOR
        private static SerializedObject SerializedSettings;
#endif

        [SerializeField]
        private List<GameObject> m_SingletonPrefabs;

        public List<GameObject> SingletonPrefabs => m_SingletonPrefabs;

        public static SingletonManagerSettings GetOrCreateSettings()
        {
#if UNITY_EDITOR
            return Settings ?? LoadSettings() ?? CreateSettings();
#else
            return Settings ?? LoadSettings();
#endif
        }

        private static SingletonManagerSettings LoadSettings()
        {
            Settings = Resources.Load<SingletonManagerSettings>("SingletonManagerSettings");

            return Settings;
        }

#if UNITY_EDITOR
        private static SingletonManagerSettings CreateSettings()
        {
            Settings = CreateInstance<SingletonManagerSettings>();
            AssetDatabase.CreateAsset(Settings, Path);
            AssetDatabase.SaveAssets();

            return Settings;
        }

        public static SerializedObject GetSerializedSettings()
        {
            return SerializedSettings ?? new SerializedObject(GetOrCreateSettings());
        }
#endif
    }
}
