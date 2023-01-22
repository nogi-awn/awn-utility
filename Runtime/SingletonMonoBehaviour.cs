using UnityEngine;

namespace AwnUtility
{
    /// <summary>
    /// 唯一のグローバルなアクセスポイントを提供するMonoBehaviour
    /// </summary>
    /// <typeparam name="T">このクラスの継承先</typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T instanceCache;

        /// <summary>
        /// インスタンスの参照。存在しない場合は検索・初期化して取得する
        /// </summary>
        public static T instance
        {
            get
            {
                if(instanceCache == null)
                {
                    instanceCache = (T)FindObjectOfType(typeof(T));

                    if(instanceCache == null)
                    {
                        Debug.LogError(typeof(T) + " is not found.");
                    }
                    instanceCache.Init();
                }
                return instanceCache;
            }
        }
        /// <summary>
        /// シングルトンの初期化を行う関数。Awake時又はそれ以前にinstanceが参照された時点で呼び出される
        /// </summary>
        protected virtual void Init() { }

        private void Awake()
        {
            if(instanceCache == null)
            {
                instanceCache = this as T;
                instanceCache.Init();
            }

            if(instanceCache != this)
                Debug.LogError(typeof(T) + " is already exists.");
        }
        private void OnDestroy()
        {
            if(instance == this)
            {
                instanceCache = null;
            }
        }
    }
}