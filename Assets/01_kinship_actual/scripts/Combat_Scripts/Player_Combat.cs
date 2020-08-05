using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class Player_Combat : MonoBehaviour
{
    //player stats
    public int playerMaxHealth = 20;
    public int playerCurrentHealth;
    public int playerStartingHealth = 10;
    private int playerMinHealth = 0;
    public float ShootSpeed = 1.5f;
    public string CardName;
    
    
    public Transform PlayerPosition;

    //healthbar variables
    public HealthBarScript healthBar; //reference to the public class health bar script
    // Start is called before the first frame update
    
    //variable to handle cards drawn
    //public string CardName; //we will parse the projectile.objectname here
    bool Anger = false; //card attribute, will be changed to projectile.type later
    bool canFire = false;

    public Deck deckManager;

    //timer variables
    public GameObject bar;
    public float time = 2;
    private bool firstCombat;


    void Start()
    {
        if (firstCombat)
        {
            newDeck();
        }
        //timer set
        AnimateBar();

        
        playerCurrentHealth = playerStartingHealth;
        healthBar.SetMaxHealth(playerMaxHealth);//once ur health reaches 100 and your opponents reaches 100 u win
        healthBar.SetHealth(playerStartingHealth);


    }

    // Update is called once per frame
    void Update()
    {
        if (canFire)
        {
            ShootCard();
        }

    }

    public void PlayerTakeDamage(int damage)
    {
        
        playerCurrentHealth -= damage;
        healthBar.SetHealth(playerCurrentHealth);
        Debug.Log("PlayerCurrentHealth: " + playerCurrentHealth);
        
        
    }

    public void ShootCard()
    {
        CardName = "PlayerEmpathySphere";
        //later this will be CardName = Deck[0] or something
        if (!Anger)//projectile.type == type.love
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Input Works!");
                ObjectPooler.Instance.SpawnFromPool(CardName, PlayerPosition.position, PlayerPosition.rotation);
                canFire = false;

                bar.transform.localScale = new Vector3(0, 1, 1); //resets the timer bar
                AnimateBar();
            }

        }

        else
        {
            ObjectPooler.Instance.SpawnFromPool(CardName, PlayerPosition.position, PlayerPosition.rotation);
            canFire = false;

            bar.transform.localScale = new Vector3(0, 1, 1); //resets the timer bar
            AnimateBar();
        }

        
    }


    IEnumerator SetFire()
    {
        yield return new WaitForSeconds(ShootSpeed);
        //canFire = true;
        //timer.FillTimer();

    }

    public void AnimateBar()
    {
        if (!canFire)
        {
            LeanTween.scaleX(bar, 1, time).setOnComplete(FireEnable);

        }

    }

    void FireEnable()
    {
        canFire = true;
    }

   


}
