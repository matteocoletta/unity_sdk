#if UNITY_METRO
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using AdjustUnityWS;

namespace com.adjust.sdk {
    public class AdjustMetro : IAdjust {
        private const string sdkPrefix = "unity4.11.0";

        public bool isEnabled() {
            return AdjustWS.IsEnabled();
        }

        public string getAdid() {
            return null;
        }

        public AdjustAttribution getAttribution() {
            return null;
        }

        public void onPause() {
            AdjustWS.ApplicationDeactivated();
        }

        public void onResume() {
            AdjustWS.ApplicationActivated();
        }

        public void setEnabled(bool enabled) {
            AdjustWS.SetEnabled(enabled);
        }

        public void setOfflineMode(bool offlineMode) {
            AdjustWS.SetOfflineMode(offlineMode);
        }

        public void start(AdjustConfig adjustConfig) {
            string logLevelString = null;
            string environment = lowercaseToString(adjustConfig.environment);
            Action<Dictionary<string, string>> attributionChangedDictionary = null;

            if (adjustConfig.logLevel != null) {
                logLevelString = lowercaseToString(adjustConfig.logLevel.Value);
            }

            if (adjustConfig.attributionChangedDelegate != null) {
                attributionChangedDictionary = (attributionDictionary) => Adjust.runAttributionChangedDictionary(attributionDictionary);
            }

            AdjustWS.ApplicationLaunching(
                appToken:adjustConfig.appToken,
                logLevelString:logLevelString,
                environment:environment,
                defaultTracker:adjustConfig.defaultTracker,
                eventBufferingEnabled:adjustConfig.eventBufferingEnabled,
                sdkPrefix:sdkPrefix,
                attributionChangedDic:attributionChangedDictionary,
                logDelegate:adjustConfig.logDelegate
            );
        }

        public static string lowercaseToString(AdjustLogLevel AdjustLogLevel)
        {
            switch (AdjustLogLevel)
            {
                case AdjustLogLevel.Verbose:
                    return "verbose";
                case AdjustLogLevel.Debug:
                    return "debug";
                case AdjustLogLevel.Info:
                    return "info";
                case AdjustLogLevel.Warn:
                    return "warn";
                case AdjustLogLevel.Error:
                    return "error";
                case AdjustLogLevel.Assert:
                    return "assert";
                case AdjustLogLevel.Suppress:
                    return "suppress";
                default:
                    return "unknown";
            }
        }

        public static string lowercaseToString(AdjustEnvironment adjustEnvironment)
        {
            switch (adjustEnvironment)
            {
                case AdjustEnvironment.Sandbox:
                    return "sandbox";
                case AdjustEnvironment.Production:
                    return "production";
                default:
                    return "unknown";
            }
        }
        public void trackEvent(AdjustEvent adjustEvent) {
            AdjustWS.TrackEvent(
                eventToken:adjustEvent.eventToken,
                revenue:adjustEvent.revenue,
                currency:adjustEvent.currency,
                callbackList:adjustEvent.callbackList,
                partnerList:adjustEvent.partnerList
            );
        }

        public void sendFirstPackages() {}

        public void setDeviceToken(string deviceToken) {}

        public static void addSessionPartnerParameter(string key, string value) {}

        public static void addSessionCallbackParameter(string key, string value) {}

        public static void removeSessionPartnerParameter(string key) {}

        public static void removeSessionCallbackParameter(string key) {}

        public static void resetSessionPartnerParameters() {}

        public static void resetSessionCallbackParameters() {}

        // iOS specific methods
        public string getIdfa() {
            return null;
        }

        // Android specific methods
        public void setReferrer(string referrer) {}

        public void getGoogleAdId(Action<string> onDeviceIdsRead) {}
    }
}
#endif
