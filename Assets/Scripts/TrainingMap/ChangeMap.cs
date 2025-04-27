using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMap : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    private int currentMap;

    private void Awake()
    {
        SelectMap(0);
    }
    private void SelectMap(int index)
    {
        previousButton.gameObject.SetActive(index != 0);
        nextButton.gameObject.SetActive(index != transform.childCount - 1);
        for (int i = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void ChangesMap(int change)
    {
        AudioManager.instance.PlayFX("ButtonPop");
        currentMap += change;
        SelectMap(currentMap);
    }
}
