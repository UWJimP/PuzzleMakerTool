using UnityEngine;
using UnityEngine.UI;

public class UpdateSliderIntText : MonoBehaviour {

    private Text wholeNumber;

    // Start is called before the first frame update
    void Start() {
        wholeNumber = GetComponent<Text>();
    }

    public void UpdateText(float value) {
        wholeNumber.text = value.ToString();
    }
}
