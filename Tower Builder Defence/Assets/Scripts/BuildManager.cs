using System;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField] private Building hqBuilding;
    
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

    private void Start()
    {
        hqBuilding.GetComponent<HealthSystem>().OnDied += hqBuilding_OnDied;
        
    }

    private void hqBuilding_OnDied(object sender, EventArgs e)
    {
       GameOverUI.Instance.Show();
    }

    private void Update()
    {
        Cursor.visible = false;
        cursor.transform.position = MouseCursorPos.GetMousePos();
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType != null)
            {
                if (CanSpawnBuilding(activeBuildingType, MouseCursorPos.GetMousePos(), out string tipMessage))
                {
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.ConstructionCostArray))
                    {
                        ResourceManager.Instance.DecreaseResource(activeBuildingType.ConstructionCostArray);
                        
                        //Instantiate(activeBuildingType.prefab, MouseCursorPos.GetMousePos(), quaternion.identity);
                        BuildingConstruction.Create(MouseCursorPos.GetMousePos(),activeBuildingType);
                    }
                    else
                    {
                        PlayerTipUI.Instance.Show("Cant afford. "+ activeBuildingType.GetCollectorCostString(),new PlayerTipUI.TipMessageTimer {timer = 2f});
                    }
                }
                else
                {
                    PlayerTipUI.Instance.Show(tipMessage,new PlayerTipUI.TipMessageTimer {timer = 2f});
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 enemyPosition = MouseCursorPos.GetMousePos() + MouseCursorPos.RandomizeSpawnDirection() * 5f;
            Enemy.Create(enemyPosition);
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

    private bool CanSpawnBuilding(BuildingTypeScriptableObject buildingType, Vector3 position,out string tipMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.Prefab.GetComponent<BoxCollider2D>();
        Collider2D[] collider2Ds=Physics2D.OverlapBoxAll(position+(Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        bool isAreaClear = collider2Ds.Length == 0;
        if (!isAreaClear)
        {
            tipMessage = "Area is not clear";
            return false;
        }
        //check the radius for same type of buildings
        collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.MinConstructionRadius);
        foreach (Collider2D collider2D in collider2Ds)
        {
            BuildingTypeClass buildingTypeHolder = collider2D.GetComponent<BuildingTypeClass>();
            if (buildingTypeHolder != null)
            {
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    //there is a same type of collector in radius
                    tipMessage = "Too close to another building of the same type!";
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
                tipMessage = "";
                    return true;
            }
        }

        tipMessage = "Too far from any other building";
        return false;
    }

    public Building GetHqBuilding()
    {
        return hqBuilding;
    }

}
