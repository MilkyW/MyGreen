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
    public List<Text> pass;
    public Text x_out;
    public Text y_out;


    // Use this for initialization
    void Start () {
        warning.Add(name_existed);
        warning.Add(xy_existed);
        warning.Add(x_illegal);
        warning.Add(y_illegal);
        warning.Add(x_out);
        warning.Add(y_out);
        required.Add(controller_name);
        required.Add(location_x);
        required.Add(location_y);
        pass.Add(name_pass);
        pass.Add(xy_pass);
        submit.onClick.AddListener(CreateControllerOnClick);
        cancel.onClick.AddListener(delegate { function.Clear(required, warning, pass); });
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        location_x.onEndEdit.AddListener(delegate { XCheck(); });
        location_y.onEndEdit.AddListener(delegate { YCheck(); });
        controller_name.onEndEdit.AddListener(delegate { NameCheck(); });
    }

    void OnEnable()
    {
        selected = function.FindSelected();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void NameCheck()
    {
        if (function.ControllerNameCheck(selected, controller_name.text))
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
        if (int.Parse(location_x.text)>selected.getLength())
        {
            x_out.gameObject.SetActive(true);
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
        x_out.gameObject.SetActive(false);
        if (function.XyCheck(selected, int.Parse(location_x.text), int.Parse(location_y.text)))
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
        if (int.Parse(location_y.text) > selected.getWidth())
        {
            y_out.gameObject.SetActive(true);
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
        y_out.gameObject.SetActive(false);
        if (function.XyCheck(selected, int.Parse(location_x.text), int.Parse(location_y.text)))
            xy_existed.gameObject.SetActive(true);
        else
            xy_existed.gameObject.SetActive(false);
    }

    void CreateControllerOnClick()
    {
        foreach (InputField e in required)
            function.RequiredInputOnEndEdit(e);
        foreach (Text e in warning)
            if (e.IsActive())
                return;
        if (function.InputFieldRequired(required))
        {
            HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/saveController"), HTTPMethods.Post, (req, res) => {
                switch (req.State)
                {
                    case HTTPRequestStates.Finished:
                        Debug.Log("Successfully save!");
                        GameObject.Find("Canvas/cover").SetActive(false);
                        GameObject.Find("Canvas/controller_box").SetActive(false);
                        function.Clear(required, warning, pass);
                        function.FreshGarden(selected);
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
