using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurgerPhysical : MonoBehaviour
{
    public GameObject burgerPrefab;


    //public List<GameObject> burgerParts;

    private GrabObject _grabObject;

    public float assembleDistance = 0.5f;

    //public float heightOffset = 0.025f;

    [SerializeField]
    private List<GameObject> _burgerParts;

    private List<GameObject> _assembledParts;

    private bool isComplete;

    private void Start()
    {
        _assembledParts = new List<GameObject>();

        foreach (Transform child in this.transform)
        {
            _burgerParts.Add(child.gameObject);
        }

        _grabObject = PlayerStatic.Player.GetComponent<GrabObject>();
    }

    private void FixedUpdate()
    {
        if (_grabObject.grabObject)
        {
            if (_grabObject.grabObject.GetComponent<BurgerPhysicalPart>())
            {

                this.gameObject.transform.position = _grabObject.grabObject.transform.position;
                this.transform.rotation = _grabObject.grabObject.transform.rotation;

                foreach (var part in _burgerParts.ToArray())
                {
                    if (part.name == _grabObject.grabObject.name)
                    {
                        part.SetActive(true);
                    }
                }

                _burgerParts.Remove(_grabObject.grabObject);

                _grabObject.grabObject.SetActive(false);

                _grabObject.ClearGrabObject();

                _grabObject.grabObject = this.gameObject;

                foreach (var part in _burgerParts)
                {
                    if (Vector3.Distance(part.transform.position, this.transform.position) < assembleDistance)
                    {

                    }
                }

                /*
                foreach (var part in _burgerParts)
                {
                    if (_grabObject.grabObject != part)
                    {
                        if (Vector3.Distance(_grabObject.grabObject.transform.position, part.transform.position) < assembleDistance)
                        {


                            //this.transform.position = _grabObject.grabObject.transform.position;
                            //this.transform.rotation = _grabObject.grabObject.transform.rotation;

                            //this.transform.Find(_grabObject.grabObject.name).gameObject.SetActive(true);
                            this.transform.Find(part.name).gameObject.SetActive(true);

                            //_grabObject.grabObject = this.gameObject;

                            //_grabObject.grabObject.gameObject.SetActive(false);
                            part.SetActive(false);

                            _assembledParts.Add(_grabObject.grabObject);
                            _assembledParts.Add(part);

                            _burgerParts.Remove(_grabObject.grabObject);
                            _burgerParts.Remove(part);
                        }
                    }
                */
            }
        }
    }
}
