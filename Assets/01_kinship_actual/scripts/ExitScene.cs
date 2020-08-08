using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ExitScene : MonoBehaviour
{
    public GameObject enterText;
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        enterText.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerStay(Collider plyr)
    {
        if(plyr.gameObject.tag == "Player")
        {
            enterText.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                SaveSystem.LoadScene(levelToLoad);
            }
        }
    }

    void OnTriggerExit(Collider plyr)
    {
        if(plyr.gameObject.tag == "Player")
        {
            enterText.SetActive(false);
        }
    }
}
