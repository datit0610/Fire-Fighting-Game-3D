using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Khi_Gas : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    private void Start()
    {
        ps.Play();
    }
}
