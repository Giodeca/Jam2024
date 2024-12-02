using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected bool isPersistent;

    public static T Instance { get; private set; } = null;

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Found more than one Singleton Instance in the scene. Destroying the newest one.");
            Destroy(gameObject);
            return;
        }

        Instance = this as T;

        if (isPersistent)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        Instance = null;
    }
}
