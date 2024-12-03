using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersistence : MonoBehaviour
{
    private void Awake()
    {
        int numScenePersistance = FindObjectsOfType<ScenePersistence>().Length;
        if (numScenePersistance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DeleteScenePersistance()
    {
        Destroy(gameObject);
    }
}
