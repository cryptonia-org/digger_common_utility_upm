// Requrie "com.unity.ads.ios-support"
using System;
using UnityEngine;
using CommonUtility.Logger;
using System.Threading.Tasks;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace CommonUtility.IOSAppTracking
{
    using Log = Log<IOSAppTrackingTransparency>;

    public class IOSAppTrackingTransparency
    {
        private const int _waitTimeMs = 3000;

#pragma warning disable CS1998 
        public static async Task<bool> Authorize()
#pragma warning restore CS1998 
        {
#if UNITY_IOS && !UNITY_EDITOR

            Log.Info($"Start AuthorizationProcess");

            var currentStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            Log.Info($"CurrentStatus = {currentStatus.ToString()}");

            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() ==
                ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                return true;
            
            Log.Info($"RequestAuthorizationTracking");
              
            try
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false; // ???
            }

            await Task.Delay(_waitTimeMs);
            while (Application.isFocused == false)
                await Task.Yield();

            Log.Info($"NewStatus = {ATTrackingStatusBinding.GetAuthorizationTrackingStatus().ToString()}");
            bool isAutorized = ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED;
            return isAutorized;
            
#else
            Log.Info($"Not UNITY_IOS");
            return true;
#endif
        }
    }
}

