using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : Spawner
{
    private void OnEnable()
    {
        EventManager.Instance.buildingDropped += StartCreateSoldiersCoroutine;
    }
    private void OnDisable()
    {
        EventManager.Instance.buildingDropped -= StartCreateSoldiersCoroutine;
    }

    private void StartCreateSoldiersCoroutine()
    {
        if (base.haveSpawnObject)
        {
            return;
        }
        base.haveSpawnObject = true;
        StartCoroutine("CreateSoldiers");
    }

    IEnumerator CreateSoldiers()
    {
        yield return new WaitForSeconds(0.5f);
        base.spawnObjectCount = 0;
        for (int i = 0; i < GameBoardManager.Instance.cellList.Count; i++)
        {
            if (GameBoardManager.Instance.cellList[i].GetComponent<CellController>().IsEmpty && base.spawnObjectCount < 4)
            {
                base.spawnObjectCount++;
            }
        }
        for (int i = 0; i < base.spawnObjectCount; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject spawnSoldier = ObjectPoolManager.Instance.GetPoolObject("Soldier");
            spawnSoldier.SetActive(false);
            base.SetObjectRandomSpawnPosition(spawnSoldier);
        }
    }
}
