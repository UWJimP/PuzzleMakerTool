using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour {

    /// <summary>
    /// The slot's x position in the array.
    /// </summary>
    [SerializeField]
    private int posX;

    /// <summary>
    /// The slot's y position in the array.
    /// </summary>
    [SerializeField]
    private int posY;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private GameObject image;

    private bool containsPiece;

    private PuzzlePiece puzzlePiece;

    /// <summary>
    /// The limit of how far a piece has to be before it snaps.
    /// </summary>
    private float snapDistanceX;

    private float snapDistanceY;

    // Start is called before the first frame update
    void Start() {
        if(boxCollider == null || image == null) {
            boxCollider = null;
            image = null;
        }
        containsPiece = false;
        puzzlePiece = null;
    }

    /// <summary>
    /// Set the box collider's boundary.
    /// </summary>
    /// <param name="vector2"></param>
    public void SetBound(Vector2 vector2) {
        boxCollider.size = vector2;
        snapDistanceX = boxCollider.size.x / 2f;
        snapDistanceY = boxCollider.size.y / 2f;
        Sprite sprite = image.GetComponent<SpriteRenderer>().sprite;
        float spriteX = sprite.rect.width / 16f;
        float spriteY = sprite.rect.height / 16f;
        float scaleX = 1f;
        float scaleY = 1f;
        //Debug.Log("Sprite:" + sprite.rect.width + " " + sprite.rect.height);
        //Debug.Log("BoxCollider:" + boxCollider.size.x + " " + boxCollider.size.y);
        if(boxCollider.size.x < spriteX) {
            scaleX = boxCollider.size.x / spriteX;
        } else if(boxCollider.size.x > spriteX) {
            scaleX = spriteX / boxCollider.size.x;
        }
        if (boxCollider.size.y < spriteY) {
            scaleY = boxCollider.size.y / spriteY;
        } else if (boxCollider.size.y > spriteY) {
            scaleY = spriteY / boxCollider.size.y;
        }
        //Debug.Log("Scale:" + scaleX + " " + scaleY);
        image.transform.localScale = new Vector3(scaleX, scaleY);
    }

    /// <summary>
    /// Set the transform position of the game object.
    /// </summary>
    /// <param name="vector3">The vector 3 position.</param>
    public void SetPosition(Vector3 vector3) {
        transform.position = vector3;
    }

    /// <summary>
    /// Sets the slot's position index.
    /// </summary>
    /// <param name="x">The slot's x index position.</param>
    /// <param name="y">The slot's y index position.</param>
    public void SetSlotPosition(int x, int y) {
        posX = x;
        posY = y;
    }

    /// <summary>
    /// Returns if the puzzle piece is in the correct position of the slot's position.
    /// </summary>
    /// <param name="x">The puzzle piece x position.</param>
    /// <param name="y">The puzzle piece y position.</param>
    /// <returns>If the puzzle piece is in the correct position of the slot's position.</returns>
    public bool IsCorrectSlot(int x, int y) {
        if(posX == x && posY == y) {
            return true;
        }
        return false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Puzzle Piece")) {
            PuzzlePiece piece = collision.gameObject.GetComponent<PuzzlePiece>();
            //Debug.Log(piece);
            if (Mathf.Abs(collision.transform.localPosition.x - transform.localPosition.x) < snapDistanceX &&
                Mathf.Abs(collision.transform.localPosition.y - transform.localPosition.y) < snapDistanceY) {
                //Debug.Log("Triggered");
                //Debug.Log(snapDistanceX + " " + snapDistanceY);
                if(!piece.IsMoving()) {
                    if(containsPiece) {
                        if(!piece.Equals(puzzlePiece)) {
                            Vector3 lastPosition = piece.GetLastPosition();
                            puzzlePiece.SetPosition(lastPosition.x, lastPosition.y);
                            puzzlePiece = piece;
                        }
                    } else {
                        //collision.gameObject.transform.localPosition = new Vector3(transform.localPosition.x,
                        //    transform.localPosition.y, 0f);
                        //piece.SetPosition(transform.localPosition.x, transform.position.y);
                        puzzlePiece = piece;
                        containsPiece = true;
                    }
                    piece.SetPosition(transform.localPosition.x, transform.position.y);
                } else if(piece.IsMoving() && containsPiece && piece.Equals(puzzlePiece)) {
                    containsPiece = false;
                    puzzlePiece = null;
                }
            }
        }
    }

}
