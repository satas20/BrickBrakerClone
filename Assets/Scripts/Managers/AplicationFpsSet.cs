using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AplicationFpsSet : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
