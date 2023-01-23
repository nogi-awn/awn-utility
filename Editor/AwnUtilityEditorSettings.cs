using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AwnUtility.Editor
{
    [System.Serializable]
    public class ConstantClassSetting
    {
        public ConstantClassSetting(string className, string comment)
        {
            this.className = className;
            this.comment = comment;
        }
        public string className;
        public string comment;

        public string filePath => AwnUtilityEditorSettings.instance.constantClassPath + className + ".cs";
    }

    [FilePath("ProjectSettings/AwnUtilitySettings.asset", FilePathAttribute.Location.ProjectFolder)]
    public class AwnUtilityEditorSettings : ScriptableSingleton<AwnUtilityEditorSettings>
    {
        [Header("Constant Class Settings")]
        public string constantClassPath = @"Assets/";
        public ConstantClassSetting
            layerNameClass = new ConstantClassSetting("LayerName", "レイヤー名を定数で管理するクラス"),
            sceneNameClass = new ConstantClassSetting("SceneName", "シーン名を定数で管理するクラス"),
            tagNameClass = new ConstantClassSetting("TagName", "タグ名を定数で管理するクラス");
        
        public void Save()
        {
            base.Save(true);
        }
    }

    public class AwnUtilityEditorSettingsProvider : SettingsProvider
    {
        private const string SettingPath = "Awn Utility/Editor";
        private UnityEditor.Editor settingsEditor;

        public AwnUtilityEditorSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords) { }
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new AwnUtilityEditorSettingsProvider(SettingPath, SettingsScope.Project, new string[]{"AwnUtility"});
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            var settings = AwnUtilityEditorSettings.instance;
            settings.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;
            UnityEditor.Editor.CreateCachedEditor(settings, null, ref settingsEditor);
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUI.BeginChangeCheck();
            settingsEditor.OnInspectorGUI();
            if(EditorGUI.EndChangeCheck())
            {
                AwnUtilityEditorSettings.instance.Save();
            }
        }
    }
}