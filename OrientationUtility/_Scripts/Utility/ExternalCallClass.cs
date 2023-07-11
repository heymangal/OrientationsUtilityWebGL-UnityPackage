using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ExternalCallClass : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public static ExternalCallClass Instance;

    public WebGLRotateDevicePopup webGLRotateDevicePopup;

    #region ENUM

    public enum Orientation
    {
        Landscape,
        Portrait
    }
    [Header("Which orientation do you want to play the game?\nતમે કયા ઓરિએન્ટેશન પર ગેમ રમાડવા માંગો છો?")]
    [Tooltip("ઓરિએન્ટેશન પસંદ કરો !")]
    public Orientation SelectOrientation;

    #endregion

    #endregion

    #region PRIVATE_VARIABLES
    [DllImport("__Internal")]
    private static extern string JF_RequestExitGame();

    [DllImport("__Internal")]
    private static extern void JF_RequestUrlOpen(string url);

    [DllImport("__Internal")]
    private static extern void JF_RequestUrlOpenNew(string url);

    [DllImport("__Internal")]
    private static extern void JF_RequestUrlOpenInSameTab(string url);
    
    [DllImport("__Internal")]
    public static extern int JF_IsRunningOnMobileDevice();

    [DllImport("__Internal")]
    public static extern string JF_GetBrowserName();

    [DllImport("__Internal")]
    public static extern int JF_GetBrowserVersion();

    [DllImport("__Internal")]
    private static extern int JF_openFullscreen();

    [DllImport("__Internal")]
    private static extern string JF_GetWindowOrigin();
   
    #endregion

    #region
    void Awake()
    {
        Debug.Log("SelectOrientation :" + SelectOrientation);
        Instance = this;
#if UNITY_WEBGL && !UNITY_EDITOR
        DontDestroyOnLoad(this);
        int platform = JF_IsRunningOnMobileDevice();
        Debug.Log("IsRunningOnMobileDevice " + platform);
        if (platform > 0)
        {
            if (platform == 1)
                webGLRotateDevicePopup.OpenPanelForAndroid();
            else
                webGLRotateDevicePopup.OpenPanelForIOS();
        }
        else
        {
            webGLRotateDevicePopup.gameObject.SetActive(false);
        }
#else
        Destroy(gameObject);
        webGLRotateDevicePopup.Close();
#endif
    }

    #endregion

    #region DELEGATE_CALLBACKS
    #endregion

    #region PUBLIC_METHODS_TO_CALL_WEB_SIDE

    public string GetWindownOriginString()
    {
        Debug.Log("UNITY GetWindownOriginString call");
#if !UNITY_EDITOR && UNITY_WEBGL
        return JF_GetWindowOrigin();
#else
        return "";
#endif
    }

    public void RequestExitGame()
    {
        Debug.Log("UNITY RequestExitGame call");
#if !UNITY_EDITOR && UNITY_WEBGL
        JF_RequestExitGame();
#elif UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenUrl(string url)
    {
        Debug.Log("UNITY OpenUrl call");
#if !UNITY_EDITOR && UNITY_WEBGL
        JF_RequestUrlOpen(url);
        //JF_RequestUrlOpenNew(url);
#endif
    }

    public void OpenURLInSameTab(string url)
    {
        Debug.Log("UNITY OpenURLInSameTab call");
#if !UNITY_EDITOR && UNITY_WEBGL
        JF_RequestUrlOpenInSameTab(url);
#endif
    }
    #endregion

    #region PUBLIC_METHODS

    public void SetScreenToFullScreen()
    {
        JF_openFullscreen();
    }

    #endregion

    

    #region PRIVATE_METHODS
    #endregion

    #region COROUTINES
    #endregion

    #region GETTER_SETTER
    #endregion
}
