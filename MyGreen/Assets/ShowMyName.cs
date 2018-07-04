using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ShowMyName : MonoBehaviour {

    Text m_Text;
    string name = "My name";

	// Use this for initialization
	void Start () {
        //Fetch the Text Component
        m_Text = GetComponent<Text>();
        //Switch the Text alignment to the middle
        m_Text.alignment = TextAnchor.MiddleCenter;
        //Text sets your text to say this message
        m_Text.text = name;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void show()
    {

    }
}
