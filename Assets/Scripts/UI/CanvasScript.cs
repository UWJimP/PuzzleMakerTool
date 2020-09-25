using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Runtime.InteropServices;
 
public class CanvasScript : MonoBehaviour {
    [DllImport("__Internal")]
    private static extern void ImageUploaderInit();

    public RawImage rawImage;
    private SwapDropData data;

    //IEnumerator LoadTexture (string url) {
    //    WWW image = new WWW (url);
    //    yield return image;
    //    Texture2D texture = new Texture2D (1, 1);
    //    image.LoadImageIntoTexture (texture);
    //    rawImage.texture = texture;
    //    Debug.Log ("Loaded image size: " + texture.width + "x" + texture.height);
    //}

    private IEnumerator LoadImage(string url) {
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (!www.isNetworkError || !www.isHttpError) {
            //Debug.Log(www.downloadHandler.data);
            Texture2D pic = new Texture2D(0, 0);
            pic.LoadImage(www.downloadHandler.data);
            rawImage.texture = pic;
            data.SetTexture(pic);
        } else {
            Debug.Log(www.error);
        }
    }

    //public void FileSelected (string url) {
    //    StartCoroutine(LoadTexture (url));
    //}

    public void FileAngularSelect(string url) {
        StartCoroutine(LoadImage(url));
    }
 
    void Start () {
        ImageUploaderInit();
        data = GameObject.Find("Data Manager").GetComponent<SwapDropData>();
    }
}
