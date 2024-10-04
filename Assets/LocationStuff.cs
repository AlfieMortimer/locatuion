using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LocationStuff : MonoBehaviour
{

    [SerializeField]
    private char unit = 'K';

    public TextMeshProUGUI debugTxt;
    public bool gps_ok = false;
    float PI = Mathf.PI;

    GPSLoc startLoc = new GPSLoc();
    GPSLoc currLoc = new GPSLoc();

    bool measureDistance = false;


    private void Start()
    {
        StartCoroutine(startLocal());
    }
    // Start is called before the first frame update
    IEnumerator startLocal()
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
        {
            debugTxt.text = "Location not enabled on device or app does not have permission to access location";
        }
        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            debugTxt.text += "\nTimed Out";
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            debugTxt.text += ("\nUnable to determine device location");

            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            debugTxt.text
               = "\nLocation: \nLat: " + Input.location.lastData.latitude
                + " \nLon: " + Input.location.lastData.longitude
                + " \nAlt: " + Input.location.lastData.altitude
                + " \nH_Acc: " + Input.location.lastData.horizontalAccuracy
                + " \nTime: " + Input.location.lastData.timestamp
                + " \nstatus: " + Input.location.status

                + "\n aba";

            gps_ok = true;

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (gps_ok)
        {
            debugTxt.text = "GPS:...";

            debugTxt.text
               = "\nLocation: \nLat: " + Input.location.lastData.latitude
                + " \nLon: " + Input.location.lastData.longitude
                + " \nAlt: " + Input.location.lastData.altitude
                + " \nH_Acc: " + Input.location.lastData.horizontalAccuracy
                + " \nTime: " + Input.location.lastData.timestamp
                + " \nstatus: " + Input.location.status

                +"\n aaannosex";

            currLoc.lat = Input.location.lastData.latitude;
            currLoc.lon = Input.location.lastData.longitude;


            debugTxt.text += "\nStored: " + startLoc.getLocData();

            
        }
    }

    public void StopGPS()
    {
        Input.location.Stop();

    }

    public void StoreCurrentGPS()
    {
        startLoc = new GPSLoc(currLoc.lon, currLoc.lat);
        measureDistance = true;
    }

}


public class GPSLoc
{
    public float lon;
    public float lat;

    public GPSLoc()
    {
        lon = 0;
        lat = 0;
    }
    public GPSLoc(float lon, float lat)
    {
        this.lon = lon;
        this.lat = lat;
    }

    public string getLocData()
    {
        return "Lat: " + lat + " \nLon: " + lon;
    }
}
