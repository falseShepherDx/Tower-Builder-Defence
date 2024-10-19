using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    [SerializeField] private OptionsUI optionsUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            optionsUI.ToggleOptions();
        }
        
    }
}
