using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class OpenCloseMainDoor : MonoBehaviour, IInteractable
{
    public GameObject door;
    public bool isCurrentlyOpen;
    private string promptTxt;
    private Animator animator;
    
    
    private EState currentState;
    private void Start()
    {
       
        animator = door.GetComponent<Animator>();
        isCurrentlyOpen = animator.GetBool("isOpen");
        
    }

    public void Interact()
    {
       

        animator.SetBool("isOpen", !animator.GetBool("isOpen")); // Đảo trạng thái cửa
        isCurrentlyOpen = animator.GetBool("isOpen");
        
        
        // Phát âm thanh dựa vào trạng thái trước khi thay đổi
        if (!isCurrentlyOpen)
        {
            
            AudioManager.instance.PlayFX("OpenDoor");
        }
        else
        {
            
            AudioManager.instance.PlayFX("CloseDoor");
        }
    }

    

    private void Update()
    {
        if (!isCurrentlyOpen)
        {
            
            promptTxt = "(E) Mở";
        }
        else
        {
            currentState = EState.run;
            promptTxt = "(E) Đóng";
        }
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