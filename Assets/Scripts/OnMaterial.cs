using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMaterial : MonoBehaviour, IInteractable
{
    public Material material;
    private string promptTxt = "(E) Bấm";
    private void Start()
    {
        material.SetFloat("_isPush", 0);
    }

    public void Interact()
    {
            material.SetFloat("_isPush", 1);
    }

    public void PopUpMessage(bool ischeck)
    {
        MessageManager.ShowMessage(ischeck);
    }

    public string ShowMessage()
    {
        return promptTxt;
    }
}
