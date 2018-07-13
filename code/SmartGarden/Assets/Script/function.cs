using BestHTTP;
using LitJson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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

    public static void SaveSensor()
    {

    }

    public static void SaveController()
    {

    }

    public static void FreshGarden(m_garden garden)
    {
        MapBG.clearAll();
        garden.cleanSensor();
        garden.cleanController();
        function.GetSensors(garden);
        function.GetControllers(garden);
        return;
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
                temp.setX((int)e["x"]);
                temp.setY((int)e["y"]);
                temp.setType(true);
                sensors.Add(temp);
                MapBG.drawOne(temp.getId(), temp.getName(), temp.getX(), temp.getY(), MapBG.SensorControllerType.Temperature, true);
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
                temp.setX((int)e["x"]);
                temp.setY((int)e["y"]);
                temp.setType(false);
                sensors.Add(temp);
                MapBG.drawOne(temp.getId(), temp.getName(), temp.getX(), temp.getY(), MapBG.SensorControllerType.Moisture, true);
            }
            garden.addSensor(sensors);
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
                temp.setX((int)e["x"]);
                temp.setY((int)e["y"]);
                temp.setState(true);
                controllers.Add(temp);
                MapBG.drawOne(temp.getId(), temp.getName(), temp.getX(), temp.getY(), MapBG.SensorControllerType.Irrigation, true);
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
}
