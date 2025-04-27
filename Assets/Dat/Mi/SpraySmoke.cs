using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpraySmoke_Mi : MonoBehaviour,IInteractable
{
    
    [SerializeField] private ParticleSystem particleSmoke;
    [SerializeField] private PickupClass_Mi pickupClass;

    public Collider sphereCollider;
    private bool isSmokeActive = false;
    private string promptTxt= "(E) Nhặt";
    public SpraySmoke_Mi()
    {
        
    }
    
   
    void Start()
    {
        
        sphereCollider.enabled = false;
        particleSmoke.Stop();
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && pickupClass.use == Useable.Use)
        {
            if (!isSmokeActive)
            {
                // Bật hiệu ứng và âm thanh
                AudioManager.instance.PlayMBG("CO2");
                sphereCollider.enabled = true;
                particleSmoke.Play();
                isSmokeActive = true;
            }
        }
        else
        {
            if (isSmokeActive)
            {
                // Tắt hiệu ứng và âm thanh
                AudioManager.instance.StopMBG("CO2");
                sphereCollider.enabled = false;
                particleSmoke.Stop();
                isSmokeActive = false;
            }
        }
    }


    public void Interact()
    {
        
    }

    public void PopUpMessage(bool ischeck)
    {
        throw new NotImplementedException();
    }

    public string ShowMessage()
    {
        return promptTxt;
    }
}
