using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using Unity.XR.CoreUtils;

public class ScaleController : MonoBehaviour
{
    XROrigin _xrorigin;
    public Slider scaleSlider;

    private void Awake()
    {
        _xrorigin = GetComponent<XROrigin>();
    }

    private void Start()
    {
        scaleSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float arg0)
    {
        if (scaleSlider != null)
        {
            /* (1,1,1) ���� ������ �����̴� �ڵ��� ���������� ������, Scale ���� ��������. -> 3D ��ü�� Ŀ����. */
            _xrorigin.transform.localScale = Vector3.one / arg0;
        }
    }

    void Update()
    {

    }
}
