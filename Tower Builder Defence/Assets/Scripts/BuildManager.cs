using System;
using Unity.Mathematics;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject cursor;
    [SerializeField] private BuildingTypeScriptableObject buildingType;
    
    
    private void Update()
    {
        Cursor.visible = false;
        cursor.transform.position = GetMousePos();
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.prefab, GetMousePos(), Quaternion.identity);
        }
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return mousePos;
    }
}
