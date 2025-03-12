using UnityEngine;

namespace Eu4ng.Manager.Singleton
{
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
