using UnityEngine;

public class CheckDevice : MonoBehaviour
{
    public static CheckDevice instance;
    #if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();
    #endif

    private void Awake()
    {
        instance = this;
    }

    public bool isMobile()
    {
        var isMobile = false;

        #if !UNITY_EDITOR && UNITY_WEBGL
            isMobile = IsMobile();
        #endif

        return isMobile;
    }
}
