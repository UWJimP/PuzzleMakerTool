using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Runtime.InteropServices;

public class CanvasScript : MonoBehaviour {
    [DllImport("__Internal")]
    private static extern void ImageUploaderInit();

    public RawImage rawImage;
    public RawImage[] rawImages;
    private SwapDropData data;

    void Start() {
        ImageUploaderInit();
        data = GameObject.Find("Data Manager").GetComponent<SwapDropData>();
        rawImages[0].enabled = true;
        rawImages[1].enabled = false;
        rawImages[2].enabled = false;
        data.SetOrientation("square");
        foreach(RawImage image in rawImages) {
            Debug.Log(image.enabled);
        }
        SetOrientation("square");
    }

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
            //rawImage.texture = pic;
            foreach(RawImage image in rawImages) {
                image.texture = pic;
            }
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

    public void SetOrientation(string orientation) {
        data.SetOrientation(orientation);
        if (orientation.Equals("square")) {
            rawImages[0].enabled = true;
            rawImages[1].enabled = false;
            rawImages[2].enabled = false;
        } else if (orientation.Equals("portrait")) {
            rawImages[0].enabled = false;
            rawImages[1].enabled = true;
            rawImages[2].enabled = false;
        } else {
            rawImages[0].enabled = false;
            rawImages[1].enabled = false;
            rawImages[2].enabled = true;
        }
    }

    public void Submit() {
        SceneManager.LoadScene("SwapAndDropScene");
    }
}
