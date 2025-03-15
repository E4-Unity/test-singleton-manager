using System;
using System.Collections.Generic;
using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
    [Serializable]
    public struct ScenePrefabsMappingData<T> where T : struct, IConvertible
    {
        public T BuildIndex;

        public List<GameObject> Prefabs;
    }

    [Serializable]
    public abstract class ScenePrefabsMappingConfig : ScriptableObject
    {
        public virtual Dictionary<int, List<GameObject>> ScenePrefabsDictionary { get; protected set; }
    }

    [Serializable]
    public abstract class ScenePrefabsMappingConfig<T> : ScenePrefabsMappingConfig where T : struct, IConvertible
    {
        [SerializeField] List<ScenePrefabsMappingData<T>> m_ScenePrefabList = new List<ScenePrefabsMappingData<T>>();

        Dictionary<int, List<GameObject>> m_ScenePrefabDictionary = new Dictionary<int, List<GameObject>>();

        public override Dictionary<int, List<GameObject>> ScenePrefabsDictionary
        {
            get
            {
                if (m_ScenePrefabDictionary.Count == 0)
                {
                    foreach (var scenePrefabsMappingData in m_ScenePrefabList)
                    {
                        m_ScenePrefabDictionary.TryAdd(Convert.ToInt32(scenePrefabsMappingData.BuildIndex), scenePrefabsMappingData.Prefabs);
                    }
                }

                return m_ScenePrefabDictionary;
            }
        }
    }
}
