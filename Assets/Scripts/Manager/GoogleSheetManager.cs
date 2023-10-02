using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GoogleSheetManager : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1ljO8w11EQVLdHaCdjYrncSMOloPL9MuXxLy49XmjvQo/export?format=tsv&range=A2:D5";


    [SerializeField]
    MonsterData[] MonsterSO;


    public void Start()
    {
        StartCoroutine(DownloadMonsterSo());
    }




    IEnumerator DownloadMonsterSo()
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();
        SetItem(request.downloadHandler.text);
    }

    void SetItem(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('t');
            for (int j = 0; j < columnSize; j++)
            {
                
            }
        }
    }



}
