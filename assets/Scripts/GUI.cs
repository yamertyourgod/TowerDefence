using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Button SimpleTower;
    public Button SplashTower;
    public Button FrozenTower;
    public Button StartWaveButton;
    public Button StartGameButon;
    public GameObject StartScreen;
    public GameObject PlayScreen;
    public GameObject GameOverScreen;
    public Text GoldText;
    public Text KilledEnemies;
    private GameController _controller;
    private Text _startWaveText;

    // Use this for initialization
    public void StartGui()
    {
        _controller = GameController.GetInstance();
        SimpleTower.onClick.AddListener(()=>OnBuildButtonPressed(GameController.Towers.Simple));
        SplashTower.onClick.AddListener(()=>OnBuildButtonPressed(GameController.Towers.Splash));
        FrozenTower.onClick.AddListener(()=>OnBuildButtonPressed(GameController.Towers.Frozen));
        SimpleTower.GetComponentInChildren<Text>().text = "Build Simple Tower "+ _controller.SimpleTowerCost + " gold";
        SplashTower.GetComponentInChildren<Text>().text = "Build Splash Tower "+ _controller.SplashTowerCost + " gold";
        FrozenTower.GetComponentInChildren<Text>().text = "Build Frozen Tower "+ _controller.FrozenTowerCost + " gold";
        StartWaveButton.onClick.AddListener(StartWave);
        _startWaveText = StartWaveButton.GetComponentInChildren<Text>();
        _startWaveText.text = "Start new wave: " + _controller.EnemiesInWave + " enemies";
        StartGameButon.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SetScreen(GameScreens.PlayScreen);
    }

    private void StartWave()
    {
        _controller.CreateWave();
        _controller.StartWave();
       
    }

    // Update is called once per frame
    void Update()
    {

        if (_controller != null)
        {
            GoldText.text = "Gold: " + _controller.Gold;
            KilledEnemies.text = "Killed: " + _controller.KilledEnemies;
            _startWaveText.text = "Start new wave: " + _controller.EnemiesInWave + " enemies";
        }
    }


    public void OnBuildButtonPressed(GameController.Towers towerType)
    {
         GameController.CurrentTowerType = towerType;
        GameController.ShowMap(true);
    }

    public void Reset()
    {
        for (var i = 0; i < transform.childCount;i++)
        {
            if(transform.GetChild(i).name!="GameScreens")Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void SetScreen(GameScreens screen)
    {
        StartScreen.SetActive(false);
        PlayScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        switch (screen)
        {
            case GameScreens.StartScreen:
                StartScreen.SetActive(true);
                break;
            case GameScreens.PlayScreen:
                PlayScreen.SetActive(true);
                break;
            case GameScreens.GameOverScreen:
                GameOverScreen.SetActive(true);
                break;
        }
    }

    public enum GameScreens
    {
        StartScreen,
        PlayScreen,
        GameOverScreen
    }
}
