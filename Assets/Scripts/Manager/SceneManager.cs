using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManager : SingletonMonoBehaviour<SceneManager> {



    #region
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
    #endregion

    public void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }
}
