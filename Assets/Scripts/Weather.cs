using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Xml;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
//using Newtonsoft.Json;

public class Weather : MonoBehaviour
{
    public string ipcity = "";

    public string LocalIPAddress()
    {
        string localIP = "";
        string url = "http://checkip.dyndns.org";
        System.Net.WebRequest req = System.Net.WebRequest.Create(url);
        System.Net.WebResponse resp = req.GetResponse();
        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
        string response = sr.ReadToEnd().Trim();
        string[] a = response.Split(':');
        string a2 = a[1].Substring(1);
        string[] a3 = a2.Split('<');
        string a4 = a3[0];
        return a4;

    }


    void Start()
    {
        LocalIPAddress();
        StartCoroutine(GetWeather());
    }

    public string city;
    public string temp;
    public string humidy;
    public string clouds;
    public string Title;


    public IEnumerator GetWeather()
    {
        string a = LocalIPAddress();
        string url = "api.ipgeolocation.io/ipgeo?apiKey=63675d7f1aa14c7fbeda269fb00bee57&ip=" + a + "&fields=city";
        WWW www = new WWW(url);
        yield return www;
        ipcity = www.text;
        string[] arr = ipcity.Split(':');
        ipcity = arr[2];
        ipcity = ipcity.Trim(new Char[] { '\"', '}' });
        print("Loaded following: " + ipcity);

        string bruh = ipcity;
        print("BRUH:" + bruh);
        string qrl = "http://api.openweathermap.org/data/2.5/find?q=" + bruh + "&type=accurate&mode=xml&lang=nl&units=imperial&appid=4f258e4971d0c07f770b8bc4759fb32e";
        WWW qww = new WWW(qrl);
        yield return qww;
        print("Loaded following XML " + qww.text);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(qww.text);
        city = xmlDoc.SelectSingleNode("cities/list/item/city/@name").InnerText;
        temp = xmlDoc.SelectSingleNode("cities/list/item/temperature/@value").InnerText;
        humidy = xmlDoc.SelectSingleNode("cities/list/item/humidity /@value").InnerText;
        clouds = xmlDoc.SelectSingleNode("cities/list/item/clouds/@value").InnerText;
        Title = xmlDoc.SelectSingleNode("cities /list/item/weather/@value").InnerText;
        print("City: " + city);
        print("Temperature: " + temp);
        print("humidity: " + humidy);
        print("Cloud : " + clouds);
        print("Title: " + Title);

    }

    void Awake()
    {
        print("City YOLO: " + ipcity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
