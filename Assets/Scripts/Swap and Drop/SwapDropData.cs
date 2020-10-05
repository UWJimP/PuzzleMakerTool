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

    private bool randomRotation;

    private Texture2D texture2d;

    private Orientation orientation;

    public static SwapDropData instance;

    // Start is called before the first frame update
    void Start() {
        //if(instance == null) {instance = this;} else {Destroy(gameObject);}
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
            orientation = Orientation.square;
        } else if(form.Equals("portrait")) {
            orientation = Orientation.portrait;
        } else {
            orientation = Orientation.landscape;
        }
    }

    public Orientation GetOrientation() {
        return orientation;
    }
}
