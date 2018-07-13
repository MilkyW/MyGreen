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
        return;
    }

    public void setType(MapBG.SensorControllerType tp)
    {
        type = tp;
        return;
    }

    public void onClick()
    {
        GameObject.Find("Canvas").transform.Find("cover").gameObject.SetActive(true);
        if (type == MapBG.SensorControllerType.Irrigation)
        {
            Debug.Log(id);
            controller_i.id = id;
            GameObject.Find("Canvas").transform.Find("controller_info").gameObject.SetActive(true);
        }
        else if (type == MapBG.SensorControllerType.Moisture || type == MapBG.SensorControllerType.Temperature)
        {
            sensor_i.id = id;
            GameObject.Find("Canvas").transform.Find("sensor_info").gameObject.SetActive(true);
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
