using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;
using TMPro;

public class Player_Combat : MonoBehaviour
{
    //player stats
    public int playerMaxHealth = 20;
    public int playerCurrentHealth;
    public int playerStartingHealth = 10;
    private int playerMinHealth = 0;
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
    public TextMeshProUGUI drawPile;
    public TextMeshProUGUI discardPile;

    //timer variables
    public GameObject bar;
    public float time = 2;
    public bool firstCombat;

    private void Awake()
    {
        drawPile = drawPile.GetComponent<TMPro.TextMeshProUGUI>();
        discardPile = discardPile.GetComponent<TMPro.TextMeshProUGUI>();
    }
    void Start()
    {
        if (firstCombat)
        {
            deckManager.newDeck(); //instantiate basic starter deck
        }

        deckManager.copyDeck(); //copy the deck to get deckInstance, we will manipulate this in the combat instead of ACTUAL deck
        deckManager.Draw(); //draw a card from the copied deck

        
        //starting bar timer
        AnimateBar();

        //instantiating health bar
        playerCurrentHealth = playerStartingHealth;
        healthBar.SetMaxHealth(playerMaxHealth);//once ur health reaches 100 and your opponents reaches 100 u win
        healthBar.SetHealth(playerStartingHealth);

    }

    // Update is called once per frame
    void Update()
    {
        //show how many cards are in draw pile and discard pile
        drawPile.text = deckManager.deckInstance.Count.ToString();
        discardPile.text = deckManager.discard.Count.ToString();

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
        CardName = deckManager.currentCard.name;
        //later this will be CardName = Deck[0] or something
        if (!deckManager.currentCard.Uncontrollable)//projectile.type == type.love
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Input Works!");
                ObjectPooler.Instance.SpawnFromPool(CardName, PlayerPosition.position, PlayerPosition.rotation);
                canFire = false;

                bar.transform.localScale = new Vector3(0, 1, 1); //resets the timer bar
                deckManager.Draw();
                AnimateBar();
            }

        }

        else
        {
            ObjectPooler.Instance.SpawnFromPool(CardName, PlayerPosition.position, PlayerPosition.rotation);
            canFire = false;

            bar.transform.localScale = new Vector3(0, 1, 1); //resets the timer bar
            deckManager.Draw();
            AnimateBar();
        }

        
    }

    public void AnimateBar()
    {
        if (!canFire)
        {
            LeanTween.scaleX(bar, 1, time).setOnComplete(FireEnable);
            //watched this tutorial for the bar timer: https://www.youtube.com/watch?v=z7bR_xYcopM

        }

    }

    void FireEnable()
    {
        canFire = true;
    }

   


}
