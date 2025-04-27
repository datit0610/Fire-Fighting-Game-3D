using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractable
{
    public Floors floors;
    public GameObject Door;
    public GameObject Door1;
    private string promptTxt = "(E) Bấm";
    public void Interact()
    {
        /*if (floors.canOpen)
        {
            Door.GetComponent<Animator>().SetBool("isOpen", true);
            Door1.GetComponent<Animator>().SetBool("isOpen", true);
        }
        else
            return;*/
        StartCoroutine(WaitAndAnimate());
    }

    public void PopUpMessage(bool ischeck)
    {
        MessageManager.ShowMessage(ischeck);
    }

    public string ShowMessage()
    {
        return promptTxt;
    }


    private IEnumerator WaitAndAnimate()
    {
        if (floors.canOpen)
        {
            Door.GetComponent<Animator>().SetBool("isOpen", true);
            Door1.GetComponent<Animator>().SetBool("isOpen", true);
            yield return new WaitForSeconds(3);
            Door.GetComponent<Animator>().SetBool("isOpen", false);
            Door1.GetComponent<Animator>().SetBool("isOpen", false);
        }
        Debug.Log(floors.canOpen);
    }
}
