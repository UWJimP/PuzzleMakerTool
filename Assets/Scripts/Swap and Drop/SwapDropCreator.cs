using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SwapDropCreator : MonoBehaviour {

    [SerializeField]
    private int width_pieces;

    [SerializeField]
    private int height_pieces;

    [SerializeField]
    private BoxCollider2D puzzleBoard;

    [SerializeField]
    private PuzzlePiece puzzlePiece;

    [SerializeField]
    private PuzzleSlot puzzleSlot;

    private GameObject[,] createdPieces;

    // Start is called before the first frame update
    void Start() {
        if (puzzlePiece == null || width_pieces <= 0 || height_pieces <= 0 || puzzleBoard == null || puzzleSlot == null) {
            puzzlePiece = null;
            width_pieces = 1;
            height_pieces = 1;
            puzzleBoard = null;
            puzzleSlot = null;
        }
        if (width_pieces > 0 && height_pieces > 0 && puzzlePiece != null && puzzleBoard != null && puzzleSlot != null) {
            createdPieces = new GameObject[width_pieces, height_pieces];
            transform.position = new Vector3(puzzleBoard.transform.position.x, puzzleBoard.transform.position.y, 0f);
            string url = "https://uwjimp.github.io/Jim-Portfolio-Website/assets/img/img_021.jpg";
            StartCoroutine(LoadImage(url));
            GeneratePieces();
        }
    }

    private void GeneratePieces() {
        //Calculates the pixels per width and height for each piece.
        float width = puzzleBoard.size.x / width_pieces;
        float height = puzzleBoard.size.y / height_pieces;
        //Calculates the starting position's x and y from the transform's position.
        float startX = transform.position.x - (puzzleBoard.size.x / 2);
        float startY = transform.position.y - (puzzleBoard.size.y / 2);
        //The container used to store the pieces in for the future.
        GameObject slotContainer = new GameObject("Slot Container");
        slotContainer.transform.position = new Vector3(0f, 0f, 0f);

        //Generates an array of box collider pieces beforehand along with the puzzle slots to store them.
        for (int x = 0; x < width_pieces; x++) {
            for (int y = 0; y < height_pieces; y++) {
                GameObject piece = Instantiate(puzzlePiece.gameObject);
                GameObject slot = Instantiate(puzzleSlot.gameObject);

                piece.transform.parent = transform; //Stores the generated puzzle piece into this transform.
                piece.transform.position = new Vector3(startX + width * (x + 0.5f), startY + height * (y + 0.5f));
                piece.GetComponent<BoxCollider2D>().size = new Vector2(width, height); //Set size of the puzzle piece's box.
                createdPieces[x, y] = piece; //Store the piece into the array.
                slot.GetComponent<PuzzleSlot>().SetBound(new Vector2(width, height)); //Sets the slot's slot size.
                slot.GetComponent<PuzzleSlot>().SetPosition(piece.transform.position); //Sets the position of the slot.
                slot.transform.parent = slotContainer.transform;
                slot.GetComponent<PuzzleSlot>().SetSlotPosition(x, y);
            }
        }

        puzzleBoard.enabled = false;
    }

    private IEnumerator LoadImage(string url) {
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
                GameObject[,] pieces = createdPieces;
                float pixelsPerWidth = texture2d.width / width_pieces;
                float pixelsPerHeight = texture2d.height / height_pieces;
                for (int x = 0; x < width_pieces; x++) {
                    for (int y = 0; y < height_pieces; y++) {
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
