using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Khan : MonoBehaviour, IInteractable
{
    private string promptTxt = "(E) Nhặt";
    
    public void Interact()
    {
        
    }

    public void PopUpMessage(bool ischeck)
    {
        
    }

    public string ShowMessage()
    {
        return promptTxt;
    }
}
