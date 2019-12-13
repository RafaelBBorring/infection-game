using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globorg : Enemy {

	// Use this for initialization
	void Start () {
        facingRight = true;
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        if (targetDistance < attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed = Time.deltaTime);
        }

        if (targetDistance < 0)
        {
            if (Mathf.Abs(targetDistance) < attackDistance)
            {
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            }

            if (!facingRight)
            {
                Flip();
            }
        }
        else
        {
            if (Mathf.Abs(targetDistance) < attackDistance)
            {
                rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            }
                
            if (facingRight)
            {
                Flip();
            }
        }
    }
}
