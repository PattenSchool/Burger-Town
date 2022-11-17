using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IcrementLevel : MonoBehaviour, IObjectEvent
{
    [SerializeField]
    private SaveLoadManager _saveLoadManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IOnEventTriggered()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
