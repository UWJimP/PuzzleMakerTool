using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakColliders : MonoBehaviour {

    [SerializeField]
    private new BoxCollider2D collider2D;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int width_pieces;

    [SerializeField]
    private int height_pieces;

    //public List<GameObject> createdPieces;
    public GameObject[,] createdPieces;

    // Start is called before the first frame update
    void Start() {
        //createdPieces = new List<GameObject>();
        if (width_pieces > 0 && height_pieces > 0) {
            createdPieces = new GameObject[width_pieces, height_pieces];
            SpliteColliders();
            transform.position = new Vector3(collider2D.transform.position.x, collider2D.transform.position.y, 0f);
        }
    }

    private void SpliteColliders() {
        float width = collider2D.size.x / width_pieces;
        float height = collider2D.size.y / height_pieces;
        float startX = transform.position.x - (collider2D.size.x / 2);
        float startY = transform.position.y - (collider2D.size.y / 2);
        for (int x = 0; x < width_pieces; x++) {
            for(int y = 0; y < height_pieces; y++) {
                GameObject piece = Instantiate(prefab);
                //piece.transform.position = new Vector3(startX + (x * width) + (width / 2), startY + (y * height) + (height / 2));
                piece.transform.parent = transform;
                piece.transform.position = new Vector3(startX + width * (x + 0.5f), startY + height * (y + 0.5f));
                piece.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
                //createdPieces.Add(piece); //Add piece to the array.
                createdPieces[x, y] = piece;
            }
        }

        collider2D.enabled = false;
    }

    public int GetWidth() {
        return width_pieces;
    }

    public int GetHeight() {
        return height_pieces;
    }
}
