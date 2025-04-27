using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour, IInteractable
{
    public int floorIndex; // Chỉ số tầng tương ứng
    public Floors floors;
    private string promptTxt = "(E) Bấm";
    public void Interact()
    {
        if (floors != null)
        {
            
            floors.floorbools[floorIndex] = true;
            Debug.Log($"Đã click vào tầng {floorIndex + 1}");
        }
    }

    public void PopUpMessage(bool ischeck)
    {
        MessageManager.ShowMessage(ischeck);
    }

    public string ShowMessage()
    {
        return promptTxt;
    }
}
