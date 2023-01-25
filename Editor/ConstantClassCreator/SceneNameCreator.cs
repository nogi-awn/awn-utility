using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AwnUtility.Editor
{
    public static class SceneNameCreator
    {
        private const string CommandName = "Tools/Create/Scene Name Class";

        [MenuItem(CommandName)]
        public static void Create()
        {
            ConstantClassSetting setting = AwnUtilityEditorSettings.instance.sceneNameClass;
            var constantClass = new ConstantClassCreator(setting.className, setting.comment);

            EditorBuildSettings.scenes
                .Select(c => Path.GetFileNameWithoutExtension(c.path))
                .Distinct()
                .ForEach(
                    sceneName => constantClass.AddConstantField<string>(sceneName, $"\"{sceneName}\"")
                );
            constantClass.Create(AwnUtilityEditorSettings.instance.constantClassPath);
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}