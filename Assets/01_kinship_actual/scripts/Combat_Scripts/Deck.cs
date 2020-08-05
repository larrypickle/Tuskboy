using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public GameObject nextPiece; //the object that is getting their sprite changed
    public Sprite[] spriteArray;


    public class Card
    {
        public string name; //name of the corresponding object in the object pooler
        public bool Uncontrollable;
        public int spriteNumber;

        public Card(string newName, bool newUncontrollable, int newSpriteNumber)
        {
            name = newName;
            Uncontrollable = newUncontrollable;
            spriteNumber = newSpriteNumber;
            
        }
    }
    public List<Card> deck = new List<Card>(); //players actual deck
    public List<Card> discard = new List<Card>(); //discard pile
    public List<Card> deckInstance = new List<Card>(); //players copy of a deck that gets created when they enter battle

    public Card currentCard;

   
    // Start is called before the first frame update
    void Start()
    {
   

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card Draw()
    {
        currentCard = deckInstance[0];
        discard.Add(deckInstance[0]);
        deckInstance.Remove(deckInstance[0]);
        //remove card from list deck into discard
        //we set currentCard = card drawn
        nextPiece.GetComponent<SpriteRenderer>().sprite = spriteArray[currentCard.spriteNumber]; //why doesnt this work?

        return currentCard;
         
    }

    public void Shuffle()
    {

    }

    public void AddCard(string card, bool uncontrollable, int spriteNumber)
    {
        deck.Add(new Card(card, uncontrollable, spriteNumber));
    }

    public void RemoveCard(string cardName)
    {
        bool firstCardRemoved = false;
        foreach(Card card in deck)
        {
            if(card.name == cardName && !firstCardRemoved)
            {
                deck.Remove(card);
                firstCardRemoved = true;//so it only removes one card of that name
            } 
        }
    }

    public void newDeck() //initialize the starter deck SHOULD ONLY BE INITIALIZED ONCE AT START OF GAME
    {
        for (int i = 0; i < 8; i++)
        {
            deck.Add(new Card("PlayerHateSphere", true, 1));

        }
        for (int j = 0; j < 2; j++)
        {
            deck.Add(new Card("PlayerEmpathySphere", false, 2));
        }



    }

    public void copyDeck()
    {

        foreach (Card card in deck)
        {
            Debug.Log("cards: " + card.name + " " + card.Uncontrollable + " " + card.spriteNumber);
            deckInstance.Add(new Card(card.name, card.Uncontrollable, card.spriteNumber));//copy of deck in deckinstance
        }

        Shuffle();
    }

    public void EndBattle()
    {
        deckInstance.Clear();
    }
}
