using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerNhapVai : MonoBehaviour
{
    [SerializeField] private GameObject panelLost;
    [SerializeField] private GameObject panelWind;

    private void Start()
    {
        Time.timeScale++;
        panelLost.SetActive(false);
        panelWind.SetActive(false);
    }

    private void Update()
    {
        if (Time.timeScale > 60)
        {
            Time.timeScale = 0;
            panelLost.SetActive(true);
        }
    }
}
