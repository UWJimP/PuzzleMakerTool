using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {

    [SerializeField]
    private GameObject image;

    [SerializeField]
    private int correctX;

    [SerializeField]
    private int correctY;

    private float startPosX;
    private float startPosY;
    private bool moving;

    // Start is called before the first frame update
    void Start() {
        if(image == null) {
            image = null;
        }
        moving = false;
        correctX = 0;
        correctY = 0;
    }

    private void Update() {
        if (moving) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0f);
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

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
