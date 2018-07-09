using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class message_b : MonoBehaviour {

    public Button tologin;
    public Button resend;

    // Use this for initialization
    void Start () {
        tologin.onClick.AddListener(ToLoginOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ToLoginOnClick()
    {
        SceneManager.LoadScene("log");
    }
}
