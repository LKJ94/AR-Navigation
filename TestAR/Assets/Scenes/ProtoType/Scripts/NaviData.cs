using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.Android;
using System;

#region Autocomplete
/**
 * @brief PlaceAPI�� Autocomplete���� Json ������ Ŭ�������� ���� 
 */
[System.Serializable]
public class PlaceAutocompleteResponse
{
    public Prediction[] predictions;
    public string status;
}

[System.Serializable]
public class Prediction
{
    public string description;
    public string place_id;
    public MatchedSubstrings[] matched_substrings;
    public StructuredFormatting structured_formatting;
    public Term[] terms;
    public string[] types;
}

public class MatchedSubstrings
{
    public int length;
    public int offset;
}

[System.Serializable]
public class StructuredFormatting
{
    public string main_text;
    public string secondary_text;
}

[System.Serializable]
public class Term
{
    public int offset;
    public string value;
}
#endregion

#region PlaceDetails
/**
 * @brief PlaceAPI�� PlaceDetails���� Json ���ϵ��� Ŭ������ ���� 
 */
[System.Serializable]
public class PlaceDetailsResponse
{
    public List<object> html_attributions;
    public Result result;
    public string status;
}

[System.Serializable]
public class Result
{
    public Geometry geometry;
    public string formatted_address;
    public string name;
    public List<AddressComponent> address_components;
    public string business_status;
    public string formatted_phone_number;
    public double rating;
    public int user_ratings_total;
    public string website;
}

[System.Serializable]
public class Geometry
{
    public Location location;
}

[System.Serializable]
public class Location
{
    public double lat;
    public double lng;
}

[System.Serializable]
public class AddressComponent
{
    public string long_name;
    public string short_name;
    public List<string> types;
}
#endregion

#region DirectionAPI
/**
 * @brief DirectionAPI ���� Json ���ϵ��� Ŭ������ ����
 */
[System.Serializable]
public class DirectionsResponse
{
    public List<GeocodedWaypoint> geocoded_waypoints;
    public List<Route> routes;
    public string status;
}

[System.Serializable]
public class GeocodedWaypoint
{
    public string geocoder_status;
    public string place_id;
    public List<string> types;
}

[System.Serializable]
public class Route
{
    public Bounds bounds;
    public string copyrights;
    public List<Leg> legs;
    public OverviewPolyline overview_polyline;
    public string summary;
    public List<string> warnings;
    public List<object> waypoint_order;
}

[System.Serializable]
public class Bounds
{
    public Northeast northeast;
    public Southwest southwest;
}

[System.Serializable]
public class Northeast
{
    public double lat;
    public double lng;
}

[System.Serializable]
public class Southwest
{
    public double lat;
    public double lng;
}

[System.Serializable]
public class Leg
{
    public ArrivalTime arrival_time;
    public DepartureTime departure_time;
    public Distance distance;
    public Duration duration;
    public string end_address;
    public EndLocation end_location;
    public string start_address;
    public StartLocation start_location;
    public List<Step> steps;
    public List<object> traffic_speed_entry;
    public List<object> via_waypoint;
}

[System.Serializable]
public class ArrivalTime
{
    public string text;
    public string time_zone;
    public int value;
}

[System.Serializable]
public class DepartureTime
{
    public string text;
    public string time_zone;
    public int value;
}

[System.Serializable]
public class Distance
{
    public string text;
    public int value;
}

[System.Serializable]
public class Duration
{
    public string text;
    public int value;
}

[System.Serializable]
public class EndLocation
{
    public double lat;
    public double lng;
}

[System.Serializable]
public class StartLocation
{
    public double lat;
    public double lng;
}

[System.Serializable]
public class Step
{
    public Distance distance;
    public Duration duration;
    public EndLocation end_location;
    public string html_instructions;
    public Polyline polyline;
    public StartLocation start_location;
    //public List<Step> steps;
    public string travel_mode;
    public TransitDetails transit_details;
}

[System.Serializable]
public class Polyline
{
    public string points;
}

[System.Serializable]
public class TransitDetails
{
    public ArrivalStop arrival_stop;
    public ArrivalTime arrival_time;
    public DepartureStop departure_stop;
    public DepartureTime departure_time;
    public string headsign;
    public int headway;
    public Line line;
    public int num_stops;
}

[System.Serializable]
public class ArrivalStop
{
    public Location location;
    public string name;
}

[System.Serializable]
public class DepartureStop
{
    public Location location;
    public string name;
}

[System.Serializable]
public class Line
{
    public List<Agency> agencies;
    public string color;
    public string name;
    public string short_name;
    public string text_color;
    public Vehicle vehicle;
}

[System.Serializable]
public class Agency
{
    public string name;
    public string url;
}

[System.Serializable]
public class Vehicle
{
    public string icon;
    public string name;
    public string type;
}

[System.Serializable]
public class OverviewPolyline
{
    public string points;
}
#endregion

#region SearchHistoryData
/** 
 * @brief �˻���Ͽ� ���õ� ������ ���� */
[System.Serializable]
public class SearchHistoryData
{
    public List<SearchItem> searches = new List<SearchItem>();
}

[System.Serializable]
public class SearchItem
{
    public string searchText;
    public string placeId;
}
#endregion

#region Vector2d
/**
 * @brief double Ÿ�� �ʵ带 ���� Vector2�� ����ϱ� ���� Ŭ����
 * Vector�� ������ ����� ���� ������ �����ε� ���
 */
public class Vector2d
{
    public double x;
    public double y;

    public static readonly Vector2d zero = new Vector2d(0.0, 0.0);

    public Vector2d(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    /* ������ �����ε� */
    public static Vector2d operator * (Vector2d vector, double scalar)
    {
        return new Vector2d(vector.x * scalar, vector.y * scalar);
    }
    public static Vector2d operator + (Vector2d a, Vector2d b)
    {
        return new Vector2d(a.x + b.x, a.y + b.y);
    }

    public static Vector2d operator - (Vector2d a, Vector2d b)
    {
        return new Vector2d(a.x - b.x, a.y - b.y);
    }

    public Vector2d(Vector2 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
    }
}
#endregion

#region Configuration ApiKey, BaseUrl
/**
 * @brief URL �� APIKEY�� ��� ��ũ��Ʈ���� ������ �� �ְ� ���� 
 */
public static class Configuration
{
    public static string ApiKey => "AIzaSyAy-uxK5GceTZKIqh3kwlhlzzFn8wlZga4";
    public static string BaseUrl => "https://maps.googleapis.com/maps/api/staticmap?";
}
#endregion

/**
 * @brief AR Navigation �� �ʿ��� ������ �� Json �Ľ�
 */
public class NaviData : MonoBehaviour
{
    #region Variables
    public static NaviData Instance { get; private set; }  ///< NaviData.cs �̱��� ����

    public string PlaceId { get; private set; } ///< Place_ID
    public string addressComponentsDescription { get; private set; } ///< �ּ�������Ʈ�� string���� ��ȯ�� ����

    [Header("Location")]
    private bool _waitingForLocationService = false;  ///< ��ġ���� ��� ����
    [Header("Camera")]
    private bool _waitingForCameraService = false; ///< ī�޶� ��� ����

    /* �˻���� */
    private SearchHistoryData _searchHistoryData = new SearchHistoryData(); ///< �˻���� ������ �ʱ�ȭ

    public SearchHistoryData searchHistoryData ///< private ���� �����ϰ� ����
    {
        get { return _searchHistoryData; }
        set { _searchHistoryData = value; }
    }
    private string filePath; ///< ���� ���

    /* ���� */
    public PlaceAutocompleteResponse autocompleteResponse { get; set; } ///< Autocomplete ����
    public PlaceDetailsResponse placeInfoResponse { get; set; } ///< PlaceDetails Location ����
    public PlaceDetailsResponse placeDetailsResponse { get; set; } ///< PlaceDetails �� ������ ����

    public Location destinationLocation { get; set; } ///< ������ ��ġ ����

    private int _zoom = 17; ///< Main ȭ�� ���� Zoom Level
    private int _routeZoom = 15; ///< ��� Ž���� Zoom Level
    private int _naviZoom = 20; ///< ��ȳ��� ZoomLevel

    /* URL ���� */
    public string directionStaticMapUrl { get; private set; } ///< ��� �ȳ��� ���� ���� API URL
    public string naviMapUrl { get; private set; } ///< ��ȳ��� ���� ���� API URL

    /**
     * @brief ��� Place_Id�� ����
     * @param[in] placeId ��� ID
     */
    public void SetPlaceId(string placeId)
    {
        PlaceId = placeId;
    }
    #endregion

    #region Awake, Start, Update
    /**
     * @brief searchHistory.json ���� ���� ����, �̱��� ����, ��ġ ���� ���
     * @details ���� ���� = App�� ����Ǵ� ��⿡ ���� �ٸ� ������ ����� ���(���⼱ searchHistory.json) ����
     */
    void Awake()
    {
        StartCoroutine(StartLocationService());
        /* App�� ����Ǵ� ��⿡ ���� �ٸ� ������ ����� ��� ����
         * searchHistory.json ������ ��ü ��θ� ���� */
        filePath = Path.Combine(Application.persistentDataPath, "searchHistory.json");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Search History 
    /**
     * @brief �˻���� �߰�
     * @details �ش� �˻�� placeId�� ���� ����Ʈ�� �ְ� ���� Method ���
     * @param[in] searchQuery �˻���� �̸� �ؽ�Ʈ
     * @param[in] placeId ��� ID
     */
    public void AddSearchToHistory(string searchQuery, string placeId)
    {
        if (!_searchHistoryData.searches.Any(item => item.searchText == searchQuery))
        {
            var newSearchItem = new SearchItem { searchText = searchQuery, placeId = placeId };
            _searchHistoryData.searches.Insert(0, newSearchItem);
            SaveSearchHistory();
        }
    }

    /**
     * @brief JsonData�� �˻���� ����
     * @details ������ Json �����ͷ� ����
     */
    public void SaveSearchHistory()
    {
        string jsonData = JsonUtility.ToJson(_searchHistoryData);
        File.WriteAllText(filePath, jsonData);
    }

    /**
     * @brief ����� �˻���� �ε�
     * @details ������ ����� ��ϵ� (�˻� ��ư�� ���ؼ� ���� ��ҵ�) ã�Ƽ� UI�� ������
     */
    public void LoadSearchHistory()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            _searchHistoryData = JsonUtility.FromJson<SearchHistoryData>(json);
            UIController.Instance.DisplaySearchHistory(); 
        }
        else
        {
            Debug.Log("ã���� ���� ���� ������ ��");
            _searchHistoryData = new SearchHistoryData();
        }
    }
    #endregion

    #region PlaceAPI Autocomplete
    /**
     * @brief ���� ��ġ�� ������� �˻���� ����� ��ҵ��� �ڵ��ϼ��ϰ� �� ���� main_text,  place_id�� �޾ƿ�
     * @param[in] input �Է°�(����)
     */
    public IEnumerator GetPlaceAutocompleteData(string input)
    {

        double latitude = Input.location.lastData.latitude;
        double longitude = Input.location.lastData.longitude;
        int radius = 5000;

        string autoUrl = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={input}&location={latitude},{longitude}&radius={radius}&language=ko&key={Configuration.ApiKey}&components=country:kr";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(autoUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("AutoWebError : " + webRequest.error);
            }
            else
            {
                //Debug.Log("json" + webRequest.downloadHandler.text);
                UIController.Instance.UpdateButton(webRequest.downloadHandler.text);
            }
        }
    }
    #endregion

    #region PlaceAPI PlaceDetails
    /**
     * @brief ��� ID ���� �̿��ؼ� �ش� ����� ����, �浵, �ּ�, �̸� �� �޾ƿ�
     * @details PlaceAPI PlaceDetails �̿�
     * @param[in] placeId ��� ID
     */
    public IEnumerator FetchPlaceInfo(string placeId)
    {
        string detailsUrl = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&fields=geometry,formatted_address,name&key={Configuration.ApiKey}";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(detailsUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                //Debug.Log("Place Details : " + webRequest.downloadHandler.text);
                placeInfoResponse = JsonUtility.FromJson<PlaceDetailsResponse>(webRequest.downloadHandler.text);
                destinationLocation = placeInfoResponse.result.geometry.location;
                yield return MapView.Instance.UpdateDestination(destinationLocation);
            }
            else
            {
                Debug.LogError("DetailsWebError : " + webRequest.error);
            }
        }
    }

    /**
     * @brief ��� ID ���� �̿��ؼ� �ش� ����� �������� ������
     * @details ���� ���� ����, �ּ�, ���� ��ȣ, ������Ʈ, ���� �� ����, Ư�� �ش� �ּҴ� string �������� �ٲٰ� �ѱ��� �ּҷ� ���� 
     */
    public IEnumerator FetchPlaceDetails(string placeId)
    {
        string detailsInfoUrl = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}" +
                                $"&fields=address_components,business_status,formatted_phone_number,website,rating,user_ratings_total" +
                                $"&language=ko" +
                                $"&key={Configuration.ApiKey}";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(detailsInfoUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                //Debug.Log("Place Info : " + webRequest.downloadHandler.text);
                placeDetailsResponse = JsonUtility.FromJson<PlaceDetailsResponse>(webRequest.downloadHandler.text);
                var addressComponents = placeDetailsResponse.result.address_components
                    .Where(ac => ac.types.Contains("administrative_area_level_1") ||
                                 ac.types.Contains("locality") ||
                                 ac.types.Contains("sublocality_level_4") ||
                                 ac.types.Contains("premise") ||
                                 ac.types.Contains("postal_code"))
                    .OrderBy(ac => ac.types.Contains("postal_code") ? 5 :
                                   ac.types.Contains("premise") ? 4 :
                                   ac.types.Contains("sublocality_level_4") ? 3 :
                                   ac.types.Contains("locality") ? 2 :
                                   ac.types.Contains("administrative_area_level_1") ? 1 : 6)
                    .Select(ac => ac.long_name)
                    .ToList();
                addressComponentsDescription = string.Join(" ", addressComponents);
            }
            else
            {
                Debug.LogError("PlaceInfoError : " + webRequest.error);
            }
        }
    }

    #endregion

    #region DirectionAPI
    /**
     * @brief ������� ������ ���� �޾ƿͼ� ��� ǥ��
     * @param[in] origin �����(������ġ)
     * @param[in] destination ������ (������ ������ ���)
     */
    public IEnumerator GetDirection(string origin, Location destinantion)
    {
        string dest = string.Format("{0},{1}", destinantion.lat, destinantion.lng);
        string directionUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={dest}&mode=transit&key={Configuration.ApiKey}";
        using (UnityWebRequest www = UnityWebRequest.Get(directionUrl))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("DirectionAPI ����" + www.error);
            }
            else
            {
                //Debug.Log("��� : " +  www.downloadHandler.text);
                /* �ش� �Լ� �ҷ��� */
                ProcessDirections(www.downloadHandler.text);
            }
        }
    }
    
    /**
     * @brief PolyLine�� �̿��Ͽ� ��� ����
     * @details DirectionAPI ���� ���� PolyLine ���� Decode �Ͽ� Vector ������� ��ȯ �� �� ��ǥ���� ���ڿ��� ��ȯ�ϰ�
     * �ϳ��� ���ڿ��� ����
     */
    void ProcessDirections(string jsonData)
    {
        DirectionsResponse directionsResponse = JsonUtility.FromJson<DirectionsResponse>(jsonData);
        if (directionsResponse.routes.Count > 0)
        {
            string encodedPolyline = directionsResponse.routes[0].overview_polyline.points;
            List<Vector2d> pathPoints = DecodePolyline(encodedPolyline);
            string pathString = string.Join("|", pathPoints.Select(p => $"{p.x},{p.y}"));
            /* ��� Ž���� ���Ǵ� URL */
            directionStaticMapUrl =
                $"https://maps.googleapis.com/maps/api/staticmap?size=640x640" +
                $"&maptype=roadmap" +
                $"&zoom={_routeZoom}" +
                $"&path={pathString}" +
                $"&markers=color:purple|label:O|{Input.location.lastData.latitude},{Input.location.lastData.longitude}" +
                $"&markers=color:red|label:D|{destinationLocation.lat},{destinationLocation.lng}" +
                $"&key={Configuration.ApiKey}";
            /* ��ȳ��� ���Ǵ� URL */
            naviMapUrl =
                $"&center={UnityWebRequest.UnEscapeURL(string.Format("{0}, {1}", Input.location.lastData.latitude, Input.location.lastData.longitude))}" +
                $"&size=640x640" +
                $"&zoom={_naviZoom}" +
                $"&scale=1" +
                $"&path={pathString}" +
                $"&markers=color:purple|label:U|{Input.location.lastData.latitude}, {Input.location.lastData.longitude}" +
                $"&markers=color:red|label:D|{destinationLocation.lat}, {destinationLocation.lng}" +
                $"&key={Configuration.ApiKey}";
        }
    }

    /**
     * @brief ���ڵ��� PolyLine ���ڿ��� ���ڵ��Ͽ� Vector2d ������� ��ȯ
     * @details 
     * @param[in] encodedPoints ������ ����Ʈ�� �����ϴ� ���ڵ��� ���ڿ�
     */
    List<Vector2d> DecodePolyline(string encodedPoints)
    {
        if (string.IsNullOrEmpty(encodedPoints)) return null;

        List<Vector2d> poly = new List<Vector2d>();
        int index = 0, len = encodedPoints.Length;
        int lat = 0, lng = 0;

        while (index < len)
        {
            int b, shift = 0, result = 0;
            do
            {
                b = encodedPoints[index++] - 63;
                result |= (b & 31) << shift;
                shift += 5;
            } while (b >= 0x20);
            int dlat = ((result & 1) != 0 ? -(result >> 1) : (result >> 1));
            lat += dlat;

            shift = 0;
            result = 0;
            do
            {
                b = encodedPoints[index++] - 63;
                result |= (b & 31) << shift;
                shift += 5;
            } while (b >= 0x20);
            int dlng = ((result & 1) != 0 ? -(result >> 1) : ( result >> 1));
            lng += dlng;

            Vector2d p = new Vector2d(Convert.ToSingle(lat) / 1E5f, Convert.ToSingle(lng) / 1E5f);
            poly.Add(p);
        }

        return poly;
    }
    #endregion

    #region Location Service
    /** 
     * @brief ����� ��ġ ���� ���� 
     */
    private IEnumerator StartLocationService()
    {
        _waitingForLocationService = true;
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            yield return new WaitForSeconds(5.0f);

            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
#if UNITY_ANDROID
                OpenAppSettings();
#endif
                Application.Quit();
            }
        }

        if (!Input.location.isEnabledByUser)
        {
            _waitingForLocationService = false;
            //Debug.Log("GPS�� �غ���� ����");
            yield break;
        }

        Input.location.Start();

        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1);
        }

        if (Input.location.status != LocationServiceStatus.Running)
        {
            //Debug.LogError("��ġ ���񽺸� ����� �� ����");
            _waitingForLocationService = false;
            yield break;
        }
        else
        {
            _waitingForLocationService = false;
        }
#endif
    }

    /**
     * @brief ī�޶� ���� ����
     */
    public IEnumerator StartCameraService()
    {
        _waitingForCameraService = true;
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
            yield return new WaitForSeconds(5.0f);

            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
#if UNITY_ANDROID
                OpenAppSettings();
#endif
                Application.Quit();
            }
        }

        if (Camera.main == null)
        {
            Debug.LogError("ī�޶� ���� ������ �����ϴ�.");
            yield break;
        }
        else
        {
            Debug.Log("ī�޶� ��� �غ� �Ϸ�");
        }
    }
#endif

    /**
     * @brief ��ġ �� ī�޶� ���� ������ ���� ��� ���� �������� �̵��Ͽ� ������ Ȱ��ȭ �ϵ��� ��û
     */
#if UNITY_ANDROID
    void OpenAppSettings()
    {
        // ��ġ
        /* ���� Ȯ�� �źν� false ��ȯ */
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            /* Android Java Ŭ���� */
            AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            /* ���� �������� Android Activity */
            AndroidJavaObject currentActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            /* Package Manager */
            AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            /* ���� �������� ���� Intent ���� */
            AndroidJavaObject launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);
            /* ȣ�� 1 -> ����ڿ��� ���� ��û */
            currentActivity.Call("startActivity", launchIntent);

            /* Uri Ŭ���� */
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromParts", "package", Application.identifier, null);
            /* Intent ���� */
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", "android.settings.APPLICATION_DETAILS_SETTINGS", uriObject);
            /* Intent ���� */
            intentObject.Call<AndroidJavaObject>("addCategory", "android.intent.category.DEFAULT");
            intentObject.Call<AndroidJavaObject>("setFlags", 0x10000000);
            currentActivity.Call("startActivity", intentObject);
        }

        // ī�޶�
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {

            AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            AndroidJavaObject launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);
            currentActivity.Call("startActivity", launchIntent);

            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromParts", "package", Application.identifier, null);
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent", "android.settings.APPLICATION_DETAILS_SETTINGS", uriObject);
            intentObject.Call<AndroidJavaObject>("addCategory", "android.intent.category.DEFAULT");
            intentObject.Call<AndroidJavaObject>("setFlags", 0x10000000);
            currentActivity.Call("startActivity", intentObject);
        }
    }
#endif

    #endregion

    #region Destination Popup
    /**
     * @brief ��ǥ�� �Ÿ� ��� ���� (�Ϲ����� ����)
     * @param[in] lat1 ���� ��ġ ����
     * @param[in] lng1 ���� ��ġ �浵
     * @param[in] lat2 ������ ����
     * @param[in] lng2 ������ �浵
     */
    public double Distance(double lat1, double lng1, double lat2, double lng2)
    {
        double theta = lng1 - lng2;
        double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) + Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));

        dist = Math.Acos(dist);
        dist = Rad2Deg(dist);
        dist = dist * 60 * 1.1515;
        dist = dist * 1609.344;
        return dist;
    }

    /**
     * @brief �������� �������� ��ȯ
     * @param[in] deg ����
     */
    private double Deg2Rad(double deg)
    {
        return (deg * Mathf.PI / 180.0f);
    }

    /**
     * @brief ���ȿ��� ������ ��ȯ
     * @param[in] rad ����
     */
    private double Rad2Deg(double rad)
    {
        return (rad * 180.0f / Mathf.PI);
    }
    #endregion
}

