using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMercenary : MonoBehaviour
{
    public GameObject mercenary_prefab;
    public MercenaryData mercenary_data;
    GameObject Spawner;

    private void Awake()
    {
        Spawner = GameObject.FindGameObjectWithTag("DaySpawner");
    }

    public void Spawn()
    {
        PlayerSO player = DataManager.Instance.player;
        player.Gold -= mercenary_data.MercenaryCost;
        Instantiate(mercenary_prefab);
        Vector3 pos = new Vector3(
            Spawner.gameObject.transform.position.x,
            Spawner.gameObject.transform.position.y,
            Spawner.gameObject.transform.position.z-2
        );
        mercenary_prefab.transform.position = pos;
    }
}
