using UnityEngine;

[System.Serializable]
public class PuzzleJSON {

    public PuzzleType puzzleType;

    public string jsonData;

    public string GetJSON() {
        return JsonUtility.ToJson(this);
    }
}
