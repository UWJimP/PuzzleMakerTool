using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapDropSubmit : MonoBehaviour {

    private string[] checkTags;

    // Start is called before the first frame update
    void Start() {
        checkTags = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
    }

    public void Submit(string url) {
        bool foundTag = false;
        foreach(string tag in checkTags) {
            if(url.EndsWith(tag)) {
                foundTag = true;
            }
        }
        if(foundTag) {
            SceneManager.LoadScene("SwapAndDropScene");
        }
    }

    public void Submit() {
        SceneManager.LoadScene("SwapAndDropScene");
    }
}
