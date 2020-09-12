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

    // Start is called before the first frame update
    void Start() {
        if(image == null) {
            image = null;
        }
        correctX = 0;
        correctY = 0;
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
}
