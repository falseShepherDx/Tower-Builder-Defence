using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransparentBuilding : MonoBehaviour
{
    private GameObject sprite;
    private BuildManager _buildManager;
    private void Awake()
    {
        sprite = transform.Find("sprite").gameObject;
        Hide();
    }

    private void Start()
    {
        _buildManager = BuildManager.Instance;
        _buildManager.OnActiveBuildingTypeChanged += TransparentBuilding_OnActiveTypeChanged;
    }

    private void TransparentBuilding_OnActiveTypeChanged(object sender, BuildManager.OnActiveBuildingTypeChangedEventArgs eventArgs)
    {
        if(eventArgs.activeBuildingType==null) Hide();
        else
        {
            Show(eventArgs.activeBuildingType.buildingSprite);
        }
    }

    private void Update()
    {
        transform.position = MouseCursorPos.GetMousePos();
    }

    public void Show(Sprite transparentSprite)
    {
        sprite.gameObject.SetActive(true);
        sprite.GetComponent<SpriteRenderer>().sprite = transparentSprite;
    }

    public void Hide()
    {
        sprite.gameObject.SetActive(false);
        
    }
}
