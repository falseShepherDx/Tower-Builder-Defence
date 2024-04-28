using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceSourceSpriteRandomizer : MonoBehaviour
{
    public List<Sprite> resourceSourceSprites;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (resourceSourceSprites.Count > 0)
        {
            _spriteRenderer.sprite = resourceSourceSprites[Random.Range(0, resourceSourceSprites.Count)];
        }
        else
        {
            Debug.Log("list is empty");
        }
    }

    
}
