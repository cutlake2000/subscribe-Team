using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMercenary : MonoBehaviour
{
    public GameObject mercenary_prefab;
    public MercenaryData mercenary_data;
    public void Spawn()
    {
        PlayerSO player = DataManager.Instance.player;
        player.Gold -= mercenary_data.MercenaryCost;
        Instantiate(mercenary_prefab);
        Vector3 pos = new Vector3(0,0,0);
        mercenary_prefab.transform.position = pos;
        Debug.Log(player.Gold);
    }
}
