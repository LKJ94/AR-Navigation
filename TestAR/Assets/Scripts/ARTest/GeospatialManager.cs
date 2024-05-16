using Google.XR.ARCoreExtensions;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GeospatialManager : MonoBehaviour
{
    [Header("Core Features")]
    //[SerializeField]
    //private TextMeshProUGUI _geospatialStatusText; ///< 다양한 정보를 담은 텍스트 UI 에 업데이트

    [SerializeField]
    private AREarthManager _earthManager; ///< AREarthManager

    [SerializeField]
    private ARCoreExtensions _arCoreExtensions; ///< ARCoreExtensions 

    /**
     * @brief 프레임을 60으로 고정시켜서 실행하게 만듬, 부드러운 애니메이션과 인터페이스 상호작용을 위해
     */
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    /**
     * @brief AR 세션 상태와 지리공간 모드의 지원 여부 확인 후 지원되면 해당 모드와 거리 추적 모드 활성화
     */
    void Update()
    {
        /* 해당 상황시 실행 중단 */
        if (!Debug.isDebugBuild || _earthManager == null) return;
        /* 해당 상황시 실행 중단 (AR 세션이 초기화 중이거나 추적 중이 아니면) */
        if (ARSession.state != ARSessionState.SessionInitializing && ARSession.state != ARSessionState.SessionTracking) return;

        /* 모드 지원 되는지 확인이 끝나면 해당 모드 활성화 */
        var featureSupport = _earthManager.IsGeospatialModeSupported(GeospatialMode.Enabled);

        switch (featureSupport)
        {
            case FeatureSupported.Unknown:
                break;
            case FeatureSupported.Unsupported:
                break;

            case FeatureSupported.Supported:
                if (_arCoreExtensions.ARCoreExtensionsConfig.GeospatialMode == GeospatialMode.Disabled)
                {
                    _arCoreExtensions.ARCoreExtensionsConfig.GeospatialMode = GeospatialMode.Enabled;

                    _arCoreExtensions.ARCoreExtensionsConfig.StreetscapeGeometryMode = StreetscapeGeometryMode.Enabled;
                }
                break;

            default:
                break;
        }
        /* AR 환경에서 사용자의 현재 지리공간 위치와 방향 정보를 얻기 위함 
         * 지구 상태와 지구 추적 상태를 확인해서 지리공간의 위치와 방향을 결정
         * 추적중인 경우 _earthManager 로부터 pose 를 가져옴
         * 아닌 경우 새로운 geospatialPose 객체를 생성 */
        var pose = _earthManager.EarthState == EarthState.Enabled &&
            _earthManager.EarthTrackingState == TrackingState.Tracking ? _earthManager.CameraGeospatialPose : new GeospatialPose();

        var supported = _earthManager.IsGeospatialModeSupported(GeospatialMode.Enabled);

        ///* AR 세션의 상태, 위치 서비스 상태, 지리공간 모드 지원 여부, 현재 지구의 상태와 추적 상태
        // * 그리고 사용자의 지리공간 위치(위도, 경도), 고도, 정확도, 방향성을 포함한 다양한 정보 를 포함해서 UI 에 보여줌 */
        //if (_geospatialStatusText != null)
        //{
        //    _geospatialStatusText.text =
        //        $"SessionState : {ARSession.state}\n" +
        //        $"LocationServiceStatus : {Input.location.status}\n" +
        //        $"FeatureSupported : {supported}\n" +
        //        $"EarthState : {_earthManager.EarthState}\n" +
        //        $"EarthTrackingState : {_earthManager.EarthTrackingState}\n" +
        //        $"LAT/LON : {pose.Latitude:F6}, {pose.Longitude:F6}\n" +
        //        $"HorizontalAcc : {pose.HorizontalAccuracy:F6}\n" +
        //        $"ALT : {pose.Altitude:F2}\n" +
        //        $"VerticalAcc : {pose.VerticalAccuracy:F2}\n" +
        //        $"EunRotation : {pose.EunRotation:F2}\n" +
        //        $"OrientationYawAcc : {pose.OrientationYawAccuracy:F2}\n";
        //}
    }
}
