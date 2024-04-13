using System;
using Unity.Mathematics;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject cursor;
    private BuildingTypeScriptableObject buildingType;
    private BuildingTypeList buildingTypeList;

    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeList>(nameof(BuildingTypeList));
        buildingType = buildingTypeList.list[0];
    }

    private void Update()
    {
        Cursor.visible = false;
        cursor.transform.position = GetMousePos();
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.prefab, GetMousePos(), Quaternion.identity);
        }
        //just testing
        if (Input.GetKeyDown(KeyCode.E))
        {
            buildingType = buildingTypeList.list[0];
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            buildingType = buildingTypeList.list[1];
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            buildingType = buildingTypeList.list[2];
        }
        
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return mousePos;
    }
}
