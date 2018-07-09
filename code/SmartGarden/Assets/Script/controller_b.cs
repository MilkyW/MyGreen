using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller_b : MonoBehaviour {

    public InputField controller_name_c;
    public InputField controller_x_c;
    public InputField controller_y_c;
    public List<InputField> controllerbox_required;
    public Button controllerbox_submit;
    public Button controllerbox_cancel;
    public List<Text> warning;
    public Text xy_existed;
    public Text name_existed;
    public Text x_illegal;
    public Text y_illegal;
    public Text name_pass;
    public Text xy_pass;


    // Use this for initialization
    void Start () {
        warning.Add(name_existed);
        warning.Add(xy_existed);
        warning.Add(x_illegal);
        warning.Add(y_illegal);
        controllerbox_required.Add(controller_name_c);
        controllerbox_required.Add(controller_x_c);
        controllerbox_required.Add(controller_y_c);
        controllerbox_submit.onClick.AddListener(CreateControllerOnClick);
        controllerbox_cancel.onClick.AddListener(delegate { function.Clear(controllerbox_required, warning); });
        foreach (InputField e in controllerbox_required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        controller_x_c.onEndEdit.AddListener(delegate { XCheck(); });
        controller_y_c.onEndEdit.AddListener(delegate { YCheck(); });
        controller_name_c.onEndEdit.AddListener(delegate { NameCheck(); });
    }

    // Update is called once per frame
    void Update () {
		
	}

    void NameCheck()
    {
        if (function.ControllerNameCheck(controller_name_c.text))
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
        if (controller_x_c.text == "")
        {
            x_illegal.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (!data.xy.IsMatch(controller_x_c.text))
        {
            x_illegal.gameObject.SetActive(true);
            xy_existed.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        x_illegal.gameObject.SetActive(false);
        if (function.XyCheck(controller_x_c.text, controller_y_c.text))
            xy_existed.gameObject.SetActive(true);
        else
            xy_existed.gameObject.SetActive(false);
    }

    void YCheck()
    {
        if (controller_y_c.text == "")
        {
            y_illegal.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        if (!data.xy.IsMatch(controller_y_c.text))
        {
            y_illegal.gameObject.SetActive(true);
            xy_existed.gameObject.SetActive(false);
            xy_pass.gameObject.SetActive(false);
            return;
        }
        y_illegal.gameObject.SetActive(false);
        if (function.XyCheck(controller_x_c.text, controller_y_c.text))
            xy_existed.gameObject.SetActive(true);
        else
            xy_existed.gameObject.SetActive(false);
    }

    void CreateControllerOnClick()
    {
        foreach (InputField e in controllerbox_required)
            function.RequiredInputOnEndEdit(e);
        if (function.InputFieldRequired(controllerbox_required))
        {
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/controller_box").SetActive(false);
            function.Clear(controllerbox_required, warning);
        }
    }
}
