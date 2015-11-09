using UnityEngine;
using System.Collections;

public abstract class Tower : MonoBehaviour
{
    public int Coast;
    public float ReloadTime;
    public GameObject TowerGO;
    public bool CanShoot = true;
    public float CanonSpeed;
    public float Radius;
    protected GameObject EnemyGO;
    protected bool IsShooting;
    private CollisionDetector _coll;

    protected abstract void Shoot(GameObject go);
    protected abstract void GetGO(Vector3 pos);
    protected abstract void SelfUpdate();

    public void Build(Vector3 pos)
    {
        GetGO(pos);
        _coll = TowerGO.AddComponent<CollisionDetector>(); 
        _coll.gameObject.GetComponent<SphereCollider>().radius = Radius;
        _coll.Hit += Collide;
        _coll.Exit += ExitCollision;
        _coll.UpdateEvent += Update;
        TowerGO.GetComponent<Collider>().isTrigger = true;
        TowerGO.tag = "tower";
    }


    private void Collide(Collider col)
    {
        if (col.tag == "enemy")
        {
            EnemyGO = col.gameObject;
            IsShooting = true;
        }
    }
    private void ExitCollision(Collider col)
    {
        if (col.gameObject == EnemyGO)
        {
            IsShooting = false;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (EnemyGO == null)
	    {
	        IsShooting = false;
	    }
	    if (IsShooting)
	    {
	        Shoot(EnemyGO);
	    }
        SelfUpdate();
	}

}
