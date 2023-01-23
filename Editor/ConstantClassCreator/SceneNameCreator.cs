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
        private const string CommandName = "Tools/Create/Scene Name";
        private const string FilePath = "Assets/SceneName.cs";
        private const string Comment = "シーン名を定数で管理するクラス";

        [MenuItem(CommandName)]
        public static void Create()
        {
            ConstantClassCreator.Create<string>(
                FilePath,
                EditorBuildSettings.scenes
                    .Select(c => Path.GetFileNameWithoutExtension(c.path))
                    .Distinct()
                    .Select(scene => 
                        new ConstantClassCreator.ConstantField(
                            scene,
                            $"\"{scene}\""
                        )
                    ),
                Comment);
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}