using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class garden : MonoBehaviour {
    public Button map;
    public Text nickname;
    public Dropdown view;
    public Button information;
    public Button chart;
    public Toggle temperature_c;
    public Toggle humidty_c;
    public InputField sensor_name_c;
    public InputField controller_name_c;
    public InputField sensor_x_c;
    public InputField sensor_y_c;
    public InputField controller_x_c;
    public InputField controller_y_c;
    public List<InputField> sensorbox_required;
    public List<InputField> controllerbox_required;
    public List<Text> warning;
    public Button sensorbox_submit;
    public Button controllerbox_submit;
    public Button sensorbox_cancel;
    public Button controllerbox_cancel;
    public List<InputField> sensorinfo_required;
    public List<InputField> controllerinfo_required;


    // Use this for initialization
    void Start () {
        sensorbox_required.Add(sensor_name_c);
        sensorbox_required.Add(sensor_x_c);
        sensorbox_required.Add(sensor_y_c);
        controllerbox_required.Add(controller_name_c);
        controllerbox_required.Add(controller_x_c);
        controllerbox_required.Add(controller_y_c);
        map.onClick.AddListener(MapOnClick);
        temperature_c.onValueChanged.AddListener(delegate { TemperatureOnValueChanged(); });
        humidty_c.onValueChanged.AddListener(delegate { HumidtyOnValueChanged(); });
        sensorbox_submit.onClick.AddListener(CreateSensorOnClick);
        controllerbox_submit.onClick.AddListener(CreateControllerOnClick);
        sensorbox_cancel.onClick.AddListener(delegate { function.Clear(sensorbox_required); });
        controllerbox_cancel.onClick.AddListener(delegate { function.Clear(controllerbox_required); });
        foreach (InputField e in sensorbox_required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        foreach (InputField e in controllerbox_required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
    }

    // Update is called once per frame
    void Update()
    {
        if (information.enabled)
        {
            information.GetComponent<Image>().color = Color.gray;
            chart.GetComponent<Image>().color = Color.white;
        }
        else
        {
            information.GetComponent<Image>().color = Color.white;
            chart.GetComponent<Image>().color = Color.gray;
        }
        if (view.value == 1)
        {
            SceneManager.LoadScene("information");
        }
        else if (view.value == 3)
        {
            SceneManager.LoadScene("log");
        }
    }

    void CreateSensorOnClick()
    {
        foreach (InputField e in sensorbox_required)
            function.RequiredInputOnEndEdit(e);
        if (function.InputFieldRequired(sensorbox_required))
        {
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/sensor_box").SetActive(false);
            function.Clear(sensorbox_required);
        }
    }

    void CreateControllerOnClick()
    {
        foreach (InputField e in controllerbox_required)
            function.RequiredInputOnEndEdit(e);
        if (function.InputFieldRequired(controllerbox_required))
        {
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/controller_box").SetActive(false);
            function.Clear(controllerbox_required);
        }
    }

    void MapOnClick()
    {
        if (GameObject.Find("map/Text").GetComponent<Text>().text.ToString() == "Map")
        {
            GameObject.Find("map/Text").GetComponent<Text>().text = "Temperature";
        }
        else if (GameObject.Find("map/Text").GetComponent<Text>().text.ToString() == "Temperature")
        {
            GameObject.Find("map/Text").GetComponent<Text>().text = "Map";
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
