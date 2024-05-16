using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MapView : MonoBehaviour
{
    #region Variables
    public static MapView Instance { get; private set; } ///< MapView.cs �̱��� ����


    [Header("RawImage")]
    public RawImage staticMapImage; ///< �⺻ static Map 
    public RawImage destinationMapImage; ///< ������ ���� ���� 
    public RawImage routeMapImage; ///< ��� ���� ����
    public RawImage useNavigation; ///< ��ȳ� ����

    [Header("Basic Zoom Level")]
    private int _zoom = 17; ///< ���� Zoom = 17�ܰ�

    [Header("Min/Max Zoom Level")]
    private int _maxZoom = 18;  ///< Ȯ�밡���� �ִ� zoom ����
    private int _minZoom = 15;  ///< ��Ұ����� �ּ� zoom ����

    [Header("Drag & Zoom speed")]
    private double _orthoZoomSpeed = 0.05; ///< 2D Ȯ��/��� �ӵ�
    public RawImage MapTouchRange;  ///< �巡��, Ȯ��/��� ��� ����
    private RectTransform _mapRectTransform; ///< ��ġ ���� ����

    [Header("MapUpdate Depending on TouchState")]
    private bool _isTouchState = false; ///< ��ġ ���� ����
    private float _updateInterval = 5.0f; ///< �� ������Ʈ ����
    private float _lastUpdate = 0f; ///< ������ ������Ʈ �� ��� �ð�

    public Location _currentLocation { get; private set; } ///< geometry ��

    public Vector2d currentCenter; ///< ���� center ��

    [Header("Map Move")]
    private static int _zoomLevel; ///< �巡�� �� Ȯ�� ��ҽ� Zoom Level
    private static double _circumference; ///< �ѷ�
    private static double _radius; ///< ������
    private static Vector2d _centre; ///< �߾�

    [Header("Mouse Position")]
    public RawImage rawImage; ///< ��ġ ������ ����
    public int imageSize = 640; ///< ���� ������
    Vector2d shiftedCentre = new Vector2d(0, 0); ///< vector2d (0, 0) ���� ����
    private Vector2d startLatLng; ///< ��ġ ���� ���� �浵
    private Vector2d endLatLng; ///< ��ġ �� ���� �浵
    private Vector2d currentLatLng; ///< ��ġ �� ���� �浵

    #endregion

    #region Awake, Start, Update
    /**
     * @brief �̱��� ����
     */
    void Awake()
    {
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
    private Coroutine _getgooglemap = null; ///< ���۸� �����ϴ� �ڷ�ƾ

    /**
     * @brief �� ��ġ ���� ���ϱ� ���� RectTransform ������Ʈ �Ҵ�
     *        MercatorProjection ����
     *        ���۸� �ҷ���
     */
    void Start()
    {
        _mapRectTransform = MapTouchRange.GetComponent<RectTransform>();
        InitializeMercatorProjection();
        StartGoogleMap();
    }

    /**
     * @brief �巡�� �� �� �ξƿ�, Static Map ��Ŀ �̵�
     */
    void Update()
    {
        OnTouchDrag();
        OnTouchZoom();

        if (_isTouchState)
        {
            _lastUpdate += Time.deltaTime;
            if (_lastUpdate >= _updateInterval)
            {
                StartGoogleMap();
                _lastUpdate = 0f;
            }
        }

        if (useNavigation == true)
        {
            _lastUpdate += Time.deltaTime;
            if (NaviData.Instance.destinationLocation != null)
            {
                if (_lastUpdate >= _updateInterval)
                {
                    StartCoroutine(UseNavigation(NaviData.Instance.naviMapUrl));
                    _lastUpdate = 0f;
                }
            }
        }
    }

    #endregion

    #region Mercator
    /**
     * @brief Mercator ������ �ʱ�ȭ �� ���� ��ġ�� ������ �°� ����
     */
    public void InitializeMercatorProjection()
    {
        MercatorProjection_Setting(_zoom);
        double currentLongitude = Input.location.lastData.longitude;
        double currentLatitude = Input.location.lastData.latitude;
        double x = GetXFromLongitude(currentLongitude);
        double y = GetYFromLatitude(currentLatitude);

        x = x - imageSize / 2;
        y = y - imageSize / 2;

        shiftedCentre = new Vector2d(x, y);
    }

    /**
     * @brief ������ Ȯ�� ������ ���� Mercator ������ ���� �ʱ�ȭ
     * @param[in] zoomLevel �� ����
     */
    public static void MercatorProjection_Setting(int zoomLevel)
    {
        _zoomLevel = zoomLevel;
        _circumference = 256 * Mathf.Pow(2, zoomLevel);

        _radius = (_circumference / (2 * Mathf.PI));
        _centre = new Vector2d(_circumference / 2, _circumference / 2);

    }

    /**
     * @brief �浵�� x ��ǥ�� ��ȯ
     * @param[in] longDegrees �浵
     */
    public static double GetXFromLongitude(double longDegrees)
    {
        double x = 0;
        double longInRadians = longDegrees * Mathf.PI / 180;

        x = _radius * longInRadians;
        x = _centre.x + x;

        return x;
    }

    /**
     * @brief x ��ǥ�� �浵�� ��ȯ
     * @param[in] xValue x ��
     */
    public static double GetLongitudeFromX(double xValue)
    {
        double longitude = 0;

        xValue = xValue - _centre.x;

        longitude = xValue / _radius;

        longitude = longitude * 180 / Mathf.PI;

        return longitude;
    }

    /**
     * @brief ������ y ��ǥ�� ��ȯ
     * @param[in] latDegrees ����
     */
    public static double GetYFromLatitude(double latDegrees)
    {
        double y = 0;
        double latInRadians = latDegrees * Mathf.PI / 180;

        double logVal = Math.Log(((1 + Math.Sin(latInRadians)) / (1 - Math.Sin(latInRadians))), Math.E);

        y = _radius * 0.5 * logVal;
        y = _centre.y - y;

        return y;
    }
    
    /**
     * @brief x ��ǥ�� ������ ��ȯ
     * @param[in] yValue y ��
     */
    public static double GetLatitudeFromY(double yValue)
    {
        double latitude = 0;

        yValue = _centre.y - yValue;

        double lnvLog = yValue / (_radius * 0.5);

        lnvLog = Math.Pow(Math.E, lnvLog);

        latitude = Math.Asin((lnvLog - 1) / (lnvLog + 1));

        latitude = latitude * 180 / Math.PI;

        return latitude;

    }

    #endregion

    #region Drag & Zoom
    /**
     * @brief �巡��
     */
    public void OnTouchDrag() 
    {
        MercatorProjection_Setting(_zoom);
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            _isTouchState = true;

            Vector2 localCursor;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_mapRectTransform, touch.position, null, out localCursor))
            {
                if (_mapRectTransform.rect.Contains(localCursor))
                {
                    Vector3 n = new Vector3(
                              (localCursor.x - _mapRectTransform.rect.min.x) / _mapRectTransform.rect.width * imageSize,
                               imageSize - ((localCursor.y - _mapRectTransform.rect.min.y) / _mapRectTransform.rect.height * imageSize), 0.0f);

                    currentLatLng = new Vector2d(GetLongitudeFromX(-n.x + shiftedCentre.x),
                                                 GetLatitudeFromY(-n.y + shiftedCentre.y));

                    /* �� ��ġ ���� �浵 ������ ���ؼ� (������ ��ġ - ó�� ��ġ) �� �̵���ŭ center ���� ����*/
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            startLatLng = currentLatLng;
                            break;
                        case TouchPhase.Moved:
                            Vector2d intermDelta = currentLatLng - startLatLng;
                            break;
                        case TouchPhase.Ended:
                            endLatLng = currentLatLng;

                            Vector2d delta = endLatLng - startLatLng;
                            if (delta.x != 0 || delta.y != 0)
                            {
                                currentCenter += delta;
                                StartCoroutine(GetGoogleMap());
                            }
                            break;
                    }
                }
            }
        }
    }

    /**
     * @brief Ȯ�� ���
     */
    public void OnTouchZoom()
    {
        MercatorProjection_Setting(_zoom);

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            _isTouchState = true;

            if (RectTransformUtility.RectangleContainsScreenPoint(_mapRectTransform, touchZero.position, null) &&
                RectTransformUtility.RectangleContainsScreenPoint(_mapRectTransform, touchOne.position, null))
            {
                if (_getgooglemap != null) return;

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                double prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                double touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                double deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                _zoom = Mathf.Clamp(_zoom - (int)(deltaMagnitudeDiff * _orthoZoomSpeed), _minZoom, _maxZoom);

                StartCoroutine(GetGoogleMap());
            }
        }
    }

    #endregion

    #region DrawGetStaticMap
    /**
     * @brief ó�� �����Ҷ� currentCenter �� �ʱ�ȭ�ϰ� ���� �ҷ��� => ���� ��ġ�� ������ �ҷ�����
     */
    public void StartGoogleMap()
    {
        currentCenter = Vector2d.zero;
        StartCoroutine(GetGoogleMap());
    }

    /**
     * @brief ���� Map API�� URL�� �̿��ؼ� ���� ��ġ�� ����� �������� ����
     */
    public IEnumerator GetGoogleMap()
    {
        string markers = $"&markers=color:purple|label:U|{UnityWebRequest.UnEscapeURL(string.Format("{0}, {1}", Input.location.lastData.latitude, Input.location.lastData.longitude))}";

        var query =
            $"&center={UnityWebRequest.UnEscapeURL(string.Format("{0}, {1}", Input.location.lastData.latitude + currentCenter.y, Input.location.lastData.longitude + currentCenter.x))}" +
            $"&zoom={_zoom}" +
            $"&size=640x640" +
            $"&scale=1" +
            $"&maptype=roadmap" +
            markers +
            $"&key={Configuration.ApiKey}";

        //Log($"center �� üũ : {currentCenter.x}, {currentCenter.y}");

        var www = UnityWebRequestTexture.GetTexture(Configuration.BaseUrl + query);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("www error" + www.error);
            Debug.LogError(Configuration.BaseUrl + query);
        }
        else
        {
            staticMapImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
        _getgooglemap = null;
        yield break;
    }
    #endregion

    #region DrawDestination
    /**
     * @brief PlaceAPI�� geometry �� �޾Ƽ� ������ ��Ŀ�� ǥ��
     * @param[in] location �ش� ����� geometry(lat, lng)��
     */
    public IEnumerator UpdateDestination(Location location)
    {
        _currentLocation = location;

        string markers = $"&markers=color:red|label:D|{UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", location.lat, location.lng))}";

        var query =
            $"&center={UnityWebRequest.UnEscapeURL(string.Format("{0},{1}", location.lat, location.lng))}" +
            $"&zoom={_zoom}" +
            $"&size=640x640" +
            $"&scale=1" +
            $"&maptype=roadmap" +
            markers +
            $"&key={Configuration.ApiKey}";

        var www = UnityWebRequestTexture.GetTexture(Configuration.BaseUrl + query);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("www ����" + www.error);
            //Debug.Log(Configuration.BaseUrl + query);
        }
        else
        {
            destinationMapImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }

        yield break;
    }
    #endregion

    #region DrawFindDirectionMap
    /**
     * @brief ��θ� ã�� �����͸� ������ ǥ��
     * @param[in] url �ش� ���� API URL
     */
    public IEnumerator RouteMap(string url)
    {
        var www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("www ����" + www.error);
            //Debug.Log(url);
        }
        else
        {
            routeMapImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }

    }
    #endregion

    #region DrawNavigation
    /**
     * @brief ��ȳ��� ������ Google Static Map
     * @param[in] url �ش� ���� API URL
     */
    public IEnumerator UseNavigation(string url)
    {
        var www = UnityWebRequestTexture.GetTexture(Configuration.BaseUrl + url);
        
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            //Debug.Log(url);
            UIController.Instance.IsNaviPanel = false;
        }
        else
        {
            UIController.Instance.IsNaviPanel = true;
            useNavigation.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }
    #endregion

}
