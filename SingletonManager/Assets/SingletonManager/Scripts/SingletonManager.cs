using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace E4.SingletonManager
{
    /// <summary>
    /// 스크립터블 오브젝트(Global Config)에 등록된 스크립트 및 프리팹을 Before Scene Loaded 때 생성시키는 역할을 한다.
    /// 
    /// 이때 생성된 MonoBehaviour 컴포넌트들의 Awake, OnEnable 이벤트들은 씬에 배치된 모든 오브젝트들보다 먼저 호출됨으로써,
    /// 다른 오브젝트들이 Awake(), OnEnable()에서 싱글톤 객체를 참조하더라도 Null 레퍼런스 오류를 방지할 수 있다.
    /// 
    /// 생성할 싱글톤 클래스는 E4.SingletonManager.GenericMonoSingleton을 상속받아도 되고,
    /// 직접 구현한 싱글톤 클래스를 사용해도 무방하다. 단, 생성된 싱글톤 오브젝트들은 모두 DontDestroyOnLoad 처린된다.
    /// </summary>
    public class SingletonManager
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoaded()
        {
            CreateGlobalSingletons();
        }

        static void CreateGlobalSingletons()
        {
            // 스크립터블 오브젝트(Global Config) 로드
            var globalConfig = Resources.Load<GlobalConfig>("SingletonManager/Global Config");

            // 싱글톤 매니저 오브젝트 생성
            var singletonManager = new GameObject("Singleton Manager");
            Object.DontDestroyOnLoad(singletonManager);
            
            // Global Config에 등록된 싱글톤 객체 생성
            foreach (var singletonClass in globalConfig.RegisteredScripts)
            {
                var singleton = new GameObject(singletonClass);
                singleton.AddComponent(Type.GetType(singletonClass));
                singleton.transform.parent = singletonManager.transform;
            }
            
            // Global Config에 등록된 프리팹 싱글톤 객체 생성
            foreach (var singletonPrefab in globalConfig.RegisteredPrefabs)
            {
                var singleton = Object.Instantiate(singletonPrefab, singletonManager.transform);
                singleton.name = singletonPrefab.name;
            }
        }
    }
}