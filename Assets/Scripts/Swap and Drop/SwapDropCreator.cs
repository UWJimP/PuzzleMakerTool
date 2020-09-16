using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SwapDropCreator : MonoBehaviour {

    /// <summary>
    /// The number of puzzle pieces along the width.
    /// </summary>
    [SerializeField]
    private int width_pieces;

    /// <summary>
    /// The number of puzzle pieces along the height.
    /// </summary>
    [SerializeField]
    private int height_pieces;

    /// <summary>
    /// The puzzle board's boundary box.
    /// </summary>
    [SerializeField]
    private BoxCollider2D puzzleBoard;

    /// <summary>
    /// The prefab of the puzzle piece.
    /// </summary>
    [SerializeField]
    private PuzzlePiece puzzlePiece;

    /// <summary>
    /// The prefab of the puzzle slot.
    /// </summary>
    [SerializeField]
    private PuzzleSlot puzzleSlot;

    /// <summary>
    /// A 2d array of the created puzzle pieces.
    /// </summary>
    private GameObject[,] createdPieces;

    /// <summary>
    /// A list of the created puzzle pieces.
    /// </summary>
    private List<GameObject> puzzlePiecesList;

    private Texture2D image;

    private string errorMessage;

    private SwapDropData dropData;

    public BoxCollider2D piecesContainer;

    // Start is called before the first frame update
    void Start() {
        puzzlePiecesList = new List<GameObject>();
        GameObject dataObject = GameObject.Find("Data Manager");
        dropData = dataObject.GetComponent<SwapDropData>();

        if (puzzlePiece == null || puzzleBoard == null || puzzleSlot == null) {
            puzzlePiece = null;
            puzzleBoard = null;
            puzzleSlot = null;
        }

        width_pieces = dropData.GetWidth();
        height_pieces = dropData.GetHeight();
        string url = dropData.GetURL();

        image = new Texture2D(0, 0);
        errorMessage = null;
        if (width_pieces > 0 && height_pieces > 0 && puzzlePiece != null && puzzleBoard != null && puzzleSlot != null) {
            createdPieces = new GameObject[width_pieces, height_pieces];
            transform.position = new Vector3(puzzleBoard.transform.position.x, puzzleBoard.transform.position.y, 0f);
            //string url = "https://uwjimp.github.io/Jim-Portfolio-Website/assets/img/img_021.jpg";
            //string url = "https://uwjimp.github.io/Jim-Portfolio-Website/index.html";
            GeneratePieces();
            StartCoroutine(LoadImage(url));
            if(errorMessage == null) {
                
                MovePiecesToContainer();
            }
        }
    }

    /// <summary>
    /// Generates the puzzle pieces and their collider.
    /// </summary>
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
        GameObject piecesContainer = new GameObject("Pieces Container");
        piecesContainer.transform.position = new Vector3(0f, 0f, 0f);

        //Generates an array of box collider pieces beforehand along with the puzzle slots to store them.
        for (int x = 0; x < width_pieces; x++) {
            for (int y = 0; y < height_pieces; y++) {
                GameObject piece = Instantiate(puzzlePiece.gameObject);
                GameObject slot = Instantiate(puzzleSlot.gameObject);

                piece.transform.parent = piecesContainer.transform; //Stores the generated puzzle piece into this transform.
                piece.transform.position = new Vector3(startX + width * (x + 0.5f), startY + height * (y + 0.5f));
                piece.GetComponent<BoxCollider2D>().size = new Vector2(width, height); //Set size of the puzzle piece's box.
                createdPieces[x, y] = piece; //Store the piece into the array.
                slot.GetComponent<PuzzleSlot>().SetBound(new Vector2(width, height)); //Sets the slot's slot size.
                slot.GetComponent<PuzzleSlot>().SetPosition(new Vector3(piece.transform.position.x,
                    piece.transform.position.y, 1f)); //Sets the position of the slot.
                slot.transform.parent = slotContainer.transform;
                slot.GetComponent<PuzzleSlot>().SetSlotPosition(x, y);
                //slot.GetComponent<UnityEngine.UI.Outline>().enabled = true;
                puzzlePiecesList.Add(piece);
            }
        }
        
        //puzzleBoard.transform.position = new Vector3(-1000f, -1000f, -1000f);
        puzzleBoard.enabled = false;
    }

    /// <summary>
    /// Load the image from an internet source.
    /// </summary>
    /// <param name="url">The url of the source.</param>
    /// <returns>The IEnumerator used by the coroutine.</returns>
    private IEnumerator LoadImage(string url) {
        UnityWebRequest www = UnityWebRequest.Get(url);
        DownloadHandler handle = www.downloadHandler;
        Debug.Log("Is Loading..."); //Before the image is loaded.
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            errorMessage = www.error;
        } else {
            //Texture2D texture2d = new Texture2D(0, 0); //Build an empty texture to be loaded in later.
            if (image.LoadImage(handle.data)) {
                Debug.Log("Loaded"); //After the image is loaded.
                CropImage();
            } else {
                errorMessage = "Error with image.";
            }
        }
    }

    private void CropImage() {
        //Load pieces
        GameObject[,] pieces = createdPieces;
        float pixelsPerWidth = image.width / width_pieces;
        float pixelsPerHeight = image.height / height_pieces;
        //Go through each piece in the array and assign the image to it.
        for (int x = 0; x < width_pieces; x++) {
            for (int y = 0; y < height_pieces; y++) {
                Sprite sprite = null;
                BoxCollider2D collider = pieces[x, y].GetComponent<BoxCollider2D>();
                Rect rect = new Rect(pixelsPerWidth * x,
                pixelsPerHeight * y, pixelsPerWidth, pixelsPerHeight);
                sprite = Sprite.Create(image, rect, Vector2.zero);
                if (sprite != null) {
                    SpriteRenderer renderer = pieces[x, y].GetComponent<PuzzlePiece>().GetSpriteRenderer();
                    renderer.sprite = sprite;
                    float xScale = 1f;
                    float yScale = 1f;
                    if (collider.size.x * 100f < sprite.rect.width) {
                        xScale = (collider.size.x * 100f) / sprite.rect.width;
                    } else if(collider.size.x * 100 > sprite.rect.width) {
                        xScale = sprite.rect.width / (collider.size.x * 100f);
                    }
                    if (collider.size.y * 100f < sprite.rect.height) {
                        yScale = (collider.size.y * 100f) / sprite.rect.height;
                    } else if (collider.size.y * 100 > sprite.rect.height) {
                        yScale = sprite.rect.height / (collider.size.y * 100f);
                    }
                    PuzzlePiece piece = pieces[x, y].GetComponent<PuzzlePiece>();
                    piece.SetImagePosition(new Vector2(collider.size.x / -2f, collider.size.y / -2f));
                    piece.SetImageScale(new Vector3(xScale, yScale));
                    piece.SetCorrectPosition(x, y);
                }
            }
        }
    }

    private void MovePiecesToContainer() {
        float box_start_x = piecesContainer.transform.position.x - (piecesContainer.size.x / 2);
        float box_end_x = piecesContainer.transform.position.x + (piecesContainer.size.x / 2);
        float box_start_y = piecesContainer.transform.position.y - (piecesContainer.size.y / 2);
        float box_end_y = piecesContainer.transform.position.y + (piecesContainer.size.y / 2);
        foreach (GameObject piece in puzzlePiecesList) {
            float x = Random.Range(box_start_x, box_end_x);
            float y = Random.Range(box_start_y, box_end_y);
            piece.transform.position = new Vector3(x, y, 0f);
        }
        piecesContainer.enabled = false;
    }
}
