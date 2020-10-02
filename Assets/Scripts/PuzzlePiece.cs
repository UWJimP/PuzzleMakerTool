using UnityEngine;

/// <summary>
/// The puzzle piece of the system.
/// </summary>
public class PuzzlePiece : MonoBehaviour {

    [SerializeField]
    private GameObject image;

    [SerializeField]
    private int correctX;

    [SerializeField]
    private int correctY;

    private float lastPosX;
    private float lastPosY;

    private float startPosX;
    private float startPosY;
    private bool moving;

    [SerializeField]
    private BoxCollider2D box;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start() {
        if(image == null) {
            image = null;
        }
        moving = false;
        correctX = 0;
        correctY = 0;
        lastPosX = 0;
        lastPosY = 0;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,
            Screen.height, Camera.main.transform.position.z));
        box = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Updates the piece's placement in real time.
    /// </summary>
    private void Update() {
        if (moving) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 newPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
            if ((newPosition.x + (box.size.x / 2)) > screenBounds.x) {
                newPosition.x = screenBounds.x - (box.size.x / 2);
            } else if ((newPosition.x - (box.size.x / 2)) < screenBounds.x * -1) {
                newPosition.x = (screenBounds.x * -1) + (box.size.x / 2);
            }
            if ((newPosition.y + (box.size.y / 2)) > screenBounds.y) {
                newPosition.y = screenBounds.y - (box.size.y / 2);
            } else if ((newPosition.y - (box.size.y / 2)) < screenBounds.y * -1) {
                newPosition.y = (screenBounds.y * -1) + (box.size.y / 2);
            }
            gameObject.transform.localPosition = newPosition;
        }
    }

    /// <summary>
    /// Tells if the piece is moving.
    /// </summary>
    /// <returns>If the piece is moving.</returns>
    public bool IsMoving() {
        return moving;
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            if(!moving) {
                lastPosX = transform.position.x;
                lastPosY = transform.position.y;
            }

            moving = true;
        }
    }

    private void OnMouseUp() {
        moving = false;
        //if (Mathf.Abs(transform.localPosition.x - correctForm.transform.localPosition.x) <= 0.5f &&
        //Mathf.Abs(transform.localPosition.y - correctForm.transform.localPosition.y) <= 0.5f) {
        //transform.localPosition = new Vector3(correctForm.transform.localPosition.x, correctForm.transform.localPosition.y, 0f);
        //} else {
        //transform.localPosition = new Vector3(resetPos.x, resetPos.y, 0f);
        //}
    }

    public void SetPosition(float x, float y) {
        transform.position = new Vector3(x, y, 0f);
        lastPosX = x;
        lastPosY = y;
    }

    public Vector3 GetLastPosition() {
        return new Vector3(lastPosX, lastPosY, 0f);
    }

    public SpriteRenderer GetSpriteRenderer() {
        return image.GetComponent<SpriteRenderer>();
    }

    public void SetImageScale(Vector3 vector3) {
        image.transform.localScale = vector3;
    }

    public void SetImagePosition(Vector2 vector2) {
        image.transform.localPosition = vector2;
    }

    public void SetCorrectPosition(int x, int y) {
        correctX = x;
        correctY = y;
    }

    public int GetCorrectX() {
        return correctX;
    }

    public int GetCorrectY() {
        return correctY;
    }
}
