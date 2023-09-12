# Singleton Manager

## 기능 설명
스크립터블 오브젝트(Global Config)에 등록된 스크립트 및 프리팹을 Before Scene Loaded 때 생성시키는 역할을 한다.

이때 생성된 MonoBehaviour 컴포넌트들의 Awake, OnEnable 이벤트들은 씬에 배치된 모든 오브젝트들보다 먼저 호출됨으로써,
다른 오브젝트들이 Awake(), OnEnable()에서 싱글톤 객체를 참조하더라도 Null 레퍼런스 오류를 방지할 수 있다.

생성할 싱글톤 클래스는 E4.SingletonManager.GenericMonoSingleton을 상속받아도 되고,
직접 구현한 싱글톤 클래스를 사용해도 무방하다. 단, 생성된 싱글톤 오브젝트들은 모두 DontDestroyOnLoad 처린된다.

## 사용법
1. SingletonManager v1.0.0.unitypackage 임포트
2. Assets > Resources > SingletonManager > 스크립터블 오브젝트(Global Config) 설정
    - Singleton Scripts에 생성하고자 하는 싱글톤 스크립트 추가
    - Singleton Prefabs에 생성하고자 하는 싱글톤 프리팹 추가
3. 플레이 시 On Before Scene Loaded 때 등록된 싱글톤 오브젝트 및 프리팹 자동 생성

> 설정된 요소들 중 `None`이나 `Missing` 요소를 제외하고 모두 `Registered Info` 항목으로 복사되며, 
이때 등록된 요소들만 생성된다.

## 스크린샷
스크립터블 오브젝트(Global Config) 설정 화면

![image](https://github.com/Eu4ng/unity-singleton-manager/assets/59055049/1ae2cb16-ff54-4c0c-8b14-127378ad3d41)

생성된 싱글톤 오브젝트

![image](https://github.com/Eu4ng/unity-singleton-manager/assets/59055049/8810e807-e168-453e-9cfe-8dc33b2f1502)