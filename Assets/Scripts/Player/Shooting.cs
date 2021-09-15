using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Sprite shootingSprite;
    [SerializeField] private Vector3 shootingOffset;
    private GameObject instantiatedProjectile;
    public bool isShooting = false;
    private bool movingRight;
    private float lastY;
    private float lastShotTime = 0;
    private SpriteRenderer characterRenderer;
    private Vector3 shootingOffsetLeft;

    // Start is called before the first frame update
    void Start()
    {
        characterRenderer = this.GetComponent<SpriteRenderer>();
        shootingOffsetLeft = new Vector3(-shootingOffset.x, shootingOffset.y, shootingOffset.z);
    }

    // Update is called once per frame
    void Update()
    {        
        if(GameState.GetInstance().hasMiles && Input.GetMouseButtonDown(0) 
            && !GameState.GetInstance().gamePaused && !EventSystem.current.IsPointerOverGameObject())
        {
            characterRenderer.sprite  = shootingSprite;
            Shoot();
            isShooting = true;
            lastShotTime = Time.time;
        }
        else if(isShooting && Time.time - lastShotTime > 0.5f)
        {
            isShooting = false;
        }
    }

    private void Shoot()
    {
        Vector3 cursorScreenPosition = Input.mousePosition;
        cursorScreenPosition.z = 10;
        Vector3 localCursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPosition);
        Vector2 localCursorPosition2D = new Vector2(localCursorPosition.x, localCursorPosition.y);
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 shootingDirection =  localCursorPosition2D - playerPosition;
        float angle = Vector2.Angle(shootingDirection, transform.up);

        if (shootingDirection.x <= 0)
        {
            if(!characterRenderer.flipX)
            {
                //TODO: AUDIO Play paper flip sound
            }
            characterRenderer.flipX = true;            
            instantiatedProjectile = GameObject.Instantiate(projectile, transform.position + shootingOffsetLeft, Quaternion.Euler(0, 0, angle));
        }
        else
        {
            if(characterRenderer.flipX)
            {
                //TODO: AUDIO Play paper flip sound
            }
            characterRenderer.flipX = false;
            instantiatedProjectile = GameObject.Instantiate(projectile, transform.position + shootingOffset, Quaternion.Euler(0, 0, angle));
        }
        //TODO: AUDIO Shooting sound
        Rigidbody2D rbProjectile = instantiatedProjectile.GetComponent<Rigidbody2D>();
        instantiatedProjectile.GetComponent<Projectile>().DegradeProjectile();

        rbProjectile.velocity = shootingDirection * projectileSpeed;        
    }
}
