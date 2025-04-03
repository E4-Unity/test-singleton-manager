using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Eu4ng.Manager.Singleton.Editor
{
    public static class DeveloperSettingsEditor
    {
        [MenuItem("Tools/DeveloperSettings/Generate/All")]
        static void GenerateAllDeveloperSettings()
        {
            var developerSettingsTypes = GetDeveloperSettingTypes();
            foreach (var developerSettingsType in developerSettingsTypes)
            {
                DeveloperSettings.CreateDeveloperSettings(developerSettingsType);
            }
        }

        static IEnumerable<Type> GetDeveloperSettingTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(DeveloperSettings)));
        }

        [MenuItem("Tools/DeveloperSettings/Generate/SingletonManagerSettings")]
        static void GenerateSingletonManagerSettings() => DeveloperSettings.CreateDeveloperSettings(typeof(SingletonManagerSettings));
    }
}
