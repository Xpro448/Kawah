using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    
    public LayerMask Whatissolid;

   public GameObject destroyEffect;

    private void Start()
    {
        Invoke("Destroyprojectile", lifetime);
        
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, Whatissolid);
        if (hitInfo.collider != null)
        {
          
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy2>().TakeDamage(damage);
            }
            Destroyprojectile();
           
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }

    private void Destroyprojectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
