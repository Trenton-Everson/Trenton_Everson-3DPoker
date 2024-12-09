using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerHand : MonoBehaviour
{
    public TextMeshProUGUI winnersHandText;

    public void setHand(string hand)
    {
        winnersHandText.text = hand;
    }
}
