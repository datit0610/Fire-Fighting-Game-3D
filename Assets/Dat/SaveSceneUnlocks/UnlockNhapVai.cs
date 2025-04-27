using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockNhapVai : MonoBehaviour
{
    private GameDatas gameDatas;

    private void Start()
    {
        gameDatas = SaveSceneUnlocks.Load();
    }

    public void OpenMap(int index)
    {
        if (gameDatas.unlocked[4] == true)
        {
            SceneManager.LoadScene(index);
        }
        
    }
}
