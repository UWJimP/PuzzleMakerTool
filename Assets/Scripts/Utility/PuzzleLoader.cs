using UnityEngine;

public class PuzzleLoader : MonoBehaviour {

    [SerializeField]
    private FirebaseController firebase;

    // Start is called before the first frame update
    void Start() {
        if(firebase == null) {
            firebase = gameObject.GetComponent<FirebaseController>();
        }
    }

    public void LoadCodePuzzle(string linkCode) {
        if(linkCode.Length == 6) {
            firebase.LoadPuzzle(linkCode);
        }
    }
}
