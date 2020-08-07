using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class CombatManager : MonoBehaviour
{
    public EnemyCombat enemyCombat;
    public Player_Combat playerCombat;
    bool CombatDone = false;

    public GameObject Tuskboy;
    public GameObject Gorffrey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCombat.playerCurrentHealth >= playerCombat.playerMaxHealth && 
            enemyCombat.enemyCurrentHealth >= enemyCombat.enemyMaxHealth && !CombatDone)
        {
            enemyCombat.canFire = false;
            playerCombat.canFire = false;
            DialogueManager.StartConversation("Gorffrey/End_Combat", Tuskboy.transform, Gorffrey.transform);
            CombatDone = true;

        }
    }
}
