using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public Button SimpleTower;
    public Button SplashTower;
    public Button FrozenTower;
    // Use this for initialization
    void Start()
    {
        SimpleTower.onClick.AddListener(()=>OnBuildButtonPressed(GameController.Towers.Simple));
        SplashTower.onClick.AddListener(()=>OnBuildButtonPressed(GameController.Towers.Splash));
        FrozenTower.onClick.AddListener(()=>OnBuildButtonPressed(GameController.Towers.Frozen));
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
