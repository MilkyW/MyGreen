using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class sensor_b : MonoBehaviour {

    public Toggle temperature_c;
    public Toggle humidty_c;
    public InputField sensor_name_c;
    public InputField sensor_x_c;
    public InputField sensor_y_c;
    public List<InputField> sensorbox_required;
    public Button sensorbox_submit;
    public Button sensorbox_cancel;
    public List<Text> warning;
    public Text name_existed;
    public Text xy_existed;
    public Text x_illegal;
    public Text y_illegal;
    public Text name_pass;
    public Text xy_pass;
    public List<Text> pass;


    // Use this for initialization
    void Start () {
        sensorbox_required.Add(sensor_name_c);
        sensorbox_required.Add(sensor_x_c);
        sensorbox_required.Add(sensor_y_c);
        warning.Add(name_existed);
        warning.Add(xy_existed);
        warning.Add(x_illegal);
        warning.Add(y_illegal);
        pass.Add(name_pass);
        pass.Add(xy_pass);
        temperature_c.onValueChanged.AddListener(delegate { TemperatureOnValueChanged(); });
        humidty_c.onValueChanged.AddListener(delegate { HumidtyOnValueChanged(); });
        sensorbox_submit.onClick.AddListener(CreateSensorOnClick);
        sensorbox_cancel.onClick.AddListener(delegate { function.Clear(sensorbox_required, warning, pass); });
        foreach (InputField e in sensorbox_required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        sensor_name_c.onEndEdit.AddListener(delegate { NameCheck(); });
        sensor_x_c.onEndEdit.AddListener(delegate { XCheck(); });
        sensor_y_c.onEndEdit.AddListener(delegate { YCheck(); });
    }

    // Update is called once per frame
    void Update () {
		
	}

    void NameCheck()
    {
        if (function.SensorNameCheck(sensor_name_c.text))
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
        if (sensor_x_c.text == "")
        {
            x_illegal.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (!data.xy.IsMatch(sensor_x_c.text))
        {
            x_illegal.gameObject.SetActive(true);
            xy_existed.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        x_illegal.gameObject.SetActive(false);
        if (function.XyCheck(sensor_x_c.text, sensor_y_c.text))
            xy_existed.gameObject.SetActive(true);
        else
            xy_existed.gameObject.SetActive(false);
    }

    void YCheck()
    {
        if (sensor_y_c.text == "")
        {
            y_illegal.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (!data.xy.IsMatch(sensor_y_c.text))
        {
            y_illegal.gameObject.SetActive(true);
            xy_existed.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        y_illegal.gameObject.SetActive(false);
        if (function.XyCheck(sensor_x_c.text, sensor_y_c.text))
            xy_existed.gameObject.SetActive(true);
        else
            xy_existed.gameObject.SetActive(false);
    }

    void CreateSensorOnClick()
    {
        foreach (InputField e in sensorbox_required)
            function.RequiredInputOnEndEdit(e);
        if (function.InputFieldRequired(sensorbox_required))
        {
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/sensor_box").SetActive(false);
        }
    }

    void TemperatureOnValueChanged()
    {
        if (!temperature_c.isOn && !humidty_c.isOn)
        {
            temperature_c.isOn = !temperature_c.isOn;
        }
        if (temperature_c.isOn && humidty_c.isOn)
        {
            humidty_c.isOn = false;
        }
    }

    void HumidtyOnValueChanged()
    {
        if (!temperature_c.isOn && !humidty_c.isOn)
        {
            humidty_c.isOn = !humidty_c.isOn;
        }
        if (temperature_c.isOn && humidty_c.isOn)
        {
            temperature_c.isOn = false;
        }
    }
}
