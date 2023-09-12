using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace E4.SingletonManager
{
    /// <summary>
    /// Singleton Manager에서 Before Scene Loaded 때 생성할 스크립트 및 프리팹을 설정하는 스크립터블 오브젝트
    /// </summary>
    [CreateAssetMenu(fileName = "Global Config", menuName = "SingletonManager/Global Config")]
    public class GlobalConfig : ScriptableObject
    {
        [Header("Config")]
#if UNITY_EDITOR
        [Tooltip("On Before Scene Loaded 때 생성하고자 하는 싱글톤 클래스 스크립트")]
        [SerializeField] List<MonoScript> m_SingletonScripts;
#endif
        [Tooltip("On Before Scene Loaded 때 생성할 싱글톤 프리팹")]
        [SerializeField] List<GameObject> m_SingletonPrefabs;
        
        [Header("Registered Info")]
        [Tooltip("On Before Scene Loaded 때 생성이 확정된 싱글톤 클래스 이름")]
        [SerializeField] List<string> m_RegisteredScripts;

        [Tooltip("On Before Scene Loaded 때 생성할 싱글톤 프리팹")]
        [SerializeField] List<GameObject> m_RegisteredPrefabs;

        public List<string> RegisteredScripts => m_RegisteredScripts;
        public List<GameObject> RegisteredPrefabs => m_RegisteredPrefabs;

#if UNITY_EDITOR
        void OnValidate()
        {
            // 유효한 Singleton Scripts 등록 (MonoScript는 에디터 전용 클래스이므로 string으로 변환하여 저장)
            m_RegisteredScripts = new List<string>();
            foreach (var singletonScript in m_SingletonScripts)
            {
                if(singletonScript)
                    m_RegisteredScripts.Add(singletonScript.GetClass().Name);
            }
            
            // 유효한 Singleton Prefabs 등록
            m_RegisteredPrefabs = new List<GameObject>();
            foreach (var singletonPrefab in m_SingletonPrefabs)
            {
                if(singletonPrefab)
                    m_RegisteredPrefabs.Add(singletonPrefab);
            }

            // 저장
            EditorUtility.SetDirty(this);
        }
#endif
    }
}