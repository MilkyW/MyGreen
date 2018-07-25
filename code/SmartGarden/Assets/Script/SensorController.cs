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
    private float current;
    private bool normal = true;

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

    public void setCurrent(float now, float max, float min)
    {
        bool next;
        current = now;
        if (now > max || now < min)
        {
            Debug.Log(name);
            next = false;
            if (normal != next)
            {
                normal = next;
                GameObject nextButObj = null;
                Image image = GetComponentInChildren<Image>();
                Button button = GetComponent<Button>();
                Debug.Log(type);
                switch (type)
                {
                    case MapBG.SensorControllerType.Temperature:
                        nextButObj = GameObject.Find("Temperature-error");
                        break;

                    case MapBG.SensorControllerType.Humidity:
                        nextButObj = GameObject.Find("Humidity-error");
                        break;

                    case MapBG.SensorControllerType.Irrigation:
                        nextButObj = GameObject.Find("Irrigation-error");
                        break;

                    default:
                        break;
                }
                image.sprite = nextButObj.GetComponent<Image>().sprite;
                button.spriteState = nextButObj.GetComponent<Button>().spriteState;
            }
        }
        else
        {
            next = true;
            if (normal != next)
            {
                normal = next;
                GameObject nextButObj = null;
                Image image = GetComponentInChildren<Image>();
                Button button = GetComponent<Button>();
                Debug.Log(type);
                switch (type)
                {
                    case MapBG.SensorControllerType.Temperature:
                        nextButObj = GameObject.Find("Temperature-normal");
                        break;

                    case MapBG.SensorControllerType.Humidity:
                        nextButObj = GameObject.Find("Humidity-normal");
                        break;

                    case MapBG.SensorControllerType.Irrigation:
                        nextButObj = GameObject.Find("Irrigation-normal");
                        break;

                    default:
                        break;
                }
                image.sprite = nextButObj.GetComponent<Image>().sprite;
                button.spriteState = nextButObj.GetComponent<Button>().spriteState;
            }
        }
        return;
    }

    public void onClick()
    {
        if (type == MapBG.SensorControllerType.Irrigation)
        {
            foreach (controller e in function.FindSelected().getControllers())
                if (e.getId() == id)
                {
                    //controller_i.selected = data.m_user.getGardens()[GameObject.Find("Canvas/garden").GetComponent<Dropdown>().value];
                    controller_i.show = e;
                    GameObject.Find("Canvas").transform.Find("cover").gameObject.SetActive(true);
                    GameObject.Find("Canvas").transform.Find("controller_info").gameObject.SetActive(true);
                }
        }
        else if (type == MapBG.SensorControllerType.Temperature)
        {
            foreach (sensor e in function.FindSelected().getSensors())
                if (e.getId() == id && e.getType())
                {
                    sensor_i.show = e;
                    GameObject.Find("Canvas").transform.Find("sensor_info").gameObject.SetActive(true);
                    GameObject.Find("Canvas").transform.Find("cover").gameObject.SetActive(true);
                }
        }
        else
        {
            foreach (sensor e in function.FindSelected().getSensors())
                if (e.getId() == id && !e.getType())
                {
                    sensor_i.show = e;
                    GameObject.Find("Canvas").transform.Find("sensor_info").gameObject.SetActive(true);
                    GameObject.Find("Canvas").transform.Find("cover").gameObject.SetActive(true);
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
