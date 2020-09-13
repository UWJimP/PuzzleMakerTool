using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start() {
        if(boxCollider == null) {
            boxCollider = null;
        }
    }

    /// <summary>
    /// Set the box collider's boundary.
    /// </summary>
    /// <param name="vector2"></param>
    public void SetBound(Vector2 vector2) {
        boxCollider.size = vector2;
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
            if (Mathf.Abs(collision.transform.localPosition.x - transform.localPosition.x) <= 0.8f &&
                Mathf.Abs(collision.transform.localPosition.y - transform.localPosition.y) <= 0.8f && !piece.IsMoving()) {
                //Debug.Log("Triggered");
                collision.gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
            }
        }
    }

}
