using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CI.HttpClient;
using LitJson;
using System;
using BestHTTP;

public class log : MonoBehaviour
{
    public Button login;
    public Button register;
    public InputField username;
    public InputField password;
    public static string uname;
    public static string pwd;
    public List<InputField> required;

    // Use this for initialization
    void Start()
    {
        required.Add(username);
        required.Add(password);
        login.onClick.AddListener(LoginOnClick);
        register.onClick.AddListener(RegisterOnClick);
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void LoginOnClick()
    {
        foreach (InputField e in required)
            function.RequiredInputOnEndEdit(e);
        if (function.InputFieldRequired(required))
        {
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/login?account=" + username.text + "&password=" + password.text), HTTPMethods.Get, (req, res) => {
                if (res.DataAsText == "true")
                {
                    HTTPRequest request1 = new HTTPRequest(new Uri(data.IP + "/getUserByAccount?account=" + username.text), HTTPMethods.Get, (req1, res1) => {
                        JsonData json = JsonMapper.ToObject(res1.DataAsText);
                        Debug.Log(res1.DataAsText);
                        data.m_user.setUsername((string)json["account"]);
                        data.m_user.setNickname((string)json["nickname"]);
                        data.m_user.setPassword((string)json["password"]);
                        data.m_user.setFirstname((string)json["firstname"]);
                        data.m_user.setLastname((string)json["lastname"]);
                        data.m_user.setPhone((string)json["phone"]);
                        data.m_user.setGender((bool)json["gender"]);
                        data.m_user.setEmail((string)json["email"]);
                        if ((bool)json["valid"] == true)
                            SceneManager.LoadScene("garden");
                    }).Send();
                }
            }).Send();
        }
    }

    void RegisterOnClick()
    {
        SceneManager.LoadScene("register");
    }
}
