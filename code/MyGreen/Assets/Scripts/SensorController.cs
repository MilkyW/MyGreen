using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SensorController : MonoBehaviour {

    private bool showName;
    private MapBG.SensorControllerType type;
    private long id;
    private string name;
    private bool valid;
    private GameObject textObj;
    private Text text;

	// Use this for initialization
	void Start () {
        showName = false;
        if (name == null)
            name = "My Name";
        textObj = this.gameObject.transform.GetChild(1).gameObject;
        textObj.SetActive(false);
        text = textObj.GetComponent<Text>();
        text.text = name;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void destroy()
    {
        Destroy(gameObject);
    }

    public void setID(long i)
    {
        id = i;
        return;
    }

    public void setName(string n)
    {
        name = n;
        return;
    }

    public void setValid(bool v)
    {
        valid = v;
        Button but = GetComponent<Button>();
        if (valid == true)
        {
            but.interactable = true;
        }
        else
        {
            but.interactable = false;
        }
        return;
    }

    public void setType(MapBG.SensorControllerType tp)
    {
        type = tp;
        return;
    }

    public void onClick()
    {

    }

    public void selected()
    {
        showName = true;
        textObj.SetActive(true);
    }

    public void deselected()
    {
        showName = false;
        textObj.SetActive(false);
    }

    public void pointerEnter()
    {
        textObj.SetActive(true);
    }

    public void pointerExit()
    {
        if (!showName)
            textObj.SetActive(false);
    }
}
