using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SensorController : MonoBehaviour
{

    private bool showName;
    private MapBG.SensorControllerType type;
    private long id;
    private string name;
    private bool valid;
    private GameObject textObj;
    private Text text;

    // Use this for initialization
    void Start()
    {
        showName = false;
        if (name == null)
            name = "My Name";
        textObj = this.gameObject.transform.GetChild(1).gameObject;
        textObj.SetActive(false);
        text = textObj.GetComponent<Text>();
        text.text = name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    public void setID(long i)
    {
        id = i;
        return;
    }

    public void setName(string n)
    {
        name = n;
        return;
    }

    public void setValid(bool v)
    {
        valid = v;
        Button but = GetComponent<Button>();
        if (valid == true)
        {
            but.interactable = true;
        }
        else
        {
            but.interactable = false;
        }
        return;
    }

    public void setType(MapBG.SensorControllerType tp)
    {
        type = tp;
        return;
    }

    public void onClick()
    {
        if (type == MapBG.SensorControllerType.Irrigation)
        {
            foreach (controller e in data.m_user.getGardens()[GameObject.Find("Canvas/garden").GetComponent<Dropdown>().value].getControllers())
                if (e.getId() == id)
                {
                    controller_i.selected = data.m_user.getGardens()[GameObject.Find("Canvas/garden").GetComponent<Dropdown>().value];
                    controller_i.show = e;
                    GameObject.Find("Canvas").transform.Find("cover").gameObject.SetActive(true);
                    GameObject.Find("Canvas").transform.Find("controller_info").gameObject.SetActive(true);
                    GameObject.Find("Canvas/controller_info/info/input_name").GetComponent<InputField>().text = e.getName();
                    GameObject.Find("Canvas/controller_info/info/label_x").GetComponent<Text>().text = "x:   " + e.getX();
                    GameObject.Find("Canvas/controller_info/info/label_y").GetComponent<Text>().text = "y:   " + e.getY();
                    GameObject.Find("Canvas/controller_info/info/switch").GetComponent<Toggle>().isOn = e.getState();
                }
        }
        else if (type == MapBG.SensorControllerType.Moisture || type == MapBG.SensorControllerType.Temperature)
        {
            foreach (sensor e in data.m_user.getGardens()[GameObject.Find("Canvas/garden").GetComponent<Dropdown>().value].getSensors())
                if (e.getId() == id)
                {
                    sensor_i.selected = data.m_user.getGardens()[GameObject.Find("Canvas/garden").GetComponent<Dropdown>().value];
                    sensor_i.show = e;
                    GameObject.Find("Canvas").transform.Find("sensor_info").gameObject.SetActive(true);
                    GameObject.Find("Canvas").transform.Find("cover").gameObject.SetActive(true);
                    GameObject.Find("Canvas/sensor_info/info/input_name").GetComponent<InputField>().text = e.getName();
                    GameObject.Find("Canvas/sensor_info/info/label_x").GetComponent<Text>().text = "x:   " + e.getX();
                    GameObject.Find("Canvas/sensor_info/info/label_y").GetComponent<Text>().text = "y:   " + e.getY();
                    if (type == MapBG.SensorControllerType.Temperature)
                    {
                        GameObject.Find("Canvas/sensor_info/info/temperature").GetComponent<Toggle>().isOn = true;
                        GameObject.Find("Canvas/sensor_info/info/humidty").GetComponent<Toggle>().isOn = false;
                    }
                    else
                    {
                        GameObject.Find("Canvas/sensor_info/info/humidty").GetComponent<Toggle>().isOn = true;
                        GameObject.Find("Canvas/sensor_info/info/temperature").GetComponent<Toggle>().isOn = false;
                    }
                }
        }
    }

    public void selected()
    {
        showName = true;
        textObj.SetActive(true);
    }

    public void deselected()
    {
        showName = false;
        textObj.SetActive(false);
    }

    public void pointerEnter()
    {
        textObj.SetActive(true);
    }

    public void pointerExit()
    {
        if (!showName)
            textObj.SetActive(false);
    }
}
