using System;
using UnityEngine;
using UnityEngine.VFX;

public class FireVEG_Hoang : MonoBehaviour
{
    public float flameRadius;
    public VisualEffect effect;
    public int indexMap;
    public FireState fireState;
    public float multiFire;
    public float minFire;
    public float maxFire;

    private FireState state;
    private CapsuleCollider capsuleCollider;
    private FireManager fireManager;
    private GameDatas _gameData;
    private bool mapUnlocked = false;
    private Chay_ODien chayODien;
    private GameManager gameManager;
    private PlayerMotor _playerMotor;
    
    private void Awake()
    {
        _playerMotor = FindObjectOfType<PlayerMotor>();
        _gameData = SaveSceneUnlocks.Load();
        AudioManager.instance.PlayMBG("Fire");
        
    }
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        effect = GetComponent<VisualEffect>();
        flameRadius = effect.GetFloat("Base_Radius_Flame");
        fireState = FireState.OnFire;
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.radius = flameRadius;
        capsuleCollider.height = flameRadius;
    }

    private void Update()
    {
        if (_playerMotor.unlockMap == true)
        {
            unlockMap(4);
        }
        if (fireState == FireState.OnFire)
        {
            if (flameRadius >= 0.3f)
            {
                capsuleCollider.radius = flameRadius;
                capsuleCollider.height = flameRadius;
            }
            flameRadius += multiFire * Time.deltaTime;
            flameRadius = Mathf.Clamp(flameRadius, minFire, maxFire);
            effect.SetFloat("Base_Radius_Flame", flameRadius);
        }
        if (flameRadius == 0)
        { 
            gameManager.pause =true;
            unlockMap(indexMap);
            this.gameObject.SetActive(false);
            gameManager.winpanel.SetActive(true);
            AudioManager.instance.StopMBG("Fire");
            AudioManager.instance.StopMBG("BaoChay");
            AudioManager.instance.StopMBG("CO2");
        }
    }
    private void LateUpdate()
    {
        fireState = FireState.OnFire;
    }

    public void unlockMap(int mapIndex)
    {
        if (mapUnlocked)
            return;
            
        if (mapIndex >= 0 && mapIndex < _gameData.unlocked.Length)
        {
            _gameData.unlocked[mapIndex] = true;
            AudioManager.instance.PlayFX("Winner");
            Debug.Log("Tất cả đám cháy đã được dập tắt! Mở khóa bản đồ: " + mapIndex);
            SaveSceneUnlocks.SaveScene(_gameData);
            mapUnlocked = true;
            
            gameManager.isFinish.isOn = true;
        }
        else
        {
            Debug.LogError("Chỉ số bản đồ không hợp lệ: " + mapIndex);
        }
    }

    

    private void OnParticleCollision(GameObject other)
    {
        fireState = FireState.Normal;
        flameRadius -= 2 * multiFire * Time.deltaTime;
        flameRadius = Mathf.Clamp(flameRadius, minFire, maxFire);
        effect.SetFloat("Base_Radius_Flame", flameRadius);
    }
}