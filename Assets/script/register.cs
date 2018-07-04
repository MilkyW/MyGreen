using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class register : MonoBehaviour {
    public Button submit;
    public Button login;

    // Use this for initialization
    void Start () {
        submit.onClick.AddListener(SubmitOnClick);
        login.onClick.AddListener(LoginOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SubmitOnClick()
    {
        SceneManager.LoadScene("garden");
    }

    void LoginOnClick()
    {
        SceneManager.LoadScene("log");
    }
}
