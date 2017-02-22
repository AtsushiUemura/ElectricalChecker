using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class AccountManager : SingletonMonoBehaviour<AccountManager> {

    private string accountDataPath = "../";
    private string accountDataName = "account_data.txt";

    private class AccountInfo {
        public string name;
        public string pass;
        public AccountInfo(string name, string pass) {
            this.name = name;
            this.pass = pass;
        }
    }

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

    private void Init() {
        UIManager.Instance.AccountNameText.text = "";
        UIManager.Instance.AccountPasswordText.text = "";
    }

    public void CreatAccount() {
        string filePath = accountDataPath + accountDataName;
        if (!CheckInputValue()) return;
        AccountInfo data = new AccountInfo(UIManager.Instance.AccountNameText.text, UIManager.Instance.AccountPasswordText.text);
        if (!ExistFile(filePath)) {
            WriteFile(filePath, data);
            Debug.Log("新規ファイルを作成しました");
        }

        if (!ExistAccount(filePath, data)) {
            WriteFile(filePath, data);
            Debug.Log("新規アカウントを作成しました");
        }
        else {
            AccountInfo accountInfo = ReadFile(filePath, data);
            if (accountInfo == null) return;
            UIManager.Instance.LoginNameText.text = accountInfo.name;
            Debug.Log("ログインしました");
        }
        Init();
    }

    private bool CheckInputValue() {
        if (UIManager.Instance.AccountNameText.text == "") {
            return false;
        }
        if (UIManager.Instance.AccountPasswordText.text == "") {
            return false;
        }
        return true;
    }

    private bool ExistAccount(string filePath, AccountInfo data) {
        bool flag = false;
        StreamReader sr = new StreamReader(
          filePath, Encoding.GetEncoding("shift_jis"));
        while (sr.Peek() >= 0) {
            string line = sr.ReadLine();
            string[] word = line.Split(',');
            if (word[0].Equals(data.name)) {
                flag = true;
                break;
            }
        }
        sr.Close();
        return flag;
    }

    /// <summary>
    /// アカウントデータが存在するか確認
    /// </summary>
    /// <returns></returns>
    private bool ExistFile(string filePath) {
        if (File.Exists(filePath)) {
            return true;
        }
        else {
            return false;
        }
    }

    private void WriteFile(string filePath, AccountInfo data) {
        StreamWriter sw = new StreamWriter(
            filePath, true, Encoding.GetEncoding("shift_jis"));
        sw.Write(data.name + "," + data.pass + "\n");
        sw.Close();
    }

    private AccountInfo ReadFile(string filePath, AccountInfo data) {
        AccountInfo accountInfo = null;
        StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("shift_jis"));
        while (sr.Peek() >= 0) {
            string line = sr.ReadLine();
            string[] word = line.Split(',');
            if (word[0].Equals(data.name) && word[1].Equals(data.pass)) {
                accountInfo = new AccountInfo(word[0], word[1]);
                return accountInfo;
            }
        }
        sr.Close();
        return accountInfo;
    }
}
