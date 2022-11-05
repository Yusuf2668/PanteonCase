using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public abstract class Spawner : MonoBehaviour
{
    protected bool haveSpawnObject = false;

    protected int spawnObjectCount;
    protected int randomNumber;

    protected void SetObjectRandomSpawnPosition(GameObject _gameObject)//Askerlerin spawnlanmasý gereken noktalarý ayarlýyor
    {
        randomNumber = Random.Range(0, GameBoardManager.Instance.cellList.Count);
        var cellList = GameBoardManager.Instance.cellList[randomNumber];
        while (true)
        {
            if (cellList.GetComponent<CellController>().IsEmpty)
            {
                _gameObject.transform.position = cellList.GetComponent<BoxCollider2D>().transform.position;
                _gameObject.SetActive(true);
                break;
            }
            randomNumber = Random.Range(0, GameBoardManager.Instance.cellList.Count);
        }
    }
}
