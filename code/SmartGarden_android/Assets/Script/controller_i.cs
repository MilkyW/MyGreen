using BestHTTP;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller_i : MonoBehaviour {

    public static controller show;
    public Text location_x;
    public Text location_y;
    public InputField controller_name;
    public List<InputField> required;
    public List<Text> warning;
    public Text name_existed;
    public Button delete;
    public Text name_pass;
    public Button save;
    public List<Text> pass;
    public m_garden selected;
    public Toggle on;

    // Use this for initialization
    void Start()
    {
        warning.Add(name_existed);
        required.Add(controller_name);
        pass.Add(name_pass);
        delete.onClick.AddListener(DeleteOnClick);
        save.onClick.AddListener(SaveOnClick);
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        controller_name.onEndEdit.AddListener(delegate { NameCheck(); });
    }

    void OnEnable()
    {
        selected = function.FindSelected();
        controller_name.text = show.getName();
        location_x.text = "x:   " + show.getX() * selected.getLength() / data.length;
        location_y.text = "y:   " + show.getY() * selected.getWidth() / data.width;
        on.isOn = show.getState();
    }

    void OnDisable()
    {
        function.Clear(required, warning, pass);
        function.FreshGarden(selected);
        on.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NameCheck()
    {
        if (controller_name.text == show.getName())
            return;
        if (function.SensorNameCheck(selected, controller_name.text))
        {
            name_existed.gameObject.SetActive(true);
            name_pass.gameObject.SetActive(false);
        }
        else
        {
            name_existed.gameObject.SetActive(false);
            name_pass.gameObject.SetActive(true);
        }
    }

    void DeleteOnClick()
    {
        HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/deleteControllerById?id=" + show.getId()), HTTPMethods.Get, (req, res) =>
        {
            switch (req.State)
            {
                case HTTPRequestStates.Finished:
                    Debug.Log(res.DataAsText);
                    GameObject.Find("Canvas/cover").SetActive(false);
                    GameObject.Find("Canvas/controller_info").SetActive(false);
                    break;
                default:
                    Debug.Log("Error!Status code:" + res.StatusCode);
                    break;
            }
        }).Send();
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
            if (show.getName() != controller_name.text)
            {
                HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/updateControllerNameById?id=" + show.getId() + "&name=" + controller_name.text), HTTPMethods.Post, (req, res) =>
                {
                    switch (req.State)
                    {
                        case HTTPRequestStates.Finished:
                            Debug.Log(res.DataAsText);
                            break;
                        default:
                            Debug.Log("Error!Status code:" + res.StatusCode);
                            break;
                    }
                }).Send();
            }
            if (show.getState() != on.isOn)
            {
                Debug.Log("my state:" + on.isOn);
                HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/updateControllerValidById?id=" + show.getId() + "&valid=" + on.isOn), HTTPMethods.Get, (req, res) =>
                {
                    switch (req.State)
                    {
                        case HTTPRequestStates.Finished:
                            Debug.Log("Successfully save!");
                            break;
                        default:
                            Debug.Log("Error!Status code:" + res.StatusCode);
                            break;
                    }
                }).Send();
            }
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/controller_info").SetActive(false);
        }
    }
}
