using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAberration : MonoBehaviour
{
   private Volume volume;
   public static ChromaticAberration Instance { get; private set; }
   private void Awake()
   {
      Instance = this;
      volume = GetComponent<Volume>();
   }

   private void Update()
   {
      if (volume.weight > 0)
      {
         float _decreaseSpeed = 1f;
         volume.weight -= _decreaseSpeed * Time.deltaTime;
      }
   }
   public void SetWeight(float newVolume)
   {
      volume.weight = newVolume;
      
   }
}
