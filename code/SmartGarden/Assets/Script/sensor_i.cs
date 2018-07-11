using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sensor_i : MonoBehaviour {

    public InputField sensor_name;
    public List<InputField> required;
    public List<Text> warning;
    public Text name_existed;
    public Button delete;
    public Text name_pass;
    public Button save;

	// Use this for initialization
	void Start () {
        warning.Add(name_existed);
        required.Add(sensor_name);
        delete.onClick.AddListener(DeleteOnClick);
        save.onClick.AddListener(SaveOnClick);
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        sensor_name.onEndEdit.AddListener(delegate { NameCheck(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void NameCheck()
    {
        if (function.SensorNameCheck(sensor_name.text))
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

    void DeleteOnClick()
    {
        function.Clear(required, warning);
    }

    void SaveOnClick()
    {
        foreach (InputField e in required)
            function.RequiredInputOnEndEdit(e);
        foreach (Text e in warning)
            if (e.IsActive())
                return;
        if (function.InputFieldRequired(required))
        {
            GameObject.Find("Canvas/cover").SetActive(false);
            GameObject.Find("Canvas/sensor_info").SetActive(false);
            function.Clear(required, warning);
            function.SaveSensor();
        }
    }
}
