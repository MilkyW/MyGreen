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
    public List<InputField> sensorinfo_required;
    public List<InputField> controllerinfo_required;
    public Button control;


    // Use this for initialization
    void Start()
    {
        nickname.text = data.m_user.getNickname();
        map.onClick.AddListener(MapOnClick);
        view.onValueChanged.AddListener(delegate { ViewChange(); });
        gardens.onValueChanged.AddListener(delegate { function.FreshGarden(data.m_user.getGardens()[gardens.value]); });
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
        control.onClick.AddListener(ControlOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ControlOnClick()
    {
        GameObject.Find("Canvas").transform.Find("garden_info").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("cover").gameObject.SetActive(true);
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
