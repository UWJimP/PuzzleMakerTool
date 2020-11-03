using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLoadData : MonoBehaviour {

    public string JSONData { get; set; }

    public void LoadData(string json) {
        PuzzleJSON puzzleJSON = JsonUtility.FromJson<PuzzleJSON>(json);
        JSONData = puzzleJSON.jsonData;
        Debug.Log("Puzzle Type: " + puzzleJSON.puzzleType);
        Debug.Log("Puzzle Type #: " + (int)puzzleJSON.puzzleType);
        if (puzzleJSON.puzzleType == PuzzleType.DRAGDROP) {
            SceneManager.LoadScene("LoadDragDropScene");
        }
        //Debug.Log("JSON Data: " + puzzleJSON.GetJSON());
        DontDestroyOnLoad(this);
    }

}
