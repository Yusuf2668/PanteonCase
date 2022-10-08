using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierControllerManager : MonoBehaviour, ISelector
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
            _selectedSoldier.GetComponent<Pathfinding.AIDestinationSetter>().canMove = false; //önceden seçili olan soldier ýn canMove özelliðini kapamak için
        }
        var hitObject = CreateRayOnScreen(_soldierHit, _soldierLayerMask);
        if (!hitObject)
        {
            return;
        }
        _selectedSoldier = hitObject.transform.gameObject;
        _target.transform.position = _selectedSoldier.transform.position;
        _selectedSoldier.GetComponent<Pathfinding.AIDestinationSetter>().canMove = true;
    }

    public GameObject CreateRayOnScreen(RaycastHit2D raycastHit2D, LayerMask mask)
    {
        raycastHit2D = Physics2D.Raycast(_camera.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero, Mathf.Infinity, mask);
        if (raycastHit2D.collider == null)
        {
            return null;
        }
        return raycastHit2D.transform.gameObject;
    }
}
