﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBG : MonoBehaviour
{
    public enum SensorControllerType
    {
        Temperature,
        Humidity,
        Irrigation
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void clearAll()
    {
        for (int i = 1; i < GameObject.Find("Canvas/painting/Scroll View/map").transform.childCount; i++)
            Destroy(GameObject.Find("Canvas/painting/Scroll View/map").transform.GetChild(i).gameObject);
    }

    public static void drawOne(long id, string name, int x, int y, SensorControllerType type, bool valid, float now, float max, float min)
    {
        Object sensorcontrollerPreb = null;
        Debug.Log(name);
        GameObject sensorcontroller;
        GameObject container = GameObject.Find("Canvas/painting/Scroll View/map");

        switch (type)
        {
            case SensorControllerType.Temperature:
                sensorcontrollerPreb = Resources.Load("Sensors/TemperatureSensor", typeof(GameObject));
                break;

            case SensorControllerType.Humidity:
                sensorcontrollerPreb = Resources.Load("Sensors/HumiditySensor", typeof(GameObject));
                break;

            case SensorControllerType.Irrigation:
                sensorcontrollerPreb = Resources.Load("Controllers/IrrigationController", typeof(GameObject));
                break;

            default:
                return;
        }

        if (sensorcontrollerPreb == null)
            return;
        sensorcontroller = Instantiate(sensorcontrollerPreb) as GameObject;
        SensorController sc = sensorcontroller.GetComponent<SensorController>();
        sc.setID(id);
        sc.setName(name);
        sc.setValid(valid);
        sc.setType(type);
        sc.setCurrent(now, max, min);
        sc.transform.SetParent(container.transform);
        RectTransform rt = sensorcontroller.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
        rt.localScale = new Vector3((float)0.5, (float)0.5, 1);
    }
}
