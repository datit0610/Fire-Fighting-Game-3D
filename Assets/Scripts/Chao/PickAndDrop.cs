using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PickAndDrop : MonoBehaviour
{
    public float range;
    public Transform grabPoint;
    public Transform source;
    public LayerMask layer;
    public bool ischao;
    private Chao chao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hitInfo;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (chao == null) 
            {
                if (Physics.Raycast(source.position, source.TransformDirection(Vector3.forward), out hitInfo, range, layer))
                {
                    if (hitInfo.collider.TryGetComponent(out chao))
                    {
                        ischao = true;
                        AudioManager.instance.PlayFX("Pick_Drop");
                        chao.Grab(grabPoint);
                        
                    }
                }
            }
            else
            {
                if (Physics.Raycast(source.position, source.TransformDirection(Vector3.forward), out hitInfo, range, layer))
                {
                    if (hitInfo.collider.tag == "Chao")
                    {
                        chao.DayNap();
                        chao = null;
                        AudioManager.instance.PlayFX("Pan");
                    }
                    
                }
                else
                {
                    AudioManager.instance.PlayFX("Pick_Drop");
                    chao.Drop();
                    chao = null;
                }
            }    
        }
    }
}
