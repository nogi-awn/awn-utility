using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AwnUtility.Editor
{
    public static class LayerNameCreator
    {
        private const string CommandName = "Tools/Create/Layer Name";
        private const string FilePath = "Assets/LayerName.cs";
        private const string Comment = "レイヤー名を定数で管理するクラス";

        [MenuItem(CommandName)]
        public static void Create()
        {
            ConstantClassCreator.Create<int>(
                FilePath,
                InternalEditorUtility.layers.Select(layer =>
                    new ConstantClassCreator.ConstantField(
                        layer,
                        LayerMask.NameToLayer(layer).ToString()
                    )
                ), Comment);
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}