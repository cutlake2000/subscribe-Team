using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMercenary : MonoBehaviour
{
    public GameObject mercenary_prefab;
    public MercenaryData mercenary_data;

    [SerializeField]
    private GameObject MercenaryPrefabs;

    GameObject Spawner;

    private void Awake()
    {
        Spawner = GameObject.FindGameObjectWithTag("DaySpawner");
    }

    public void Spawn()
    {
        PlayerSO player = DataManager.Instance.player;
        if (player.MaxUnitCount <= player.CurrentUnitCountt)
            return;
        player.CurrentUnitCountt++;
        BuildingController.Instance.clickBuildingUI.Refresh(BuildingController.Instance.clickBuildingUIModel.clickBuilding);

        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        player.Gold -= mercenary_data.MercenaryCost;
        var mercenary = Instantiate(mercenary_prefab);
        Vector3 pos = new Vector3(
            Spawner.gameObject.transform.position.x + x,
            Spawner.gameObject.transform.position.y + 0.5f,
            Spawner.gameObject.transform.position.z + y
        );
        mercenary.transform.parent = MercenaryPrefabs.transform;
        mercenary.transform.position = pos;
    }
}
