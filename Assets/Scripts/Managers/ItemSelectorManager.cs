using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ItemSelectorManager : MonoSingleton<ItemSelectorManager>
{
    [SerializeField] BuildType buildType;

    public bool SelectedItem => _selectedItem;

    [SerializeField] private LayerMask _buildingHitLayerMask;
    [SerializeField] private LayerMask _productMenuHitLayerMask;

    private Camera _camera;

    private RaycastHit2D _productMenuhit;
    private RaycastHit2D _buildingHit;

    private bool _selectedItem = false;
    private bool _canDropItem = true;

    private GameObject _selectedObject;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (_selectedItem)
        {
            _productMenuhit = Physics2D.Raycast(_camera.ScreenPointToRay(Input.mousePosition).origin, Vector2.zero, Mathf.Infinity, _productMenuHitLayerMask);
            if (_productMenuhit.collider == null)
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

    private void MouseLeftClick()//Kullan�c� ekrana sol t�k yaparsa �al���yor (evente ba�l�)
    {
        DropBuilding();
        SelectBuildingProduct();
        SelectBuilding();
    }

    private void CanDropItem(bool value)//se�ilen obje ba�ka bir objeye temas ederse Rengini de�i�tirip bize bilgilendirme yap�yor
    {
        if (!_selectedObject)
        {
            return;
        }
        _canDropItem = value;
        if (!_canDropItem)
        {
            _selectedObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            _selectedObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void FollowMouse()
    {
        if (_productMenuhit.transform.CompareTag("Cell") && _selectedObject.CompareTag("PowerPlant"))
        {
            _selectedObject.transform.position = _productMenuhit.transform.position + new Vector3(_productMenuhit.transform.GetComponent<SpriteRenderer>().sprite.bounds.min.x, _productMenuhit.transform.GetComponent<SpriteRenderer>().sprite.bounds.min.y * 2);
            _selectedObject.transform.position = new Vector3(Mathf.Clamp(_selectedObject.transform.position.x, -1.2f + buildType.powerPlantSprite.bounds.size.x / 2, 1.2f), Mathf.Clamp(_selectedObject.transform.position.y, -1.18f + buildType.powerPlantSprite.bounds.size.y / 3, 1.18f));
        }
        else if (_productMenuhit.transform.CompareTag("Cell") && _selectedObject.CompareTag("Barracks"))
        {
            _selectedObject.transform.position = _productMenuhit.transform.position + _productMenuhit.transform.GetComponent<SpriteRenderer>().sprite.bounds.min;
            _selectedObject.transform.position = new Vector3(Mathf.Clamp(_selectedObject.transform.position.x, -1.2f + buildType.barracksSprite.bounds.size.x / 2, 1.2f - buildType.barracksSprite.bounds.size.x / 2.5f), Mathf.Clamp(_selectedObject.transform.position.y, -1f + buildType.barracksSprite.bounds.size.y / 4, 1.2f - buildType.barracksSprite.bounds.size.y / 2));
        }
    }

    private void DropBuilding()//B�rakma i�lemi burada yap�l�yor
    {
        if (!_selectedObject)
        {
            return;
        }
        if (_selectedItem && _canDropItem)
        {
            _selectedItem = false;
            EventManager.Instance.BuildingDropped();
            _selectedObject.GetComponent<BoxCollider2D>().isTrigger = false;
            AstarPathEditor.MenuScan();//PathFinder i�in ortam taramas�n� ba�lat�yor
            _selectedObject = null;
        }
    }
    private void SelectBuildingProduct()//ekran�n solundaki Product tablosundan yap� se�memize yard�mc� oluyor
    {
        var hitObject = CreateRayOnScreen(_productMenuhit, _productMenuHitLayerMask);
        if (!hitObject)
        {
            return;
        }
        if (!_selectedItem && !hitObject.transform.CompareTag("Cell"))
        {
            _selectedItem = true;
            _selectedObject = ObjectPoolManager.Instance.GetPoolObject(hitObject.transform.tag);
        }
    }
    private void SelectBuilding()//Yerle�tirilen yap�lar aras�nda se�im yapmam�za yar�yor
    {
        if (CreateRayOnScreen(_buildingHit, _buildingHitLayerMask))
        {
            EventManager.Instance.SelectedBuilding(CreateRayOnScreen(_buildingHit, _buildingHitLayerMask));
        }
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
