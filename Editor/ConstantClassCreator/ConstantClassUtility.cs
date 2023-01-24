using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AwnUtility.Editor
{
    public static class ConstantClassUtility
    {
        private const string CommandName = "Tools/Create/All Name Class";
        [MenuItem(CommandName)]
        public static void Create()
        {
            LayerNameCreator.Create();
            LayerMaskNameCreator.Create();
            SceneNameCreator.Create();
            TagNameCreator.Create();
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}
