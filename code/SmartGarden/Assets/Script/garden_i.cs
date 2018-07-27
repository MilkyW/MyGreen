using BestHTTP;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class garden_i : MonoBehaviour {

    public m_garden selected;
    public InputField garden_name;
    public InputField length;
    public InputField width;
    public InputField temperature;
    public InputField humidity;
    public Button save;
    public Button delete;
    public Text name_existed;
    public Text name_pass;
    public List<InputField> required;
    public List<Text> warning;
    public List<Text> pass;


	// Use this for initialization
	void Start () {
        required.Add(garden_name);
        required.Add(length);
        required.Add(width);
        required.Add(temperature);
        required.Add(humidity);
        warning.Add(name_existed);
        pass.Add(name_pass);
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        save.onClick.AddListener(SaveOnClick);
	}

    void OnEnable()
    {
        selected = function.FindSelected();
        garden_name.text = selected.getName();
        length.text = selected.getLength().ToString();
        width.text = selected.getWidth().ToString();
        temperature.text = selected.getIdealTemperature().ToString();
        humidity.text = selected.getIdealHumidity().ToString();
    }

    void OnDisable()
    {
        function.Clear(required, warning, pass);
        function.FreshGarden(selected);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void NameCheck()
    {
        if (function.GardenNameCheck(garden_name.text))
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
            if (garden_name.text != selected.getName() || temperature.text != selected.getIdealTemperature().ToString() || humidity.text != selected.getIdealHumidity().ToString() || length.text != selected.getLength().ToString() || width.text != selected.getWidth().ToString())
            {
                HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/updateGarden"), HTTPMethods.Post, (req, res) =>
                {
                    switch (req.State)
                    {
                        case HTTPRequestStates.Finished:
                            Debug.Log(res.DataAsText);
                            function.FreshGardens(selected);
                            break;
                        default:
                            Debug.Log("Error!Status code:" + res.StatusCode);
                            break;
                    }
                });
                request.AddHeader("Content-Type", "application/json");

                JsonData newGarden = new JsonData();
                newGarden["id"] = selected.getId().ToString();
                newGarden["userId"] = data.m_user.getId().ToString();
                newGarden["width"] = width.text;
                newGarden["length"] = length.text;
                newGarden["name"] = garden_name.text;
                newGarden["idealTemperature"] = temperature.text;
                newGarden["idealWetness"] = humidity.text;
                request.RawData = System.Text.Encoding.UTF8.GetBytes(newGarden.ToJson());

                request.Send();
            }
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/garden_info").SetActive(false);
        }
    }
}
