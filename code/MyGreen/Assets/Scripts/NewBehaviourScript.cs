using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    SpriteRenderer m_SpriteRenderer;

    // Use this for initialization
    void Start () {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.white;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void ChangeColor()
    {
        if (m_SpriteRenderer.color == Color.white)
        {
            m_SpriteRenderer.color = Color.red;
        }

        else if (m_SpriteRenderer.color == Color.red)
        {
            m_SpriteRenderer.color = Color.white;
        }
    }
}
