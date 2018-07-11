using BestHTTP;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class garden_b : MonoBehaviour {

    public Button submit;
    public Button cancel;
    public InputField garden_name;
    public InputField length;
    public InputField width;
    public InputField temperature;
    public InputField humidty;
    public List<InputField> required;
    public List<Text> warning;
    public Text name_pass;
    public Text name_existed;

    // Use this for initialization
    void Start () {
        required.Add(garden_name);
        required.Add(length);
        required.Add(width);
        required.Add(temperature);
        required.Add(humidty);
        warning.Add(name_existed);
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        garden_name.onEndEdit.AddListener(delegate { NameCheck(); });
        submit.onClick.AddListener(SubmitOnClick);
        cancel.onClick.AddListener(delegate { function.Clear(required, warning); });
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

    void SubmitOnClick()
    {
        foreach (InputField e in required)
            function.RequiredInputOnEndEdit(e);
        foreach (Text e in warning)
            if (e.IsActive())
                return;
        if (function.InputFieldRequired(required))
        {
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/garden_box").SetActive(false);
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/saveGarden"), HTTPMethods.Post, (req, res) => {
                switch (req.State)
                {
                    case HTTPRequestStates.Finished:
                        Debug.Log("Successfully save!");
                        GameObject.Find("Canvas/cover").SetActive(false);
                        GameObject.Find("Canvas/garden_box").SetActive(false);
                        m_garden temp = new m_garden();
                        temp.setId(long.Parse(res.DataAsText));
                        temp.setName(garden_name.text);
                        temp.setLength(int.Parse(length.text));
                        temp.setWidth(int.Parse(width.text));
                        temp.setIdealTemperature(float.Parse(temperature.text));
                        temp.setIdealHumidty(float.Parse(humidty.text));
                        data.m_user.addGardens(temp);
                        Dropdown.OptionData newOption = new Dropdown.OptionData();
                        newOption.text = garden_name.text;
                        List<Dropdown.OptionData> newOptions = new List<Dropdown.OptionData>();
                        newOptions.Add(newOption);
                        GameObject.Find("Canvas/garden").GetComponent<Dropdown>().AddOptions(newOptions);
                        break;
                    default:
                        Debug.Log("Error!Status code:" + res.StatusCode);
                        break;
                }
            });
            request.AddHeader("Content-Type", "application/json");

            JsonData newGarden = new JsonData();
            newGarden["userId"] = data.m_user.getId();
            newGarden["width"] = width.text;
            newGarden["length"] = length.text;
            newGarden["name"] = garden_name.text;
            newGarden["idealTemperature"] = temperature.text;
            newGarden["ideaWetness"] = humidty.text;
            request.RawData = System.Text.Encoding.UTF8.GetBytes(newGarden.ToJson());

            request.Send();

        }
    }
}
