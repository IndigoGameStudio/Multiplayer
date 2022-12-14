using Photon.Pun;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _goPC;
    [SerializeField] List<GameObject> _goMobile;

    int device;
    float time = 2;
    public static DeviceManager instance;

    private void Awake()
    {
        instance = this;
        ChooseDevice();
    }

    private void ChooseDevice()
    {
        #if UNITY_EDITOR
        if (Application.isEditor && !PlayerPrefs.HasKey("FixSpam"))
        {
            int option = EditorUtility.DisplayDialogComplex("What do you want to test?",
            "Choose which device you want to test.",
            "Canvas",
            "None",
            "Mobile");

            device = option;
            PhotonNetwork.Disconnect();
            DeviceManager.instance.SetDevice(option);

            switch (option)
            {
                case 0:
                    GameViewUtils.AddAndSelectCustomSize(1920, 1080);
                    SetGameViewScale(0.47f);
                    break;
                case 1:
                    GameViewUtils.AddAndSelectCustomSize(1920, 1080);
                    SetGameViewScale(0.52f);
                    break;
                case 2:
                    GameViewUtils.SetSize(GameViewUtils.FindSize(GameViewSizeGroupType.Standalone, "Mob"));
                    SetGameViewScale(1);
                    break;
            }

            PlayerPrefs.SetFloat("FixSpam", 1);
        }
        #endif
    }
    // ===================================================================

    private void OnApplicationQuit() => PlayerPrefs.DeleteKey("FixSpam");

    // ===================================================================

    public void LoadScene()
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

    // ===================================================================

    public void SetDevice(int value)
    {
        if (value == 0)
        {
            foreach (var item in _goPC) { item.SetActive(true); }
            foreach (var item in _goMobile) { item.SetActive(false); }
        }
        else if(value == 2)
        {
            foreach (var item in _goMobile) { item.SetActive(true); }
            foreach (var item in _goPC) { item.SetActive(false); }
        }
    }

    // ===================================================================
#if UNITY_EDITOR

    private void Update()
    {
        if (Application.isEditor)
        {
            if (time > 0) { time -= Time.deltaTime; }
            if (time <= 0)
                return;

            if (device == 0)
            {
                SetGameViewScale(0.47f);
            }
            else if(device == 2)
            {
                SetGameViewScale(1);
            }
        }
    }

    // ===================================================================

    void SetGameViewScale(float value)
    {
        System.Reflection.Assembly assembly = typeof(UnityEditor.EditorWindow).Assembly;
        System.Type type = assembly.GetType("UnityEditor.GameView");
        UnityEditor.EditorWindow v = UnityEditor.EditorWindow.GetWindow(type);

        var defScaleField = type.GetField("m_defaultScale", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        float defaultScale = value;

        var areaField = type.GetField("m_ZoomArea", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        var areaObj = areaField.GetValue(v);

        var scaleField = areaObj.GetType().GetField("m_Scale", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        scaleField.SetValue(areaObj, new Vector2(defaultScale, defaultScale));
    }
    #endif

}
