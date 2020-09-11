using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour {

    [SerializeField]
    private GameObject image;

    // Start is called before the first frame update
    void Start() {

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
}
