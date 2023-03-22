using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // SetName 씬에서 이름을 받는다.
    public InputField inputName;

    // 이름을 저장하고 불러오는 함수
    // PlayerPrefs를 이용해 클라이언트에 데이터 저장
    public void Save()
    {
        // 지정한 키(Name)로 String 타입의 값을 저장
        PlayerPrefs.SetString("Name", inputName.text);
    }

    //이름을 결정한 뒤에 넘어갈 씬
    public void ChangeSToExplainScene()
    {
        //이름 정보를 저장해야함
        Save();
        //씬 넘어가는 것 설정
        //SceneManager.LoadScene("ExplainGame");

    }


}
