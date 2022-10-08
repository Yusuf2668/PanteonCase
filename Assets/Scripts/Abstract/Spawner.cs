using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    protected bool haveSpawnObject = false;

    protected int spawnObjectCount;
    protected int randomNumber;

    protected void SetObjectRandomSpawnPosition(GameObject _gameObject)//Askerlerin spawnlanmasý gereken noktalarý ayarlýyor
    {
        randomNumber = Random.Range(0, GameBoardManager.Instance.cellList.Count);
        while (true)
        {
            if (GameBoardManager.Instance.cellList[randomNumber].GetComponent<CellController>().IsEmpty)
            {
                _gameObject.transform.position = GameBoardManager.Instance.cellList[randomNumber].GetComponent<BoxCollider2D>().transform.position;
                _gameObject.SetActive(true);
                break;
            }
            randomNumber = Random.Range(0, GameBoardManager.Instance.cellList.Count);
        }
    }
}
