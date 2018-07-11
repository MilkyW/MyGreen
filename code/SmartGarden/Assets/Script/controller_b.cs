using BestHTTP;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller_b : MonoBehaviour {

    public InputField controller_name;
    public InputField location_x;
    public InputField location_y;
    public List<InputField> required;
    public Button submit;
    public Button cancel;
    public List<Text> warning;
    public Text xy_existed;
    public Text name_existed;
    public Text x_illegal;
    public Text y_illegal;
    public Text name_pass;
    public Text xy_pass;
    public m_garden selected;


    // Use this for initialization
    void Start () {
        selected = data.m_user.getGardens()[garden.gardens.value];
        warning.Add(name_existed);
        warning.Add(xy_existed);
        warning.Add(x_illegal);
        warning.Add(y_illegal);
        required.Add(controller_name);
        required.Add(location_x);
        required.Add(location_y);
        submit.onClick.AddListener(CreateControllerOnClick);
        cancel.onClick.AddListener(delegate { function.Clear(required, warning); });
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        location_x.onEndEdit.AddListener(delegate { XCheck(); });
        location_y.onEndEdit.AddListener(delegate { YCheck(); });
        controller_name.onEndEdit.AddListener(delegate { NameCheck(); });
    }

    // Update is called once per frame
    void Update () {
		
	}

    void NameCheck()
    {
        if (function.ControllerNameCheck(controller_name.text))
        {
            name_existed.gameObject.SetActive(true);
            name_pass.gameObject.SetActive(false);
        }
        else
        {
            name_existed.gameObject.SetActive(false);
            name_pass.gameObject.SetActive(true);
        }
    }

    void XCheck()
    {
        if (location_x.text == "")
        {
            x_illegal.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (!data.xy.IsMatch(location_x.text))
        {
            x_illegal.gameObject.SetActive(true);
            xy_existed.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        x_illegal.gameObject.SetActive(false);
        if (function.XyCheck(location_x.text, location_y.text))
            xy_existed.gameObject.SetActive(true);
        else
            xy_existed.gameObject.SetActive(false);
    }

    void YCheck()
    {
        if (location_y.text == "")
        {
            y_illegal.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (!data.xy.IsMatch(location_y.text))
        {
            y_illegal.gameObject.SetActive(true);
            xy_existed.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        y_illegal.gameObject.SetActive(false);
        if (function.XyCheck(location_x.text, location_y.text))
            xy_existed.gameObject.SetActive(true);
        else
            xy_existed.gameObject.SetActive(false);
    }

    void CreateControllerOnClick()
    {
        foreach (InputField e in required)
            function.RequiredInputOnEndEdit(e);
        if (function.InputFieldRequired(required))
        {
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/controller_box").SetActive(false);
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/saveController"), HTTPMethods.Post, (req, res) => {
                switch (req.State)
                {
                    case HTTPRequestStates.Finished:
                        Debug.Log("Successfully save!");
                        GameObject.Find("Canvas/cover").SetActive(false);
                        GameObject.Find("Canvas/garden_box").SetActive(false);
                        controller temp = new controller();
                        temp.setId(long.Parse(res.DataAsText));
                        temp.setX(int.Parse(location_x.text));
                        temp.setY(int.Parse(location_y.text));
                        temp.setState(true);
                        temp.setName(controller_name.text);
                        selected.addController(temp);
                        break;
                    default:
                        Debug.Log("Error!Status code:" + res.StatusCode);
                        break;
                }
            });
            request.AddHeader("Content-Type", "application/json");

            JsonData newController = new JsonData();
            newController["gardenId"] = selected.getId();
            newController["x"] = location_x.text;
            newController["y"] = location_y.text;
            newController["name"] = controller_name.text;
            newController["valid"] = true;
            request.RawData = System.Text.Encoding.UTF8.GetBytes(newController.ToJson());

            request.Send();
        }
    }
}
