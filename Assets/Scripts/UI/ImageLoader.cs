using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour {

    public RawImage image;

    private string path;

    private SwapDropData data;

    private void Start() {
        path = "";
        data = GameObject.Find("Data Manager").GetComponent<SwapDropData>();
    }

    public void FileUploadFromAngular(string url) {
        path = url;
        data.SetURL("file:///" + path);
        StartCoroutine(LoadImage());
    }

    public void FileUpload3() {
        path = FileExplorer.OpenExplorer();
        if(path != null || !path.Equals("")) {
            StartCoroutine(LoadImage());
        }
    }

    public void FileUpload2(string url) {
        path = url;
        data.SetURL(path);
        StartCoroutine(LoadImage(url));
    }

    public void FileUpload() {
        path = FileExplorer.OpenExplorer();
        //Debug.Log(path);
        StartCoroutine(LoadImage());
    }

    private IEnumerator LoadImage(string url) {
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (!www.isNetworkError || !www.isHttpError) {
            //Debug.Log(www.downloadHandler.data);
            Texture2D pic = new Texture2D(0, 0);
            pic.LoadImage(www.downloadHandler.data);
            image.texture = pic;
            data.SetTexture(pic);
        } else {
            Debug.Log(www.error);
        }
    }

    private IEnumerator LoadImage() {
        UnityWebRequest www = UnityWebRequest.Get("file:///" + path);
        yield return www.SendWebRequest();

        if (!www.isNetworkError || !www.isHttpError) {
            //Debug.Log(www.downloadHandler.data);
            Texture2D pic = new Texture2D(0, 0);
            pic.LoadImage(www.downloadHandler.data);
            image.texture = pic;
            data.SetTexture(pic);
        } else {
            Debug.Log(www.error);
        }
    }
}
