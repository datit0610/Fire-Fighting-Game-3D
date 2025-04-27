using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Chao : MonoBehaviour, IInteractable
{
    private Collider collider;
    private Rigidbody rigidObject;
    private Transform grabPoint;
    public float fireRadius;
    private GameManager gameManager;
    private Sound s;
    private GameDatas _gameData;
    private bool mapUnlocked = false;
    private string promptTxt = "";
    private PickAndDrop _pickAndDrop;

    public bool isDayNap = false;
    public PlayerLook player;
    public Transform napChao;
    public VisualEffect fire;
    public Light light;
    public Toggle isFinish;
    public float multiFire;
    public float minFire;
    public float maxFire;
    public int indexMap;
    public bool isComplete;
    public GameObject icon;
    
    private void Awake()
    {
        _pickAndDrop = GameObject.FindObjectOfType<PickAndDrop>();
        AudioManager.instance.PlayMBG("Fire");
        _gameData = SaveSceneUnlocks.Load();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        rigidObject = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        fireRadius = fire.GetFloat("Base_Radius_Flame");
        isFinish.isOn = false;  
        isComplete = false;
    }

    private void Update()
    {
        if (_pickAndDrop.ischao == true)
        {
            gameManager._tasks[0].isOn = true;
            gameManager.icon[0].SetActive(false);
        }
        else
        {
            gameManager._tasks[0].isOn = false;
            gameManager.icon[0].SetActive(true);
        }
        
        if (grabPoint != null)
        {
            promptTxt = "(E) Đậy";
            Vector3 newPos = Vector3.Lerp(transform.position, grabPoint.position, 20f * Time.deltaTime);
            rigidObject.MovePosition(newPos);
        }
        else
        {
            promptTxt = "(E) Nhặt";
        }

        if (isDayNap)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, napChao.position, 20f * Time.deltaTime);
            rigidObject.MovePosition(newPos);
        }

        fire.SetFloat("Base_Radius_Flame", fireRadius);

        FireOff();
    }

    public void Grab(Transform grabPoint)
    {
       
        icon.SetActive(false);
        isDayNap = false;
        this.grabPoint = grabPoint;
        rigidObject.useGravity = false;
        collider.enabled = false;
        rigidObject.freezeRotation = true;
    }

    public void Drop()
    {
        icon.SetActive(true);
        this.grabPoint = null;
        rigidObject.useGravity = true;
        collider.enabled = true;
    }

    public void DayNap()
    {
        icon.SetActive(false);
        promptTxt = "";
        this.grabPoint = null;
        collider.enabled = true;
        isDayNap = true;
        
    }

    private void FireOff()
    {
        if (isDayNap)
        {
            fireRadius -= multiFire;
            fireRadius = Mathf.Clamp(fireRadius, minFire, maxFire);
            light.intensity -= 0.0001f;

            
            
            if (fire.GetFloat("Base_Radius_Flame") <= 0)
            {
                
                isComplete = true;
                unlockMap(indexMap);
                gameManager.winpanel.SetActive(true);
                AudioManager.instance.StopMBG("Fire");
                AudioManager.instance.StopMBG("BaoChay");
                
                
                fire.gameObject.SetActive(false);
                isFinish.isOn = true;
                
                gameManager.pause =true;
            }
        }
        else
        {
            fireRadius += multiFire / 2;
            fireRadius = Mathf.Clamp(fireRadius, minFire, maxFire);
        }
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


        }
        else
        {
            Debug.LogError("Chỉ số bản đồ không hợp lệ: " + mapIndex);
        }
    }

    public void Interact()
    {
        gameManager._tasks[0].isOn = true;
    }

    public void PopUpMessage(bool ischeck)
    {
        
    }

    public string ShowMessage()
    {
        return promptTxt;
    }
}
