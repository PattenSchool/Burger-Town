using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Linq;

public class CheckPointManager : MonoBehaviour
{
    [Tooltip("The instance of the checkpoint manager")]
    [SerializeField]
    public static CheckPointManager instance;

    [Tooltip("The transform of the new checkpoint")]
    [SerializeField]
    public GameObject checkPointTracker;

    [Tooltip("Which checkpoint is being respawned to on next player death")]
    [SerializeField]
    private int _index;

    #region Self Destruction
    public void DestroyCheckPointManager()
    {
        if (instance.transform.parent == null)
            Destroy(instance.gameObject);
        else
            Destroy(instance.transform.parent.gameObject);
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;

            var checkPoints = GameObject.FindObjectsOfType<CheckPoint>().ToList();
            checkPoints.Sort(SortCheckPoints);
            instance._index = 0;
            instance.SetCheckpointTracker(checkPoints[0].transform);
        }
        else if (instance != null && instance == this)
        {

        }
        else
        {
            this.gameObject.SetActive(false);
        }

        //SceneManager.activeSceneChanged += OnSceneChanged;
        
    }
    #endregion


    public void ResetManager()
    {
        var checkPoints = GameObject.FindObjectsOfType<CheckPoint>().ToList();
        checkPoints.Sort(SortCheckPoints);
        instance._index = 0;
        instance.SetCheckpointTracker(checkPoints[0].transform);
    }

    //public void OnSceneChanged(Scene current, Scene next)
    //{
    //    if (current.name != next.name)
    //    {
    //        DestroyCheckPointManager();
    //    }
    //}

    public int SortCheckPoints(CheckPoint a, CheckPoint b)
    {
        if (a.index > b.index)
        {
            return 1;
        }
        else if (a.index < b.index)
        {
            return -1;
        }

        return 0;
    }

    #region CheckPoint related
    public void RegisterCheckPoint(CheckPoint checkPoint)
    {
        //Only allow any checkpoints morethan or equal to be registered
        if (checkPoint.index < instance._index)
            return;


        SetCheckpointTracker(checkPoint.transform);
        instance._index = checkPoint.index;
    }

    public void SetCheckpointTracker(Transform newTransform)
    {
        //Set the checkpoint transform to the manager transform
        instance.checkPointTracker.transform.position = newTransform.position;
        instance.checkPointTracker.transform.rotation = newTransform.rotation;
    }
    #endregion

    #region Player Related
    public void RespawnPlayer()
    {
        PlayerStatic.Player.transform.position = instance.checkPointTracker.transform.position;
        PlayerStatic.Player.transform.rotation = instance.checkPointTracker.transform.rotation;

    }
    #endregion
}