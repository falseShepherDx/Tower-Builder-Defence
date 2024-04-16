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
    public EventHandler OnBuildSelected;
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
        cursor.transform.position = GetMousePos();
        if (Input.GetMouseButtonDown(0) &&!EventSystem.current.IsPointerOverGameObject())
        {
            Instantiate(activeBuildingType.prefab, GetMousePos(), Quaternion.identity);
        }
        
        
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return mousePos;
    }

    public void SetActiveBuildingType(BuildingTypeScriptableObject buildingType)
    {
        activeBuildingType = buildingType;
        OnBuildSelected?.Invoke(buildingType,EventArgs.Empty);
        
    }
    public BuildingTypeScriptableObject GetBuildingTypeByIndex(int index)
    {
        return buildingTypeList.list[index];
    }

}
