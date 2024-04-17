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
   
}
