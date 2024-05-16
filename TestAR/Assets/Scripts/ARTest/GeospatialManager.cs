using Google.XR.ARCoreExtensions;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GeospatialManager : MonoBehaviour
{
    [Header("Core Features")]
    //[SerializeField]
    //private TextMeshProUGUI _geospatialStatusText; ///< �پ��� ������ ���� �ؽ�Ʈ UI �� ������Ʈ

    [SerializeField]
    private AREarthManager _earthManager; ///< AREarthManager

    [SerializeField]
    private ARCoreExtensions _arCoreExtensions; ///< ARCoreExtensions 

    /**
     * @brief �������� 60���� �������Ѽ� �����ϰ� ����, �ε巯�� �ִϸ��̼ǰ� �������̽� ��ȣ�ۿ��� ����
     */
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    /**
     * @brief AR ���� ���¿� �������� ����� ���� ���� Ȯ�� �� �����Ǹ� �ش� ���� �Ÿ� ���� ��� Ȱ��ȭ
     */
    void Update()
    {
        /* �ش� ��Ȳ�� ���� �ߴ� */
        if (!Debug.isDebugBuild || _earthManager == null) return;
        /* �ش� ��Ȳ�� ���� �ߴ� (AR ������ �ʱ�ȭ ���̰ų� ���� ���� �ƴϸ�) */
        if (ARSession.state != ARSessionState.SessionInitializing && ARSession.state != ARSessionState.SessionTracking) return;

        /* ��� ���� �Ǵ��� Ȯ���� ������ �ش� ��� Ȱ��ȭ */
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
        /* AR ȯ�濡�� ������� ���� �������� ��ġ�� ���� ������ ��� ���� 
         * ���� ���¿� ���� ���� ���¸� Ȯ���ؼ� ���������� ��ġ�� ������ ����
         * �������� ��� _earthManager �κ��� pose �� ������
         * �ƴ� ��� ���ο� geospatialPose ��ü�� ���� */
        var pose = _earthManager.EarthState == EarthState.Enabled &&
            _earthManager.EarthTrackingState == TrackingState.Tracking ? _earthManager.CameraGeospatialPose : new GeospatialPose();

        var supported = _earthManager.IsGeospatialModeSupported(GeospatialMode.Enabled);

        ///* AR ������ ����, ��ġ ���� ����, �������� ��� ���� ����, ���� ������ ���¿� ���� ����
        // * �׸��� ������� �������� ��ġ(����, �浵), ��, ��Ȯ��, ���⼺�� ������ �پ��� ���� �� �����ؼ� UI �� ������ */
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
