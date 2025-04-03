using Eu4ng.Manager.Singleton;
using UnityEngine;

public enum SampleSceneType
{
    MainMenu,
    Lobby
}

[CreateAssetMenu(fileName = "SampleScenePrefabsConfig", menuName = "Scriptable Objects/SingletonManager/SampleScenePrefabsConfig")]
public class SampleScenePrefabsConfig : ScenePrefabsConfig<SampleSceneType>
{

}
