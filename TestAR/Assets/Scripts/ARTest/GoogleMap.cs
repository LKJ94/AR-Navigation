using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Globalization;
using System;

public class GoogleMap : MonoBehaviour
{
    public string apiKey; ///< ������� Google API Key
    public float lat = 0.0f; ///< ���� �ʱⰪ 
    public float lng = 0.0f; ///< �浵 �ʱⰪ 
    public int zoom = 14; ///< ���� Zoom �ʱⰪ

    public enum resloution { low = 0, high = 2 } ///< ���� �ػ� enum Ÿ��
    public resloution mapResolution = resloution.low; ///< �ػ� �ʱⰪ

    public enum type { roadmap, satellite, hybrid, terrain }; ///< ���� ���� enum Ÿ��
    public type mapType = type.roadmap; ///< ���� �ʱ� ���� roadmap 

    private string url = string.Empty; ///< URL �ʱ�ȭ
    private int mapWidth = 640; ///< ���� �ʺ�
    private int mapHeight = 640; ///< ���� ����
    private bool mapIsLoading = false; ///< ���� �ε� ����
    private Rect rect; ///< Raw Image �� Rectransform ���� ����
    private string apiKeyLast; ///< ���������� ����� API Key ����

    private float latLast = 33.85660f; ///< ���������� ����� ���� ����
    private float lonLast = 151.21500f; ///< ���������� ����� �浵 ����
    private int zoomLast = 14; ///< ���������� ����� ���� Zoom ����
    private resloution mapResolutionLast = resloution.low; ///< ���������� ����� �ػ� �ʱⰪ ����
    private type mapTypeLast = type.roadmap; ///< ���������� ����� ���� ���� ����
    private bool updateMap = true; ///< ���� ������Ʈ �ʿ伺 (������ �������� �� ���ο� ���� �̹��� ��û �� �ε��� ��Ȳ)

    private void Start()
    {
        /* ������ �� GetGoogleMap �ڷ�ƾ ���� */
        StartCoroutine(GetGoogleMap());
        /* Raw Image ������Ʈ ũ�� �������� */
        rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
        /* rect �� �ʺ�, ���̸� mapWidth, mapHeight �� �Ҵ� */
        mapWidth = (int)Mathf.Round(rect.width);
        mapHeight = (int)Mathf.Round(rect.height);

        /* Test */
        StartCoroutine(GetDirections("37.5665, 126.97801", "37.5656, 126.97511"));
    }

    private void Update()
    {
        /* ������ ����� ������ ���ο� ���� �ҷ��� */
        if (updateMap && (apiKeyLast != apiKey || !Mathf.Approximately(latLast, lat) || !Mathf.Approximately(lonLast, lng) ||
                          zoomLast != zoom || mapResolutionLast != mapResolution || mapTypeLast != mapType))
        {
            rect = gameObject.GetComponent<RawImage>().rectTransform.rect;
            mapWidth = (int)Mathf.Round(rect.width);
            mapHeight = (int)Mathf.Round(rect.height);

            StartCoroutine(GetGoogleMap());
            /* ���ο� ������ �ҷ������� ������Ʈ �ʿ伺 X */
            updateMap = false;
        }
    }

    /**
     * @brief Google Maps Static API URL ���� �ϰ� �̹����� �ҷ��ͼ� ����Ƽ�� ǥ��
     */
    private IEnumerator GetGoogleMap()
    {
        //url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lng +
        //                                                              "&zoom=" + zoom +
        //                                                              "&size=" + mapWidth +
        //                                                              "x" + mapHeight +
        //                                                              "&scale=" + mapResolution +
        //                                                              "&maptype=" + mapType +
        //                                                              "&key=" + apiKey;
        ///* �ε� ���� */
        //mapIsLoading = true;
        ///* url �� �ִ� �ؽ�ó�� �������� ���� ��û */
        //UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        ///* ��û ��� */
        //yield return www.SendWebRequest();

        ///* ���� */
        //if (www.result != UnityWebRequest.Result.Success)
        //{
        //    Debug.LogError("www error" + www.error);
        //    Debug.LogError(url);
        //}
        //else
        //{
        //    /* ���� */
        //    mapIsLoading = false;
        //    /* Raw Image �� �ٿ�ε� ��û�� �ؽ�ó�� ���� */
        //    gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

        //    apiKeyLast = apiKey;
        //    latLast = lat;
        //    lonLast = lng;
        //    zoomLast = zoom;
        //    mapResolutionLast = mapResolution;
        //    mapTypeLast = mapType;
        //    updateMap = true;
        //}

        /* �ڷ�ƾ ����  */
        yield break;
    }

    private IEnumerator GetDirections(string startLocation, string endLocation)
    {

        /*
         * driving: ���� ����Ͽ� �Ÿ��� ���
         * walking: ������ ��ο� ������ ����Ͽ� �Ÿ��� ���
         * bicycling: ������ ��θ� ����Ͽ� �Ÿ��� ���
         * transit: ���߱����� ����Ͽ� �Ÿ��� ���
         */

        string url = "https://maps.googleapis.com/maps/api/directions/json?" +
            $"origin={startLocation}" +
            $"&destination={endLocation}" +
            $"&region=KR" +
            $"&mode=transit" +
            $"&key={apiKey}";

        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error :" + webRequest.error);
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);

            //Root r = JsonConvert.DeserializeObject<Root>(webRequest.downloadHandler.text);
        }
    }
}

