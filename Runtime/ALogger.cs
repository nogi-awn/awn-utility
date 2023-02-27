using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwnUtility
{
    public class ALogger : MonoBehaviour
    {
        [SerializeField] private bool showLog = true;
        [SerializeField] private string prefix = "Default";
        [SerializeField] private Color32 color = Color.white;

        public static string GetColorMessage(object message, string color)
        {
            return $"<color={color}>{message}</color>";
        }
        public static string GetHexColor(Color32 color)
        {
            return $"#{color.r.ToString("x2") + color.g.ToString("x2") + color.b.ToString("x2")}";
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void Log(object message, Object sender = null)
        {
            if(showLog && enabled)
                Debug.Log($"{GetColorMessage($"[{prefix}]", GetHexColor(color))} {message}", sender);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public void LogError(object message, Object sender = null)
        {
            if(showLog && enabled)
                Debug.LogError($"{GetColorMessage($"[{prefix}]", GetHexColor(color))} {message}", sender);
        }
    }
}