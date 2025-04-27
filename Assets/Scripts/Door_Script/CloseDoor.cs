using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour, IInteractable
{
    public GameObject Door;
    public GameObject Door1;
    private string promptTxt = "(E) Bấm";

    public void Interact()
    {
        Door.GetComponent<Animator>().SetBool("isOpen", false);
        Door1.GetComponent<Animator>().SetBool("isOpen", false);
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
