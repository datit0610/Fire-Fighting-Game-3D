using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject nvPanel;
    public PlayerLook player;
    public GameObject winpanel;
    public Toggle isFinish;
    public GameObject taskPanel;
    public bool pause;
    public Toggle[] _tasks;
    public GameObject[] icon;
    
    
    private InputManager inputManager;
    private SpraySmoke_Hoang smoke;
    private PickupClass_Mi _pickupClassMi;
    private Interactor _interactor;
    private Chao _chao;
    
    
    private void Start()
    {
        
        _chao = GameObject.FindObjectOfType<Chao>();
        _interactor = GameObject.FindObjectOfType<Interactor>();
        _pickupClassMi = FindObjectOfType<PickupClass_Mi>();
        inputManager = FindObjectOfType<InputManager>();
        smoke = GameObject.FindObjectOfType<SpraySmoke_Hoang>();
        AudioManager.instance.StopMBG("MenuGame");
        winpanel.SetActive(false);
        nvPanel.SetActive(false);
        isFinish.isOn = false;
        pause = true;
    }

    private void Update()
    {
        if (pause)
        {
            Cursor.lockState = CursorLockMode.None;
            player.enabled = false;
            
            smoke.enabled = false;
            inputManager.enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            inputManager.enabled = true;
            smoke.enabled = true;
            player.enabled = true;
            Time.timeScale = 1;
        }
        Pause();
        Task();
    }

    private void Task()
    {
        if (_interactor.baochay == true)
        {
            _tasks[0].isOn = true;
        }
        
        if (_pickupClassMi.binhcuaHoa == true )
        {
            _tasks[0].isOn = true;
            icon[0].SetActive(false);
        }
        else
        {
            _tasks[0].isOn = false;
            icon[0].SetActive(true);
        }

        if (_pickupClassMi.khan == true)
        {
            _tasks[1].isOn = true;
            icon[1].SetActive(false);
        }
        else
        {
            if (_pickupClassMi.use == Useable.Khan)
            {
                _tasks[1].isOn = false;
                icon[1].SetActive(true);
            }
            
        }
    }
    
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.instance.PlayFX("ButtonPop");
            pause = !pause;
            if (pause)
            {
                Cursor.lockState = CursorLockMode.None;
                
                open();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Close();
            }
            
        }
    }

    public void ClosePanel()
    {
        AudioManager.instance.PlayFX("ButtonPop");
        pause = !pause;
    }

    public void BacktoMenu()
    {
        AudioManager.instance.PlayMBG("MenuGame");
        AudioManager.instance.StopMBG("Fire");
        AudioManager.instance.StopMBG("BaoChay");
        AudioManager.instance.PlayFX("ButtonPop");
        SceneManager.LoadScene(0);
        pause = false;
        
    }
    public void RestartGame(int indexScene)
    {
        AudioManager.instance.PlayFX("ButtonPop");
        AudioManager.instance.StopMBG("Fire");
        AudioManager.instance.StopMBG("BaoChay");
        SceneManager.LoadScene(indexScene);
    }

    public void Close()
    {
        nvPanel.SetActive(true);
        taskPanel.SetActive(false);
        /*taskPanel.transform.DOLocalMove(pos, timePos).SetEase(Ease.OutQuint);
        taskPanel.transform.DOScale(scale, timeScale).SetEase(Ease.OutQuint);*/
    }
    public void open()
    {
        nvPanel.SetActive(false);
        taskPanel.SetActive(true);
        /*taskPanel.transform.DOLocalMove(posOpen, timePos).SetEase(Ease.OutQuint);
        taskPanel.transform.DOScale(scaleOpen, 0.5f).SetEase(Ease.OutQuint);*/
    }
}
