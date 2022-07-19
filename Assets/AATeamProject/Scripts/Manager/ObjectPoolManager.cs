using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolData
{
    public GameObject prefab;
    public int count;
    public List<GameObject> list;
}

public class ObjectPoolManager : MonoBehaviour
{
    public List<PoolData> poolData = new List<PoolData>();

    private void Awake()
    {
        AddObjectToPool();
    }

    private void AddObjectToPool()
    {
        foreach (var pool in poolData)
        {
            for (int i = 0; i < pool.count; i++)
            {
                var Object = Instantiate(pool.prefab, transform);
                pool.list.Add(Object);
                Object.transform.localPosition = Vector3.zero;
                Object.SetActive(false);
            }
        }
    }

    public GameObject GetObject(string prefabName)
    {
        foreach (var pool in poolData)
        {
            if (pool.prefab.name.Equals(prefabName))
            {
                if (pool.list != null)
                {
                    foreach (var item in pool.list)
                    {
                        if (!item.activeSelf)
                        {
                            item.SetActive(true);
                            return item;
                        }
                        //var Object = Instantiate(pool.prefab, transform);
                        //pool.list.Add(Object);
                        //Object.transform.localPosition = Vector3.zero;
                        //Object.SetActive(false);

                        //return Object;
                    }
                }
            }
        }
        return null;
    }
}
