using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : MonoSingleton<FactoryManager>
{
    [SerializeField] BuildType buildPrefabType;

    public GameObject CreateNewObject(string objectName, Transform parent)
    {
        GameObject resultObject = null;
        switch (objectName)
        {
            case "Cell":
                resultObject = Instantiate(buildPrefabType.cellPrefab, parent);
                break;
            case "PowerPlant":
                resultObject = Instantiate(buildPrefabType.powerPlantPrefab, parent);
                break;
            case "Barracks":
                resultObject = Instantiate(buildPrefabType.barrackPrefab, parent);
                break;
            case "Soldier":
                resultObject = Instantiate(buildPrefabType.soldierPrefab, parent);
                break;
            case "BarracksUI":
                resultObject = Instantiate(buildPrefabType.barrackPrefab, parent);
                break;
            case "PowerPlantUI":
                resultObject = Instantiate(buildPrefabType.powerPlantPrefab, parent);
                break;
            case "UIBarrackItem":
                resultObject = Instantiate(buildPrefabType.barrackUIPrefab, parent);
                break;
            case "UIPowerPlantItem":
                resultObject = Instantiate(buildPrefabType.powerPlantUIPrefab, parent);
                break;
        }
        return resultObject;
    }
}
