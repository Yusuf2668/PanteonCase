using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenüManager : MonoBehaviour
{
    [SerializeField] private BuildType _buildType;

    [SerializeField] private Image _buildImage;
    [SerializeField] private TextMeshProUGUI _buildName;
    [SerializeField] private Image _ProductImage;
    [SerializeField] private TextMeshProUGUI _ProductName;

    private void OnEnable()
    {
        EventManager.Instance.onSelectedItem += ShowSelectedItemInformation;
    }

    private void ShowSelectedItemInformation(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "Barracks":
                _buildImage.sprite = _buildType.barracksSprite;
                _buildName.text = _buildType.barrakcsDescriptionText;
                _ProductImage.sprite = _buildType.soldierSprite;
                _ProductName.text = _buildType.soldierDescriptionText;
                break;
            case "PowerPlant":
                _buildImage.sprite = _buildType.powerPlantSprite;
                _buildName.text = _buildType.powerPlantDescriptionText;
                _ProductImage.sprite = null;
                _ProductName.text = null;
                break;
        }
    }
}
