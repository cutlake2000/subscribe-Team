using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
    }

    private void Update()
    {
        SetRemainTimeText();
        SetGauge();
    }

    private void SetGauge()
    {
        frontGaugeRectTransform.sizeDelta = new Vector2(
            (DayManager.Instance.DayTime - DayManager.Instance.NowTime)
                / DayManager.Instance.DayTime
                * GaugeWidth,
            16
        );
    }

    private void SetRemainTimeText()
    {
        remainTime.text = (DayManager.Instance.DayTime - DayManager.Instance.NowTime).ToString(
            "N0"
        );
    }
}
