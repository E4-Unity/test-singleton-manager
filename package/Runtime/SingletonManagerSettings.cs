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
        private static SingletonManagerSettings s_Instance;

#if UNITY_EDITOR
        private static SerializedObject SerializedSettings;
#endif

        [SerializeField]
        private List<GameObject> m_SingletonPrefabs;

        public List<GameObject> SingletonPrefabs => m_SingletonPrefabs;

        public static SingletonManagerSettings Instance
        {
            get
            {
#if UNITY_EDITOR
                return s_Instance ?? LoadSettings() ?? CreateSettings();
#else
                return s_Instance ?? LoadSettings();
#endif
            }
        }

        private static SingletonManagerSettings LoadSettings()
        {
            s_Instance = Resources.Load<SingletonManagerSettings>("SingletonManagerSettings");

            return s_Instance;
        }

#if UNITY_EDITOR
        private static SingletonManagerSettings CreateSettings()
        {
            s_Instance = CreateInstance<SingletonManagerSettings>();
            AssetDatabase.CreateAsset(s_Instance, Path);
            AssetDatabase.SaveAssets();

            return s_Instance;
        }

        public static SerializedObject GetSerializedSettings()
        {
            return SerializedSettings ?? new SerializedObject(Instance);
        }
#endif
    }
}
