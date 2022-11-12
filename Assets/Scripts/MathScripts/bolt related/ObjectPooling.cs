using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial by: Prof. Gullotta
//https://www.youtube.com/watch?v=r2uY_JYvFZk
//Coded by Jacob
public static class ObjectPooling
{
    //Prefab object and its Pool
    private static Dictionary<GameObject, Pool> _pools;

    //Checks if pool is empty 
    //Activates object at position and rotation
    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (_pools == null)
        {
            _pools = new Dictionary<GameObject, Pool>();
        }

        if (_pools != null && _pools.ContainsKey(prefab) == false)
        {
            _pools[prefab] = new Pool(prefab);
        }

        return _pools[prefab].Spawn(position, rotation);
    }

    //Checks if is pool member
    //If is, deactivates object
    //else, destroy object
    public static void Despawn(GameObject obj)
    {
         

        PoolMember poolMember = obj.GetComponent<PoolMember>();

        if (poolMember == null)
        {
            GameObject.Destroy(obj);
        }
        else
        {
            poolMember.MyPool.Despawn(obj);
        }
    }

    //Stores the prefab type
    //Stores a stack of X prefab
    private class Pool
    {
        private int _curIndex;
        private Stack<GameObject> _inactiveObjects;
        private GameObject _prefab;

        public Pool(GameObject prefab)
        {
            _prefab = prefab;
            _inactiveObjects = new Stack<GameObject>();
        }

        //Checks if there are any objects in stack
        //if none, make one
        //else use first element
        //if element is empty, try again
        //Set position and rotation to correct values
        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject obj;

            if (_inactiveObjects.Count == 0)
            {
                obj = (GameObject)GameObject.Instantiate(_prefab, position, rotation);
                obj.name = _prefab.name + "_" + _curIndex;
                _curIndex++;
                obj.AddComponent<PoolMember>().MyPool = this;
            }
            else
            {
                obj = _inactiveObjects.Pop();

                if (obj == null)
                {
                    return Spawn(position, rotation);
                }
            }

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }

        //Turn object off
        public void Despawn(GameObject obj)
        {
            obj.SetActive(false);
            _inactiveObjects.Push(obj);
        }
    }

    //Tracks if is a member of a pool
    //tracks which pool that is
    private class PoolMember : MonoBehaviour
    {
        private Pool _myPool;

        public Pool MyPool { get { return _myPool; } set { _myPool = value; } }
    }
}