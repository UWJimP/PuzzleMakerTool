using UnityEngine;

public class BreakColliders : MonoBehaviour {

    public GameObject puzzleSlotContainer;

    [SerializeField]
    private new BoxCollider2D collider2D;

    [SerializeField]
    private PuzzlePiece puzzlePiece;

    [SerializeField]
    private PuzzleSlot puzzleSlot;

    [SerializeField]
    private int width_pieces;

    [SerializeField]
    private int height_pieces;

    public GameObject[,] createdPieces;

    // Start is called before the first frame update
    void Start() {
        if(puzzlePiece == null || width_pieces <=0 || height_pieces <= 0 || collider2D == null || puzzleSlot == null) {
            puzzlePiece = null;
            width_pieces = 1;
            height_pieces = 1;
            collider2D = null;
            puzzleSlot = null;
        }
        if (width_pieces > 0 && height_pieces > 0 && puzzlePiece != null && collider2D != null && puzzleSlot != null) {
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
                GameObject piece = Instantiate(puzzlePiece.gameObject);
                GameObject slot = Instantiate(puzzleSlot.gameObject);
                //piece.transform.position = new Vector3(startX + (x * width) + (width / 2), startY + (y * height) + (height / 2));
                piece.transform.parent = transform;
                piece.transform.position = new Vector3(startX + width * (x + 0.5f), startY + height * (y + 0.5f));
                piece.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
                createdPieces[x, y] = piece;
                puzzleSlot.SetBound(new Vector2(width, height));
                puzzleSlot.SetPosition(piece.transform.position);
                slot.transform.parent = puzzleSlotContainer.transform;
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
