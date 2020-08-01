using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OnTrigger : MonoBehaviour
{
    public GameObject enterText;
    // Start is called before the first frame update
    void Start()
    {
        enterText.SetActive(false);

    }

    void OnTriggerStay(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            enterText.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                enterText.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            enterText.SetActive(false);
        }
    }

   
}
