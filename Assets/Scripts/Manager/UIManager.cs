using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager> {

    public delegate void UIDelegate();
    public UIDelegate YesButtonDelegate;
    public UIDelegate NoButtonDelegate;

    public Text LoginNameText;

    public InputField AccountNameText;
    public InputField AccountPasswordText;

    public InputField NewAccountNameText;
    public InputField NewAccountPasswordText;
    public InputField NewReAccountPasswordText;

    public Text LogText;

    public GameObject AlartImage;
    public float alartTakeTime;

    public GameObject MaskImage;
    public GameObject RegistrationObj;


    void Awake() {
        if (this != Instance) {
            Destroy(this.gameObject);
            return;
        }
    }
    // Use this for initialization
    void Start() {
        NoButtonDelegate = () => {
            AlartImage.GetComponent<RectTransform>().localScale = Vector2.zero;
            MaskImage.SetActive(false);
            YesButtonDelegate = () => { };
        };
    }

    // Update is called once per frame
    void Update() {

    }

    public void Yes() {
        YesButtonDelegate();
    }

    public void No() {
        NoButtonDelegate();
    }

    public void PopAlart(string message) {
        MaskImage.SetActive(true);
        AlartImage.GetComponentInChildren<Text>().text = message;
        StartCoroutine(ScaleUp(AlartImage.GetComponent<RectTransform>(), Vector2.one, alartTakeTime));
    }

    private bool scaleUpIsRunning;
    public AnimationCurve animationCurve;
    private IEnumerator ScaleUp(RectTransform rectTransform, Vector2 destination, float takeTime) {
        if (scaleUpIsRunning) {
            yield break;
        }
        scaleUpIsRunning = true;
        float elapsedTime = 0;
        Vector2 pivot = rectTransform.localScale;
        bool flag = false;
        while (!flag) {
            if (Vector2.Distance(rectTransform.localScale, destination) < 0.01f) {
                flag = true;
            }
            elapsedTime += Time.deltaTime * takeTime;
            float progress = animationCurve.Evaluate(elapsedTime);
            rectTransform.localScale = Vector2.Lerp(pivot, destination, progress);
            yield return null;
        }
        rectTransform.localScale = destination;
        scaleUpIsRunning = false;
    }

    //--------------------------------------------------------
    // フェードイン・アウト
    //-------------------------------------------------------
    public Image fadeImage;
    //フェードにかかる時間
    public float fadeOutInterval;
    private bool fadeOutIsRunning;
    /// <summary>
    /// フェードアウト
    /// </summary>
    private IEnumerator FadeOut(float interval) {
        if (fadeOutIsRunning) {
            yield break;
        }
        fadeOutIsRunning = true;
        float fadeAlpha = 0;
        float time = 0;
        while (time <= interval) {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            fadeImage.color = new Color(0, 0, 0, fadeAlpha);
            time += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1);
        fadeOutIsRunning = false;
    }

    public void MoveSceneTo(string sceneName) {
        SceneManager.Instance.LoadScene(sceneName);
    }
}
