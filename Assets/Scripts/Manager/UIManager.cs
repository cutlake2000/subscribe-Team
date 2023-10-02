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
        dayCount.text = DayManager.Instance.DayCount.ToString("N0");
    }

    private void SetGauge()
    {
        float remainTime = DayManager.Instance.DayTime - DayManager.Instance.NowTime;

        frontGaugeRectTransform.sizeDelta = new Vector2(
            remainTime / DayManager.Instance.DayTime * GaugeWidth,
            GaugeHeight
        );

        Debug.Log("SizeDelta + " + frontGaugeRectTransform.sizeDelta);

        if (remainTime <= 0.0f)
        {
            FrontGauge.GetComponent<UnityEngine.UI.Image>().color =
                FrontGauge.GetComponent<UnityEngine.UI.Image>().color == Color.red
                    ? Color.blue
                    : Color.red;
            BackGauge.GetComponent<UnityEngine.UI.Image>().color =
                BackGauge.GetComponent<UnityEngine.UI.Image>().color == Color.red
                    ? Color.blue
                    : Color.red;
        }
    }

    private void SetRemainTimeText()
    {
        remainTime.text = (DayManager.Instance.DayTime - DayManager.Instance.NowTime).ToString(
            "N0"
        );
    }
}
