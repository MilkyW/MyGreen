using BestHTTP;
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

    public static bool SensorNameCheck(string name)
    {
        return true;
    }

    public static bool ControllerNameCheck(string name)
    {
        return true;
    }

    public static bool GardenNameCheck(string name)
    {
        return false;
    }

    public static bool XyCheck(string x,string y)
    {
        return true;
    }

    public static void SaveSensor()
    {

    }

    public static void SaveController()
    {

    }

    public static List<sensor> GetSensors(long gardenId)
    {
        List<sensor> sensors = new List<sensor>();
        HTTPRequest request_getSensor1 = new HTTPRequest(new Uri(data.IP + "/getTemperatureSensorByGardenId?gardenId=" + gardenId), HTTPMethods.Get, (req_sensor1, res_sensor1) => {
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
            }
        }).Send();
        HTTPRequest request_getSensor2 = new HTTPRequest(new Uri(data.IP + "/getWetnessSensorByGardenId?gardenId=" + gardenId), HTTPMethods.Get, (req_sensor2, res_sensor2) => {
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
            }
        }).Send();
        while (request_getSensor1.State != HTTPRequestStates.Finished || request_getSensor2.State != HTTPRequestStates.Finished) ;
        return sensors;
    }

    public static List<controller> GetControllers(long gardenId)
    {
        bool done = false;
        List<controller> controllers = new List<controller>();
        HTTPRequest request_getController = new HTTPRequest(new Uri(data.IP + "/getControllerByGardenId?gardenId=" + gardenId), HTTPMethods.Get, (req_controller, res_controller) => {
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
            }
            done = true;
        }).Send();
        while (!done) { Debug.Log(done); };
        Debug.Log(done);
        return controllers;
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
