using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject EmpathySphere;
    public GameObject HateSphere;
    public Transform EnemyPosition;
    public bool canFire;


    //maybe have an enemymanager script that handles this stuff later
    //enemy stats
    public int enemyMaxHealth = 20;
    public int enemyCurrentHealth;
    public int enemyStartingHealth = 5;
    public float shootSpeed = 2.5f; //how many seconds between shots

    public HealthBarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyStartingHealth;
        healthBar.SetMaxHealth(enemyMaxHealth);//once ur health reaches 100 and your opponents reaches 100 u win
        healthBar.SetHealth(enemyStartingHealth);
        canFire = true;
    }
    // Update is called once per frame
    void Update()
    {
        
        if(canFire == true && enemyCurrentHealth != enemyMaxHealth)
        {
            //choosing random ability 
            int randomNumber = Random.Range(1, 4);
            Debug.Log("randomNumber: " + randomNumber);

            if(randomNumber == 1 || randomNumber == 3)
            {
                ShootHateSphere();

            }

            else if(randomNumber == 2)
            {
                //Debug.Log("empathy sphere");
                ShootEmpathySphere();
                
            }
        }

       
    }

    //coroutine to fire bullets in random interval
    IEnumerator RandomFire()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, shootSpeed));
        canFire = true;

    }

    public void EnemyTakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        healthBar.SetHealth(enemyCurrentHealth);
    }

    public void ShootHateSphere()
    {
        //Instantiate(HateSphere, EnemyPosition.position, EnemyPosition.rotation);

        //using singleton allows me to access objectpooler just through objectpooler.instance instead of having to establish the class in this script first
        ObjectPooler.Instance.SpawnFromPool("EnemyHateSphere", EnemyPosition.position, EnemyPosition.rotation);

        canFire = false;

        StartCoroutine(RandomFire());

    }

    public void ShootEmpathySphere()
    {
        ObjectPooler.Instance.SpawnFromPool("EnemyEmpathySphere", EnemyPosition.position, EnemyPosition.rotation);

        canFire = false;

        StartCoroutine(RandomFire());
    }

    
}
