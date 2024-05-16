using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* �߰� */
using UnityEngine.XR.ARFoundation;

public class ARPlacement : MonoBehaviour
{
   public ARRaycastManager _raycastManager;

    /// <summary>
    /// ������ ��ü�� ���� ����Ʈ
    /// </summary>
    static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    public Camera arCamera;
    public GameObject gameobject;

    private void Awake()
    {
               
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("���콺 Ŭ��");
            //Vector3 centerScreen = new Vector3(Screen.width / 2, Screen.height / 2);
            Ray ray = arCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log($"{ray}");

            if (_raycastManager.Raycast(ray, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitpose = raycastHits[0].pose;
                //Vector3 hitposition = hitpose.position;
                ///* ��ü ��ġ�� ������ */
                //gameobject.transform.position = hitposition;
                Instantiate(gameobject, hitpose.position, Quaternion.identity);

            }
            else
            {
                Debug.Log($"{raycastHits}");
                Debug.Log($"??? : {_raycastManager.Raycast(ray, raycastHits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon)}");
                Debug.Log($"Hits Count: {raycastHits.Count}");

            }
        }
    }
}
