using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public void PlaySoundButton()
    {
        AudioManager.instance.PlayFX("ButtonPop");
    }

    public void TurnSoundButton()
    {
        AudioManager.instance.PlayFX("Hover");
    }
}
