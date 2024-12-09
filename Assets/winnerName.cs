using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class winnerName : MonoBehaviour
{
    public TextMeshProUGUI winnersNameText;

    public void setName(string name)
    {
        winnersNameText.text = name;
    }

}
