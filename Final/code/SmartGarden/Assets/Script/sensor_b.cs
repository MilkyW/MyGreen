using BestHTTP;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class sensor_b : MonoBehaviour {

    public Toggle temperature;
    public Toggle humidity;
    public InputField sensor_name;
    public InputField location_x;
    public InputField location_y;
    public List<InputField> required;
    public Button submit;
    public Button cancel;
    public List<Text> warning;
    public Text name_existed;
    public Text xy_existed;
    public Text x_illegal;
    public Text y_illegal;
    public Text name_pass;
    public Text xy_pass;
    public List<Text> pass;
    public m_garden selected;
    public Text x_out;
    public Text y_out;


    // Use this for initialization
    void Start () {    
        required.Add(sensor_name);
        required.Add(location_x);
        required.Add(location_y);
        warning.Add(name_existed);
        warning.Add(xy_existed);
        warning.Add(x_illegal);
        warning.Add(y_illegal);
        warning.Add(x_out);
        warning.Add(y_out);
        pass.Add(name_pass);
        pass.Add(xy_pass);
        temperature.onValueChanged.AddListener(delegate { TemperatureOnValueChanged(); });
        humidity.onValueChanged.AddListener(delegate { HumidityOnValueChanged(); });
        submit.onClick.AddListener(CreateSensorOnClick);
        cancel.onClick.AddListener(delegate { function.Clear(required, warning, pass); });
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        sensor_name.onEndEdit.AddListener(delegate { NameCheck(); });
        location_x.onEndEdit.AddListener(delegate { XCheck(); });
        location_y.onEndEdit.AddListener(delegate { YCheck(); });
    }

    void OnEnable()
    {
        selected = function.FindSelected();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void NameCheck()
    {
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

    void XCheck()
    {
        if (location_x.text == "")
        {
            x_illegal.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (int.Parse(location_x.text) > selected.getLength())
        {
            x_out.gameObject.SetActive(true);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (!data.xy.IsMatch(location_x.text))
        {
            x_illegal.gameObject.SetActive(true);
            xy_existed.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        x_illegal.gameObject.SetActive(false);
        x_out.gameObject.SetActive(false);
        if (function.XyCheck(selected, int.Parse(location_x.text), int.Parse(location_y.text)))
            xy_existed.gameObject.SetActive(true);
        else
            xy_existed.gameObject.SetActive(false);
    }

    void YCheck()
    {
        if (location_y.text == "")
        {
            y_illegal.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (int.Parse(location_y.text) > selected.getLength())
        {
            y_out.gameObject.SetActive(true);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (!data.xy.IsMatch(location_y.text))
        {
            y_illegal.gameObject.SetActive(true);
            xy_existed.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        y_illegal.gameObject.SetActive(false);
        y_out.gameObject.SetActive(false);
        if (function.XyCheck(selected,int.Parse(location_x.text), int.Parse(location_y.text)))
            xy_existed.gameObject.SetActive(true);
        else
            xy_existed.gameObject.SetActive(false);
    }

    void CreateSensorOnClick()
    {
        foreach (InputField e in required)
            function.RequiredInputOnEndEdit(e);
        foreach (Text e in warning)
            if (e.IsActive())
                return;
        if (function.InputFieldRequired(required))
        {
            string requestType;
            if (temperature.isOn)
                requestType = "/saveTemperatureSensor";
            else
                requestType = "/saveWetnessSensor";
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + requestType), HTTPMethods.Post, (req, res) => {
                switch (req.State)
                {
                    case HTTPRequestStates.Finished:
                        Debug.Log(res.DataAsText);
                        GameObject.Find("Canvas/cover").SetActive(false);
                        GameObject.Find("Canvas/sensor_box").SetActive(false);
                        function.Clear(required,warning,pass);
                        function.FreshGarden(selected);
                        break;
                    default:
                        Debug.Log("Error!Status code:" + res.StatusCode);
                        break;
                }
            });
            request.AddHeader("Content-Type", "application/json");

            JsonData newSensor = new JsonData();
            newSensor["gardenId"] = selected.getId();
            newSensor["x"] = location_x.text;
            newSensor["y"] = location_y.text;
            newSensor["name"] =sensor_name.text;
            newSensor["valid"] = true;
            request.RawData = System.Text.Encoding.UTF8.GetBytes(newSensor.ToJson());

            request.Send();
        }
    }

    void TemperatureOnValueChanged()
    {
        if (!temperature.isOn && !humidity.isOn)
        {
            temperature.isOn = !temperature.isOn;
        }
        if (temperature.isOn && humidity.isOn)
        {
            humidity.isOn = false;
        }
    }

    void HumidityOnValueChanged()
    {
        if (!temperature.isOn && !humidity.isOn)
        {
            humidity.isOn = !humidity.isOn;
        }
        if (temperature.isOn && humidity.isOn)
        {
            temperature.isOn = false;
        }
    }
}
