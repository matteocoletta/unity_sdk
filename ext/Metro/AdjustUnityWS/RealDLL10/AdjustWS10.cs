using AdjustSdk;
using System;
using System.Collections.Generic;

namespace AdjustUnityWS10
{
    public class AdjustWS10
    {
        //private static Action<string> _LogDelegate;
        public static void ApplicationLaunching(string appToken, string environment, string logLevelString, string defaultTracker, bool? eventBufferingEnabled, string sdkPrefix, Action<Dictionary<string, string>> attributionChangedDic, Action<String> logDelegate)
        {
            //_LogDelegate = logDelegate;
            
            LogLevel logLevel;
            if (Enum.TryParse(logLevelString, out logLevel))
            {
                Adjust.SetupLogging(logDelegate: logDelegate,
                    logLevel: logLevel);
            }
            else
            {
                Adjust.SetupLogging(logDelegate: logDelegate);
            }

            var config = new AdjustConfig(appToken, environment);

            config.DefaultTracker = defaultTracker;

            if (eventBufferingEnabled.HasValue)
            {
                config.EventBufferingEnabled = eventBufferingEnabled.Value;
            }

            config.SdkPrefix = sdkPrefix;

            if (attributionChangedDic != null)
            {
                config.AttributionChanged = (attribution) => attributionChangedDic(attribution.ToDictionary());
            }

            Adjust.ApplicationLaunching(config);
            
            //_LogDelegate?.Invoke("start");
        }

        public static void TrackEvent(string eventToken, double? revenue, string currency, List<string> callbackList, List<string> partnerList)
        {
            
            var adjustEvent = new AdjustEvent(eventToken);

            if (revenue.HasValue)
            {
                adjustEvent.SetRevenue(revenue.Value, currency);
            }

            if (callbackList != null)
            {
                for (int i = 0; i < callbackList.Count; i += 2)
                {
                    var key = callbackList[i];
                    var value = callbackList[i + 1];

                    adjustEvent.AddCallbackParameter(key, value);
                }
            }

            if (partnerList != null)
            {
                for (int i = 0; i < partnerList.Count; i += 2)
                {
                    var key = partnerList[i];
                    var value = partnerList[i + 1];

                    adjustEvent.AddPartnerParameter(key, value);
                }
            }

            Adjust.TrackEvent(adjustEvent);
            
            //_LogDelegate?.Invoke("track event");
        }

        public static void ApplicationActivated()
        {
            Adjust.ApplicationActivated();
            //_LogDelegate?.Invoke("ApplicationActivated");
        }

        public static void ApplicationDeactivated()
        {
            Adjust.ApplicationDeactivated();
            //_LogDelegate?.Invoke("ApplicationDeactivated");
        }

        public static void SetEnabled(bool enabled)
        {
            Adjust.SetEnabled(enabled);
            //_LogDelegate?.Invoke("SetEnabled");
        }

        public static void SetOfflineMode(bool offlineMode)
        {
            Adjust.SetOfflineMode(offlineMode);
            //_LogDelegate?.Invoke("SetOfflineMode");
        }
        public static bool IsEnabled()
        {
            return Adjust.IsEnabled();
            //_LogDelegate?.Invoke("IsEnabled");
            //return false;
        }
    }
}
