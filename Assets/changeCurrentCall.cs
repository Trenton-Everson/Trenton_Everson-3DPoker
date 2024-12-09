using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class changeCurrentCall : MonoBehaviour
{
    public TextMeshProUGUI callsAmount;

    public void setCallAmount(int callAmount)
    {
        callsAmount.text = callAmount.ToString();
    }
}
