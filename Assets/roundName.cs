using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class roundName : MonoBehaviour
{
   public TextMeshProUGUI namer;

   public void setName(string givenName)
   {
        namer.text = givenName;
   }
}
