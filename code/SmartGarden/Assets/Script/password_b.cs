using BestHTTP;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class password_b : MonoBehaviour {

    public InputField origin_password;
    public InputField new_password;
    public InputField comfirm_password;
    public Button submit;
    public Button cancel;
    public List<InputField> required;
    public List<Text> warning;
    public List<Text> pass;
    public Text origin_wrong;
    public Text origin_pass;
    public Text new_wrong;
    public Text new_pass;
    public Text comfirm_wrong;
    public Text comfirm_pass;

	// Use this for initialization
	void Start () {
        required.Add(origin_password);
        required.Add(new_password);
        required.Add(comfirm_password);
        warning.Add(origin_wrong);
        warning.Add(new_wrong);
        warning.Add(comfirm_wrong);
        pass.Add(origin_pass);
        pass.Add(new_pass);
        pass.Add(comfirm_pass);
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        cancel.onClick.AddListener(delegate { function.Clear(required, warning, pass); });
        submit.onClick.AddListener(SubmitOnClick);
        origin_password.onEndEdit.AddListener(delegate { OriginCheck(); });
        new_password.onEndEdit.AddListener(delegate { NewCheck(); });
        comfirm_password.onEndEdit.AddListener(delegate { ComfirmCheck(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OriginCheck()
    {
        if (function.EncryptWithMD5(origin_password.text) == data.m_user.getPassword())
        {
            origin_pass.gameObject.SetActive(true);
            origin_wrong.gameObject.SetActive(false);
        }
        else
        {
            origin_pass.gameObject.SetActive(false);
            origin_wrong.gameObject.SetActive(true);
        }
    }

    void NewCheck()
    {
        if (function.EncryptWithMD5(new_password.text) == data.m_user.getPassword())
        {
            new_pass.gameObject.SetActive(false);
            new_wrong.gameObject.SetActive(true);
        }
        else
        {
            new_pass.gameObject.SetActive(true);
            new_wrong.gameObject.SetActive(false);
        }
    }

    void ComfirmCheck()
    {
        if (new_password.text == comfirm_password.text)
        {
            comfirm_pass.gameObject.SetActive(true);
            comfirm_wrong.gameObject.SetActive(false);
        }
        else
        {
            comfirm_pass.gameObject.SetActive(false);
            comfirm_wrong.gameObject.SetActive(true);
        }
    }

    void SubmitOnClick()
    {
        foreach (InputField e in required)
            function.RequiredInputOnEndEdit(e);
        foreach (Text e in warning)
            if (e.IsActive())
                return;
        if (function.InputFieldRequired(required))
        {
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/updateUser"), HTTPMethods.Post, (req, res) => {
                switch (req.State)
                {
                    case HTTPRequestStates.Finished:
                        Debug.Log("Successfully save!");
                        HTTPRequest request_getUser = new HTTPRequest(new Uri(data.IP + "/getUserByAccount?account=" + data.m_user.getUsername()), HTTPMethods.Get, (req_user, res_user) =>
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
                            GameObject.Find("Canvas/cover").SetActive(false);
                            GameObject.Find("Canvas/password_box").SetActive(false);
                        }).Send();
                        break;
                    default:
                        Debug.Log("Error!Status code:" + res.StatusCode);
                        break;
                }
            });
            request.AddHeader("Content-Type", "application/json");

            JsonData newUser = new JsonData();
            newUser["id"] = data.m_user.getId();
            newUser["username"] = data.m_user.getUsername();
            newUser["password"] = function.EncryptWithMD5(new_password.text);
            newUser["nickname"] = data.m_user.getNickname();
            newUser["gender"] = data.m_user.getGender();
            newUser["phone"] = data.m_user.getPhone();
            newUser["email"] = data.m_user.getEmail();
            newUser["firstname"] = data.m_user.getFirstname();
            newUser["lastname"] = data.m_user.getLastname();
            newUser["enabled"] = true;
            request.RawData = System.Text.Encoding.UTF8.GetBytes(newUser.ToJson());
            request.Send();
        }
    }
}
