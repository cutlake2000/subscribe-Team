using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ClickBtnType { Back, MaxLvUp, LvUp, Destroy, BuyMode, SellMode, Trade }

public class ClickBuildingUI : MonoBehaviour
{
    enum InfoTextType { Name, CurrentEffect, Upgrade }

    [SerializeField] Image[] buttonList; // 하이라키에 있는 버튼들
    private Dictionary<ClickUIType, List<Sprite>> ImageList;
    [SerializeField] List<Sprite> defaultBtnSprites; // 기본 버튼 스프라이트
    [SerializeField] List<Sprite> ResourceBtnSprites; // 자원 관리용 스프라이트

    [SerializeField] TMP_Text[] infoTexts; // 하단 텍스트
    private StringBuilder newStatusText;

    private void Awake()
    {
        newStatusText = new();
        ImageList = new() { { ClickUIType.Default, defaultBtnSprites }, { ClickUIType.Buy, ResourceBtnSprites }, { ClickUIType.Sell, ResourceBtnSprites } };

    }

    private void Start()
    {
        UIManger.Instance.DayUsedUIOff += Off;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
            Off();
    }

    // 해당 위치에 버튼을 활성화
    public void On(BaseBuilding building)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(building.transform.position);
        transform.position = screenPos;

        UIManger.Instance.ClosePopUpUI -= Off;
        UIManger.Instance.ClosePopUpUI += Off;
    }

    public void Off()
    {
        DeactivateRaycastTargrt();
        gameObject.SetActive(false);
        UIManger.Instance.ClosePopUpUI -= Off;
    }

    // 버튼 클릭시 호출
    public void ClickBtnIndex(int index)
    {
        BuildingController.Instance.ActionUIOptionSelect(index);
    }

    // 버튼들을 새로고침
    // typeList[i]는 selectSprite[i]의 스프라이트를 사용함
    // selectSprite는 clickUItype에 따라 결정됨
    public void RefreshOptionButton<T>(List<T> typeList, ClickUIType clickUIType) where T : Enum
    {
        // 사용할 스프라이트 선택
        List<Sprite> selectSprite = ImageList[clickUIType];

        // 활성화 및 스프라이트 할당
        for (int i = 0; i < buttonList.Length; i++)
        {

            if (i >= typeList.Count + 1)
            {
                buttonList[i].gameObject.SetActive(false);
                continue;
            }
            else if (buttonList[i].gameObject.activeSelf == false)
            {
                buttonList[i].gameObject.SetActive(true);
            }

            if (i == typeList.Count)
            {
                buttonList[i].sprite = defaultBtnSprites[0];
                continue;
            }
            int spriteNum = (int)(object)typeList[i];
            buttonList[i].sprite = selectSprite[spriteNum];
        }

        SortOptionButton(typeList.Count + 1); // +1 = 되돌아가기 추가
    }


    // 옵션 버튼 정렬
    public void SortOptionButton(int count)
    {
        float angle = 25;
        float startAngle = 0 + (angle * (count - 1) * 0.5f);
        for (int i = 0; i < count; i++)
        {
            buttonList[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, startAngle - angle * i));
            buttonList[i].rectTransform.localPosition = buttonList[i].rectTransform.up * 150;
            // TODO : local vector.zero 에서 buttonList[i].rectTransform.up * 150으로 가는 애니메이션
        }
    }


    // 인포UI 정보 갱신
    public void Refresh(BaseBuilding target)
    {
        // 이름
        newStatusText.Clear();
        newStatusText.Append($"Lv{target.level}. {target.buildingName}");
        infoTexts[(int)InfoTextType.Name].text = newStatusText.ToString();
        // 상세 정보
        newStatusText.Clear();
        switch (target.buildingType)
        {
            case BuildingType.Inn:
                newStatusText.Append($"인구 수 <sprite=1>: {((InnBuilding)target).MaxUnitValue}");
                break;
            case BuildingType.Forge:
                newStatusText.Append($"공격력 <sprite=1>: {((ForgeBuilding)target).AddUnitAtk}");
                break;
            case BuildingType.Market:
                MarketBuilding market = (MarketBuilding)target;
                int price = (int)(market.CurrentPrice * 100);
                newStatusText.Append($"현재 시세 : {price}%");
                break;
            default:
                break;
        }
        infoTexts[(int)InfoTextType.CurrentEffect].text = newStatusText.ToString();
        // 레벨업 재료
        newStatusText.Clear();
        if (target.level < target.maxLevel)
            newStatusText.Append($"<sprite=0>{target.upgradeWood}");
        else
            newStatusText.Append($"최고 레벨");
        infoTexts[(int)InfoTextType.Upgrade].text = newStatusText.ToString();
    }

    // 아이콘 레이캐스트 스위치
    public void DeactivateRaycastTargrt()
    {
        foreach (var item in buttonList)
        {
            item.raycastTarget = false;
        }
    }
    public void ActivateRaycastTargrt()
    {
        foreach (var item in buttonList)
        {
            item.raycastTarget = true;
        }
    }


}
