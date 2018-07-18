using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

using BestHTTP;
using BestHTTP.WebSocket;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ChartAndGraph;

public class CGTest : MonoBehaviour {

	public Slider slider;
	WebSocket webSocket;

	public GraphChart graph;
	GraphAnimation animation;

	double minTime;
	double maxTime;
	[SerializeField]
	private double gap = 20;

	// Unity function
	void Awake () {
        init();
	}

    void OnEnable()
    {
        drawLineChart();
    }

    void OnDisable()
    {
        close();
    }

    void Update () {
		
	}

	// Interface
	public void drawLineChart() {
        Debug.Log("draw line chart");//debug

		/* Get latest data */
		getTemperatureByGardenId ();

		/* Open websocket */
		console ("try to connect...");
		webSocket.Open();
	}

    public void test()
    {
        HTTPRequest request = new HTTPRequest(new Uri(data.IP + "/login?account=dennis&password=" + function.EncryptWithMD5("123456")), HTTPMethods.Post, (req, res) =>
        {
            Debug.Log(res.DataAsText);

            drawLineChart();
        }).Send();
    }

	public void close() {
		webSocket.Close ();
	}


	private void getTemperatureByGardenId() {
		HTTPRequest requeset = new HTTPRequest (new Uri (data.IP+"/getRecentTemperatureDataById?id="+sensor_i.show.getId()+"&num=10"), HTTPMethods.Get, (req, res) => {
            /* Handle and insert data */
            Debug.Log(res.DataAsText);
            JArray array = JArray.Parse(res.DataAsText);
			graph.DataSource.StartBatch();
			graph.DataSource.ClearCategory("Temperature");
			foreach (var obj in array) {
                float temperature = (float)obj["temperature"];
                DateTime time = Convert.ToDateTime(obj["time"]);
                graph.DataSource.AddPointToCategory("Temperature", time, temperature);
            }
			graph.DataSource.EndBatch();
			
			/* Set time */
			minTime = (Convert.ToDateTime(array.Last["time"]) - new DateTime(1970, 1, 1)).TotalSeconds;
			maxTime = (Convert.ToDateTime(array.First["time"]) - new DateTime(1970, 1, 1)).TotalSeconds - gap;

			/* Avtivate slider */
			slider.interactable = true;
		}).Send ();
	}

	public void onSliderValueChanged(float val) {
		if (val == 1) {
			graph.AutoScrollHorizontally = true;
			return;
		}

		if (graph.AutoScrollHorizontally) {
			graph.AutoScrollHorizontally = false;
		}
		graph.HorizontalScrolling = (maxTime-minTime)*val+minTime;
	}

	// Websocket
	private void init()
	{
        Debug.Log("web socket init");

		webSocket = new WebSocket(new Uri("ws://192.168.1.87:8080/getSingleLatestTemperature"));
		webSocket.OnOpen += OnOpen;
		webSocket.OnMessage += OnMessageReceived;
		webSocket.OnError += OnError;
		webSocket.OnClosed += OnClosed;
	}

	private void antiInit()
	{
		webSocket.OnOpen = null;
		webSocket.OnMessage = null;
		webSocket.OnError = null;
		webSocket.OnClosed = null;
		webSocket = null;
	}
		void OnOpen(WebSocket ws)
	{
		console ("connected");
        webSocket.Send(sensor_i.show.getId().ToString());
	}

	void OnMessageReceived(WebSocket ws, string message)
	{
		console("received:"+message);

		JObject data = (JObject)JsonConvert.DeserializeObject(message);
        float temperature = float.Parse(data["temperature"].ToString());
        DateTime time = Convert.ToDateTime(data["time"].ToString());

        graph.DataSource.AddPointToCategoryRealtime("Temperature", time, temperature);

        maxTime = (time - new DateTime(1970, 1, 1)).TotalSeconds - gap;
    }

	void OnClosed(WebSocket ws, UInt16 code, string message)
	{
		console ("closing...");
		console (message);
		antiInit();
		init ();
	}

	private void OnDestroy()
	{
		if(webSocket!=null && webSocket.IsOpen)
		{
			webSocket.Close();
			antiInit();
		}
	}

	/// <summary>
	/// Called when an error occured on client side
	/// </summary>
	void OnError(WebSocket ws, Exception ex)
	{
		string errorMsg = string.Empty;
		#if !UNITY_WEBGL || UNITY_EDITOR
		if (ws.InternalRequest.Response != null)
			errorMsg = string.Format ("Status Code from Server: {0} and Message: {1}", ws.InternalRequest.Response.StatusCode, ws.InternalRequest.Response.Message);
		#endif
		console (errorMsg);
		antiInit ();
		init ();
	}


	// Other
	private void console(String msg) {
		Debug.Log("[WebSocketTest]" + msg);
	}
		
	private byte[] getBytes(string message)
	{

		byte[] buffer = System.Text.Encoding.Default.GetBytes(message);
		return buffer;
	}
}
