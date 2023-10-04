using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogUI : MonoBehaviour
{
    [SerializeField] TMP_Text ScreenLog;
    WaitForSecondsRealtime time;

    private void Awake()
    {
        time = new WaitForSecondsRealtime(2.0f);
    }


    public IEnumerator ActiveScreenLog(string textLog)
    {
        gameObject.SetActive(true);
        ScreenLog.text = textLog;
        yield return time;
        gameObject.SetActive(false);
    }
}
