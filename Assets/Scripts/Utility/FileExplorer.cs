using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public static class FileExplorer {

    private static string path = "";

    public static string OpenExplorer() {
        string[] filters = new string[] { "Image files", "png,jpg,jpeg" };
#if UNITY_EDITOR
        path = EditorUtility.OpenFilePanelWithFilters("Overwrite with png", "", filters);
#endif
        return path;
    }
}
