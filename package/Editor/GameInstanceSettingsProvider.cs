using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Eu4ng.GameInstance.Editor
{
    public class GameInstanceSettingsProvider : SettingsProvider
    {
        class Styles
        {
            public static GUIContent SubsystemPrefabs = new GUIContent("Subsystem Prefabs");
        }
        public GameInstanceSettingsProvider(string path, SettingsScope scope = SettingsScope.Project)
            : base(path, scope) {}

        public static bool IsSettingsAvailable()
        {
            return GameInstanceSettings.GetOrCreateSettings() is not null;
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            // This function is called when the user clicks on the MyCustom element in the Settings window.
        }

        public override void OnGUI(string searchContext)
        {
            var serializedSettings = GameInstanceSettings.GetSerializedSettings();
            serializedSettings.Update();

            EditorGUILayout.PropertyField(serializedSettings.FindProperty("m_SubsystemPrefabs"), Styles.SubsystemPrefabs);

            serializedSettings.ApplyModifiedProperties();
        }

        // Register the SettingsProvider
        [SettingsProvider]
        public static SettingsProvider CreateGameInstanceSettingsProvider()
        {
            if (IsSettingsAvailable())
            {
                // Automatically extract all keywords from the Styles.
                var provider = new GameInstanceSettingsProvider("Project/Game Instance");
                provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();

                return provider;
            }

            // Settings Asset doesn't exist yet; no need to display anything in the Settings window.
            return null;
        }
    }
}
