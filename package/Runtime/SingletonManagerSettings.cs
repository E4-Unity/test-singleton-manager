using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Eu4ng.Manager.Singleton
{
    public class SingletonManagerSettings : DeveloperSettings<SingletonManagerSettings>
    {
        [SerializeField] private List<GameObject> m_GlobalPrefabs = new List<GameObject>();

        public List<GameObject> GlobalPrefabs => m_GlobalPrefabs;

        [SerializeReference] private ScenePrefabsConfig m_ScenePrefabsConfig;

        public Dictionary<int, List<GameObject>> ScenePrefabsDictionary => m_ScenePrefabsConfig == null ? new Dictionary<int, List<GameObject>>() : m_ScenePrefabsConfig.ScenePrefabsDictionary;

        public List<GameObject> GetScenePrefabs(int buildIndex) => ScenePrefabsDictionary.GetValueOrDefault(buildIndex, new List<GameObject>());

#if UNITY_EDITOR
        private static SerializedObject s_SerializedSettings;

        public static SerializedObject SerializedSettings => s_SerializedSettings ?? new SerializedObject(Instance);
#endif
    }
}
