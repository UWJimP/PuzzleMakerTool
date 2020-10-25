using UnityEngine;

[System.Serializable]
public class TextureJSON {

    /// <summary>
    /// Encoded String of the image.
    /// </summary>
    public string image;

    /// <summary>
    /// Width of the texture.
    /// </summary>
    public int width;

    /// <summary>
    /// Height of the texture.
    /// </summary>
    public int height;

    /// <summary>
    /// The texture format.
    /// </summary>
    public TextureFormat textureFormat;

    public string GetJSON() {
        return JsonUtility.ToJson(this);
    }
}
