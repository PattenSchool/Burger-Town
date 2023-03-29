using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnPlayerEnter : MonoBehaviour
{
    #region Unity Event
    [Header("Event Variables")]

    [Tooltip("The method that plays when player enters trigger")]
    [SerializeField]
    private UnityEvent triggerableEvent;

    [Tooltip("THe event that is triggered whenever it collides with something else")]
    [SerializeField]
    private UnityEvent elseTriggerableEvent;
    #endregion

    #region Unity Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PlayerStatic.PlayerTag)
        {
            triggerableEvent.Invoke();
            //this.GetComponentInParent<Rigidbody>().constraints =
            //    ~RigidbodyConstraints.FreezeAll;
        }
        else
        {
            elseTriggerableEvent.Invoke();
        }
    }
    #endregion

    #region Test Methods
    public void TestMethod()
    {
        print("Test Works");
    }
    #endregion
}
