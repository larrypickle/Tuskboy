using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 5;
    public float DespawnTimer = 2f;

    

    public string ObjectFX; //name of the fx that will be spawned on collision
    public bool Stationary; //whether speed = 0
    public bool EnemyFired; //to ensure enemies dont hit themselves

    //2 ways of handling public classes from other scripts
    //plugging in the public game object and using get component to access the script
    //public GameObject player;

    //OR accessing the script and then accessing the classes inside that script
    //public Player_Combat PlayerCombat;

    void Start()
    {
        if (Stationary)
        {
            StartCoroutine(Despawn());
        }
    }
    

    void Update()
    {
        //moves the projectile forward
        transform.position += (transform.forward) * speed * Time.deltaTime;
    }

    //triggers damage on hit
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Combat pc = other.GetComponent<Player_Combat>();
            pc.PlayerTakeDamage(damage);
            gameObject.SetActive(false);
            SpawnEffect();

        }
        else if (other.gameObject.CompareTag("Enemy") && !EnemyFired)
        {
            Debug.Log("enemy hit");
            gameObject.SetActive(false);
            EnemyCombat ec = other.GetComponent<EnemyCombat>();
            ec.EnemyTakeDamage(damage);
            SpawnEffect();

        }

    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(DespawnTimer);
        gameObject.SetActive(false);

    }

    IEnumerator FXDespawn()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);

    }

    void SpawnEffect()
    {
        ObjectPooler.Instance.SpawnFromPool(ObjectFX, this.transform.position, this.transform.rotation);
        StartCoroutine(FXDespawn());

    }

    
}
