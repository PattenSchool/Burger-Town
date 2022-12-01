using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObjectiveObtainBurger : AbstractObjective
{
    public Burger burger;

    [HideInInspector]
    public List<string> progress;

    [HideInInspector]
    public List<GameObject> _currentParts;

    [HideInInspector]
    public int partsLeft;

    private GrabObject _grabObject;

    [HideInInspector]
    public GameObject _currentItem;

    

    private void Start()
    {
         _grabObject = PlayerStatic.Player.GetComponent<GrabObject>();

        _currentParts = new List<GameObject>(burger.allParts);

        partsLeft = _currentParts.Count;
    }

    public override void UpdateThis()
    {
        if (_grabObject.currentItem)
        {
            _currentItem = _grabObject.currentItem;
            _grabObject.ClearItem();
            _currentItem.SetActive(false);

            foreach (var part in _currentParts.ToArray())
            {
                if (part.name == _currentItem.name)
                {
                    _currentParts.Remove(part);
                    partsLeft--;
                }
            }
        }
     
        if (_currentParts.Count <= 0)
        {
            MarkComplete();
        }
    }

    public List<string> GetProgress()
    {
        progress.Clear();
        foreach (var part in _currentParts)
        {
            //progress.Add($"Obtain {part.name}.");
            progress.Add(part.name);
        }
        return progress;
    }
}
