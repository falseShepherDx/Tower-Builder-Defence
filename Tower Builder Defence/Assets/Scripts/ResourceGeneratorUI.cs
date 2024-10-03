using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorUI : MonoBehaviour
{
     [SerializeField] private ResourceGenerator _resourceGenerator;
     private Transform bar,text;
     private TextMeshPro _textMeshPro;
     
     

     private void Start()
     {
          
          ResourceGeneratorData resourceData = _resourceGenerator.GetResourceGeneratorData();
          transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceData.resourceType.resourceSprite;
          bar = transform.Find("bar");
          text = transform.Find("text");
          _textMeshPro = text.GetComponent<TextMeshPro>();
     }

     private void Update()
     {
          bar.localScale = new Vector3(1-_resourceGenerator.GetTimer(),1,1);
          _textMeshPro.text = _resourceGenerator.ResourcePerSecond().ToString("F1");
     }
}