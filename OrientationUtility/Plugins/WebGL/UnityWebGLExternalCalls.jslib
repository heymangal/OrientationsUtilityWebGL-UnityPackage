mergeInto(LibraryManager.library, {

  JF_IsRunningOnMobileDevice: function (urlString) {
    console.log("JF_IsRunningOnMobileDevice");

    var userAgent = navigator.userAgent;
    isMobile = (/\b(BlackBerry|webOS|iPhone|IEMobile)\b/i.test(userAgent) || /\b(Android|Windows Phone|iPad|iPod)\b/i.test(userAgent) || (userAgent.includes("Mac") && "ontouchend" in document));
    if (isMobile) {
        if (/\b(Android|Windows Phone|iPad|iPod)\b/i.test(userAgent)) {
            return 1;
        }
        else if (/\b(BlackBerry|webOS|iPhone|IEMobile)\b/i.test(userAgent) || (userAgent.includes("Mac") && "ontouchend" in document)) {
            return 2;
        }                
    }            
    else {
        console.log("JF_IsDesktop TRUE");
        return 0;
    }                                      
  },

JF_RequestUrlOpenNew: function (urlString) {
    console.log("JF_RequestUrlOpen " + Pointer_stringify(urlString));
    const params = new URLSearchParams(window.location.href);
    const origin = params.get('origin');
    const iframe = params.get('iframe');
    window.parent.postMessage(JSON.stringify({
        type: iframe+'_open_link',
        url: urlString
    }), origin);
  },

  JF_RequestUrlOpen: function (urlString) {
    console.log("JF_RequestUrlOpen " + Pointer_stringify(urlString));
    var openUrl = window.open(Pointer_stringify(urlString), '_blank');
  },

  JF_RequestUrlOpenInSameTab: function (urlString) {
    console.log("JF_RequestUrlOpenInSameTab " + Pointer_stringify(urlString));
    var openUrl = window.open(Pointer_stringify(urlString), '_self');
  },  

  JF_RequestExitGame: function () {
    console.log("JF_RequestExitGame");
  },
  
  
  JF_GetBrowserName: function()
      {
        var returnStr = GetBrowserName();
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;        
      },

      JF_GetBrowserVersion: function()
      {
        return GetBrowserVersion();                 
      },

      JF_openFullscreen: function()
      {
        FullScreen();
      },           

      JF_GetWindowOrigin: function()
      {
        var returnStr = GetWindowOrigin();
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        console.log("JF_GetWindowOrigin : " + buffer);
        return buffer;        
      }

  });