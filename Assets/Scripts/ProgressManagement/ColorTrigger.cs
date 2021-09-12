using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTrigger : MonoBehaviour
{
    [SerializeField] private bool endOfLerp;
    [SerializeField] private bool beginningOfLerp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(beginningOfLerp)
            {
                if (collision.gameObject.transform.position.x < transform.position.x)
                {
                    ColorController.GetInstance().SetCanLerp(true);
                    ColorController.GetInstance().NextColor();
                }
                else
                {
                    ColorController.GetInstance().SetCanLerp(false);
                    ColorController.GetInstance().PreviousColor();
                }

            }
            else if(endOfLerp)
            {
                if (collision.gameObject.transform.position.x < transform.position.x)
                {
                    ColorController.GetInstance().SetCanLerp(false);
                }
                else
                {
                    ColorController.GetInstance().SetCanLerp(true);
                }                
            }
            else
            {
                if (collision.gameObject.transform.position.x < transform.position.x)
                {
                    ColorController.GetInstance().NextColor();
                    ColorController.GetInstance().NextMiddlePoint();
                    ColorController.GetInstance().NextSection();
                }
                else
                {
                    ColorController.GetInstance().PreviousColor();
                    ColorController.GetInstance().PreviousMiddlePoint();
                    ColorController.GetInstance().PreviousSection();
                }
                

            }
        }
    }
}
