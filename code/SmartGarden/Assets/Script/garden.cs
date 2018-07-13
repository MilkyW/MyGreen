using BestHTTP;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class garden : MonoBehaviour {
    public Button map;
    public Text nickname;
    public Dropdown view;
    public Dropdown gardens;
    public Button information;
    public Button chart;
    public List<InputField> sensorinfo_required;
    public List<InputField> controllerinfo_required;
    public Button sensor;
    public Button controller;


    // Use this for initialization
    void Start()
    {
        nickname.text = data.m_user.getNickname();
        map.onClick.AddListener(MapOnClick);
        view.onValueChanged.AddListener(delegate { ViewChange(); });
        gardens.onValueChanged.AddListener(delegate { function.FreshGarden(data.m_user.getGardens()[gardens.value]); });
        sensor.onClick.AddListener(SensorOnClick);
        controller.onClick.AddListener(ControllerOnClick);
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (m_garden e in data.m_user.getGardens())
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = e.getName();
            options.Add(option);
        }
        gardens.AddOptions(options);
        Debug.Log(data.m_user.getGardens().Count);
        if (data.m_user.getGardens().Count != 0)
        {
            function.GetSensors(data.m_user.getGardens()[0]);
            function.GetControllers(data.m_user.getGardens()[0]);
        }
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
    }

    void SensorOnClick()
    {
        if (gardens.options.Count == 0)
        {
            GameObject.Find("Canvas/sensor_box").SetActive(false);
            GameObject.Find("Canvas").transform.Find("message_box").gameObject.SetActive(true);
        }
    }

    void ControllerOnClick()
    {
        if (gardens.options.Count == 0)
        {
            GameObject.Find("Canvas/controller_box").SetActive(false);
            GameObject.Find("Canvas").transform.Find("message_box").gameObject.SetActive(true);
        }
    }

    void ViewChange()
    {
        if (view.value == 1) // information
        {
            SceneManager.LoadScene("information");
        }
        else if (view.value == 3) //log out
        {
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/logout"), HTTPMethods.Post, (req, res) => {
                switch (req.State)
                {
                    case HTTPRequestStates.Finished:
                        function.ClearUser();
                        SceneManager.LoadScene("log");
                        break;
                    default:
                        Debug.Log("Error!Status code:" + res.StatusCode);
                        break;
                }
            }).Send();

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
}
