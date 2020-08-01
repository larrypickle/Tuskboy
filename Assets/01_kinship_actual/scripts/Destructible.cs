using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public GameObject enterText;
    private bool canBreakAgain;

    // Start is called before the first frame update
    void Start()
    {
        enterText.SetActive(false);
        canBreakAgain = true;
    }
    void OnTriggerStay(Collider plyr)
    {
        //Debug.Log("Collider works");
        if(plyr.gameObject.tag == "Player")
        {
            enterText.SetActive(true);
            if (Input.GetButtonDown("Interact") && canBreakAgain == true)
            {
                Debug.Log("interact works");
                Instantiate(destroyedVersion, transform.position, transform.rotation);
                Destroy(gameObject);
                enterText.SetActive(false);
                canBreakAgain = false;

            }
        }
    }

    void OnTriggerExit(Collider plyr)
    {
        enterText.SetActive(false);

    }
}
