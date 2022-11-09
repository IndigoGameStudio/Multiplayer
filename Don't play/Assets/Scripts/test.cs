using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    private GameObject img;
    // Start is called before the first frame update
    void Start()
    {
        if(CheckDevice.instance.isMobile())
        {
            if (img != null) { img.SetActive(true); }

            if (TryGetComponent(out TextMeshProUGUI tekst))
            {
                tekst.text = "You are using Mobile";
            }
        }
        else
        {
            if (TryGetComponent(out TextMeshProUGUI tekst))
            {
                tekst.text = "You are using PC";
            }
        }
    }

}
