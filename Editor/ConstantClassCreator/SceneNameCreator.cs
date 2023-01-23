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
            ConstantClassCreator.Create<string>(
                setting.filePath,
                EditorBuildSettings.scenes
                    .Select(c => Path.GetFileNameWithoutExtension(c.path))
                    .Distinct()
                    .Select(scene => 
                        new ConstantClassCreator.ConstantField(
                            scene,
                            $"\"{scene}\""
                        )
                    ),
                setting.comment);
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}