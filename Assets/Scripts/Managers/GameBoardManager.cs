using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardManager : MonoSingleton<GameBoardManager>
{
    [SerializeField] private int gridHeight;
    [SerializeField] private int gridWidth;

    [SerializeField] private Sprite cellSprite;

    [SerializeField] private Transform firstCellinstantiateposition;

    public List<GameObject> cellList = new List<GameObject>();

    private int cellLine;

    private Vector3 cellPositionOffset;
    private Vector2 cellBound;

    private void Awake()
    {
        cellBound = cellSprite.bounds.size;
    }

    private void Start()
    {
        InstantiateCell();
        SetPositionCell();
    }

    private void InstantiateCell()//Bütün hücreleri yaratýr 
    {
        for (int i = 0; i < gridHeight * gridWidth; i++)
        {
            cellList.Add(FactoryManager.Instance.CreateNewObject("Cell", transform));
        }
    }

    private void SetPositionCell()//Bütün hücrelerin pozisyonunu düzenler
    {
        cellLine = 0;
        cellPositionOffset = Vector3.zero;
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                cellList[cellLine].transform.position = firstCellinstantiateposition.position + cellPositionOffset;
                cellPositionOffset.x += cellBound.x;
                cellLine++;
            }
            cellPositionOffset.x = 0;
            cellPositionOffset.y -= cellBound.y;
        }
    }
}
