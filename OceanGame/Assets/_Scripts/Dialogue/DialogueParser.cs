using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();//대사 리스트 
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); //csv 파일 가져옴

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); //,단위로 쪼개줌

            Dialogue dialogue = new Dialogue();

            dialogue.name = row[1];
           
            List<string> contextList = new List<string>(); //크기모르니까리스트에저장
            List<string> spriteList = new List<string>();

            do
            {
                contextList.Add(row[2]);
                spriteList.Add(row[3]);
               
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else break; //i가 더커지면빠져나오게

            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();//배열로바꿔서 넣어주고
            dialogue.spriteName = spriteList.ToArray();
            dialogueList.Add(dialogue);

        }

        return dialogueList.ToArray(); //다시 배열형태로 바꿔서 반환해주기
    }

}