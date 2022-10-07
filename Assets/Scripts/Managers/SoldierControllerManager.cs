using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierControllerManager : MonoBehaviour
{
    [SerializeField] private LayerMask soldierLayerMask;

    [SerializeField] GameObject target;
    private GameObject selectedSoldier;

    private RaycastHit2D soldierHit;

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
        if (selectedSoldier)
        {
            selectedSoldier.GetComponent<Pathfinding.AIDestinationSetter>().canMove = false; //önceden seçili olan soldier ýn canMove özelliðini kapamak için
        }
        soldierHit = Physics2D.Raycast(_camera.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero, Mathf.Infinity, soldierLayerMask);
        if (soldierHit.collider == null)
        {
            return;
        }
        selectedSoldier = soldierHit.transform.gameObject;
        target.transform.position = selectedSoldier.transform.position;
        selectedSoldier.GetComponent<Pathfinding.AIDestinationSetter>().canMove = true;
    }
}
