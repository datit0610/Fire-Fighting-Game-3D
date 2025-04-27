using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpraySmoke_Hoang : MonoBehaviour,IInteractable
{
    
    [SerializeField] private ParticleSystem particleSmoke;
    [SerializeField] private PickupClass_Mi pickupClass;
    private bool isSmokeActive = false;
    //public BoxCollider sphereCollider;
    
    private string promptTxt= "(E) Nhặt";
    

    public SpraySmoke_Hoang()
    {
        
    }
    
   
    void Start()
    {
        //sphereCollider.enabled = false;
        particleSmoke.Stop();
        
    }

    

    void Update()
    {
        if (Input.GetMouseButton(0) && pickupClass.use == Useable.Use)
        {
            /*if (particleSmoke.isPlaying)
            {
                //sphereCollider.enabled = false;
                particleSmoke.Stop();
                
            }
            else*/
            {
                if (!isSmokeActive)
                {
                    particleSmoke.Play();
                    AudioManager.instance.PlayMBG("CO2");
                    isSmokeActive = true;
                }
                //sphereCollider.enabled = true;
                
            }
        }
        else
        {
            if (isSmokeActive)
            {
                isSmokeActive = false;
                particleSmoke.Stop();
                AudioManager.instance.StopMBG("CO2");
            }
            
            
        }
        
    }


    public void Interact()
    {
        
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
