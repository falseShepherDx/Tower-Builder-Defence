using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    private Light2D light2D;
    [SerializeField] private Gradient gradient;
    [SerializeField] private float secondsPerDay;
    private float dayTime;
    private float dayTimeSpeed;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        dayTimeSpeed = 1 / secondsPerDay;
    }

    private void Update()
    {
        dayTime += dayTimeSpeed * Time.deltaTime;
        light2D.color=gradient.Evaluate(dayTime % 1f);
    }
}
