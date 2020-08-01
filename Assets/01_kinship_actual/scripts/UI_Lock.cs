using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lock : MonoBehaviour
{
    public GameObject buttonLabel;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        buttonLabel.transform.position = namePos;
    }
}
