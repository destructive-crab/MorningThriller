using System.Collections;
using System.Collections.Generic;
using MothDIed;
using TMPro;
using UnityEngine;

namespace rainy_morning
{
    public class SavedDataDisplay : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<TMP_Text>().text = "";
            
            foreach (var VARIABLE in Game.RunData.PlayerData.stuff)
            {
                GetComponent<TMP_Text>().text += $"\n {VARIABLE}";
            }
        }
    }
}
