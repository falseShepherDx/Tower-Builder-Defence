using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseCursorPos
{

    private static Camera mainCamera;
    public static Vector3 GetMousePos()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return mousePos;
    }

    public static Vector3 RandomizeSpawnDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
   
}
