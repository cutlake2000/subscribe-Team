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
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        PlayerSO player = DataManager.Instance.player;
        player.Gold -= mercenary_data.MercenaryCost;
        Instantiate(mercenary_prefab);
        Vector3 pos = new Vector3(
            Spawner.gameObject.transform.position.x+x,
            Spawner.gameObject.transform.position.y+0.5f,
            Spawner.gameObject.transform.position.z+y
        );
        mercenary_prefab.transform.position = pos;
    }
}
