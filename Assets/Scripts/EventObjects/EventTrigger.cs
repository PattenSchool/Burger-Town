using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used only with event triggers and event event recievers
public interface IObjectEvent
{
    //The object events defined in the object recievers
    public abstract void IOnEventTriggered();
}


/// <summary>
/// Sends a unity event to any object
/// </summary>
[RequireComponent(typeof(Collider))]
public class EventTrigger : MonoBehaviour
{
    #region Event Components
    [Header("Event recieving objects")]

    [Tooltip("Objects that an event can be sent to")]
    [SerializeField]
    private GameObject[] eventRecievers;

    [Tooltip("The event components")]
    private IObjectEvent[] eventRecieverInterfaces;
    #endregion

    #region object components
    [Header("Relevant object components")]

    [Tooltip("Trigger of the event")]
    private Collider eventCollider;
    #endregion

    #region Trigger Type
    [Header("Triggers")]

    [Tooltip("The trigger state of this event trigger")]
    [SerializeField]
    private TriggerState triggerState;

    [Tooltip("The states of a trigger event")]
    private enum TriggerState
    {
        OnEnter,
        OnStay,
        OnExit
    }
    #endregion


    #region Editor components
    [Header("Editor components")]

    [Tooltip("The color of the gizmos")]
    [SerializeField]
    private Color lineColor = Color.red;
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        //Set on the thing
        if (eventCollider == null)
        {
            eventCollider = GetComponent<Collider>();
        }

        eventRecieverInterfaces = new IObjectEvent[eventRecievers.Length];

        //Check if all gameobjects have a IObjectEvent
        for(int i = 0; i < eventRecievers.Length; i++)
        {
            var reciever = eventRecievers[i];

            //Get the required component
            if (reciever.GetComponent<IObjectEvent>() == null)
            {
                Debug.Break();
                Debug.LogError($"Event object does not have a IObjectEvent inheritance in {reciever.name}");
            }

            eventRecieverInterfaces[i] = reciever.GetComponent<IObjectEvent>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Safe check if rigid body exists
        if (!HasRigidBody(other))
        {
            return;
        }

        //Activate the events
        ActivateEvents(TriggerState.OnEnter);
    }

    private void OnTriggerStay(Collider other)
    {
        //Safe check if rigid body exists
        if (!HasRigidBody(other))
        {
            return;
        }

        //Activate the events
        ActivateEvents(TriggerState.OnStay);
    }

    private void OnTriggerExit(Collider other)
    {
        //Safe check if rigid body exists
        if (!HasRigidBody(other))
        {
            return;
        }

        //Activate the events
        ActivateEvents(TriggerState.OnExit);
    }

    private void OnDrawGizmos()
    {
        //Set the line color
        Gizmos.color = lineColor;

        //Draw lines to show where the recievers are
        foreach (var reciever in eventRecievers)
        {
            if (reciever != null)
                Gizmos.DrawLine(transform.position, reciever.gameObject.transform.position);
        }
    }
    #endregion

    #region Event trigger Methods
    private void ActivateEvents(TriggerState desiredTriggerState)
    {
        //Breaks if trigger state is not desired trigger state
        if (triggerState != desiredTriggerState)
        {
            return;
        }

        //Set off the events
        foreach (var reciever in eventRecieverInterfaces)
        {
            reciever.IOnEventTriggered();
        }
    }

    private bool HasRigidBody(Collider collided)
    {
        return collided.attachedRigidbody != null;
    }
    #endregion
}
