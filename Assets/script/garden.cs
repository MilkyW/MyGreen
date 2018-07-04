using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class garden : MonoBehaviour {
    public Button map;
    public Text nickname;
    public Dropdown view;
    public Button information;
    public Button chart;


    // Use this for initialization
    void Start () {
        map.onClick.AddListener(MapOnClick);
	}
	
	// Update is called once per frame
	void Update () {
        if (information.enabled)
        {
            information.GetComponent<Image>().color=Color.gray;
            chart.GetComponent<Image>().color = Color.white;
        }
        else
        {
            information.GetComponent<Image>().color = Color.white;
            chart.GetComponent<Image>().color = Color.gray;
        }
        if (view.value == 1)
        {
            SceneManager.LoadScene("information");
        }
		else if (view.value == 3)
        {
            SceneManager.LoadScene("log");
        }
	}

    void MapOnClick()
    {
        if (GameObject.Find("map/Text").GetComponent<Text>().text.ToString() == "Map")
        {
            GameObject.Find("map/Text").GetComponent<Text>().text = "Temperature";
        }
        else if (GameObject.Find("map/Text").GetComponent<Text>().text.ToString() == "Temperature")
        {
            GameObject.Find("map/Text").GetComponent<Text>().text = "Map";
        }
    }
}
