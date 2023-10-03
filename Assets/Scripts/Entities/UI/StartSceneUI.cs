using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour
{
    public Button GameStart;
    public Button Option;
    public Button Optionexit;
    public Button Exit;

    public GameObject GameOptionl;

    void Start()
    {
        GameStart.onClick.AddListener(() =>
        {
            LoadingScenController.LoadScene("MainScene");
        });

        Option.onClick.AddListener(() =>
        {
            GameOptionl.SetActive(true);
        });
        Optionexit.onClick.AddListener(() =>
        {
            GameOptionl.SetActive(false);
        });

        Exit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
