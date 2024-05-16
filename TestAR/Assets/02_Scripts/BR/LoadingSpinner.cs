using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSpinner : MonoBehaviour
{
    public float rotationSpeed = 100f; // ȸ�� �ӵ�

    private void Update()
    {
        // ȸ�� �ӵ��� ���� �̹����� �ð�������� ȸ��
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}