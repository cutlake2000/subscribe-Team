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

    [SerializeField]
    private BuildUI buildUI;

    private float GaugeWidth;
    private float GaugeHeight;
    private RectTransform backGaugeRectTransform;
    private RectTransform frontGaugeRectTransform;

    public Action ClosePopUpUI;
    public Action DayUsedUIOn;
    public Action DayUsedUIOff;
    public Action HUDRefresh;

    public GameObject BuyMercenaryUI;
    public GameObject BuyMercenaryUIList;

    [SerializeField]
    TMP_Text goldText;

    [SerializeField]
    TMP_Text woodText;

    private void Awake()
    {
        Instance = this;

        Init();
        HUDRefresh += GoldWoodRefresh;
    }

    private void Start()
    {
        HUDRefresh();
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

    // 낮밤 바뀔 때 필요한 함수들 넣으세요~ UI 쪽만
    public void DayUI(DayNight time)
    {
        switch (time)
        {
            case DayNight.Day:
                BuildingController.Instance.DayChange?.Invoke();
                BuyMercenaryUI.SetActive(true);
                buildUI.On();
                break;
            case DayNight.Night:
                BuildingController.Instance.clickBuildingUI.Off();
                BuyMercenaryUI.SetActive(false);
                BuyMercenaryUIList.SetActive(false);
                buildUI.Off();
                break;
        }
    }

    public void GoldWoodRefresh()
    {
        goldText.text = DataManager.Instance.player.Gold.ToString();
        woodText.text = DataManager.Instance.player.Wood.ToString();
    }
}
