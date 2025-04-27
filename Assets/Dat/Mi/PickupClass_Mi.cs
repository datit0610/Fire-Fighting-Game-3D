using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Useable
{
    None,
    Use,
    Khan,
}
public class PickupClass_Mi : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private float ThrowingForce;
    [SerializeField] private float PickupRange;
    [SerializeField] private Transform hand;
    
    private Rigidbody currentObjectRigidbody;
    private Collider currentObjectCollider;
    
    public Useable use;
    private GameManager gameManager;
    public bool binhcuaHoa;
    public bool khan;
    public PickupClass_Mi()
    {
        
    }
    void Start()
    {
        
       gameManager = FindObjectOfType<GameManager>();
        use = Useable.None;
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            Ray PickupRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(PickupRay, out RaycastHit hitInfo, PickupRange))
            {
               
                
                if (hitInfo.collider.tag == "BinhCuuHoa")
                {
                    binhcuaHoa = true;
                   // gameManager._tasks[0].isOn = true;
                    use = Useable.Use;
                   // gameManager.icon[0].SetActive(false);
                    AudioManager.instance.PlayFX("Loot");
                    if (currentObjectRigidbody)
                    {
                        
                        currentObjectRigidbody.isKinematic = false;
                        currentObjectCollider.enabled = true;
                    
                        currentObjectRigidbody = hitInfo.rigidbody;
                        currentObjectCollider = hitInfo.collider;
                    
                        currentObjectRigidbody.isKinematic = true;
                        currentObjectCollider.enabled = false;
                    }
                    else
                    {
                        
                        currentObjectRigidbody = hitInfo.rigidbody;
                        currentObjectCollider = hitInfo.collider;

                        currentObjectRigidbody.isKinematic = true;
                        currentObjectCollider.enabled = false;
                    }
                    return;
                }

                if (hitInfo.collider.tag == "KhanUot")
                {
                    
                    use = Useable.Khan;
                    khan = true;
                    //gameManager.icon[1].SetActive(false);//icon khăn ướt
                    //gameManager._tasks[0].isOn = true;//toggle Khan uot
                    AudioManager.instance.PlayFX("Loot");
                    if (currentObjectRigidbody)
                    {
                        
                        currentObjectRigidbody.isKinematic = false;
                        currentObjectCollider.enabled = true;
                    
                        currentObjectRigidbody = hitInfo.rigidbody;
                        currentObjectCollider = hitInfo.collider;
                    
                        currentObjectRigidbody.isKinematic = true;
                        currentObjectCollider.enabled = false;
                    }
                    else
                    {
                        
                        currentObjectRigidbody = hitInfo.rigidbody;
                        currentObjectCollider = hitInfo.collider;

                        currentObjectRigidbody.isKinematic = true;
                        currentObjectCollider.enabled = false;
                    }
                    return;
                }
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            use = Useable.None;
            binhcuaHoa = false;
            //gameManager.icon[0].SetActive(true);
            if (use == Useable.Khan)
            {
                //gameManager.icon[1].SetActive(true);
                khan = false;
            }
            
            if (currentObjectRigidbody)
            {
                AudioManager.instance.PlayFX("Pickup_Drop");
                currentObjectRigidbody.isKinematic = false;
                currentObjectCollider.enabled = true;
                
                currentObjectRigidbody.AddForce(playerCamera.transform.forward * ThrowingForce, ForceMode.Impulse);
                
                currentObjectRigidbody = null;
                currentObjectCollider = null;
            }
        }

        if (currentObjectRigidbody)
        {
            currentObjectRigidbody.position = hand.position;
            currentObjectRigidbody.rotation = hand.rotation;
        }
    }

    public void OnDrawGizmos()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * PickupRange, Color.red);
    }
}
