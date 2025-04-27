using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCComponent : MonoBehaviour
{
    protected NPC npc;
    protected OpenCloseMainDoor _door;
    protected virtual void Awake()
    {
        _door = FindObjectOfType<OpenCloseMainDoor>();
        npc = GetComponentInParent<NPC>();
    }
}
