using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //deal damage to enemy
            Debug.Log("Hit an enemy!");
            Destroy(this.gameObject);
        }
    }

    public void DegradeProjectile()
    {
        StartCoroutine(Degrade());
    }

    IEnumerator Degrade()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
}
