using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class FileExplorer : MonoBehaviour {

    private string path;

    public RawImage image;

    public void OpenExplorer() {
        string[] filters = new string[] { "Image files", "png,jpg,jpeg"};
        path = EditorUtility.OpenFilePanelWithFilters("Overwrite with png", "", filters);
        LoadImage();
    }

    private void LoadImage() {
        if(path != null) {
            WWW www = new WWW("file:///" + path);
            image.texture = www.texture;
        }
    }
}
