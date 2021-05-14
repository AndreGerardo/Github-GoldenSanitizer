using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T Instance = null;

    public static T instance
    {
        get
        {
            Instance = Instance ?? (FindObjectOfType(typeof(T)) as T);
            Instance = Instance ?? new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
            return Instance;
        }
    }

    private void OnApplicationQuit()
    {
        Instance = null;
    }
}