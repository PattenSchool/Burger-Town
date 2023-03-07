using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSave : MonoBehaviour
{
    [SerializeField]
    public static ObjectSave instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        Object.DontDestroyOnLoad(instance);
    }


    public void OnSceneChange(Scene current, Scene next)
    {
        if (current.name != next.name)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    public void SaveObjectData(GameObject saveableObject)
    {
        saveableObject.transform.SetParent(instance.transform, true);
    }

    public void UnsetObjectData(GameObject saveableObject)
    {
        saveableObject.transform.SetParent(null, true); ;
    }
}
