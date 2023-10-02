using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]private GameObject[] objectPrefabs;

    public GameObject GetObject(string type)
    {
        for(int i = 0; i < objectPrefabs.Length; i++)
        {
            Debug.Log("pool");
            if (objectPrefabs[i].name == type)
            {
                GameObject newObject = Instantiate(objectPrefabs[i]);
                return newObject;
            }
        }
        return null;
    }
}
