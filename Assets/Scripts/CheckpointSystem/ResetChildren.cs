using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetChildren : MonoBehaviour, IResettable
{
    public GameObject IgameObject { get; set; }

    public Transform IOriginalTransform { get; set; }
    public Vector3 IStoredPosition { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public Quaternion IStoredRotation { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField]
    private List<Vector3> storedPositions;

    [SerializeField]
    private List<Quaternion> storedRotations;

    [SerializeField]
    private List<bool> storedActiveStates;

    private void Awake()
    {
        IgameObject = this.gameObject;

        IStoreOriginalTransform();
    }

    private void Update()
    {
        print(this.transform.childCount);
    }

    public void IResetTransform()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var child = this.gameObject.transform.GetChild(i);

            child.gameObject.SetActive(storedActiveStates[i]);

            child.transform.position = storedPositions[i];
            child.transform.rotation = storedRotations[i];
        }
    }

    public void IStoreOriginalTransform()
    {
        storedPositions = new();
        storedRotations = new();
        storedActiveStates = new();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            var child = this.gameObject.transform.GetChild(i);

            storedPositions.Add(child.transform.position);
            storedRotations.Add(child.transform.rotation);

            storedActiveStates.Add(child.gameObject.activeSelf);
        }
    }
}
