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
    public Toggle humidty;
    public InputField sensor_name;
    public List<InputField> required;
    public List<Text> warning;
    public Text name_existed;
    public Button delete;
    public Text name_pass;
    public Button save;
    public List<Text> pass;
    public static m_garden selected;
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

    void DeleteOnClick()
    {
        function.Clear(required, warning, pass);
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
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/sensor_info").SetActive(false);
            function.Clear(required, warning, pass);
            function.FreshGarden(selected);
            temperature.isOn = false;
            humidty.isOn = false;
            /*if (sensor_name.text != show.getName())
            {
                HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/updateSensor"), HTTPMethods.Post, (req, res) =>
                {
                    switch (req.State)
                    {
                        case HTTPRequestStates.Finished:
                            Debug.Log(res.DataAsText);
                            GameObject.Find("Canvas/cover").SetActive(false);
                            GameObject.Find("Canvas/sensor_info").SetActive(false);
                            function.Clear(required, warning, pass);
                            function.FreshGarden(selected);
                            temperature.isOn = false;
                            humidty.isOn = false;
                            break;
                        default:
                            Debug.Log("Error!Status code:" + res.StatusCode);
                            break;
                    }
                });
                request.AddHeader("Content-Type", "application/json");

                JsonData newSensor = new JsonData();
                newSensor["gardenId"] = selected.getId();
                newSensor["x"] = show.getX();
                newSensor["y"] = show.getY();
                newSensor["name"] = sensor_name.text;
                newSensor["valid"] = true;
                request.RawData = System.Text.Encoding.UTF8.GetBytes(newSensor.ToJson());

                request.Send();
            }*/
        }
    }
}
