using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Eu4ng.Manager.Singleton.Editor
{
    public class SingletonManagerSettingsProvider : SettingsProvider
    {
        class Styles
        {
            public static GUIContent GlobalPrefabs = new GUIContent("Global Prefabs");

            public static GUIContent ScenePrefabsConfig = new GUIContent("Scene Prefabs Config");
        }
        public SingletonManagerSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
            : base(path, scope) {}

        public static bool IsSettingsAvailable()
        {
            return SingletonManagerSettings.Instance is not null;
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            // This function is called when the user clicks on the MyCustom element in the Settings window.
        }

        public override void OnGUI(string searchContext)
        {
            var serializedSettings = SingletonManagerSettings.SerializedSettings;
            serializedSettings.Update();

            EditorGUILayout.PropertyField(serializedSettings.FindProperty("m_GlobalPrefabs"), Styles.GlobalPrefabs);
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("m_ScenePrefabsMappingConfig"), Styles.ScenePrefabsConfig);

            serializedSettings.ApplyModifiedProperties();
        }

        // Register the SettingsProvider
        [SettingsProvider]
        public static SettingsProvider CreateSingletonManagerSettingsProvider()
        {
            if (IsSettingsAvailable())
            {
                // Automatically extract all keywords from the Styles.
                var provider = new SingletonManagerSettingsProvider("Project/Singleton Manager");
                provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();

                return provider;
            }

            // Settings Asset doesn't exist yet; no need to display anything in the Settings window.
            return null;
        }
    }
}
