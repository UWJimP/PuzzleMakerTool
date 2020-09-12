using UnityEngine;

public class DragDrop : MonoBehaviour {

    public GameObject correctForm;
    [SerializeField]
    private bool moving;

    private float startPosX;
    private float startPosY;

    private Vector3 resetPos;

    // Start is called before the first frame update
    void Start() {
        //resetPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        if(moving) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
        }
    }

    private void OnMouseDown() {
        if(Input.GetMouseButtonDown(0)) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
            //PuzzlePiece piece = gameObject.GetComponent<PuzzlePiece>();
            //Debug.Log("Mouse down on X: " + piece.GetCorrectX() + " Y: " + piece.GetCorrectY());
        }
    }

    private void OnMouseUp() {
        moving = false;

        //if(Mathf.Abs(transform.localPosition.x - correctForm.transform.localPosition.x) <= 0.5f &&
        //    Mathf.Abs(transform.localPosition.y - correctForm.transform.localPosition.y) <= 0.5f) {
        //    transform.localPosition = new Vector3(correctForm.transform.localPosition.x, correctForm.transform.localPosition.y, 0f);
        //} else {
            //transform.localPosition = new Vector3(resetPos.x, resetPos.y, 0f);
        //}
    }
}
