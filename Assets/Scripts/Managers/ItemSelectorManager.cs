using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectorManager : MonoBehaviour
{
    [SerializeField] BuildType buildType;

    [SerializeField] private LayerMask buildingHitLayerMask;
    [SerializeField] private LayerMask productMenuHitLayerMask;

    private Camera _camera;

    private RaycastHit2D productMenuhit;
    private RaycastHit2D buildingHit;

    private bool selectedItem = false;
    private bool canDropItem = true;

    private GameObject selectedObject;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            productMenuhit = Physics2D.Raycast(_camera.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero, Mathf.Infinity, productMenuHitLayerMask);
            if (productMenuhit.collider == null)
            {
                return;
            }
            DropBuilding();
            SelectBuildingProduct();
            SelectBuilding();
        }
        if (selectedItem)
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
    }

    private void OnDisable()
    {
        EventManager.Instance.canDropItem -= CanDropItem;
    }

    private void CanDropItem(bool value)
    {
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
            selectedObject.transform.position = new Vector3(Mathf.Clamp(selectedObject.transform.position.x, -1.2f + buildType.powerPlantSprite.bounds.size.x / 2, 1.2f), Mathf.Clamp(selectedObject.transform.position.y, -1.18f + buildType.powerPlantSprite.bounds.size.y / 2, 1.18f));
        }
        else if (productMenuhit.transform.CompareTag("Cell") && selectedObject.CompareTag("Barracks"))
        {
            selectedObject.transform.position = productMenuhit.transform.position + productMenuhit.transform.GetComponent<SpriteRenderer>().sprite.bounds.min;
            selectedObject.transform.position = new Vector3(Mathf.Clamp(selectedObject.transform.position.x, -1.2f + buildType.barracksSprite.bounds.size.x / 2, 1.2f - buildType.barracksSprite.bounds.size.x / 2.5f), Mathf.Clamp(selectedObject.transform.position.y, -1.18f + buildType.barracksSprite.bounds.size.y / 2, 1.18f - buildType.barracksSprite.bounds.size.y / 3));
        }
    }

    private void DropBuilding()//Býrakma iþlemi burada yapýlýyor
    {
        if (!selectedObject)
        {
            return;
        }
        if (selectedItem && canDropItem)
        {
            selectedItem = false;
            selectedObject.GetComponent<BuildingController>().enabled = true;
            EventManager.Instance.BuildingDropped();
        }
    }

    private void SelectBuildingProduct()//saðdaki Product tablosundan yapý seçmemize yardýmcý oluyor
    {
        if (!selectedItem && !productMenuhit.transform.CompareTag("Cell"))
        {
            selectedItem = true;
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
        EventManager.Instance.SelectedProduct(buildingHit.transform.gameObject);
    }
}
