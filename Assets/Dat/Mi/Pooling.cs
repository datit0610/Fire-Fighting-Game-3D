using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pooling_Mi : MonoBehaviour
{
    [SerializeField] private int initSize = 30;
    [SerializeField] private GameObject prefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    public static Pooling_Mi Instance
    {   
        get;
        private set;
    }

    public Pooling_Mi()
    {
        
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < initSize; i++)
        {
            var poolItem = Instantiate(prefab, transform, true);
            AddObjectToPool(poolItem);
        }
    }

    public void AddObjectToPool(GameObject prf)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        
        prf.SetActive(false);
        pool.Enqueue(prf);
    }

    public GameObject GetObjectFromPool(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            InitPool();
        }

        var item = pool.Dequeue();
        item.SetActive(true);
        item.transform.position = position;
        item.transform.rotation = rotation;
        return item;
    }
}
