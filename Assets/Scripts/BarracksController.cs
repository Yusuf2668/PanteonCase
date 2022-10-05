using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksController : MonoBehaviour
{
    private bool haveSoldier = false;


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
        for (int i = 0; i < 4; i++)
        {
            GameObject spawnSoldier = ObjectPoolManager.Instance.GetPoolObject("Soldier");
            spawnSoldier.transform.SetParent(transform);
            spawnSoldier.transform.localPosition = Vector3.zero;
            yield return new WaitForSeconds(1f);
        }
    }
}
