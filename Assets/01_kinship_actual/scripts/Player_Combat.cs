using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public int playerMaxHealth = 20;
    public int playerCurrentHealth;
    public int playerStartingHealth = 10;
    private int playerMinHealth = 0;
    public float ShootSpeed = 1.5f;
    
    public bool canFire = true;
    public Transform PlayerPosition;

    public HealthBarScript healthBar; //reference to the public class health bar script
    // Start is called before the first frame update
    void Start()
    {
        canFire = false;
        playerCurrentHealth = playerStartingHealth;
        healthBar.SetMaxHealth(playerMaxHealth);//once ur health reaches 100 and your opponents reaches 100 u win
        healthBar.SetHealth(playerStartingHealth);

        StartCoroutine(SetFire());

    }

    // Update is called once per frame
    void Update()
    {
        if (canFire == true)
        {
            //choosing random ability 
            PlayerEmpathySphere();

            //Debug.Log("empathy sphere");

        }
    }

    public void PlayerTakeDamage(int damage)
    {
        
        playerCurrentHealth -= damage;
        healthBar.SetHealth(playerCurrentHealth);
        
        
    }

    void PlayerEmpathySphere()
    {
        ObjectPooler.Instance.SpawnFromPool("PlayerEmpathySphere", PlayerPosition.position, PlayerPosition.rotation);

        canFire = false;

        StartCoroutine(SetFire());
    }

    IEnumerator SetFire()
    {
        yield return new WaitForSeconds(ShootSpeed);
        canFire = true;

    }


}
