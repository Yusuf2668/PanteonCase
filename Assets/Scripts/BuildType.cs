using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuildObjectType", fileName = "BuildObjectType")]
public class BuildType : ScriptableObject
{
    public GameObject cellPrefab;
    public GameObject powerPlantUIPrefab;
    public GameObject barrackUIPrefab;
    public GameObject barrackPrefab;
    public GameObject powerPlantPrefab;
    public GameObject soldierPrefab;

    public Sprite barracksSprite;
    public Sprite powerPlantSprite;
    public Sprite soldierSprite;

    public string barrakcsDescriptionText;
    public string powerPlantDescriptionText;
    public string soldierDescriptionText;

}
