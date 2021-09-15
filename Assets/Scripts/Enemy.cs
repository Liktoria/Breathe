using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AIPath path;
    private int health = 4;
    private bool isAttacking;
    
    // Update is called once per frame
    void Update()
    {
        if(GameState.GetInstance().gotOrchid && !isAttacking)
        {
            isAttacking = true;
            path.canSearch = true;
        }
    }

    public void DecreaseHealth()
    {
        if(health > 0)
        {
            health--;
        }
        else
        {
            Destroy(path.gameObject);
        }
    }
}
