using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class lastAction : MonoBehaviour
{
    public TextMeshProUGUI lastActionText;
    

    public void setLastAction(string text)
    {
        lastActionText.text = text;
    }
}
