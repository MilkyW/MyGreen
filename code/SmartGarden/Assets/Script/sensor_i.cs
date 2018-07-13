using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sensor_i : MonoBehaviour {

    public static long id;
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
    public m_garden selected;

	// Use this for initialization
	void Start () {
        selected = data.m_user.getGardens()[GameObject.Find("Canvas/garden").GetComponent<Dropdown>().value];
        warning.Add(name_existed);
        required.Add(sensor_name);
        pass.Add(name_pass);
        delete.onClick.AddListener(DeleteOnClick);
        save.onClick.AddListener(SaveOnClick);
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        sensor_name.onEndEdit.AddListener(delegate { NameCheck(); });
        foreach (sensor e in selected.getSensors())
            if (e.getId() == id)
            {
                sensor_name.text = e.getName();
                location_x.text = "x:   " + e.getX();
                location_y.text = "y:   " + e.getY();
                if (e.getType())
                    temperature.isOn = true;
                else
                    humidty.isOn = true;
            }
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
            function.SaveSensor();
        }
    }
}
