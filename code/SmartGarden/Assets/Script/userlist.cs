using BestHTTP;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class userlist : MonoBehaviour
{
    List<user> list = new List<user>();
    public GameObject item;
    GameObject parent;
    Vector3 itemLocalPos;
    float itemHeight;
    Vector2 contentSize;
    public Button logout;

    // Use this for initialization
    void Start()
    {
        logout.onClick.AddListener(LogoutOnClick);
        parent = GameObject.Find("Content");
        itemHeight = item.GetComponent<RectTransform>().rect.height;
        itemLocalPos = item.transform.localPosition;
        contentSize = parent.GetComponent<RectTransform>().sizeDelta;
        HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/getAllUser"), HTTPMethods.Get, (req, res) => {
            Debug.Log(res.DataAsText);
            JArray array = JArray.Parse(res.DataAsText);
            foreach (var e in array)
            {
                user temp = new user();
                temp.setId((long)e["id"]);
                temp.setUsername((string)e["username"]);
                temp.setNickname((string)e["nickname"]);
                temp.setPassword((string)e["password"]);
                temp.setFirstname((string)e["firstname"]);
                temp.setLastname((string)e["lastname"]);
                temp.setPhone((string)e["phone"]);
                temp.setGender((bool)e["gender"]);
                temp.setEmail((string)e["email"]);
                temp.setEnable((bool)e["enabled"]);
                list.Add(temp);
                AddItem(temp.getUsername(), temp.getEnabled());
            }
        }).Send();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(string username, bool enabled)
    {
        GameObject a = Instantiate(item) as GameObject;
        a.GetComponent<Transform>().SetParent(parent.GetComponent<Transform>(), false);
        a.transform.localPosition = new Vector2(itemLocalPos.x, itemLocalPos.y - list.Count * itemHeight);
        a.transform.Find("name").GetComponent<Text>().text = username;
        a.transform.Find("state").GetComponent<Text>().text = enabled.ToString();
        a.transform.Find("switch").GetComponent<Button>().onClick.AddListener(delegate { SwitchOnClick(a); });

        if (contentSize.y <= list.Count * itemHeight)
        {
            parent.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.x, list.Count * itemHeight);
        }
    }

    public void SwitchOnClick(GameObject a)
    {
        user selected = list[0];
        foreach (user e in list)
            if (e.getUsername() == a.transform.Find("name").GetComponent<Text>().text)
                selected = e;
        HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/updateUserEnabledById?id=" + selected.getId() + "&enabled=" + !selected.getEnabled()), HTTPMethods.Get, (req, res) =>
        {
            Debug.Log(res.DataAsText);
        }).Send();
    }

    public void LogoutOnClick()
    {
        HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/logout"), HTTPMethods.Post, (req, res) => {
            switch (req.State)
            {
                case HTTPRequestStates.Finished:
                    function.ClearUser();
                    SceneManager.LoadScene("log");
                    break;
                default:
                    Debug.Log("Error!Status code:" + res.StatusCode);
                    break;
            }
        }).Send();
    }
}
