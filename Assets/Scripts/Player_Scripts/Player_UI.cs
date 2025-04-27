using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_UI : MonoBehaviour
{
    public TextMeshProUGUI promptText;

    public void UpdateText(string prompt)
    {
        promptText.text = prompt;
    }
}
