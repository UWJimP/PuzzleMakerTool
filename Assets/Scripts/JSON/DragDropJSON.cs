using UnityEngine;

[System.Serializable]
public class DragDropJSON : FirebaseJSON {

    public TextureJSON textureJSON;

    //public string linkCode;

    public int width;

    public int height;

    public bool isRotateRandom;

    public SwapDropData.Orientation orientation;

    public override string GetJSON() {
        return JsonUtility.ToJson(this);
    }

    public override string ToString() {
        return string.Format("Width: {0}, Height: {1}, Rotation: {2}, Orientation: {3}", width,
            height, isRotateRandom, orientation);
    }
}
