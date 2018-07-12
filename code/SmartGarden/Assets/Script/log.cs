using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
using System;
using BestHTTP;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Security.Cryptography;

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
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/login?account=" + username.text + "&password=" + password.text), HTTPMethods.Post, (req, res) => {
                Debug.Log(res.DataAsText);
                if (res.DataAsText == "true")
                {
                    HTTPRequest request_getUser = new HTTPRequest(new Uri(data.IP + "/getUserByAccount?account=" + username.text), HTTPMethods.Get, (req_user, res_user) =>
                    {
                        JsonData json = JsonMapper.ToObject(res_user.DataAsText);
                        Debug.Log(res_user.DataAsText);
                        data.m_user.setId((long)json["id"]);
                        data.m_user.setUsername((string)json["username"]);
                        data.m_user.setNickname((string)json["nickname"]);
                        data.m_user.setPassword((string)json["password"]);
                        data.m_user.setFirstname((string)json["firstname"]);
                        data.m_user.setLastname((string)json["lastname"]);
                        data.m_user.setPhone((string)json["phone"]);
                        data.m_user.setGender((bool)json["gender"]);
                        data.m_user.setEmail((string)json["email"]);
                        if ((bool)json["enabled"] == true)
                        {
                            HTTPRequest request_getGarden = new HTTPRequest(new Uri(data.IP + "/getGardenByUserId?userId=" + data.m_user.getId()), HTTPMethods.Get, (req_garden, res_garden) => {
                                Debug.Log(res_garden.DataAsText);
                                JArray array = JArray.Parse(res_garden.DataAsText);
                                foreach (var e in array)
                                {
                                    m_garden newGarden = new m_garden();
                                    newGarden.setId((long)e["id"]);
                                    newGarden.setName((string)e["name"]);
                                    newGarden.setLength((int)e["length"]);
                                    newGarden.setWidth((int)e["width"]);
                                    newGarden.setIdealTemperature((float)e["idealTemperature"]);
                                    newGarden.setIdealHumidty((float)e["idealWetness"]);
                                    Debug.Log(newGarden.getName());
                                    data.m_user.addGardens(newGarden);
                                }
                                SceneManager.LoadScene("garden");
                            }).Send();
                        }
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
