using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireManager : MonoBehaviour
{
    private List<FireVEG> allFires = new List<FireVEG>();
    private List<HeatSource> allHeatSources = new List<HeatSource>();
    private GameDatas _gameData;
    private bool mapUnlocked = false;
    
    
    
    
    
    
    private void Awake()
    {
        _gameData = SaveSceneUnlocks.Load();
    }
    
    private void Start()
    {
        // Tìm tất cả các nguồn lửa trong cảnh
        FireVEG[] fireVEGs = FindObjectsOfType<FireVEG>();
        HeatSource[] heatSources = FindObjectsOfType<HeatSource>();
        
        foreach (FireVEG fire in fireVEGs)
        {
            allFires.Add(fire);
        }
        
        foreach (HeatSource source in heatSources)
        {
            allHeatSources.Add(source);
        }
        
        
        Debug.Log("Đã tìm thấy " + allFires.Count + " đám cháy và " + allHeatSources.Count + " nguồn nhiệt.");
    }
    
    private void Update()
    {
        
        if (mapUnlocked)
            return;
        
        // Kiểm tra nếu tất cả các đám cháy đã tắt
        bool allFiresOut = true;
        
        foreach (FireVEG fire in allFires)
        {
            if (fire.fireState == FireState.OnFire || (fire.fireState == FireState.CO2 && fire.flameRadius > 0.01f))
            {
                allFiresOut = false;
                break;
            }
        }
        
        // Nếu tất cả đám cháy đã tắt, mở khóa bản đồ
        if (allFiresOut && allFires.Count > 0)
        {
            //unlockMap(mapIndex);
        }

        
    }
    
    
    
    
}