using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    private bool haveSoldier = false;

    private int spawnSoldierCount;

    private int randomNumber;

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
        if (haveSoldier)
        {
            return;
        }
        haveSoldier = true;
        StartCoroutine("CreateSoldiers");
    }

    IEnumerator CreateSoldiers()
    {
        yield return new WaitForSeconds(0.5f);
        spawnSoldierCount = 0;
        for (int i = 0; i < GameBoardManager.Instance.cellList.Count; i++)
        {
            if (GameBoardManager.Instance.cellList[i].GetComponent<CellController>().IsEmpty && spawnSoldierCount < 4)
            {
                spawnSoldierCount++;
            }
        }
        for (int i = 0; i < spawnSoldierCount; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject spawnSoldier = ObjectPoolManager.Instance.GetPoolObject("Soldier");
            spawnSoldier.SetActive(false);
            SetSoldierRandomSpawnPosition(spawnSoldier);
        }
    }
    private void SetSoldierRandomSpawnPosition(GameObject soldier)//Askerlerin spawnlanmasý gereken noktalarý ayarlýyor
    {
        randomNumber = Random.Range(0, GameBoardManager.Instance.cellList.Count);
        while (true)
        {
            if (GameBoardManager.Instance.cellList[randomNumber].GetComponent<CellController>().IsEmpty)
            {
                soldier.transform.position = GameBoardManager.Instance.cellList[randomNumber].GetComponent<BoxCollider2D>().transform.position;
                soldier.SetActive(true);
                break;
            }
            randomNumber = Random.Range(0, GameBoardManager.Instance.cellList.Count);
        }
    }
}
