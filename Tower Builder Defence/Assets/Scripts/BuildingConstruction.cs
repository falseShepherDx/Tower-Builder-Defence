using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{

    private float constructionTimer;
    private float constructionTimerMax;
    private BuildingTypeScriptableObject buildingType;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeClass buildingTypeClass;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        buildingTypeClass = GetComponent<BuildingTypeClass>();
    }

    public static BuildingConstruction Create(Vector3 position,BuildingTypeScriptableObject buildingType)
    {
        Transform buildingConstructionPF = Resources.Load<Transform>("BuildingConstruction");
        Transform buildingConstructionTransform=Instantiate(buildingConstructionPF, position,Quaternion.identity);
        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.PrepareBuildingTypeToConstruction(buildingType);
        return buildingConstruction;
    }
    private void Update()
    {
        constructionTimer -= Time.deltaTime;
        if (constructionTimer <= 0)
        {
            Debug.Log("bomm");
            Instantiate(buildingType.Prefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void PrepareBuildingTypeToConstruction(BuildingTypeScriptableObject buildingType)
    {
        this.buildingType = buildingType;
        spriteRenderer.sprite = buildingType.BuildingSprite;
        
        constructionTimerMax = buildingType.ConstructionTimerMax;
        constructionTimer = constructionTimerMax;

        boxCollider2D.offset = buildingType.Prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider2D.size = buildingType.Prefab.GetComponent<BoxCollider2D>().size;
        buildingTypeClass.buildingType = buildingType;
    }

    public float GetConstructionTimerProgress()
    {
        return (constructionTimer / constructionTimerMax);
    }
}
