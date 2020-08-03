using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public GameObject EmpathySphere;
    public GameObject HateSphere;
    public Transform EnemyPosition;
    public Transform EmpathyPosition;
    public bool canFire;


    //maybe have an enemymanager script that handles this stuff later
    //enemy stats
    public int enemyMaxHealth = 20;
    public int enemyCurrentHealth;
    public int enemyStartingHealth = 20;
    private int enemyMinHealth = 0;
    public float shootSpeed = 2.5f; //how many seconds between shots

    public HealthBarScript healthBar;

    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyStartingHealth;
        healthBar.SetMaxHealth(enemyMaxHealth);//once ur health reaches 100 and your opponents reaches 100 u win
        healthBar.SetHealth(enemyStartingHealth);

        StartCoroutine(RandomFire());

    }
    // Update is called once per frame
    void Update()
    {

        if (canFire == true)
        {
            //choosing random ability 
            int randomNumber = Random.Range(1, 6);
            int empNumber = 4;
            //Debug.Log("randomNumber: " + randomNumber);
            Move(randomNumber);

            if(enemyCurrentHealth >= enemyMaxHealth)
            {
                empNumber = 2;
            }
            
            if (randomNumber < empNumber)
            {
                ShootHateSphere();
                //EnemyTakeDamage(1);
            }

            else if (randomNumber >= empNumber)
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

    IEnumerator QuickFire()
    {
        yield return new WaitForSeconds(1f);
        canFire = true;

    }

    public void EnemyTakeDamage(int damage)
    {
       
        enemyCurrentHealth -= damage;
        healthBar.SetHealth(enemyCurrentHealth);
        Debug.Log("Enemy Health: " + enemyCurrentHealth);

        
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
        ObjectPooler.Instance.SpawnFromPool("EnemyEmpathySphere", EmpathyPosition.position, EmpathyPosition.rotation);

        canFire = false;

        StartCoroutine(RandomFire());
    }

    public void Move(int direction)
    {
        //maybe find another way to do this or move to another file as this will vary from enemy to enemy
        switch (direction)
        {
            case 1:
                this.transform.position = new Vector3(3, 1.5f, 9);
                break;
            case 2:
                this.transform.position = new Vector3(0, 1.5f, 9);
                break;
            case 3:
                this.transform.position = new Vector3(-3, 1.5f, 9);
                break;
            case 4:
                this.transform.position = new Vector3(-6, 1.5f, 9);
                break;
            case 5:
                this.transform.position = new Vector3(6, 1.5f, 9);
                break;
       
        }
    }
}
