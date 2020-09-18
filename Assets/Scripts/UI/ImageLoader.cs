using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour {

    public RawImage image;

    private string path;

    public void FileUpload(string url) {
        path = url;
        StartCoroutine(LoadImage());
    }

    public void FileUpload() {
        
    }

    private IEnumerator LoadImage() {
        UnityWebRequest www = UnityWebRequest.Get("file:///" + path);
        yield return www.SendWebRequest();

        if (!www.isNetworkError || !www.isHttpError) {
            //Debug.Log(www.downloadHandler.data);
            Texture2D pic = new Texture2D(0, 0);
            pic.LoadImage(www.downloadHandler.data);
            image.texture = pic;
        } else {
            Debug.Log(www.error);
        }
    }
}
