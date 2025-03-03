using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Eu4ng.GameInstance
{
    public class GameInstanceSettings : ScriptableObject
    {
        private const string Path = "Assets/Resources/GameInstanceSettings.asset";
        private static GameInstanceSettings Settings;
        private static SerializedObject SerializedSettings;

        [SerializeField]
        private List<GameObject> m_SubsystemPrefabs;

        public static GameInstanceSettings GetOrCreateSettings()
        {
            return Settings ?? LoadSettings() ?? CreateSettings();
        }

        public static SerializedObject GetSerializedSettings()
        {
            return SerializedSettings ?? new SerializedObject(GetOrCreateSettings());
        }

        private static GameInstanceSettings LoadSettings()
        {
            Settings = AssetDatabase.LoadAssetAtPath<GameInstanceSettings>(Path);

            return Settings;
        }

        private static GameInstanceSettings CreateSettings()
        {
            Settings = CreateInstance<GameInstanceSettings>();
            AssetDatabase.CreateAsset(Settings, Path);
            AssetDatabase.SaveAssets();

            return Settings;
        }
    }
}
