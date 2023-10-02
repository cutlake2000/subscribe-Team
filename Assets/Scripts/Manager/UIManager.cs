using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class UIManger : MonoBehaviour
{
    public static UIManger Instance;

    [SerializeField]
    private TextMeshProUGUI remainTime;

    [SerializeField]
    private TextMeshProUGUI dayCount;

    [SerializeField]
    private GameObject BackGauge;

    [SerializeField]
    private GameObject FrontGauge;

    private float GaugeWidth;
    private float GaugeHeight;
    private RectTransform backGaugeRectTransform;
    private RectTransform frontGaugeRectTransform;

    private void Awake()
    {
        Instance = this;

        Init();
    }

    private void Init()
    {
        backGaugeRectTransform = BackGauge.GetComponent<RectTransform>();
        frontGaugeRectTransform = FrontGauge.GetComponent<RectTransform>();

        GaugeWidth = backGaugeRectTransform.rect.width;
        GaugeHeight = backGaugeRectTransform.rect.height;
    }

    private void Update()
    {
        SetDayCountText();
        SetRemainTimeText();
        SetGauge();
    }

    private void SetDayCountText()
    {
        float day = DayManager.Instance.EntireTime / (DayManager.Instance.DayTime * 2);

        Debug.Log("day + " + day);

        dayCount.text = (day + 1).ToString("N0");
    }

    private void SetGauge()
    {
        if (DayManager.Instance.dayNight == DayNight.Day)
        {
            FrontGauge.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            BackGauge.GetComponent<UnityEngine.UI.Image>().color = Color.blue;
        }
        else
        {
            FrontGauge.GetComponent<UnityEngine.UI.Image>().color = Color.blue;
            BackGauge.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }

        frontGaugeRectTransform.sizeDelta = new Vector2(
            (DayManager.Instance.DayTime - DayManager.Instance.NowTime)
                / DayManager.Instance.DayTime
                * GaugeWidth,
            GaugeHeight
        );
    }

    private void SetRemainTimeText()
    {
        remainTime.text = (DayManager.Instance.DayTime - DayManager.Instance.NowTime).ToString(
            "N0"
        );
    }
}
