using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eu4ng.Manager.Singleton
{
    /// <summary>
    /// SingletonManagerSettings에 등록된 프리팹들을 Before Scene Loaded 때 DontDestroyOnLoad 방식으로 생성합니다.
    ///
    /// 이때 생성된 MonoBehaviour 컴포넌트들의 Awake, OnEnable 이벤트들은 씬에 배치된 모든 오브젝트들보다 먼저 호출됨으로써,
    /// 다른 오브젝트들이 Awake(), OnEnable()에서 싱글톤 객체를 참조하더라도 Null 레퍼런스 오류를 방지할 수 있습니다.
    ///
    /// MonoSingleton 클래스를 상속받은 싱글톤 컴포넌트가 부착된 프리팹들만 등록하는 것을 권장드리지만,
    /// 현재는 어떤 종류의 프리팹을 등록해도 동일한 방식으로 동작합니다.
    /// </summary>
    public class SingletonManager : MonoSingleton<SingletonManager>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoaded()
        {
            CreateSingletonManager();
        }

        static void CreateSingletonManager()
        {
            var root = new GameObject("Singleton Manager");
            var singletonManager = root.AddComponent<SingletonManager>();
        }

        GameObject m_GlobalPrefabsRoot;

        GameObject m_ScenePrefabsRoot;

        List<GameObject> m_ScenePrefabInstances = new List<GameObject>();

        int m_CurrentBuildIndex = -1;

        /* MonoSingleton */

        protected override void OnInitialize()
        {
            base.OnInitialize();

            DontDestroyOnLoad(gameObject);

            m_GlobalPrefabsRoot = new GameObject("Global Prefabs");
            m_GlobalPrefabsRoot.transform.SetParent(gameObject.transform);

            m_ScenePrefabsRoot = new GameObject("Scene Prefabs");
            m_ScenePrefabsRoot.transform.SetParent(gameObject.transform);

            CreateGlobalPrefabs();

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        /* SingletonManager */

        void CreateGlobalPrefabs()
        {
            // 설정 가져오기
            var settings = SingletonManagerSettings.Instance;

            // 설정에 등록된 Global Prefabs 생성
            foreach (var globalPrefab in settings.GlobalPrefabs)
            {
                var globalPrefabInstance = Instantiate(globalPrefab, m_GlobalPrefabsRoot.transform);
                globalPrefabInstance.name = globalPrefab.name;

                LogSingletonManager.Log("Global Prefab (" + globalPrefabInstance.name + ") is created.");
            }
        }

        void OnActiveSceneChanged(Scene currentScene, Scene nextScene)
        {
            DestroyScenePrefabs();

            CreateScenePrefabs(nextScene.buildIndex);
        }

        void DestroyScenePrefabs()
        {
            LogSingletonManager.Log("Destroy scene prefabs for " + m_CurrentBuildIndex + ".");

            foreach (var scenePrefabInstance in m_ScenePrefabInstances)
            {
                Destroy(scenePrefabInstance);

                LogSingletonManager.Log("Scene Prefab (" + scenePrefabInstance.name + ") is destroyed.");
            }
            m_ScenePrefabInstances.Clear();
        }

        void CreateScenePrefabs(int buildIndex)
        {
            LogSingletonManager.Log("Create scene prefabs for " + buildIndex + ".");

            m_CurrentBuildIndex = buildIndex;

            var settings = SingletonManagerSettings.Instance;
            var scenePrefabs = settings.GetScenePrefabs(buildIndex);
            foreach (var scenePrefab in scenePrefabs)
            {
                var scenePrefabInstance = Instantiate(scenePrefab, m_ScenePrefabsRoot.transform);
                scenePrefabInstance.name = scenePrefab.name;
                m_ScenePrefabInstances.Add(scenePrefabInstance);

                LogSingletonManager.Log("Scene Prefab (" + scenePrefabInstance.name + ") is created.");
            }
        }
    }
}
