using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierControllerManager : MonoBehaviour
{
    [SerializeField] private LayerMask _soldierLayerMask;

    [SerializeField] GameObject _target;
    private GameObject _selectedSoldier;

    private RaycastHit2D _soldierHit;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        EventManager.Instance.onLeftMouseClick += SelectSoldier;
    }

    private void OnDisable()
    {
        EventManager.Instance.onLeftMouseClick -= SelectSoldier;
    }

    private void SelectSoldier()
    {
        if (_selectedSoldier)
        {
            _selectedSoldier.GetComponent<Pathfinding.AIDestinationSetter>().canMove = false; //�nceden se�ili olan soldier �n canMove �zelli�ini kapamak i�in
        }
        _soldierHit = Physics2D.Raycast(_camera.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero, Mathf.Infinity, _soldierLayerMask);
        if (_soldierHit.collider == null)
        {
            return;
        }
        _selectedSoldier = _soldierHit.transform.gameObject;
        _target.transform.position = _selectedSoldier.transform.position;
        _selectedSoldier.GetComponent<Pathfinding.AIDestinationSetter>().canMove = true;
    }
}
