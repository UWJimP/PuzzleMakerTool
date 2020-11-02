using System;
using UnityEngine;

public class SwapDropData : MonoBehaviour {

    public enum Orientation {
        square,
        portrait,
        landscape
    }

    [SerializeField]
    private int puzzle_width;

    [SerializeField]
    private int puzzle_height;

    [SerializeField]
    private string image_url;

    [SerializeField]
    private FirebaseController firebase;

    private bool randomRotation;

    private Texture2D texture2d;

    private Orientation imageOrientation;

    public static SwapDropData instance;

    // Start is called before the first frame update
    void Start() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        if(firebase == null) {
            firebase = null;
        }
        puzzle_width = 3;
        puzzle_height = 3;
        image_url = "";
        randomRotation = false;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPuzzleWidth(float width) {
        puzzle_width = (int)width;
    }

    public void SetPuzzleHeight(float height) {
        puzzle_height = (int)height;
    }

    public void SetURL(string url) {
        image_url = url;
    }

    public int GetWidth() {
        return puzzle_width;
    }

    public int GetHeight() {
        return puzzle_height;
    }

    public string GetURL() {
        return image_url;
    }

    public void SetTexture(Texture2D texture) {
        texture2d = texture;
    }

    public Texture2D GetTexture() {
        return texture2d;
    }

    public void SetRandomRotation(int value) {
        if(value > 0) {
            randomRotation = true;
        } else {
            randomRotation = false;
        }
    }

    public bool IsRandomRotation() {
        return randomRotation;
    }

    public void SetOrientation(string form) {
        if (form.Equals("square")) {
            imageOrientation = Orientation.square;
        } else if(form.Equals("portrait")) {
            imageOrientation = Orientation.portrait;
        } else {
            imageOrientation = Orientation.landscape;
        }
    }

    public Orientation GetOrientation() {
        return imageOrientation;
    }

    public void SaveNewDropData(string path) {
        firebase.SaveNewPuzzle(path, GenerateJSONObject());
    }

    private FirebaseJSON GenerateJSONObject() {
        byte[] textureBytes = texture2d.GetRawTextureData();
        string encodedBytes = Convert.ToBase64String(textureBytes);

        TextureJSON textureData = new TextureJSON {
            textureFormat = texture2d.format,
            width = texture2d.width,
            height = texture2d.height,
            image = encodedBytes
        };

        DragDropJSON json = new DragDropJSON {
            height = puzzle_height,
            width = puzzle_width,
            isRotateRandom = randomRotation,
            orientation = imageOrientation,
            textureJSON = textureData
        };

        return json;
    }
}
