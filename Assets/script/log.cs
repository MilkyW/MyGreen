using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class log : MonoBehaviour
{
    public Button login;
    public Button register;
    public InputField username;
    public InputField password;
    public static string uname;
    public static string pwd;

    // Use this for initialization
    void Start()
    {
        login.onClick.AddListener(LoginOnClick);
        register.onClick.AddListener(RegisterOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoginOnClick()
    {
        username.placeholder.color = Color.gray;
        password.placeholder.color = Color.gray;
        if (username.text == "")
        {
            username.placeholder.GetComponent<Text>().text = "Required";
            username.placeholder.color = Color.red;
        }
        else if (password.text == "")
        {
            password.placeholder.GetComponent<Text>().text = "Required";
            password.placeholder.color = Color.red;
        }
        else
        {
            uname = username.text;
            pwd = password.text;
            SceneManager.LoadScene("garden");
        }
    }

    void RegisterOnClick()
    {
        SceneManager.LoadScene("register");
    }
}
