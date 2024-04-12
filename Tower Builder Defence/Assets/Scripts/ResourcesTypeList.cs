using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/ResourcesList")]
public class ResourcesTypeList : ScriptableObject
{
    public List<ResourceTypeScriptableObject> resourceList;
}
