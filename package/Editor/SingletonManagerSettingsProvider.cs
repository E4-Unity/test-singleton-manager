using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Eu4ng.Manager.Singleton.Editor
{
    public class SingletonManagerSettingsProvider : SettingsProvider
    {
        class Styles
        {
            public static GUIContent SingletonPrefabs = new GUIContent("Singleton Prefabs");
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
            var serializedSettings = SingletonManagerSettings.GetSerializedSettings();
            serializedSettings.Update();

            EditorGUILayout.PropertyField(serializedSettings.FindProperty("m_SingletonPrefabs"), Styles.SingletonPrefabs);

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
