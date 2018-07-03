using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class register : MonoBehaviour {
    public Button submit;

    // Use this for initialization
    void Start () {
        submit.onClick.AddListener(SubmitOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SubmitOnClick()
    {
        SceneManager.LoadScene("garden");
    }
}
