using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
    void PopUpMessage(bool ischeck);
    string ShowMessage();
}

//Thang May
public class Interactor : MonoBehaviour
{
    public float range;
    public Transform source;
    public LayerMask layer;
    private IInteractable currentInteractable;
    public bool baochay;
   

    private GameManager gameManager;
    public EState currentState;
    
    
    private Player_UI playerUI;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        playerUI = GetComponent<Player_UI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
       
        
        RaycastHit hitInfo;
        
            
            if (Physics.Raycast(source.position, source.TransformDirection(Vector3.forward), out hitInfo, range))
            {
                /*if (hitInfo.collider.TryGetComponent(out IInteractable _interactable))
                {
                    isCheck = true;
                    currentInteractable = _interactable;
                    currentInteractable.PopUpMessage(isCheck);
                    
                }
                else
                {
                    isCheck = false;
                    if(currentInteractable != null)
                        currentInteractable.PopUpMessage(isCheck);
                }*/
                
                
                    if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
                    {
                        playerUI.UpdateText(hitInfo.collider.GetComponent<IInteractable>().ShowMessage().ToString());
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            interactable.Interact();
                        }
                    }

                    
                    
                    if (hitInfo.collider.CompareTag("BaoChay"))
                    {
                       
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            currentState = EState.run;
                            AudioManager.instance.PlayMBG("BaoChay");
                            baochay = true;
                        }
                    }
                
            }
            /*else
            {
                if (currentInteractable != null)
                {
                    isCheck = false;
                    currentInteractable.PopUpMessage(isCheck);
                    Debug.Log("goi");
                    currentInteractable = null;
                }
                
            }*/
            
        }
    }

