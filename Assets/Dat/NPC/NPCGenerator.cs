using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] NPC NPCPrefab;
    
    [SerializeField] Area area;

    [SerializeField] private int Count = 15;
    private NPC npc;
    [SerializeField] private GameObject parent;
    private void Start()
    {
        for (int i = 0; i < Count; i++)
        {
            Vector3 position = area.GetRandomPoint();
            
            Quaternion rotation = Quaternion.Euler(0f,Random.Range(0f,360f), 0f);
            
            npc = Instantiate(NPCPrefab, position, rotation);
            
            npc.transform.SetParent(parent.transform);
            
            npc.GetComponent<NPCWander>()._area = area;
        }
    }

    
}
