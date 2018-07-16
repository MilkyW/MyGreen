using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text), typeof(Button), typeof(Image))]
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
    private bool normal;

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
        normal = true;
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

    public void trySet()
    {
        setCurrent(1.0f, 2.8f, 3.4f);
        return;
    }

    public void setCurrent(float now, float max, float min)
    {
        bool next;
        current = now;
        if (now > max || now < min)
        {
            next = false;
            if (normal != next)
            {
                normal = next;
                GameObject nextButObj = null;
                Image image = GetComponentInChildren<Image>();
                Button button = GetComponent<Button>();
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
