using UnityEngine;
using System.Collections;
using System.IO;
using com.adjust.sdk;

#if UNITY_EDITOR
using UnityEditor;
[InitializeOnLoad]
#endif

public class AdjustData : ScriptableObject {
    public bool startManually = true;
    public bool eventBuffering = false;
    public bool printAttribution = true;
    public bool sendInBackground = false;
    public bool launchDeferredDeeplink = true;

    public string appToken = "{Your App Token}";

    public AdjustLogLevel logLevel = AdjustLogLevel.Info;
    public AdjustEnvironment environment = AdjustEnvironment.Sandbox;

    public string androidAppScheme = "{Your Android App Scheme}";
    public string iOSAppScheme = "{Your iOS App Scheme}";

    private const string ADAssetPath = "Adjust/Resources";
    private const string ADAssetName = "AdjustData";
    private const string ADAssetExtension = ".asset";
    private static AdjustData instance;

    public static AdjustData Instance {
        get {
            if(instance == null) {
                LoadAdjustData();
            }

            return instance;
        }
    }

    private static void LoadAdjustData() {
        instance = Resources.Load(ADAssetName) as AdjustData;

        if(instance == null) {
            instance = CreateInstance<AdjustData>();

#if UNITY_EDITOR

            if (!Directory.Exists(ADAssetPath)) {
                Directory.CreateDirectory(ADAssetPath);
                AssetDatabase.Refresh();
            }

            string fullPath = Path.Combine(Path.Combine("Assets", ADAssetPath), ADAssetName + ADAssetExtension);
            AssetDatabase.CreateAsset(instance, fullPath);

#endif
        }
    }
}
