using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _goPC;
    [SerializeField] List<GameObject> _goMobile;

    public static DeviceManager instance;

    private void Awake()
    {
        instance = this;
#if UNITY_EDITOR
        if (Application.isEditor)
        {
            bool check = EditorUtility.DisplayDialog("Što želiš testirati?", "Odaberi koju tehnologiju želiš testirati.", "Platno", "Mobitel");
            device = check;
            DeviceManager.instance.SetDevice(check);

            if (check)
            {
                GameViewUtils.AddAndSelectCustomSize(1920, 1080);
                SetGameViewScale(0.47f);
            }
            else
            {
                GameViewUtils.SetSize(GameViewUtils.FindSize(GameViewSizeGroupType.Standalone, "Mob"));
                SetGameViewScale(1);
            }
        }
#endif
    }
    private void Start()
    {
        

        if (Application.isEditor)
            return;


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

    public void SetDevice(bool value)
    {
        if (value)
        {
            foreach (var item in _goPC) { item.SetActive(true); }
            foreach (var item in _goMobile) { item.SetActive(false); }
        }
        else
        {
            foreach (var item in _goMobile) { item.SetActive(true); }
            foreach (var item in _goPC) { item.SetActive(false); }
        }
    }


#if UNITY_EDITOR

    bool device;
    float time = 2;


    private void Update()
    {
        if (Application.isEditor)
        {
            if (time > 0) { time -= Time.deltaTime; }
            if (time <= 0)
                return;

            if (device)
            {
                SetGameViewScale(0.47f);
            }
            else
            {
                SetGameViewScale(1);
            }
        }
    }


    void SetGameViewScale(float value)
    {
        System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
        System.Type type = assembly.GetType("UnityEditor.GameView");
        UnityEditor.EditorWindow v = UnityEditor.EditorWindow.GetWindow(type);

        var defScaleField = type.GetField("m_defaultScale", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        //whatever scale you want when you click on play
        float defaultScale = value;

        var areaField = type.GetField("m_ZoomArea", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var areaObj = areaField.GetValue(v);

        var scaleField = areaObj.GetType().GetField("m_Scale", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        scaleField.SetValue(areaObj, new Vector2(defaultScale, defaultScale));
    }
    #endif
}
