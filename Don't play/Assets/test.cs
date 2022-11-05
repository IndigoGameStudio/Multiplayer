using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(CheckDevice.instance.isMobile())
        {
            GetComponent<TextMeshProUGUI>().text = "You are using Mobile";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "You are using PC";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
