using UnityEngine;

[System.Serializable]
public class TestJSON {
    public string Name;
    public int Id;
    public string Image;
    public int WidthPixels;
    public int HeightPixels;
    public TextureFormat textureFormat;
    
    public override string ToString() {
        return string.Format("Name: {0}, Id: {1}, Width: {2}, Height: {3}, Image: {4}", 
            Name, Id, WidthPixels, HeightPixels, Image);
    }
}
