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

    [SerializeField]
    private GameObject GameOverPanel;

    [SerializeField]
    private TextMeshProUGUI DayCountTextMeshPro;

    private float GaugeWidth;
    private float GaugeHeight;
    private RectTransform backGaugeRectTransform;
    private RectTransform frontGaugeRectTransform;

    public static Action ClosePopUpUI;

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
        if (GameManager.Instance.isGameOver == false)
        {
            SetDayCountText();
            SetRemainTimeText();
            SetGauge();
        }
        else
        {
            SetGameOverPanel();
        }
    }

    private void SetGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        DayCountTextMeshPro.text = (DayManager.Instance.EntireTime).ToString("N1") + "초";
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

        if (remainTime <= 0.0f && DayManager.Instance.isSkyRotating == false)
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
