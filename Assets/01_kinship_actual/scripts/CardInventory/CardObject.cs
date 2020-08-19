using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardType
{
    Hate,
    Love
}
public abstract class CardObject : ScriptableObject
{
    public GameObject inventoryDisplay; //this will be the display for the card in inventory
    public GameObject sprite;
}
