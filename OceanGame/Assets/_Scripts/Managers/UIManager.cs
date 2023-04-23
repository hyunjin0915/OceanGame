using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private HealthManagerScriptableObject healthManagerScriptableObject;

    [SerializeField] GameObject saveUIBoard;
    [SerializeField] GameObject[] saveSlot;

    public GameObject menuSet;

    public GameObject[] saveButton;
    public GameObject[] loadButton;

    [Serializable]
    public struct SaveUIData
    {
        public Text emptyText;
        public Text curCharacterName;
        public Text curLv;
        public Text curHp;
        public Text playTime;
    }

    public SaveUIData[] saveUIDatas = new SaveUIData[3];

    private void OnEnable()
    {
        // using event 
        //healthManagerScriptableObject.healthChangeEvent.AddListener();
    }
    private void OnDisable()
    {
        //healthManagerScriptableObject.healthChangeEvent.RemoveListener();
    }
    
    public void SetUIOn(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void SetUIOff(GameObject obj)
    {
        obj.SetActive(false);
    }

    //
    public void UpdateSaveUI(int slotIndex)
    {
        saveSlot[slotIndex].SetActive(true);

        saveUIDatas[slotIndex].emptyText.enabled = false;
        saveUIDatas[slotIndex].curCharacterName.text = GameManager.Instance.curCharacterName;
        saveUIDatas[slotIndex].curLv.text = "Lv : " + GameManager.Instance.curLv.ToString();
        saveUIDatas[slotIndex].curHp.text = "Hp : " + GameManager.Instance.curLv.ToString();

        float curPlayTime = GameManager.Instance.playTime;
        saveUIDatas[slotIndex].playTime.text = ((int)curPlayTime / 3600).ToString() + "시간 "
            + ((int)curPlayTime / 60).ToString() + "분";
    }
}
