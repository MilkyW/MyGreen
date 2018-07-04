using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class info : MonoBehaviour {
    public Dropdown view;
    public Text username;

	// Use this for initialization
	void Start () {
        username.text = log.uname;
	}
	
	// Update is called once per frame
	void Update () {
		if (view.value == 1)
        {
            SceneManager.LoadScene("garden");
        }
        else if (view.value == 3)
        {
            SceneManager.LoadScene("log");
        }
	}
}
