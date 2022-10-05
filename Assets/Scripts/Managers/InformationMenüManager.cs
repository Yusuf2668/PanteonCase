using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationMenüManager : MonoBehaviour
{
    [SerializeField] private BuildType buildType;

    [SerializeField] private Image buildImage;
    [SerializeField] private TextMeshProUGUI buildName;
    [SerializeField] private Image ProductImage;
    [SerializeField] private TextMeshProUGUI ProductName;

    private void OnEnable()
    {
        EventManager.Instance.onSelectedItem += ShowSelectedItemInformation;
    }

    private void ShowSelectedItemInformation(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "Barracks":
                buildImage.sprite = buildType.barracksSprite;
                buildName.text = buildType.barrakcsDescriptionText;
                ProductImage.sprite = buildType.soldierSprite;
                ProductName.text = buildType.soldierDescriptionText;
                break;
            case "PowerPlant":
                buildImage.sprite = buildType.powerPlantSprite;
                buildName.text = buildType.powerPlantDescriptionText;
                ProductImage.sprite = null;
                ProductName.text = null;
                break;
        }
    }
}
