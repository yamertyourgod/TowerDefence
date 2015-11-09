using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Button SimpleTower;
    public Button SplashTower;
    public Button FrozenTower;
    public Button StartButton;
    // Use this for initialization
    void Start()
    {
        SimpleTower.onClick.AddListener(()=>OnBuildButtonPressed(GameController.Towers.Simple));
        SplashTower.onClick.AddListener(()=>OnBuildButtonPressed(GameController.Towers.Splash));
        FrozenTower.onClick.AddListener(()=>OnBuildButtonPressed(GameController.Towers.Frozen));
        StartButton.onClick.AddListener(()=>GameController.StartGame());
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnBuildButtonPressed(GameController.Towers towerType)
    {
         GameController.CurrentTowerType = towerType;
        GameController.ShowMap(true);
    }
}
