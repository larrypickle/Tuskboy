using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Movement : MonoBehaviour
{
    //Based off of https://www.youtube.com/watch?v=G4aAUodsU3o
    [SerializeField]
    float moveSpeed = 0.25f;

    [SerializeField]
    float rayLength = 3.0f;




    Vector3 targetPosition;
    Vector3 startPosition;
    bool moving;
    public bool CanMove = true;
    public float MoveDelayTime = 0.5f;

    //for collision
    //public LayerMask collision;


    // Start is called before the first frame update
    void Start()
    {
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (Vector3.Distance(startPosition, transform.position) > 1f) //snap to the targetPosition at a certain frame
            {
                transform.position = targetPosition;
                moving = false;
                
                return;
            }

            transform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;
            return;

        }



        if (Input.GetKeyDown(KeyCode.W))
        {
            if (IsTileEmpty(Vector3.forward) && CanMove == true)
            {
                targetPosition = transform.position + (Vector3.forward * 3);
                startPosition = transform.position;
                moving = true;
                //StartCoroutine(MoveDelay());

            }

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (IsTileEmpty(Vector3.back) && CanMove == true)
            {
                targetPosition = transform.position + (Vector3.back * 3);
                startPosition = transform.position;
                moving = true;
                //StartCoroutine(MoveDelay());

            }


        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (IsTileEmpty(Vector3.left) && CanMove == true)
            {
                targetPosition = transform.position + (Vector3.left * 3);
                startPosition = transform.position;
                moving = true;
                //StartCoroutine(MoveDelay());

            }


        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (IsTileEmpty(Vector3.right) && CanMove == true)
            {
                targetPosition = transform.position + (Vector3.right * 3);
                startPosition = transform.position;
                moving = true;
                //StartCoroutine(MoveDelay());

            }



        }
    }
    
   
    private bool IsTileEmpty(Vector3 direction)
    {
        Ray r = new Ray(transform.position, direction);
        RaycastHit hit;

        // visualise the direction we are testing for
        Debug.DrawRay(transform.position, r.direction, Color.blue, rayLength);

        if(Physics.Raycast(r, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "Enemy")
            {
                Debug.Log("Hit wall");
                return false;
            }
        }

        return Physics.Raycast(r, rayLength) == false; //return true if it hits nothing
    }

    IEnumerator MoveDelay()
    {
        CanMove = false;
        yield return new WaitForSeconds(MoveDelayTime);
        CanMove = true;
        

    }
}
