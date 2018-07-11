using BestHTTP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class info : MonoBehaviour {
    public Dropdown view;
    public Text username;
    public InputField nickname;
    public InputField firstname;
    public InputField lastname;
    public InputField phone;
    public InputField email;
    public Toggle male;
    public Toggle female;
    public List<Text> warning;
    public List<InputField> required;
    public Text phone_existed;
    public Text email_existed;
    public Text phone_illegal;
    public Text email_illegal;
    public Text phone_pass;
    public Text email_pass;
    public Button save;
    public HTTPRequest request_phone;
    public HTTPRequest request_email;

    // Use this for initialization
    void Start () {
        required.Add(firstname);
        required.Add(lastname);
        required.Add(nickname);
        required.Add(phone);
        required.Add(email);
        warning.Add(phone_illegal);
        warning.Add(email_illegal);
        warning.Add(phone_existed);
        warning.Add(email_existed);
        male.onValueChanged.AddListener(delegate { MaleOnValueChanged(); });
        female.onValueChanged.AddListener(delegate { FemaleOnValueChanged(); });
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        email.onEndEdit.AddListener(delegate { EmailCheck(); });
        phone.onEndEdit.AddListener(delegate { PhoneCheck(); });
        save.onClick.AddListener(SaveOnClick);
        username.text = data.m_user.getUsername();
        nickname.text = data.m_user.getNickname();
        firstname.text = data.m_user.getFirstname();
        lastname.text = data.m_user.getLastname();
        phone.text = data.m_user.getPhone();
        email.text = data.m_user.getEmail();
        if (data.m_user.getGender() == true)
            male.isOn = true;
        else
            female.isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (view.value == 1)
        {
            SceneManager.LoadScene("garden");
        }
        else if (view.value == 3)
        {
            SceneManager.LoadScene("log");
        }
	}

    void PhoneCheck()
    {
        if (phone.text == data.m_user.getPhone())
            return;
        if (phone.text == "")
        {
            phone_illegal.gameObject.SetActive(false);
            phone_pass.gameObject.SetActive(false);
            phone_existed.gameObject.SetActive(false);
            return;
        }
        if (!data.phone.IsMatch(phone.text))
        {
            phone_illegal.gameObject.SetActive(true);
            phone_existed.gameObject.SetActive(false);
            phone_pass.gameObject.SetActive(false);
            return;
        }
        phone_illegal.gameObject.SetActive(false);
        HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/isPhoneExist?phone=" + phone.text), HTTPMethods.Get, (req, res) =>
        {
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
        });
        if (request_phone != null && request_phone.State == HTTPRequestStates.Processing)
            request_phone.Abort();
        request_phone = request;
        request_phone.Send();
    }

    void EmailCheck()
    {
        if (email.text == data.m_user.getEmail())
            return;
        if (email.text == "")
        {
            email_pass.gameObject.SetActive(false);
            email_illegal.gameObject.SetActive(false);
            email_existed.gameObject.SetActive(false);
            return;
        }
        if (!data.email.IsMatch(email.text))
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
        });
        if (request_email != null && request_email.State == HTTPRequestStates.Processing)
            request_email.Abort();
        request_email = request;
        request_email.Send();
    }

    void SaveOnClick()
    {
        foreach (InputField e in required)
            function.RequiredInputOnEndEdit(e);
        foreach (Text e in warning)
            if (e.IsActive())
                return;
        if (function.InputFieldRequired(required))
        {
        }
    }

    void MaleOnValueChanged()
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
