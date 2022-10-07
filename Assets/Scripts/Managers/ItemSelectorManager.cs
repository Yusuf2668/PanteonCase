using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ItemSelectorManager : MonoSingleton<ItemSelectorManager>
{
    [SerializeField] BuildType buildType;

    public bool SelectedItem => _selectedItem;

    [SerializeField] private LayerMask buildingHitLayerMask;
    [SerializeField] private LayerMask productMenuHitLayerMask;

    private Camera _camera;

    private RaycastHit2D productMenuhit;
    private RaycastHit2D buildingHit;

    private bool _selectedItem = false;
    private bool canDropItem = true;

    private GameObject selectedObject;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (_selectedItem)
        {
            productMenuhit = Physics2D.Raycast(_camera.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero, Mathf.Infinity, productMenuHitLayerMask);
            if (productMenuhit.collider == null)
            {
                return;
            }
            FollowMouse();
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.canDropItem += CanDropItem;
        EventManager.Instance.onLeftMouseClick += MouseLeftClick;
    }
    private void OnDisable()
    {
        EventManager.Instance.canDropItem -= CanDropItem;
        EventManager.Instance.onLeftMouseClick -= MouseLeftClick;
    }

    private void MouseLeftClick()
    {
        DropBuilding();
        productMenuhit = Physics2D.Raycast(_camera.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero, Mathf.Infinity, productMenuHitLayerMask);
        if (productMenuhit.collider == null)
        {
            return;
        }
        SelectBuildingProduct();
        SelectBuilding();
    }


    private void CanDropItem(bool value)
    {
        if (!selectedObject)
        {
            return;
        }
        canDropItem = value;
        if (!canDropItem)
        {
            selectedObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            selectedObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void FollowMouse()
    {
        if (productMenuhit.transform.CompareTag("Cell") && selectedObject.CompareTag("PowerPlant"))
        {
            selectedObject.transform.position = productMenuhit.transform.position + new Vector3(productMenuhit.transform.GetComponent<SpriteRenderer>().sprite.bounds.min.x, productMenuhit.transform.GetComponent<SpriteRenderer>().sprite.bounds.min.y * 2);
            selectedObject.transform.position = new Vector3(Mathf.Clamp(selectedObject.transform.position.x, -1.2f + buildType.powerPlantSprite.bounds.size.x / 2, 1.2f), Mathf.Clamp(selectedObject.transform.position.y, -1.18f + buildType.powerPlantSprite.bounds.size.y / 3, 1.18f));
        }
        else if (productMenuhit.transform.CompareTag("Cell") && selectedObject.CompareTag("Barracks"))
        {
            selectedObject.transform.position = productMenuhit.transform.position + productMenuhit.transform.GetComponent<SpriteRenderer>().sprite.bounds.min;
            selectedObject.transform.position = new Vector3(Mathf.Clamp(selectedObject.transform.position.x, -1.2f + buildType.barracksSprite.bounds.size.x / 2, 1.2f - buildType.barracksSprite.bounds.size.x / 2.5f), Mathf.Clamp(selectedObject.transform.position.y, -1f + buildType.barracksSprite.bounds.size.y / 4, 1.2f - buildType.barracksSprite.bounds.size.y / 2));
        }
    }

    private void DropBuilding()//Býrakma iþlemi burada yapýlýyor
    {
        if (!selectedObject)
        {
            return;
        }
        if (_selectedItem && canDropItem)
        {
            _selectedItem = false;
            EventManager.Instance.BuildingDropped();
            selectedObject.GetComponent<BoxCollider2D>().isTrigger = false;
            AstarPathEditor.MenuScan();
            selectedObject = null;
        }
    }
    private void SelectBuildingProduct()//saðdaki Product tablosundan yapý seçmemize yardýmcý oluyor
    {
        if (!_selectedItem && !productMenuhit.transform.CompareTag("Cell"))
        {
            _selectedItem = true;
            selectedObject = ObjectPoolManager.Instance.GetPoolObject(productMenuhit.transform.tag);
        }
    }
    private void SelectBuilding()//Yerleþtirilen yapýlar arasýnda seçim yapmamýza yarýyor
    {
        buildingHit = Physics2D.Raycast(_camera.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero, Mathf.Infinity, buildingHitLayerMask);
        if (buildingHit.collider == null)
        {
            return;
        }
        EventManager.Instance.SelectedBuilding(buildingHit.transform.gameObject);
    }
}
