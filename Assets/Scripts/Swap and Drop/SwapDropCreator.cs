using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class SwapDropCreator : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void FinishLoading();

    [DllImport("__Internal")]
    private static extern void ChangeMenu(int value);

    public bool isDebug;

    [SerializeField]
    private bool randomRotation;

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

    public BoxCollider2D[] piecesContainer;

    // Start is called before the first frame update
    void Start() {
        if(isDebug) {
            string test = "https://vignette.wikia.nocookie.net/among-us-wiki/images/a/ab/Cyan.png/revision/latest/scale-to-width-down/340?cb=20200927084517";
            //string test = "https://i.pinimg.com/236x/7b/4d/2c/7b4d2c600f17fb2cff1fd7418306c5bc--fantasy-armor-dark-fantasy.jpg";
            DebugInitialize(5, 5, test);
            FinishLoading();
            ChangeMenu((int)MenuType.NONE);
            ChangeMenu((int)MenuType.DRAGDROP);
        } else {
            InitializeCreator3();
            FinishLoading();
            ChangeMenu((int)MenuType.DRAGDROP);
        }
    }

    private void InitializeCreator() {
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
            if (errorMessage == null) {

                MovePiecesToContainer();
            }
        }
    }

    private void InitializeCreator2() {
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
            LoadImage();
            if (errorMessage == null) {
                MovePiecesToContainer();
            }
        }
    }

    private void InitializeCreator3() {
        puzzlePiecesList = new List<GameObject>();
        GameObject dataObject = GameObject.Find("Data Manager");
        dropData = dataObject.GetComponent<SwapDropData>();

        if (puzzlePiece == null || puzzleBoard == null || puzzleSlot == null) {
            puzzlePiece = null;
            puzzleBoard = null;
            puzzleSlot = null;
        }

        //Initialize the puzzle data into this project.
        width_pieces = dropData.GetWidth();
        height_pieces = dropData.GetHeight();
        image = dropData.GetTexture();
        randomRotation = dropData.IsRandomRotation();
        if (dropData.GetOrientation() == SwapDropData.Orientation.square) {
            puzzleBoard.size.Set(8, 8);
        } else if (dropData.GetOrientation() == SwapDropData.Orientation.portrait) {
            puzzleBoard.size.Set(6, 8);
        } else {
            puzzleBoard.size.Set(8, 6);
        }

        errorMessage = null;
        if (width_pieces > 0 && height_pieces > 0 && puzzlePiece != null && puzzleBoard != null && puzzleSlot != null) {
            createdPieces = new GameObject[width_pieces, height_pieces];
            transform.position = new Vector3(puzzleBoard.transform.position.x, puzzleBoard.transform.position.y, 0f);
            GeneratePieces();
            LoadImage();
            if (errorMessage == null) {
                MovePiecesToContainer();
                if (randomRotation) {
                    RotatePieces();
                }
                GameObject data = GameObject.Find("Data Manager");
                Destroy(data);
            }
        }
    }

    private void DebugInitialize(int width, int height, string url) {
        puzzlePiecesList = new List<GameObject>();

        if (puzzlePiece == null || puzzleBoard == null || puzzleSlot == null) {
            puzzlePiece = null;
            puzzleBoard = null;
            puzzleSlot = null;
        }

        //Initialize the puzzle data into this project.
        width_pieces = width;
        height_pieces = height;
        image = new Texture2D(0, 0);

        errorMessage = null;
        if (width_pieces > 0 && height_pieces > 0 && puzzlePiece != null && puzzleBoard != null && puzzleSlot != null) {
            createdPieces = new GameObject[width_pieces, height_pieces];
            transform.position = new Vector3(puzzleBoard.transform.position.x, puzzleBoard.transform.position.y, 0f);
            DebugGeneratePieces();
            StartCoroutine(LoadImage(url));
            if (errorMessage == null) {
                MovePiecesToContainer();
                if(randomRotation) {
                    RotatePieces();
                }
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

    private void LoadImage() {
        CropImage();
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
                //CropImage();
                DebugCropImage();
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
                    } else if(collider.size.x * 100f > sprite.rect.width) {
                        //xScale = sprite.rect.width / (collider.size.x * 100f);
                        xScale = (collider.size.x * 100f) / sprite.rect.width;
                    }
                    if (collider.size.y * 100f < sprite.rect.height) {
                        yScale = (collider.size.y * 100f) / sprite.rect.height;
                    } else if (collider.size.y * 100f > sprite.rect.height) {
                        //yScale = sprite.rect.height / (collider.size.y * 100f);
                        yScale = (collider.size.y * 100f) / sprite.rect.height;
                    }
                    PuzzlePiece piece = pieces[x, y].GetComponent<PuzzlePiece>();
                    piece.SetImagePosition(new Vector2(collider.size.x / -2f, collider.size.y / -2f));
                    piece.SetImageScale(new Vector3(xScale, yScale));
                    piece.SetCorrectPosition(x, y);
                    piece.SetAlphaSize();
                }
            }
        }
    }

    private struct PiecesContainer {
        public PiecesContainer(float pos_x, float pos_y, float size_x, float size_y) {
            Start_x = pos_x - (size_x / 2);
            End_x = pos_x + (size_x / 2);
            Start_y = pos_y - (size_y / 2);
            End_y = pos_y + (size_y / 2);
        }

        public float Start_x { get; }
        public float Start_y { get; }
        public float End_x { get; }
        public float End_y { get; }
    }

    private void MovePiecesToContainer() {
        //float box_start_x = piecesContainer.transform.position.x - (piecesContainer.size.x / 2);
        //float box_end_x = piecesContainer.transform.position.x + (piecesContainer.size.x / 2);
        //float box_start_y = piecesContainer.transform.position.y - (piecesContainer.size.y / 2);
        //float box_end_y = piecesContainer.transform.position.y + (piecesContainer.size.y / 2);
        //foreach (GameObject piece in puzzlePiecesList) {
        //    float x = Random.Range(box_start_x, box_end_x);
        //    float y = Random.Range(box_start_y, box_end_y);
        //    //piece.transform.position = new Vector3(x, y, 0f);
        //    piece.GetComponent<PuzzlePiece>().SetPosition(x, y);
        //}
        //piecesContainer.enabled = false;
        PiecesContainer[] containers = new PiecesContainer[piecesContainer.Length];
        for(int index = 0; index < piecesContainer.Length; index++) {
            BoxCollider2D box = piecesContainer[index];
            containers[index] = new PiecesContainer(box.transform.position.x,
                box.transform.position.y, box.size.x, box.size.y);
            box.enabled = false;
        }
        foreach(GameObject piece in puzzlePiecesList) {
            int index = Random.Range(0, containers.Length);
            float x = Random.Range(containers[index].Start_x, containers[index].End_x);
            float y = Random.Range(containers[index].Start_y, containers[index].End_y);
            piece.GetComponent<PuzzlePiece>().SetPosition(x, y);
        }
    }

    private void RotatePieces() {
        foreach(GameObject piece in puzzlePiecesList) {
            int rotation = Random.Range(0, 4) * 90;
            piece.transform.Rotate(0, 0, rotation);
        }
    }

    private void DebugGeneratePieces() {
        Debug.Log("Debug: Generate Pieces");
        //Calculates the pixels per width and height for each piece.
        float width = puzzleBoard.size.x / width_pieces;
        float height = puzzleBoard.size.y / height_pieces;
        Debug.Log("Puzzle Board: " + width + ", " + height);
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
                puzzlePiecesList.Add(piece);
            }
        }

        //puzzleBoard.transform.position = new Vector3(-1000f, -1000f, -1000f);
        puzzleBoard.enabled = false;
    }

    private void DebugCropImage() {
        Debug.Log("Debug: Crop Image");
        //Load pieces
        GameObject[,] pieces = createdPieces;
        float pixelsPerWidth = image.width / width_pieces;
        float pixelsPerHeight = image.height / height_pieces;
        Debug.Log("Image Size: " + image.width + ", " + image.height);
        Debug.Log("Pixels per size: " + pixelsPerWidth + ", " + pixelsPerHeight);
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
                    } else if (collider.size.x * 100f > sprite.rect.width) {
                        //xScale = sprite.rect.width / (collider.size.x * 100f);
                        xScale = (collider.size.x * 100f) / sprite.rect.width;
                    }
                    if (collider.size.y * 100f < sprite.rect.height) {
                        yScale = (collider.size.y * 100f) / sprite.rect.height;
                    } else if (collider.size.y * 100f > sprite.rect.height) {
                        //yScale = sprite.rect.height / (collider.size.y * 100f);
                        yScale = (collider.size.y * 100f) / sprite.rect.height;
                    }
                    if(x == 0 && y == 0) {
                        Debug.Log("Collider: " + collider.size.x + ", " + collider.size.y);
                        Debug.Log("Sprite Rect: " + sprite.rect.width + ", " + sprite.rect.height);
                        Debug.Log("Scale: " + xScale + ", " + yScale);
                    }
                    PuzzlePiece piece = pieces[x, y].GetComponent<PuzzlePiece>();
                    piece.SetImagePosition(new Vector2(collider.size.x / -2f, collider.size.y / -2f));
                    piece.SetImageScale(new Vector3(xScale, yScale));
                    piece.SetCorrectPosition(x, y);
                    piece.SetAlphaSize();
                }
            }
        }
    }
}
