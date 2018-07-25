using BestHTTP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class message_b : MonoBehaviour {

    public Button tologin;
    public Button resend;
    public static long id;

    // Use this for initialization
    void Start () {
        tologin.onClick.AddListener(ToLoginOnClick);
        resend.onClick.AddListener(ReSendOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ToLoginOnClick()
    {
        SceneManager.LoadScene("log");
    }

    void ReSendOnClick()
    {
        HTTPRequest request_getSensorData1 = new HTTPRequest(new Uri(data.IP + "/resendEmail?id=" + id), HTTPMethods.Get, (req_data1, res_data1) => {
            Debug.Log(res_data1.DataAsText);
        }).Send();
    }
}
