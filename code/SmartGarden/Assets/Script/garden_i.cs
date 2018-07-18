using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class garden_i : MonoBehaviour {

    public static m_garden selected;
    public InputField garden_name;
    public InputField length;
    public InputField width;
    public InputField temperature;
    public InputField humidty;
    public Button save;
    public Button delete;
    public Text name_existed;
    public Text name_pass;
    public List<InputField> required;
    public List<Text> warning;
    public List<Text> pass;


	// Use this for initialization
	void Start () {
        required.Add(garden_name);
        required.Add(length);
        required.Add(width);
        required.Add(temperature);
        required.Add(humidty);
        warning.Add(name_existed);
        pass.Add(name_pass);
        foreach (InputField e in required)
            e.onEndEdit.AddListener(delegate { function.RequiredInputOnEndEdit(e); });
        save.onClick.AddListener(SaveOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void NameCheck()
    {
        if (function.GardenNameCheck(garden_name.text))
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
        function.Clear(required, warning, pass);
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
            GameObject.Find("Canvas/garden_info").SetActive(false);
            function.Clear(required, warning, pass);
            //function.FreshGarden(selected);
        }
    }
}
