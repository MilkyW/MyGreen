using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBG : MonoBehaviour
{
    public enum SensorControllerType
    {
        Temperature,
        Moisture,
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

    public void letUsDraw()
    {
        drawOne(12345, "ME", 111, 112, SensorControllerType.Temperature, true);
    }

    public void drawOne(long id, string name, int x, int y, SensorControllerType type, bool valid)
    {
        Object sensorcontrollerPreb = null;
        GameObject sensorcontroller;
        GameObject container = GameObject.Find("Map");

        switch (type)
        {
            case SensorControllerType.Temperature:
                sensorcontrollerPreb = Resources.Load("Sensors/TemperatureSensor", typeof(GameObject));
                break;

            case SensorControllerType.Moisture:
                sensorcontrollerPreb = Resources.Load("Sensors/MoistureSensor", typeof(GameObject));
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
        sc.transform.SetParent(container.transform);
        RectTransform rt = sensorcontroller.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(x, y);
        rt.localScale = new Vector3(1, 1, 1);
    }
}
