using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardManager : MonoSingleton<GameBoardManager>
{
    [SerializeField] private Sprite _cellSprite;

    [SerializeField] private Transform _firstCellinstantiateposition;

    private int _gridHeight = 8;
    private int _gridWidth = 8;

    public List<GameObject> cellList = new List<GameObject>();

    private int _cellLine;

    private Vector3 _cellPositionOffset;
    private Vector2 _cellBound;

    private void Awake()
    {
        _cellBound = _cellSprite.bounds.size;
    }

    private void Start()
    {
        InstantiateCell();
        SetPositionCell();
    }

    private void InstantiateCell()//Bütün hücreleri yaratýr 
    {
        for (int i = 0; i < _gridHeight * _gridWidth; i++)
        {
            cellList.Add(FactoryManager.Instance.CreateNewObject("Cell", transform));
        }
    }

    private void SetPositionCell()//Bütün hücrelerin pozisyonunu düzenler
    {
        _cellLine = 0;
        _cellPositionOffset = Vector3.zero;
        for (int i = 0; i < _gridHeight; i++)
        {
            for (int j = 0; j < _gridWidth; j++)
            {
                cellList[_cellLine].transform.position = _firstCellinstantiateposition.position + _cellPositionOffset;
                _cellPositionOffset.x += _cellBound.x;
                _cellLine++;
            }
            _cellPositionOffset.x = 0;
            _cellPositionOffset.y -= _cellBound.y;
        }
    }
}
