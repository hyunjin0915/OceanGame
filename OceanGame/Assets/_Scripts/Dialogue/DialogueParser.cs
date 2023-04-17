using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{

    public void Parse(string _CSVFileName)
    {

        List<Dialogue> dialogueList = new List<Dialogue>();//대사 리스트 
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); //csv 파일 가져옴

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;i++)
        {
            string[] row = data[i].Split(new char[] { ',' }); //,단위로 쪼개줌
          
            if (row[0].Trim().Equals("end") || row[2].Trim().Equals("")) continue;

            Dialogue dialogue = new Dialogue();

            dialogue.talkId = int.Parse(row[2].Trim());

            Debug.Log( dialogue.talkId + "/");
            int loopnum = 0;
            while(!row[2].Trim().Equals("end"))
            {
                if (loopnum++ > 10000) throw new Exception("Infinite loop");

                List<string> contextList = new List<string>(); //크기모르니까리스트에저장
                List<string> spriteList = new List<string>();
                do
                {
                    contextList.Add(row[4].ToString());
                    spriteList.Add(row[5].ToString());
               
                    if (++i < data.Length)
                    {
                        row = data[i].Split(new char[] { ',' });
                    }
                    else break; //i가 더커지면빠져나오게

                } while (row[2] == ""&&!row[2].Equals("end"));

                dialogue.contexts = contextList.ToArray();//배열로바꿔서 넣어주고
                dialogue.spriteName = spriteList.ToArray();
                //dialogueList.Add(dialogue);
            }
            DatabaseManager.Instance.QuestDic.Add(dialogue.talkId, dialogue);
            
        }
    }

}