// Author: AIS Technolabs PVT LTD
// Team ID: CS-UNITY
// Date: July 10, 2023
// Description: A C# program to validate just for WEBGL 
// Contact to Support Email Us Your Queries or Request a Quote :: biz@aistechnolabs.com


using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Utility : MonoBehaviour
{
    #region PUBLIC_VARIABLES
    public static Utility Instance = null;

    [Header("Boolean Variables")]
    public bool LogEnable = false;

    [Header("GameObject")]
    public GameObject gameObjectReporter;
    #endregion

    #region PRIVATE_VARIABLES

    [Header("string")]
    [SerializeField] private string webglDeviceId = "";

    #endregion

    #region UNITY_CALLBACKS
    private void Awake()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        
#endif
        if (Instance == null)
            Instance = this;

        bool isLogEnable = IsRunningOnTestingDevice();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        Debug.unityLogger.logEnabled = isLogEnable;
        if(gameObjectReporter)
            gameObjectReporter.gameObject.SetActive(isLogEnable);

        Application.runInBackground = true;
    }

    public void RefreshLogMode()
    {
        bool isLogEnable = IsRunningOnTestingDevice();        
        Debug.unityLogger.logEnabled = isLogEnable;
        //reporter.gameObject.SetActive(isLogEnable);
    }
#endregion

#region DELEGATE_CALLBACKS
#endregion

#region PUBLIC_METHODS
    public void OpenLink(string url)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
		ExternalCallClass.Instance.OpenUrl(url);
#else
        Application.OpenURL(url);
#endif
    }

    public string GetOSName()
    {
#if UNITY_ANDROID
		return "android";
#elif UNITY_IOS
		return "ios";
#else
        return "other";
#endif
    }

    public string GetApplicationVersion()
    {
        return Application.version;
    }

    public string GetApplicationVersionWithOS()
    {
#if UNITY_EDITOR
        return "v" + Application.version + "u";
#elif UNITY_ANDROID
		return "v" + Application.version + "a";	
#elif UNITY_IOS
		return "v" + Application.version + "i";	
#else
		return "";
#endif
    }

    /// <summary>
    /// Date time string format should be "dd/MM/yyyy HH:mm:ss"
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public DateTime GetDateTime(string dateTime)
    {
        dateTime = dateTime.Replace("-", "/");
        return DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm:ss", null);
    }

    /// <summary>
    /// Date time string format should be "dd/MM/yyyy HH:mm:ss"
    /// UTC to local conversion
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public DateTime GetDateTimeLocal(string dateTime)
    {
        dateTime = dateTime.Replace("-", "/");
        return TimeZone.CurrentTimeZone.ToLocalTime(GetDateTime(dateTime));
    }

    public string GetDateTime(DateTime dateTime)
    {
        return dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year;
    }



    public bool IsRunningOnTestingDevice()
    {
        if (LogEnable)
            return true;

#if UNITY_EDITOR || UNITY_STANDALONE
        return true;
#endif

        //foreach (TestingDeviceData data in TestingDeviceList)
        //{
        //    if (data.deviceId == DeviceId)
        //    {
        //        Debug.Log("Testing device found!");
        //        return true;
        //    }
        //}

        return false;
    }


    #endregion

    #region PRIVATE_METHODS

    #endregion

    #region COROUTINES

#endregion

#region GETTER_SETTER
    public string DeviceId
    {
        get
        {
#if UNITY_WEBGL
            if (webglDeviceId == "")
                webglDeviceId = "webgl-" + Guid.NewGuid().ToString();
            return webglDeviceId;
#else
            return SystemInfo.deviceUniqueIdentifier;
#endif
        }
    }

    public string AppVersion
    {
        get
        {
            return Application.version;
        }
    }

    public string OSname
    {
        get
        {
#if UNITY_ANDROID
            return "android";
#elif UNITY_IOS
            return "iOS";
#elif UNITY_WEBGL
            return "WebGL";
#else
            return "other";
#endif
        }
    }
#endregion
}

public static class MyExtension
{
    /// <summary>
    /// Open the specified component.
    /// </summary>
    /// <param name="component">Component.</param>
    public static void Open(this MonoBehaviour component)
    {
        if (component.gameObject != null)
            component.gameObject.SetActive(true);
    }

    /// <summary>
    /// Open the specified component.
    /// </summary>
    /// <param name="component"></param>
    public static void Open(this GameObject component)
    {
        if (component != null)
            component.SetActive(true);
    }

    /// <summary>
    /// Close the specified component.
    /// </summary>
    /// <param name="component">Component.</param>
    public static void Close(this MonoBehaviour component)
    {
        if (component.gameObject != null)
            component.gameObject.SetActive(false);
    }

    /// <summary>
    /// Close the specified component.
    /// </summary>
    /// <param name="component"></param>
    public static void Close(this GameObject component)
    {
        if (component != null)
            component.SetActive(false);
    }
}