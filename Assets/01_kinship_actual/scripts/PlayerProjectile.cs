using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 5;


    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("enemy hit");
            gameObject.SetActive(false);
            EnemyCombat ec = other.GetComponent<EnemyCombat>();
            ec.EnemyTakeDamage(damage);

        }
    }
}
