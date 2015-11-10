using UnityEngine;
using System.Collections;

public class SimpleTower : Tower {

	// Use this for initialization

    public SimpleTower()
    {
        Cost = Controller.SimpleTowerCost;
        ReloadTime = Controller.SimpleTowerReloadTime;
        CanonSpeed = Controller.SimpleTowerCanonSpeed;
        Radius = Controller.SimpleTowerRadius;
    }
    protected override void Shoot(GameObject go)
    {
        if (CanShoot)
        {
            CanShoot = false;
            var reloadTimer = Timer.AddTimer(ReloadTime);
            reloadTimer.Done += () => { CanShoot = true; };
            var prefab = Skin.GetPrefab(Skin.GamePrefabs.Bullet);
            var bullet = Instantiate(prefab, TowerGO.transform.position, prefab.transform.rotation) as GameObject;
            iTween.MoveTo(bullet, iTween.Hash("position", go.transform.position, "speed", CanonSpeed, "easetype", iTween.EaseType.linear));
            var destroyTimer = Timer.AddTimer(bullet.GetComponent<iTween>().time);
            destroyTimer.Done += () => { if (bullet != null) Destroy(bullet); };
        }

    }

    protected override void GetGO(Vector3 pos)
    {
        var prefab = Skin.GetPrefab(Skin.GamePrefabs.SimpleTower);
        TowerGO = Instantiate(prefab, pos, prefab.transform.rotation) as GameObject;
        TowerGO.name = "SimpleTower";
    }

    protected override void SelfUpdate()
    {
        
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
