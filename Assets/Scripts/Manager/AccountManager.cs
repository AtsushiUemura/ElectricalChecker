using UnityEngine;
using System.Collections;
using System.IO;

public class AccountManager : SingletonMonoBehaviour<AccountManager> {

    [System.Serializable]
    public class AccountInfo {
        public string Name;
        public string Password;
    }
    public AccountInfo CurrentAccount;

    #region
    void Awake() {
        if (this != Instance) {
            Destroy(this.gameObject);
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

    public void CreateButton() {
        UIManager.Instance.RegistrationObj.SetActive(true);
    }

    public void RegistrationButton() {
        AccountInfo data = new AccountInfo();
        data.Name = UIManager.Instance.NewAccountNameText.text;
        data.Password = UIManager.Instance.NewAccountPasswordText.text;
        string rePassword = UIManager.Instance.NewReAccountPasswordText.text;
        Create(data, rePassword);
    }

    public void CancelButton() {
        UIManager.Instance.NewAccountNameText.text = "";
        UIManager.Instance.NewAccountPasswordText.text = "";
        UIManager.Instance.NewReAccountPasswordText.text = "";
        UIManager.Instance.RegistrationObj.SetActive(false);
    }

    public void LoginButton() {
        AccountInfo data = new AccountInfo();
        data.Name = UIManager.Instance.AccountNameText.text;
        data.Password = UIManager.Instance.AccountPasswordText.text;
        if (Login(data)) {
            SceneManager.Instance.LoadScene("Main");
        }
    }

    /// <summary>
    /// アカウントを登録する
    /// </summary>
    /// <param name="data"></param>
    private bool Create(AccountInfo data, string rePassword) {
        if (data.Name.Equals("") || data.Password.Equals("") || rePassword.Equals("")) {
            UIManager.Instance.LogText.text = "入力された値が不正です";
            return false;
        }
        if (!data.Password.Equals(rePassword)) {
            UIManager.Instance.LogText.text = "再入力パスワードが間違っています";
            return false;
        }
        if (SaveData.ContainsKey(data.Name)) {
            UIManager.Instance.LogText.text = "すでに登録されています";
            return false;
        }
        UIManager.Instance.PopAlart(data.Name + " を登録しますか？");
        UIManager.Instance.YesButtonDelegate = () => {
            SaveData.SetString(data.Name, data.Password);
            SaveData.Save();
            UIManager.Instance.LogText.text = "登録に成功しました";
            UIManager.Instance.NewAccountNameText.text = "";
            UIManager.Instance.NewAccountPasswordText.text = "";
            UIManager.Instance.NewReAccountPasswordText.text = "";
            UIManager.Instance.RegistrationObj.SetActive(false);

            UIManager.Instance.NoButtonDelegate();
        };
        return true;
    }

    /// <summary>
    /// ログイン処理
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private bool Login(AccountInfo data) {
        if (data.Name.Equals("") || data.Password.Equals("")) {
            UIManager.Instance.LogText.text = "入力された値が不正です";
            return false;
        }
        foreach (var item in SaveData.Keys()) {
            if (item.Equals(data.Name)) {
                if (SaveData.GetString(item).Equals(data.Password)) {
                    CurrentAccount.Name = item;
                    CurrentAccount.Password = SaveData.GetString(item);
                    UIManager.Instance.LoginNameText.text = item;
                    UIManager.Instance.LogText.text = "ログインに成功しました";
                    return true;
                }
            }
        }
        UIManager.Instance.LogText.text = "登録されていないアカウントです";
        return false;
    }

    public void CrearButton() {
        SaveData.Clear();
        UIManager.Instance.LogText.text = "データベースを削除しました";
    }

}
