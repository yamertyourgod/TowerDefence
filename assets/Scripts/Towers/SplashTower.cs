using UnityEngine;
using System.Collections;

public class SplashTower : Tower
{

    public float SplashRadius;
    public SplashTower()
    {
        Cost = Controller.SplashTowerCost;
        ReloadTime = Controller.SplashTowerReloadTime;
        CanonSpeed = Controller.SplashTowerCanonSpeed;
        SplashRadius = Controller.SplashRadius;
        Radius = Controller.SplashTowerRadius;
    }
    protected override void Shoot(GameObject go)
    {
        if (CanShoot)
        {
            CanShoot = false;
            var reloadTimer = Timer.AddTimer(ReloadTime);
            reloadTimer.Done += () => { CanShoot = true; };
            var prefab = Skin.GetPrefab(Skin.GamePrefabs.Splash);
            var bullet = Instantiate(prefab, TowerGO.transform.position, prefab.transform.rotation) as GameObject;
            var destroyTimer = Timer.AddTimer(2f);
            destroyTimer.Done += () => { if (bullet != null) Destroy(bullet); };
            iTween.MoveTo(bullet, iTween.Hash("position", go.transform.position, "speed", CanonSpeed, "easetype", iTween.EaseType.linear));
            var splashTime = bullet.GetComponent<iTween>().time;
            var splashTimer = Timer.AddTimer(splashTime);
            splashTimer.Done += () => { Splash(bullet); };
        }

    }

    protected override void GetGO(Vector3 pos)
    {
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.SplashTower);
        TowerGO = Instantiate(prefab, pos, prefab.transform.rotation) as GameObject;
        TowerGO.name = "SplashTower";
    }

    protected void Splash(GameObject canon)
    {
        Debug.Log("Splash "+canon.name);
        
        canon.GetComponent<SphereCollider>().radius = SplashRadius;
        Debug.Log(canon.GetComponent<SphereCollider>().radius);
    }

    protected override void SelfUpdate()
    {

    }

    void Start()
    {

    }

    // Update is called once per frame
}
