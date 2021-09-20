using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AIPath path;
    [SerializeField] private SpriteRenderer minusOne;
    private int health = 3;
    private bool isAttacking;
    private Vector3 initialPosition;

    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        
        initialPosition = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.GetInstance().gotOrchid && !isAttacking)
        {
            ActivateEnemy();
        }
    }

    public void DecreaseHealth()
    {
        if(health > 0)
        {
            health--;
            ShowText();
        }
        else
        {
            path.gameObject.SetActive(false);
            ShowText();
        }
    }

    public void ActivateEnemy()
    {
        isAttacking = true;
        GetComponent<Collider2D>().enabled = true;
        path.canSearch = true;
    }

    public void ShowText()
    {
        minusOne.color = new Color(minusOne.color.r, minusOne.color.g, minusOne.color.b, 1);
        StartCoroutine(WaitToHide(minusOne));
    }

    IEnumerator WaitToHide(SpriteRenderer minusOne)
    {
        yield return new WaitForSeconds(1.5f);
        minusOne.color = new Color(minusOne.color.r, minusOne.color.g, minusOne.color.b, 0);
    }

    public void ResetEnemy()
    {
            health = 3;
            transform.parent.position = initialPosition;
    }
}
