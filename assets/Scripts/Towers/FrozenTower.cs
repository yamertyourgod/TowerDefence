using UnityEngine;
using System.Collections;

public class FrozenTower : SplashTower {

    public float SplashRadius;
    public FrozenTower()
    {
        ReloadTime = 5f;
        CanonSpeed = 250;
        SplashRadius = 200;
        Radius = 8;
    }
    protected override void Shoot(GameObject go)
    {
        if (CanShoot)
        {
            CanShoot = false;
            var reloadTimer = Timer.AddTimer(ReloadTime);
            reloadTimer.Done += () => { CanShoot = true; };
            var prefab = Skin.GetPrefab(Skin.GamePrefabs.Freeze);
            var bullet = Instantiate(prefab, TowerGO.transform.position, prefab.transform.rotation) as GameObject;
            var destroyTimer = Timer.AddTimer(1f);
            destroyTimer.Done += () => { if (bullet != null) Destroy(bullet); };
            iTween.MoveTo(bullet, iTween.Hash("position", go.transform.position, "speed", CanonSpeed, "easetype", iTween.EaseType.linear));
            var splashTime = bullet.GetComponent<iTween>().time;
            var splashTimer = Timer.AddTimer(splashTime);
            splashTimer.Done += () => { Splash(bullet); };
        }

    }

    protected override void GetGO(Vector3 pos)
    {
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.FrozenTower);
        TowerGO = Instantiate(prefab, pos, prefab.transform.rotation) as GameObject;
        TowerGO.name = "FrozenTower";
    }

}
