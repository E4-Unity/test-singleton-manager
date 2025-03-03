using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Eu4ng.GameInstance
{
    public class GameInstanceSettings : ScriptableObject
    {
        private const string Path = "Assets/Resources/GameInstanceSettings.asset";
        private static GameInstanceSettings Settings;

#if UNITY_EDITOR
        private static SerializedObject SerializedSettings;
#endif

        [SerializeField]
        public List<GameObject> m_SubsystemPrefabs;

        public static GameInstanceSettings GetOrCreateSettings()
        {
#if UNITY_EDITOR
            return Settings ?? LoadSettings() ?? CreateSettings();
#else
            return Settings ?? LoadSettings();
#endif
        }

        private static GameInstanceSettings LoadSettings()
        {
            Settings = Resources.Load<GameInstanceSettings>("GameInstanceSettings");

            return Settings;
        }

#if UNITY_EDITOR
        private static GameInstanceSettings CreateSettings()
        {
            Settings = CreateInstance<GameInstanceSettings>();
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
