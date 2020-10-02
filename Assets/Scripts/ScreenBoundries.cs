using UnityEngine;

public class ScreenBoundries : MonoBehaviour {

    private Vector2 screenBounds;
    //private float objectWidth;
    //private float objectHeight;

    //// Start is called before the first frame update
    //void Start() {
    //    screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 
    //        Screen.height, Camera.main.transform.position.z));
    //    objectWidth = transform.GetComponent<BoxCollider2D>().size.x / 2;
    //    objectHeight = transform.GetComponent<BoxCollider2D>().size.y / 2;
    //}

    //// Update is called once per frame
    //void LateUpdate() {
    //    Vector3 viewPos = transform.position;
    //    viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x);
    //    viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, screenBounds.y);
    //    transform.position = viewPos;
    //}
    private void Start() {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 
            Screen.height, Camera.main.transform.position.z));
        Debug.Log(screenBounds.x + ", " + screenBounds.y);
    }
}
