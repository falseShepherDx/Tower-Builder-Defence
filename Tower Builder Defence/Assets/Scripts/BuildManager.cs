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
        if (Input.GetMouseButtonDown(0) &&!EventSystem.current.IsPointerOverGameObject())
        {
            if(activeBuildingType==null)return;
            Instantiate(activeBuildingType.prefab, MouseCursorPos.GetMousePos(), Quaternion.identity);
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

}
