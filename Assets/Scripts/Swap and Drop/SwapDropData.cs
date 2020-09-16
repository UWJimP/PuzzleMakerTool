using UnityEngine;

public class SwapDropData : MonoBehaviour {

    [SerializeField]
    private int puzzle_width;

    [SerializeField]
    private int puzzle_height;

    [SerializeField]
    private string image_url;

    // Start is called before the first frame update
    void Start() {
        puzzle_width = 3;
        puzzle_height = 3;
        image_url = "";
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
}
