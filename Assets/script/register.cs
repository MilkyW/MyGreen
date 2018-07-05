using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CI.HttpClient;
using System;
using System.Text.RegularExpressions;
using BestHTTP;

public class register : MonoBehaviour {
    public Button submit;
    public Button login;
    public InputField username;
    public InputField nickname;
    public Toggle male;
    public Toggle female;
    public InputField email;
    public InputField phone;
    public InputField password;
    public InputField c_password;
    public InputField firstname;
    public InputField lastname;
    public List<InputField> required;
    public Text c_pwd_warning;
    public Text username_existed;
    public List<Text> warning;
    public Text phone_existed;
    public Text email_existed;
    public Text phone_illegal;
    public Text email_illegal;
    public Text username_pass;
    public Text phone_pass;
    public Text email_pass;
    public Text c_pwd_pass;

    // Use this for initialization
    void Start () {
        warning.Add(c_pwd_warning);
        warning.Add(username_existed);
        warning.Add(phone_existed);
        warning.Add(email_existed);
        warning.Add(phone_illegal);
        warning.Add(email_illegal);
        required.Add(username);
        required.Add(nickname);
        required.Add(password);
        required.Add(c_password);
        required.Add(firstname);
        required.Add(lastname);
        required.Add(phone);
        required.Add(email);
        submit.onClick.AddListener(SubmitOnClick);
        login.onClick.AddListener(LoginOnClick);
        male.onValueChanged.AddListener(delegate { MaleOnValueChanged(); });
        female.onValueChanged.AddListener(delegate { FemaleOnValueChanged(); });
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        c_password.onEndEdit.AddListener(delegate { ComfirmPasswordCheck(); });
        username.onEndEdit.AddListener(delegate { UsernameCheck(); });
        phone.onEndEdit.AddListener(delegate { PhoneCheck(); });
        email.onEndEdit.AddListener(delegate { EmailCheck(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UsernameCheck()
    {
        HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/isAccountExist?account=" + username.text), HTTPMethods.Get, (req, res) => {
            if (res.DataAsText == "true")
            {
                username_existed.gameObject.SetActive(true);
                username_pass.gameObject.SetActive(false);
            }
            else
            {
                username_existed.gameObject.SetActive(false);
                username_pass.gameObject.SetActive(true);
            }
        }).Send();
    }

    void PhoneCheck()
    {
        Regex r = new Regex("^13|15|18[0-9]{9}$");

        if (!r.IsMatch(phone.text))
        {
            phone_illegal.gameObject.SetActive(true);
            phone_existed.gameObject.SetActive(false);
            phone_pass.gameObject.SetActive(false);
            return;
        }
        phone_illegal.gameObject.SetActive(false);
        HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/isPhoneExist?phone=" + phone.text), HTTPMethods.Get, (req, res) => {
            if (res.DataAsText == "true")
            {
                phone_existed.gameObject.SetActive(true);
                phone_pass.gameObject.SetActive(false);
            }
            else
            {
                phone_existed.gameObject.SetActive(false);
                phone_pass.gameObject.SetActive(true);
            }
        }).Send();
    }

    void EmailCheck()
    {
        Regex r = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");

        if (!r.IsMatch(email.text))
        {
            email_illegal.gameObject.SetActive(true);
            email_existed.gameObject.SetActive(false);
            email_pass.gameObject.SetActive(false);
            return;
        }
        email_illegal.gameObject.SetActive(false);
        HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/isEmailExist?email=" + email.text), HTTPMethods.Get, (req, res) => {
            if (res.DataAsText == "true")
            {
                email_existed.gameObject.SetActive(true);
                email_pass.gameObject.SetActive(false);
            }
            else
            {
                email_existed.gameObject.SetActive(false);
                email_pass.gameObject.SetActive(true);
            }
        }).Send();
    }

    void ComfirmPasswordCheck()
    {
        if (c_password.text == "")
            return;
        if (!function.InputEqualCheck(password, c_password))
        {
            c_pwd_warning.gameObject.SetActive(true);
            c_pwd_pass.gameObject.SetActive(false);
        }
        else
        {
            c_pwd_warning.gameObject.SetActive(false);
            c_pwd_pass.gameObject.SetActive(true);
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
            SceneManager.LoadScene("garden");
    }

    void LoginOnClick()
    {
        SceneManager.LoadScene("log");
    }

    void  MaleOnValueChanged()
    {
        if (!male.isOn && !female.isOn)
        {
            male.isOn = !male.isOn;
        }
        if (male.isOn && female.isOn)
        {
            female.isOn = false;
        }
    }

    void FemaleOnValueChanged()
    {
        if (!male.isOn && !female.isOn)
        {
            female.isOn = !female.isOn;
        }
        if (male.isOn && female.isOn)
        {
            male.isOn = false;
        }
    }
}
