using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // SetName ������ �̸��� �޴´�.
    public InputField inputName;

    // �̸��� �����ϰ� �ҷ����� �Լ�
    // PlayerPrefs�� �̿��� Ŭ���̾�Ʈ�� ������ ����
    public void Save()
    {
        // ������ Ű(Name)�� String Ÿ���� ���� ����
        PlayerPrefs.SetString("Name", inputName.text);
    }

    //�̸��� ������ �ڿ� �Ѿ ��
    public void ChangeSToExplainScene()
    {
        //�̸� ������ �����ؾ���
        Save();
        //�� �Ѿ�� �� ����
        //SceneManager.LoadScene("ExplainGame");

    }


}
