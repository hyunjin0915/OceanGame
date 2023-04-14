using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();//��� ����Ʈ 
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName); //csv ���� ������

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' }); //,������ �ɰ���

            Dialogue dialogue = new Dialogue();

            dialogue.name = row[1];
           
            List<string> contextList = new List<string>(); //ũ��𸣴ϱ��Ʈ������
            List<string> spriteList = new List<string>();

            do
            {
                contextList.Add(row[2]);
                spriteList.Add(row[3]);
               
                if (++i < data.Length)
                {
                    row = data[i].Split(new char[] { ',' });
                }
                else break; //i�� ��Ŀ�������������

            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();//�迭�ιٲ㼭 �־��ְ�
            dialogue.spriteName = spriteList.ToArray();
            dialogueList.Add(dialogue);

        }

        return dialogueList.ToArray(); //�ٽ� �迭���·� �ٲ㼭 ��ȯ���ֱ�
    }

}