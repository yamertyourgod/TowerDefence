using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    public int Health;
    public float Speed;
    public GameObject EnemyGO;
    private CollisionDetector _coll;
    // Use this for initialization
    public abstract void Move();
    public abstract void Die();
    protected abstract void GetGO();
    protected abstract void GetDamage();

    public void Summon()
    {
        GetGO();
        _coll = EnemyGO.AddComponent<CollisionDetector>();
        _coll.Hit += Collide;
        EnemyGO.GetComponent<Collider>().isTrigger = true;      
        EnemyGO.tag = "enemy";
        //AddTriger();
        var controller = GameController.GetInstance();
        var spawnPos = controller.Spawn.transform.position;
        var go = Instantiate(EnemyGO, spawnPos, EnemyGO.transform.rotation);
        
        Move();
    }

    protected abstract void Collide(string t);

}
