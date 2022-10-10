using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    [SerializeField] private Pool[] poolList = null;

    [System.Serializable]
    class Pool
    {
        public Queue<GameObject> poolObjectList;
        public GameObject poolObjectPrefab;
        public int poolListSize;
    }

    private void Awake()
    {
        for (int i = 0; i < poolList.Length; i++)
        {
            poolList[i].poolObjectList = new Queue<GameObject>();
            for (int j = 0; j < poolList[i].poolListSize; j++)
            {
                var gameObject = FactoryManager.Instance.CreateNewObject(poolList[i].poolObjectPrefab.tag, transform);
                gameObject.SetActive(false);
                poolList[i].poolObjectList.Enqueue(gameObject);
            }
        }
    }

    public GameObject GetPoolObject(string objectTag)
    {
        GameObject nextObject = null;
        switch (objectTag)
        {
            case "BarracksUI":
                nextObject = FindNextObject(0);
                break;
            case "PowerPlantUI":
                nextObject = FindNextObject(1);
                break;
            case "Soldier":
                nextObject = FindNextObject(2);
                break;
        }
        return nextObject;
    }

    private GameObject FindNextObject(int poolListNumber)
    {
        var gameObject = poolList[poolListNumber].poolObjectList.Dequeue();
        gameObject.SetActive(true);
        poolList[poolListNumber].poolObjectList.Enqueue(gameObject);
        return gameObject;
    }
}
