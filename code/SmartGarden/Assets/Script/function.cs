using BestHTTP;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class function {

    public static void ClearUser()
    {
        data.m_user.setId(0);
        data.m_user.setUsername("");
        data.m_user.setPassword("");
        data.m_user.setFirstname("");
        data.m_user.setLastname("");
        data.m_user.setNickname("");
        data.m_user.setPhone("");
        data.m_user.setEmail("");
        data.m_user.setGender(false);
        data.m_user.getGardens().Clear();
    }

    public static void RequiredInputOnEndEdit(InputField input)
    {
        if (input.text == "")
        {
            input.placeholder.GetComponent<Text>().text = "Required";
            input.placeholder.color = Color.red;
        }
    }

    public static bool InputFieldRequired(List<InputField> inputFields)
    {
        foreach (InputField e in inputFields)
            if (e.text == "")
                return false;
        return true;
    }

    public static bool InputEqualCheck(InputField input1,InputField input2)
    {
        if (input1.text != input2.text)
            return false;
        return true;
    }

    public static void Clear(List<InputField> input,List<Text> warning,List<Text> pass)
    {
        foreach (InputField e in input)
        {
            e.text = "";
            e.placeholder.GetComponent<Text>().text = "Enter text...";
            e.placeholder.color = Color.gray;
        }
        foreach (Text e in warning)
        {
            e.gameObject.SetActive(false);
        }
        foreach(Text e in pass)
        {
            e.gameObject.SetActive(false);
        }
    }

    public static bool SensorNameCheck(m_garden garden, string name)
    {
        foreach (sensor e in garden.getSensors())
            if (e.getName() == name)
                return true;
        return false;
    }

    public static bool ControllerNameCheck(m_garden garden, string name)
    {
        foreach (controller e in garden.getControllers())
            if (e.getName() == name)
                return true;
        return false;
    }

    public static bool GardenNameCheck(string name)
    {
        foreach (m_garden e in data.m_user.getGardens())
            if (e.getName() == name)
                return true;
        return false;
    }

    public static bool XyCheck(m_garden garden, int x, int y)
    {
        foreach (sensor e in garden.getSensors())
            if (e.getX() == x && e.getY() == y)
                return true;
        foreach (controller e in garden.getControllers())
            if (e.getX() == x && e.getY() == y)
                return true;
        return false;
    }

    public static void FreshGarden(m_garden garden)
    {
        Debug.Log(garden.getName());
        MapBG.clearAll();
        GameObject.Find("Canvas/map").GetComponent<Dropdown>().value = 0;
        GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapT").gameObject.SetActive(false);
        GameObject.Find("HeatCanvas").transform.Find("painting/Scroll View/map/background/HeatMapH").gameObject.SetActive(false);
        garden.cleanSensor();
        garden.cleanController();
        GetSensors(garden);
        GetControllers(garden);
        return;
    }

    public static void FreshGardens(m_garden garden)
    {
        data.m_user.clearGardens();
        HTTPRequest request_getGarden = new HTTPRequest(new Uri(data.IP + "/getGardenByUserId?userId=" + data.m_user.getId()), HTTPMethods.Get, (req_garden, res_garden) => {
            Debug.Log(res_garden.DataAsText);
            JArray array = JArray.Parse(res_garden.DataAsText);
            foreach (var e in array)
            {
                m_garden newGarden = new m_garden();
                newGarden.setId((long)e["id"]);
                newGarden.setName((string)e["name"]);
                newGarden.setLength((int)e["length"]);
                newGarden.setWidth((int)e["width"]);
                newGarden.setIdealTemperature((float)e["idealTemperature"]);
                newGarden.setIdealHumidity((float)e["idealWetness"]);
                Debug.Log(newGarden.getName());
                data.m_user.addGardens(newGarden);
            }
            FreshGarden(garden);
        }).Send();
    }

    public static void GetSensors(m_garden garden)
    {
        List<sensor> sensors = new List<sensor>();
        HTTPRequest request_getSensor1 = new HTTPRequest(new Uri(data.IP + "/getTemperatureSensorByGardenId?gardenId=" + garden.getId()), HTTPMethods.Get, (req_sensor1, res_sensor1) => {
            Debug.Log(res_sensor1.DataAsText);
            JArray array = JArray.Parse(res_sensor1.DataAsText);
            foreach (var e in array)
            {
                sensor temp = new sensor();
                temp.setId((long)e["id"]);
                temp.setName((string)e["name"]);
                temp.setX((int)e["x"] * data.width / garden.getWidth());
                temp.setY((int)e["y"] * data.length / garden.getLength());
                temp.setType(true);
                sensors.Add(temp);
            }
            garden.addSensor(sensors);
        }).Send();
        HTTPRequest request_getSensor2 = new HTTPRequest(new Uri(data.IP + "/getWetnessSensorByGardenId?gardenId=" + garden.getId()), HTTPMethods.Get, (req_sensor2, res_sensor2) => {
            Debug.Log(res_sensor2.DataAsText);
            JArray array = JArray.Parse(res_sensor2.DataAsText);
            foreach (var e in array)
            {
                sensor temp = new sensor();
                temp.setId((long)e["id"]);
                temp.setName((string)e["name"]);
                temp.setX((int)e["x"] * data.width/garden.getWidth());
                temp.setY((int)e["y"] * data.length/garden.getLength());
                temp.setType(false);
                sensors.Add(temp);
            }
            garden.addSensor(sensors);
        }).Send();
        HTTPRequest request_getSensorData1 = new HTTPRequest(new Uri(data.IP + "/getLatestTemperatureByGardenId?gardenId=" + garden.getId()), HTTPMethods.Get, (req_data1, res_data1) => {
            Debug.Log(res_data1.DataAsText);
            JArray array = JArray.Parse(res_data1.DataAsText);
            foreach (var e in array)
                foreach (sensor n in garden.getSensors())
                    if (n.getId() == (long)e["id"] && n.getType())
                        n.setData((float)e["temperature"]);
            foreach (sensor e in garden.getSensors())
            {
                if (e.getType() && e.getData() != 0)
                    MapBG.drawOne(e.getId(), e.getName(), e.getX(), e.getY(), MapBG.SensorControllerType.Temperature, true, e.getData(), garden.getIdealTemperature() * (float)1.2, garden.getIdealTemperature() * (float)0.8);
                else if (e.getType() && e.getData() == 0)
                    MapBG.drawOne(e.getId(), e.getName(), e.getX(), e.getY(), MapBG.SensorControllerType.Temperature, false, garden.getIdealTemperature(), garden.getIdealTemperature() * (float)1.2, garden.getIdealTemperature() * (float)0.8);
            }
        }).Send();
        HTTPRequest request_getSensorData2 = new HTTPRequest(new Uri(data.IP + "/getLatestWetnessByGardenId?gardenId=" + garden.getId()), HTTPMethods.Get, (req_data2, res_data2) => {
            Debug.Log(res_data2.DataAsText);
            JArray array = JArray.Parse(res_data2.DataAsText);
            foreach (var e in array)
                foreach (sensor n in garden.getSensors())
                    if (n.getId() == (long)e["id"] && !n.getType())
                        n.setData((float)e["wetness"]);
            foreach (sensor e in garden.getSensors())
            {
                if (!e.getType() && e.getData() != 0)
                    MapBG.drawOne(e.getId(), e.getName(), e.getX(), e.getY(), MapBG.SensorControllerType.Humidity, true, e.getData(), garden.getIdealHumidity() * (float)1.2, garden.getIdealHumidity() * (float)0.8);
                else if (!e.getType() && e.getData() == 0)
                    MapBG.drawOne(e.getId(), e.getName(), e.getX(), e.getY(), MapBG.SensorControllerType.Humidity, false, garden.getIdealHumidity(), garden.getIdealHumidity() * (float)1.2, garden.getIdealHumidity() * (float)0.8);
            }
        }).Send();
        return;
    }

    public static void GetControllers(m_garden garden)
    {
        List<controller> controllers = new List<controller>();
        HTTPRequest request_getController = new HTTPRequest(new Uri(data.IP + "/getControllerByGardenId?gardenId=" + garden.getId()), HTTPMethods.Get, (req_controller, res_controller) => {
            Debug.Log(res_controller.DataAsText);
            JArray array = JArray.Parse(res_controller.DataAsText);
            foreach (var e in array)
            {
                controller temp = new controller();
                temp.setId((long)e["id"]);
                temp.setName((string)e["name"]);
                temp.setX((int)e["x"] * data.width / garden.getWidth());
                temp.setY((int)e["y"] * data.length / garden.getLength());
                temp.setState((bool)e["valid"]);
                controllers.Add(temp);
                MapBG.drawOne(temp.getId(), temp.getName(), temp.getX(), temp.getY(), MapBG.SensorControllerType.Irrigation, temp.getState(), 0, 0, 0);
            }
            garden.addController(controllers);
        }).Send();
        return;
    }

    public static string EncryptWithMD5(string source)
    {
        byte[] sor = Encoding.UTF8.GetBytes(source);
        MD5 md5 = MD5.Create();
        byte[] result = md5.ComputeHash(sor);
        StringBuilder strbul = new StringBuilder(40);
        for (int i = 0; i < result.Length; i++)
        {
            strbul.Append(result[i].ToString("x2"));
        }
        return strbul.ToString();
    }

    public static m_garden FindSelected()
    {
        return data.m_user.getGardens()[GameObject.Find("Canvas/gardens").GetComponent<Dropdown>().value];
    }
}
