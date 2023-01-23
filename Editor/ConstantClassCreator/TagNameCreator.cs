using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AwnUtility.Editor
{
    public static class TagNameCreator
    {
        private const string CommandName = "Tools/Create/Tag Name";
        private const string FilePath = "Assets/TagName.cs";
        private const string Comment = "タグ名を定数で管理するクラス";

        [MenuItem(CommandName)]
        public static void Create()
        {
            ConstantClassCreator.Create<string>(
                FilePath,
                InternalEditorUtility.tags.Select(tag =>
                    new ConstantClassCreator.ConstantField(
                        tag,
                        $"\"{tag}\""
                    )
                ), Comment);
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}