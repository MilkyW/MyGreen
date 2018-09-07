﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class data : MonoBehaviour {

    //WebGL
    //public static string IP = "http://localhost:8080";
    //public static string wsIP = "ws://localhost:8080";

    //Android: 3101
    //public static string IP = "http://192.168.1.87:8080";
    //public static string wsIP = "ws://192.168.1.87:8080";

    //Android: Home
    public static string IP = "http://192.168.3.22:8080";
    public static string wsIP = "ws://192.168.3.22:8080";

    public static user m_user = new user();

    public static Regex xy = new Regex("(^[0-9]*[1-9][0-9]*$)|(^([0-9]{1,}[.][0-9]*)$)");

    public static Regex email = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");

    public static Regex phone = new Regex("^13|15|18[0-9]{9}$");

    public static int width = 500;

    public static int length = 750;
}
