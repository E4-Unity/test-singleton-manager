using UnityEngine;

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
    public class SingletonManager : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoaded()
        {
            CreateGlobalSingletons();
        }

        static void CreateGlobalSingletons()
        {
            // 프로젝트 설정 가져오기
            var settings = SingletonManagerSettings.Instance;

            // 싱글톤 매니저 오브젝트 생성
            var singletonManager = new GameObject("Singleton Manager");
            DontDestroyOnLoad(singletonManager);

            // Global Config에 등록된 프리팹 싱글톤 객체 생성
            foreach (var singletonPrefab in settings.SingletonPrefabs)
            {
                var singleton = Instantiate(singletonPrefab, singletonManager.transform);
            }
        }
    }
}
