using BestHTTP;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class garden : MonoBehaviour {
    public Dropdown map;
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
        map.onValueChanged.AddListener(delegate { MapChange(); });
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

    void MapChange() { 
        if (map.value == 0)
        {
            GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapT").gameObject.SetActive(false);
            GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapH").gameObject.SetActive(false);
            function.FreshGarden(data.m_user.getGardens()[gardens.value]);
        }
        else if (map.value == 1)
        {        
            GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapT").gameObject.SetActive(true);
            GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapH").gameObject.SetActive(false);
            GameObject containerT = GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapT").gameObject;
            SpringMesh.HeatMapT heatmapT = containerT.GetComponent<SpringMesh.HeatMapT>();
            heatmapT.sswitch();
        }
        else
        {
            GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapT").gameObject.SetActive(false);
            GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapH").gameObject.SetActive(true);
            GameObject containerH = GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapH").gameObject;
            SpringMesh.HeatMapH heatmapH = containerH.GetComponent<SpringMesh.HeatMapH>();
            heatmapH.sswitch();
        }
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
        else if (view.value == 2) //log out
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
}
