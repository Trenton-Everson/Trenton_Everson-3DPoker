using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPot : MonoBehaviour
{
   public int pot = 0;
    [SerializeField] private TextMeshProUGUI _potAmmount;
   public void ChangePot(int bet)
   {
    pot += bet;
    _potAmmount.text = pot + "";
   }
}
