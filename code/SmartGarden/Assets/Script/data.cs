﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class data : MonoBehaviour {

    public static string IP = "http://192.168.1.87:8080";

    public static user m_user = new user();

    public static Regex xy = new Regex("(^[0-9]*[1-9][0-9]*$)|(^([0-9]{1,}[.][0-9]*)$)");

    public static Regex email = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");

    public static Regex phone = new Regex("^13|15|18[0-9]{9}$");
}
