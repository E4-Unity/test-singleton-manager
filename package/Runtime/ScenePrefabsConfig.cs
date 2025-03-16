using System;
using System.Collections.Generic;
using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
    [Serializable]
    public struct ScenePrefabsData<T> where T : struct, IConvertible
    {
        public T BuildIndex;

        public List<GameObject> Prefabs;
    }

    [Serializable]
    public abstract class ScenePrefabsConfig : ScriptableObject
    {
        public virtual Dictionary<int, List<GameObject>> ScenePrefabsDictionary { get; protected set; }
    }

    [Serializable]
    public abstract class ScenePrefabsConfig<T> : ScenePrefabsConfig where T : struct, IConvertible
    {
        [SerializeField] List<ScenePrefabsData<T>> m_ScenePrefabsList = new List<ScenePrefabsData<T>>();

        Dictionary<int, List<GameObject>> m_ScenePrefabsDictionary = new Dictionary<int, List<GameObject>>();

        public override Dictionary<int, List<GameObject>> ScenePrefabsDictionary
        {
            get
            {
                if (m_ScenePrefabsDictionary.Count == 0)
                {
                    foreach (var scenePrefabsMappingData in m_ScenePrefabsList)
                    {
                        m_ScenePrefabsDictionary.TryAdd(Convert.ToInt32(scenePrefabsMappingData.BuildIndex), scenePrefabsMappingData.Prefabs);
                    }
                }

                return m_ScenePrefabsDictionary;
            }
        }
    }
}
