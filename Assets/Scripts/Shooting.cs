using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    private GameObject instantiatedProjectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 cursorScreenPosition = Input.mousePosition;
        Vector3 localCursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPosition);
        Vector2 localCursorPosition2D = new Vector2(localCursorPosition.x, localCursorPosition.y);
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 shootingDirection =  localCursorPosition2D - playerPosition;
        float angle = Vector2.Angle(shootingDirection, transform.up);

        instantiatedProjectile = GameObject.Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rbProjectile = instantiatedProjectile.GetComponent<Rigidbody2D>();
        instantiatedProjectile.GetComponent<Projectile>().DegradeProjectile();

        rbProjectile.velocity = shootingDirection * projectileSpeed;
    }

}
