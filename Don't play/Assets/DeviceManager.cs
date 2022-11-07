using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _goPC;
    [SerializeField] List<GameObject> _goMobile;


    private void Start()
    {

        if (CheckDevice.instance.isMobile())
        {
            foreach (var item in _goMobile) { item.SetActive(true); }
            foreach (var item in _goPC) { item.SetActive(false); }
        }
        else
        {
            foreach (var item in _goPC) { item.SetActive(true); }
            foreach (var item in _goMobile) { item.SetActive(false); }
        }

        
    }
}
