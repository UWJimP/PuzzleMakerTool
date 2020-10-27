using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseController : MonoBehaviour {

    private readonly string FB_API = "https://angular-random-name-picker.firebaseio.com/";
    private readonly string ERROR_0 = "Error 0: Data Not Found";
    private readonly int CODE_LENGTH = 6;

    public string Code { get; set; }

    private string LoadedJSON { get; set; }

    private string ErrorMessage { get; set; }

    //public SpriteRenderer SaveSprite;
    //public SpriteRenderer LoadSprite;


    // Start is called before the first frame update
    void Start() {
        Code = "";
        LoadedJSON = "";
        ErrorMessage = "";
    }

    /// <summary>
    /// Loads the data from the firebase database.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="linkCode"></param>
    /// <returns></returns>
    public IEnumerator LoadData(string path, string linkCode) {
        ErrorMessage = "";
        LoadedJSON = "";
        UnityWebRequest www = UnityWebRequest.Get(path + linkCode + ".json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            ErrorMessage = www.error;
        } else {
            string data = www.downloadHandler.text;
            if (string.IsNullOrEmpty(data) || data.Equals("null")) {
                ErrorMessage = ERROR_0;
            } else {
                Debug.Log("Download complete!");
                LoadedJSON = data;
            }
        }
    }

    /// <summary>
    /// Saves new data into the firebase database.
    /// </summary>
    /// <param name="path">The path of the database.</param>
    /// <param name="json">The Saveable JSON data.</param>
    /// <returns></returns>
    public IEnumerator SaveNewData(string path, FirebaseJSON json) {
        ErrorMessage = "";
        yield return GenerateUniqueCode();

        json.linkCode = Code;
        string data = json.GetJSON();
        UnityWebRequest www = UnityWebRequest.Put(path + Code + ".json", data);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            ErrorMessage = www.error;
        } else {
            Debug.Log("Upload complete!");
        }
    }

    /// <summary>
    /// Update existing data.
    /// </summary>
    /// <param name="path">The path of the firebase.</param>
    /// <param name="json">The json being updated.</param>
    /// <returns></returns>
    public IEnumerator UpdateData(string path, FirebaseJSON json) {
        ErrorMessage = "";
        if (string.IsNullOrEmpty(json.linkCode)) {
            Debug.Log("Error: LinkCodes do not match.");
        } else {
            UnityWebRequest www = UnityWebRequest.Put(path + json.linkCode + ".json", json.GetJSON());
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
                ErrorMessage = www.error;
            } else {
                Debug.Log("Upload complete!");
            }
        }
    }

    /// <summary>
    /// Generates a unique code not found in the Firebase database.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GenerateUniqueCode() {
        ErrorMessage = "";
        Code = CodeGenerator.GenerateCode(CODE_LENGTH);
        UnityWebRequest www = UnityWebRequest.Get(FB_API + Code + ".json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            ErrorMessage = www.error;
        } else {
            string data = www.downloadHandler.text;
            if (string.IsNullOrEmpty(data) || data.Equals("null")) {
                Debug.Log("Code Unique");
            } else {
                yield return GenerateUniqueCode();
            }
        }
    }

    private IEnumerator GenerateUniqueCode(string testCode) {
        Code = testCode;
        //Debug.Log(code);
        UnityWebRequest www = UnityWebRequest.Get(FB_API + Code + ".json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else {
            //Debug.Log("Code exists");
            string data = www.downloadHandler.text;
            //Debug.Log(data);
            if (string.IsNullOrEmpty(data) || data.Equals("null")) {
                //Debug.Log("Code Unique");
            } else {
                //Debug.Log("Code Found");
                //Debug.Log(data);
                yield return GenerateUniqueCode();
            }
        }
    }

    //private void TestStart() {
    //    Texture2D texture = SaveSprite.sprite.texture;
    //    byte[] bytesToEncode = texture.GetRawTextureData();
    //    string imageEncoded = Convert.ToBase64String(bytesToEncode);
    //    TextureJSON textureJson = new TextureJSON {
    //        image = imageEncoded,
    //        width = texture.width,
    //        height = texture.height,
    //        textureFormat = texture.format
    //    };
    //    DragDropJSON dragDropJSON = new DragDropJSON {
    //        textureJSON = textureJson,
    //        width = 5,
    //        height = 5,
    //        isRotateRandom = false,
    //        orientation = SwapDropData.Orientation.square,
    //        linkCode = "PU7T77"
    //    };
    //    //StartCoroutine(SaveNewData(FB_API, dragDropJSON));
    //    StartCoroutine(UpdateData(FB_API, dragDropJSON));
    //}

    //private IEnumerator Test() {
    //    Texture2D texture = SaveSprite.sprite.texture;
    //    //byte[] bytesToEncode = ImageConversion.EncodeToPNG(texture);
    //    byte[] bytesToEncode = texture.GetRawTextureData();
    //    //Debug.Log(TestJSON.BytesToString(bytesToEncode));
    //    string imageEncoded = Convert.ToBase64String(bytesToEncode);
    //    //Debug.Log(texture.format.GetType());
    //    TextureJSON textureJson = new TextureJSON {
    //        image = imageEncoded,
    //        width = texture.width,
    //        height = texture.height,
    //        textureFormat = texture.format
    //    };
    //    DragDropJSON dragDropJSON = new DragDropJSON {
    //        textureJSON = textureJson,
    //        width = 4,
    //        height = 4,
    //        isRotateRandom = false,
    //        orientation = SwapDropData.Orientation.square
    //    };
    //    //Debug.Log(me);
    //    string json = dragDropJSON.GetJSON();
    //    //Debug.Log(json);
    //    using (UnityWebRequest www = UnityWebRequest.Put(FB_API + "test.json", json)) {
    //        yield return www.SendWebRequest();

    //        if (www.isNetworkError || www.isHttpError) {
    //            Debug.Log(www.error);
    //        } else {
    //            //Debug.Log(json);
    //            Debug.Log("Form upload Complete!");
    //        }
    //    }
    //}

    //private IEnumerator LoadImage() {
    //    DragDropJSON data;
    //    using (UnityWebRequest www = UnityWebRequest.Get(FB_API + "test.json")) {
    //        yield return www.SendWebRequest();

    //        if (www.isNetworkError || www.isHttpError) {
    //            Debug.Log(www.error);
    //        } else {
    //            //Debug.Log(json);
    //            Debug.Log("Form download Complete!");
    //            string json = www.downloadHandler.text;
    //            //Debug.Log(www.downloadHandler.text);
    //            data = JsonUtility.FromJson<DragDropJSON>(json);
    //            //Debug.Log(me.textureFormat);
    //            TextureJSON textureData = data.textureJSON;
    //            string codedImage = textureData.image;
    //            byte[] decodedImage = Convert.FromBase64String(codedImage);
    //            //Debug.Log(TestJSON.BytesToString(decodedImage));
    //            Texture2D texture = new Texture2D(textureData.width, textureData.height, textureData.textureFormat, false);
    //            texture.LoadRawTextureData(decodedImage);
    //            texture.Apply();
    //            Vector2 pivot = new Vector2(LoadSprite.transform.position.x, LoadSprite.transform.position.y);
    //            Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height),
    //                Vector2.zero);
    //            LoadSprite.sprite = sprite;
    //            //LoadSprite.transform.position = new Vector2(0f, 0f) ;
    //            Guid myGuid = Guid.NewGuid();
    //            //string test = 1.ToString();
    //            //byte[] test2 = Encoding.UTF32.GetBytes(test);
    //            //string test3 = Convert.ToBase64String(test2);
    //            //Debug.Log(test3);
    //            //Debug.Log(CodeGenerator.GenerateCode(6));
    //        }
    //    }
    //}

    //private IEnumerator Test3() {
    //    string code = CodeGenerator.GenerateCode(6);
    //    Texture2D texture = SaveSprite.sprite.texture;
    //    byte[] bytesToEncode = texture.GetRawTextureData();
    //    string imageEncoded = Convert.ToBase64String(bytesToEncode);
    //    TextureJSON textureJson = new TextureJSON {
    //        image = imageEncoded,
    //        width = texture.width,
    //        height = texture.height,
    //        textureFormat = texture.format
    //    };
    //    DragDropJSON dragDropJSON = new DragDropJSON {
    //        textureJSON = textureJson,
    //        width = 4,
    //        height = 4,
    //        isRotateRandom = false,
    //        orientation = SwapDropData.Orientation.square
    //    };

    //    string json = dragDropJSON.GetJSON();

    //    using (UnityWebRequest www = UnityWebRequest.Get(FB_API + "test.json")) {
    //        yield return www.SendWebRequest();

    //        if (www.isNetworkError || www.isHttpError) {
    //            //Debug.Log(www.error);

    //        } else {
    //            Debug.Log("Code exists");
    //        }
    //    }
    //}
}
