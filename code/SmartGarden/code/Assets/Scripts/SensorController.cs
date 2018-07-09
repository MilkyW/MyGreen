using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode()]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Text))]
public class SensorController : MonoBehaviour
{

    //private Button button;
    private GameObject textObj;
    private int[] position = { 200, 300 };
    private string type = "temperature";
    private string name = "My name";
    private bool pressed = false;

    // Use this for initialization
    void Start()
    {

    }

    protected virtual void OnSkinUI()
    {
        //button = GetComponent<Button>();
        //if (type == "temperature")
        //{
        //    button = Resources.Load<Button>("Sensors\\TemperatureSensor") as Button;
        //}
        //else if (type == "humidity")
        //{
        //    button = Resources.Load<Button>("Sensors\\MoistureSensor") as Button;
        //}
        //else if (type == "irrigation")
        //{
        //    button = Resources.Load<Button>("Controllers\\IrrigationController") as Button;
        //}
        //button = GetComponent<Button>();
        Text m_Text;
        textObj = gameObject.transform.GetChild(1).gameObject;
        if (textObj != null)
        {
            m_Text = textObj.GetComponent<Text>();
            //Switch the Text alignment to the middle
            m_Text.alignment = TextAnchor.MiddleCenter;
            //Text sets your text to say this message
            m_Text.text = name;
        }

        //GameObject mytext = Resources.Load("PointText") as GameObject;
        //GameObject prefabInstance = Instantiate(mytext);
        //prefabInstance.transform.parent = container.transform;

        //Fetch the Text Component
        //m_Text = GetComponent<Text>();
    }

    public virtual void Awake()
    {
        OnSkinUI();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Application.isEditor)
        {
            //OnSkinUI();
        }
    }

    public void showName()
    {
        textObj = gameObject.transform.GetChild(1).gameObject;
        if (textObj != null)
        {
            textObj.SetActive(true);
        }
    }

    public void hideName()
    {
        textObj = gameObject.transform.GetChild(1).gameObject;
        if (textObj != null && pressed == false)
        {
            textObj.SetActive(false);
        }
    }

    public void keepName()
    {
        pressed = true;
    }

    public void noKeepName()
    {
        pressed = false;
    }
}
