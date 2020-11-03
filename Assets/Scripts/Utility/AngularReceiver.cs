using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class AngularReceiver : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void FinishLoading();

    [DllImport("__Internal")]
    private static extern void ChangeMenu(int value);

    public static AngularReceiver instance;

    // Start is called before the first frame update
    void Start() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DisableUnityKeyboard();
        DontDestroyOnLoad(gameObject);
        ChangeMenu((int)PuzzleType.NONE);
        FinishLoading();
    }

    public void EnableUnityKeyboard() {
        WebGLInput.captureAllKeyboardInput = true;
    }

    public void DisableUnityKeyboard() {
        WebGLInput.captureAllKeyboardInput = false;
    }

    public void LoadScene(string scene) {
        Debug.Log("Scene loading: " + scene);
        SceneManager.LoadScene(scene);
    }
}
