using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ManagerSceneUnlocks : MonoBehaviour
{
    public GameDatas gameData;
    public Button[] buttons;
    public LoadingScene _loadingScene;

    private void Awake()
    {
        gameData = SaveSceneUnlocks.Load();

        // Kiểm tra dữ liệu
        if (gameData == null)
        {
            Debug.LogError("Dữ liệu tải về là null.");
            return;
        }

        // In ra giá trị của mảng unlocked
        Debug.Log("Dữ liệu unlocked:");
        for (int i = 0; i < gameData.unlocked.Length; i++)
        {
            Debug.Log($"unlocked[{i}] = {gameData.unlocked[i]}");
        }

        
    }

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < gameData.unlocked.Length)
            {
                buttons[i].interactable = gameData.unlocked[i];
            }
        }
    }

    private void Update()
    {
       

        // Lưu dữ liệu sau khi cập nhật
        SaveSceneUnlocks.SaveScene(gameData);
    }
    
    public void LoadSceneButton(int index)
    {
        _loadingScene.LoadLevel(index);
    }
}
