using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Chips : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chips;
    public void UpdateChips(int givenChips)
    {
        chips.text = givenChips.ToString();
    }
}
