using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeList")]
public class BuildingTypeList : ScriptableObject
{
    public List<BuildingTypeScriptableObject> list;
    
    public static BuildingTypeList Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
