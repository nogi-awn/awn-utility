using UnityEngine;

namespace AwnUtility
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T instanceCache;

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