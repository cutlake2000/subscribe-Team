using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MercenaryUI : MonoBehaviour
{
    public Text Attack;
    public Text AttackSpeed;
    public Text MovingSpeed;
    public Text AttackRange;
    public Text Name;

    GetMercenaryDatas Mercenary;
    public MercenaryData[] datas;


    private void Awake()
    {
        Mercenary = GetComponent<GetMercenaryDatas>();
    }
    private void showStatus()
    {
        string name = Mercenary.name;
        if(name == "SwordMan")
        {
            Name.text = "이름 : " + name;
            Attack.text = "공격력 : " + datas[0].Attack;
            AttackSpeed.text = "공격속도 : " + datas[0].AttackSpeed;
            MovingSpeed.text = "이동속도 : " + datas[0].MovingSpeed;
            AttackRange.text = "공격범위 : " + datas[0].AttackRange;
        }
        else if(name == "KatanaMan")
        {
            Name.text = "이름 : " + name;
            Attack.text = "공격력 : " + datas[1].Attack;
            AttackSpeed.text = "공격속도 : " + datas[1].AttackSpeed;
            MovingSpeed.text = "이동속도 : " + datas[1].MovingSpeed;
            AttackRange.text = "공격범위 : " + datas[1].AttackRange;
        }
        else if(name == "GunMan")
        {
            Name.text = "이름 : " + name;
            Attack.text = "공격력 : " + datas[2].Attack;
            AttackSpeed.text = "공격속도 : " + datas[2].AttackSpeed;
            MovingSpeed.text = "이동속도 : " + datas[2].MovingSpeed;
            AttackRange.text = "공격범위 : " + datas[2].AttackRange;
        }
    }
}
