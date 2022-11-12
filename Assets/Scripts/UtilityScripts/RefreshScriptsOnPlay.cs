using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class RefreshScriptsOnPlay
{
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        EditorApplication.playModeStateChanged += OnEnterPlayMode;
    }

    private static void OnEnterPlayMode(PlayModeStateChange obj)
    {
        if (obj == PlayModeStateChange.ExitingEditMode) AssetDatabase.Refresh(ImportAssetOptions.Default);
    }

}
