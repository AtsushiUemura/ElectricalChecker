using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager> {

    public Text LoginNameText;

    public InputField AccountNameText;
    public InputField AccountPasswordText;

    void Awake() {
        if (this != Instance) {
            Destroy(this);
            return;
        }
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
