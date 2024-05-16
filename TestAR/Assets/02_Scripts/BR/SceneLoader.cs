using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// ���ӿ�����Ʈ �ν����� â�� ��ȯ�� ���� �̸��� �ֵ��� ����
    /// �ε�Ÿ�� ����
    /// </summary>
    public string nextSceneName = "HomeScene";
    public float loadingTime = 5f;

    void Start()
    {
        // Invoke �Լ��� ����Ͽ� ���� �ð� �Ŀ� LoadNextScene �Լ��� ȣ���մϴ�.
        Invoke("LoadNextScene", loadingTime);
    }

    void LoadNextScene()
    {
        // ���� ������ ��ȯ�մϴ�.
        SceneManager.LoadScene(nextSceneName);
    }
}
