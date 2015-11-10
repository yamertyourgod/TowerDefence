using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Tower : MonoBehaviour
{
    public int Cost;
    public float ReloadTime;
    public GameObject TowerGO;
    public bool CanShoot = true;
    public float CanonSpeed;
    public float Radius;
    protected GameObject EnemyGO;
    protected bool IsShooting;
    protected GameController Controller = GameController.GetInstance();
    private CollisionDetector _coll;
    private List<GameObject> _enemies; 

    protected abstract void Shoot(GameObject go);
    protected abstract void GetGO(Vector3 pos);
    protected abstract void SelfUpdate();

    public void Build(Vector3 pos)
    {
        if (Controller.Gold - Cost >= 0)
        {
            GetGO(pos);
            _enemies = new List<GameObject>();
            _coll = TowerGO.AddComponent<CollisionDetector>();
            _coll.gameObject.GetComponent<SphereCollider>().radius = Radius;
            _coll.Hit += Collide;
            _coll.Exit += ExitCollision;
            _coll.UpdateEvent += Update;
            TowerGO.GetComponent<Collider>().isTrigger = true;
            TowerGO.tag = "tower";
            Controller.Gold -= Cost;
        }
    }


    private void Collide(Collider col)
    {
        if (col.tag == "enemy")
        {
            _enemies.Add(col.gameObject);
            EnemyGO = col.gameObject;
            IsShooting = true;
        }
    }
    private void ExitCollision(Collider col)
    {
        if (_enemies.Contains(col.gameObject))
        {
            _enemies.Remove(col.gameObject);
        }
        if (_enemies.Count == 0)
        {
            IsShooting = false;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (_enemies.Count<1)
	    {
	        IsShooting = false;
	    }
	    if (IsShooting)
	    {
            var index = Random.Range(0, _enemies.Count);
            Debug.Log("Index = " + index);
            if (_enemies[index] == null)
	        {
	            _enemies.RemoveAt(index);
	        }
	        if (_enemies.Count > 0)
	        {
	            EnemyGO = _enemies[index];
	            Shoot(EnemyGO);
	            Debug.Log(_enemies.Count);
	        }
	    }
        SelfUpdate();
	}

}
