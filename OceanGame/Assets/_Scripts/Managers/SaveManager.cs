using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    [System.Serializable]
    public class Data
    {
        public float playerPosX;
        public float playerPosY;
        public float playerPosZ;

        public string sceneName;
        public string mapName;

        public int questId;
        public int questActionIndex;

        public string curCharacterName;
        public int curLv;
        public int curHp;
        public float playTime;
    }

    public Data[] slot = new Data[4];

    private Vector3 pos;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) Save(1);
        if(Input.GetKeyDown(KeyCode.F6)) Load(1);
    }
    public void Save(int slotIndex)
    {
        //게임의 정보들을 클래스에 저장합니다.
        slot[slotIndex].playerPosX = GameManager.Instance.player.transform.position.x;
        slot[slotIndex].playerPosY = GameManager.Instance.player.transform.position.y;
        slot[slotIndex].playerPosZ = GameManager.Instance.player.transform.position.z;

        slot[slotIndex].sceneName = GameManager.Instance.sceneName;
        slot[slotIndex].mapName = GameManager.Instance.mapName;

        slot[slotIndex].questId = QuestManager.Instance.questId;
        slot[slotIndex].questActionIndex = QuestManager.Instance.questActionIndex;

        slot[slotIndex].curCharacterName = GameManager.Instance.curCharacterName;
        slot[slotIndex].curLv = GameManager.Instance.curLv;
        slot[slotIndex].curHp = GameManager.Instance.curHp;
        slot[slotIndex].playTime = GameManager.Instance.playTime;

        //이진 파일 변환기
        BinaryFormatter bf = new BinaryFormatter();

        //파일 입출력
        FileStream file = File.Create(Application.dataPath +
            "/Save/SaveFile" + slotIndex.ToString() + ".dat");

        //만든 파일에 정보를 기록하고 직렬화
        bf.Serialize(file, slot[slotIndex]);
        file.Close();

        //UI 업데이트
        UIManager.Instance.UpdateSaveUI(slotIndex);

        UIManager.Instance.saveButton[slotIndex].SetActive(false);
        UIManager.Instance.loadButton[slotIndex].SetActive(false);
    }

    public void Load(int slotIndex)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath +
            "/Save/SaveFile" + slotIndex.ToString() + ".dat", FileMode.Open);

        //파일이 없거나 아무것도 쓰인 것이 없다면 실행하지 않음
        if(file != null && file.Length > 0)
        {
            //직렬화된 파일을 원래 형식으로 변환
            slot[slotIndex] = (Data)bf.Deserialize(file);

            //데이터 로드
            pos = new Vector3(slot[slotIndex].playerPosX, slot[slotIndex].playerPosY, slot[slotIndex].playerPosZ);
            GameManager.Instance.player.transform.position = pos;

            GameManager.Instance.sceneName = slot[slotIndex].sceneName;
            GameManager.Instance.mapName = slot[slotIndex].mapName;

            QuestManager.Instance.questId = slot[slotIndex].questId;
            QuestManager.Instance.questActionIndex = slot[slotIndex].questActionIndex;

            GameManager.Instance.curCharacterName = slot[slotIndex].curCharacterName;
            GameManager.Instance.curLv = slot[slotIndex].curLv;
            GameManager.Instance.curHp = slot[slotIndex].curHp;
            GameManager.Instance.playTime = slot[slotIndex].playTime;
        }

        UIManager.Instance.saveButton[slotIndex].SetActive(false);
        UIManager.Instance.loadButton[slotIndex].SetActive(false);

        file.Close();
    }
}
