using System;
using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject cursor;
    private BuildingTypeScriptableObject activeBuildingType;
    private BuildingTypeList buildingTypeList;
    public EventHandler<OnActiveBuildingTypeChangedEventArgs>  OnActiveBuildingTypeChanged;
    
    public class OnActiveBuildingTypeChangedEventArgs: EventArgs
    {
        
        public BuildingTypeScriptableObject activeBuildingType;
    }
    public static BuildManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeList>(nameof(BuildingTypeList));
        //activeBuildingType = buildingTypeList.list[0];
    }

    private void Update()
    {
        Cursor.visible = false;
        cursor.transform.position = MouseCursorPos.GetMousePos();
        if (Input.GetMouseButtonDown(0) &&!EventSystem.current.IsPointerOverGameObject() &&CanSpawnBuilding(activeBuildingType,MouseCursorPos.GetMousePos()) &&activeBuildingType!=null)
        {
            if (ResourceManager.Instance.CanAfford(activeBuildingType.constructionCostArray))
            {
                ResourceManager.Instance.DecreaseResource(activeBuildingType.constructionCostArray);
                Instantiate(activeBuildingType.prefab, MouseCursorPos.GetMousePos(), Quaternion.identity);
            }
            
        }
    }

  

    public void SetActiveBuildingType(BuildingTypeScriptableObject buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this,new OnActiveBuildingTypeChangedEventArgs()
        {
            activeBuildingType = activeBuildingType
        });
        
    }
    public BuildingTypeScriptableObject GetBuildingTypeByIndex(int index)
    {
        return buildingTypeList.list[index];
    }

    private bool CanSpawnBuilding(BuildingTypeScriptableObject buildingType, Vector3 position)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2Ds=Physics2D.OverlapBoxAll(position+(Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        bool isAreaClear = collider2Ds.Length == 0;
        if (!isAreaClear)
        {
            return false;
        }
        //check the radius for same type of buildings
        collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in collider2Ds)
        {
            BuildingTypeClass buildingTypeHolder = collider2D.GetComponent<BuildingTypeClass>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    //there is a same type of collector in radius
                    return false;
                    
                }
            }
        }
        //if there is a building in max radius then return true
        float maxConstructRadius = 25f;
        collider2Ds = Physics2D.OverlapCircleAll(position, maxConstructRadius);
        foreach (Collider2D collider2D in collider2Ds)
        {
            BuildingTypeClass buildingTypeHolder = collider2D.GetComponent<BuildingTypeClass>();
            if (buildingTypeHolder != null)
            {  
                    return true;
            }
        }
        return false;
    }

}
