using BestHTTP;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sensor_i : MonoBehaviour {

    public static sensor show;
    public Text location_x;
    public Text location_y;
    public Toggle temperature;
    public Toggle humidity;
    public InputField sensor_name;
    public List<InputField> required;
    public List<Text> warning;
    public Text name_existed;
    public Button delete;
    public Text name_pass;
    public Button save;
    public List<Text> pass;
    public m_garden selected;
    public Button information;
    public Button chart;

    // Use this for initialization
    void Start () {
        warning.Add(name_existed);
        required.Add(sensor_name);
        pass.Add(name_pass);
        delete.onClick.AddListener(DeleteOnClick);
        save.onClick.AddListener(SaveOnClick);
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        sensor_name.onEndEdit.AddListener(delegate { NameCheck(); });
	}

    void OnEnable()
    {
        selected = function.FindSelected();
        sensor_name.text = show.getName();
        location_x.text = "x:   " + show.getX();
        location_y.text = "y:   " + show.getY();
        if (show.getType())
            temperature.isOn = true;
        else
            humidity.isOn = true;
    }

    void OnDisable()
    {
        function.Clear(required, warning, pass);
        function.FreshGarden(selected);
        temperature.isOn = false;
        humidity.isOn = false;
    }

    // Update is called once per frame
    void Update () {
		
	}

    void NameCheck()
    {
        if (sensor_name.text == show.getName())
            return;
        if (function.SensorNameCheck(selected, sensor_name.text))
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
        if (show.getType())
        {
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/deleteTemperatureSensorById?id=" + show.getId()), HTTPMethods.Get, (req, res) =>
            {
                switch (req.State)
                {
                    case HTTPRequestStates.Finished:
                        Debug.Log(res.DataAsText);
                        GameObject.Find("Canvas/cover").SetActive(false);
                        GameObject.Find("Canvas/sensor_info").SetActive(false);
                        break;
                    default:
                        Debug.Log("Error!Status code:" + res.StatusCode);
                        break;
                }
            }).Send();
        }
        else
        {
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/deleteWetnessSensorById?id=" + show.getId()), HTTPMethods.Get, (req, res) =>
            {
                switch (req.State)
                {
                    case HTTPRequestStates.Finished:
                        Debug.Log("delete");
                        GameObject.Find("Canvas/cover").SetActive(false);
                        GameObject.Find("Canvas/sensor_info").SetActive(false);
                        break;
                    default:
                        Debug.Log("Error!Status code:" + res.StatusCode);
                        break;
                }
            }).Send();
        }
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
            if (sensor_name.text != show.getName())
            {
                if (show.getType())
                {
                    HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/updateTemperatureSensorNameById?id=" + show.getId() + "&name=" + sensor_name.text), HTTPMethods.Post, (req, res) =>
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
                else
                {
                    HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/updateWetnessSensorNameById?id=" + show.getId() + "&name=" + sensor_name.text), HTTPMethods.Post, (req, res) =>
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
            }
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/sensor_info").SetActive(false);
        }
    }
}
