using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : NPCComponent
{
    private void Update()
    {
        npc.animator.SetFloat("inputMagnitude",npc.CurrentSpeed);
    }
}
