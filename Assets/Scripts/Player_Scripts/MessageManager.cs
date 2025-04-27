using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour, IInteractable
{
   
    public GameObject messageBox;
    private static MessageManager _instance;
    public static MessageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MessageManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        // Kiểm tra xem có instance nào khác không
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
       
    }
    // Được gọi khi scene được tải
    private void OnEnable()
    {
        if (messageBox == null)
        {
            messageBox = GameObject.Find("MessagePopOut");
            messageBox.SetActive(false);
            
            if (messageBox == null)
            {
                Debug.LogWarning("Không tìm thấy MessagePopOut trong scene!");
            }
        }
    }
    private void Start()
    {
        
        if (messageBox == null)
        {
            messageBox = GameObject.Find("MessagePopOut");
            messageBox.SetActive(false);
        }
      
    }

    public void PopUpMessage(bool ischeck)
    {
        
        if (messageBox != null)
        {
            messageBox.SetActive(ischeck);
        }
        else
        {
            messageBox = GameObject.Find("MessagePopOut");
            Debug.LogError("messageBox chưa được gán!");
        }
    }

    public string ShowMessage()
    {
        return null;
    }

    // Phương thức static để sử dụng từ bất kỳ script nào khác
    public static void ShowMessage(bool isVisible)
    {
        if (Instance != null)
        {
            Instance.PopUpMessage(isVisible);
        }
    }
    
    public void Interact()
    {
       
    }

}
