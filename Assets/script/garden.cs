using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class garden : MonoBehaviour {
    public Button map;

	// Use this for initialization
	void Start () {
        map.onClick.AddListener(MapOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
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
