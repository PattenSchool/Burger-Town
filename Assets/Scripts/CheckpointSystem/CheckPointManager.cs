using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    #region Singleton
    //!===========Variables and Properties===========!//
    [Header("Singleton variables")]

    [Tooltip("The instance that is called in other classes")]
    [SerializeField]
    public static CheckPointManager instance;

    //!===================Methods====================!//
    /// <summary>
    /// Sets up the singleton to be called from other classes
    /// </summary>
    public void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    #endregion

    #region CheckPoint related
    //!===========Variables and Properties===========!//
    [Header("Checkpoint Related")]

    [Tooltip("The checkpoints sorted by index (index is set inside of each gameobject")]
    [SerializeField]
    public List<CheckPoint> checkpoints;

    //!===================Methods====================!//
    /// <summary>
    /// Sets up the checkpoint list sorted by index
    /// </summary>
    private void SetUpCheckPoints()
    {
        checkpoints = Object.FindObjectsOfType<CheckPoint>().ToList();
        checkpoints.Sort(SortCheckPoint);
    }

    /// <summary>
    /// Sorts the checkpoints by index
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int SortCheckPoint(CheckPoint a, CheckPoint b)
    {
        if (a.index < b.index)
        {
            return -1;
        }
        else if (a.index > b.index)
        {
            return 1;
        }

        return 0;
    }
    #endregion

    #region ResettableObjects
    //!===========Variables and Properties===========!//
    [Header("Puzzle Related")]

    [Tooltip("All related resettable objects")]
    [SerializeField]
    public List<IResettable> resettableObjects;

    //!===================Methods====================!//
    /// <summary>
    /// Sets up the resettable objects
    /// </summary>
    private void SetUpResettables()
    {
        resettableObjects = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IResettable>().ToList();


    }
    #endregion

    #region Recorded Index/index related
    //!===========Variables and Properties===========!//
    [Header("Index")]

    [Tooltip("The index that will teleport")]
    [SerializeField]
    public int checkPointIndex;

    //!===================Methods====================!//
    /// <summary>
    /// Resets the index to 0, not necessarily to the first index on the checkpoint list
    /// </summary>
    public void ResetIndex()
    {
        checkPointIndex = 0;
    }

    /// <summary>
    /// Registers and sets a new index for the player to respawn at
    /// </summary>
    /// <param name="newIndex"></param>
    public void RegisterCheckPoint(CheckPoint newCheckPoint)
    {
        checkPointIndex = newCheckPoint.index;
    }
    #endregion

    #region Unity Methods
    //!===================Methods====================!//
    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        SetUpResettables();

        SetUpCheckPoints();

        //Set index to min
        checkPointIndex = checkpoints[0].index;

        //REspawn player
        RespawnPlayer();
    }

    public void Update()
    {
    }
    #endregion

    #region Reset Methods
    /// <summary>
    /// "reset" the level
    /// </summary>
    public void RestartLevelByManager()
    {
        //Reset objects that can be reset
        ResetResettables();

        //RespawnPlayer
        RespawnPlayer();
    }

    public void ResetResettables()
    {
        foreach(var resettable in resettableObjects)
        {
            resettable.IResetTransform();
        }
    }

    public void RespawnPlayer()
    {
        foreach (var checkPoint in checkpoints)
        {
            if (checkPointIndex == checkPoint.index)
            {
                PlayerStatic.Player.transform.position = checkPoint.transform.position;
                PlayerStatic.Player.transform.rotation = checkPoint.transform.rotation;
            }
        }
    }
    #endregion

}
