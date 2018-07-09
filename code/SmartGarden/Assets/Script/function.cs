using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class function {

    public static void ClearUser()
    {
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

    public static void Clear(List<InputField> input,List<Text> warning)
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
    }

    public static bool SensorNameCheck(string name)
    {
        return true;
    }

    public static bool ControllerNameCheck(string name)
    {
        return true;
    }

    public static bool XyCheck(string x,string y)
    {
        return true;
    }

    public static void SaveSensor()
    {

    }
}
