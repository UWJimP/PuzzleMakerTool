using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadImage : MonoBehaviour {

    public SpriteRenderer spriteRender;
    public new BoxCollider2D collider2D;
    public BreakColliders breaker;

    // Start is called before the first frame update
    void Start() {
        string url = "https://uwjimp.github.io/Jim-Portfolio-Website/assets/img/img_021.jpg";
        StartCoroutine(GetTexture(url));
    }

    IEnumerator GetTexture() {
        string url = "https://uwjimp.github.io/Jim-Portfolio-Website/assets/img/img_017.jpg";
        UnityWebRequest www = UnityWebRequest.Get(url);
        DownloadHandler handle = www.downloadHandler;
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else {
            Texture2D texture2d = new Texture2D(0, 0);
            Sprite sprite = null;
            if(texture2d.LoadImage(handle.data)) {
                Rect rect = new Rect(texture2d.width / 4, texture2d.height / 4, texture2d.width / 2, texture2d.height / 2);
                sprite = Sprite.Create(texture2d, rect, Vector2.zero);
                //sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);
            }
            if(sprite != null) {
                spriteRender.sprite = sprite;
                float xScale = 1f;
                float yScale = 1f;
                if(collider2D.size.x < spriteRender.sprite.rect.xMax) {
                    xScale = (collider2D.size.x * 100f) / spriteRender.sprite.rect.xMax;
                }
                if (collider2D.size.y < spriteRender.sprite.rect.yMax) {
                    yScale = (collider2D.size.y * 100f) / spriteRender.sprite.rect.yMax;
                }
                transform.localScale = new Vector3(xScale, yScale);
                transform.localPosition = new Vector2(collider2D.size.x / -2f, collider2D.size.y / -2f);
            }
        }
    }

    IEnumerator GetTexture(string url) {
        UnityWebRequest www = UnityWebRequest.Get(url);
        DownloadHandler handle = www.downloadHandler;
        Debug.Log("Is Loading..."); //Before the image is loaded.
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        } else {
            Texture2D texture2d = new Texture2D(0, 0); //Build an empty texture to be loaded in later.
            if (texture2d.LoadImage(handle.data)) {
                Debug.Log("Loaded"); //After the image is loaded.
                Sprite sprite = null;

                //Load pieces
                GameObject[,] pieces = breaker.createdPieces;
                float pixelsPerWidth = texture2d.width / breaker.GetWidth();
                float pixelsPerHeight = texture2d.height / breaker.GetHeight();
                for (int x = 0; x < breaker.GetWidth(); x++) {
                    for (int y = 0; y < breaker.GetHeight(); y++) {
                        BoxCollider2D collider = pieces[x, y].GetComponent<BoxCollider2D>();
                        Rect rect = new Rect(pixelsPerWidth * x,
                        pixelsPerHeight * y, pixelsPerWidth, pixelsPerHeight);
                        sprite = Sprite.Create(texture2d, rect, Vector2.zero);
                        if (sprite != null) {
                            SpriteRenderer renderer = pieces[x, y].GetComponent<PuzzlePiece>().GetSpriteRenderer();
                            renderer.sprite = sprite;
                            float xScale = 1f;
                            float yScale = 1f;
                            if (collider.size.x * 100f < sprite.rect.width) {
                                xScale = (collider.size.x * 100f) / sprite.rect.width;
                            }
                            if (collider.size.y * 100f < sprite.rect.height) {
                                yScale = (collider.size.y * 100f) / sprite.rect.height;
                            }
                            PuzzlePiece piece = pieces[x, y].GetComponent<PuzzlePiece>();
                            piece.SetImagePosition(new Vector2(collider.size.x / -2f, collider.size.y / -2f));
                            piece.SetImageScale(new Vector3(xScale, yScale));
                            piece.SetCorrectPosition(x, y);
                        }
                    }
                }
            }
        }
    }

}
