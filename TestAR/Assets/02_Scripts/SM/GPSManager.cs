using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GPSManager : MonoBehaviour
{
    public Text text;
    public GameObject popUp;
    public bool isFirst = false;

    public double lat = 37.715137;
    public double lon = 126.742504;
    IEnumerator Start()
    {
        while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            yield return null;
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start(10, 1);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            yield break;
        }
        else
        {
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            while (true)
            {
                yield return null;
                text.text = Input.location.lastData.latitude + "/" + Input.location.lastData.longitude;
            }
        }
    }

    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            double myLat = Input.location.lastData.latitude;
            double myLong = Input.location.lastData.longitude;

            double remainDistance = distance(myLat, myLong, lat, lon);

            if (remainDistance <= 5f)
            {
                if (!isFirst)
                {
                    isFirst = true;
                    popUp.SetActive(true);
                }
            }
        }
    }

    // ��ǥ�� �Ÿ� ��� ����(�Ϲ����� ����)
    private double distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;

            double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

            dist = Math.Acos(dist);

            dist = Rad2Deg(dist);

            dist = dist * 60 * 1.1515;

            dist = dist * 1609.344; // ���� ��ȯ

            return dist;
        }

        private double Deg2Rad(double deg)
        {
            return (deg * Mathf.PI / 180.0f);
        }

        private double Rad2Deg(double rad)
        {
            return (rad * 180.0f / Mathf.PI);
        }
    }

