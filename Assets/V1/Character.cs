using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Character : NetworkBehaviour
{
    public bool right = true;
    public Vector3 leftPos;
    public Vector3 rightPos;
    public bool isDown;
    public string playerId;
    void Start()
    {
    }
    public void Update()
    {
        if (!isDown)
        {
            if (right)
            {
                transform.Translate(1 * Time.deltaTime, 0, 0);
                if ((leftPos - transform.position).magnitude > (leftPos - rightPos).magnitude)
                {
                    right = false;
                }
            }
            else
            {
                transform.Translate(-1 * Time.deltaTime, 0, 0);
                if ((rightPos - transform.position).magnitude > (rightPos - leftPos).magnitude)
                {
                    right = true;
                }
            } 
        }
        else
        {
            if (GetComponent<Rigidbody2D>().isKinematic)
            {
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(leftPos, 0.1f);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(leftPos, rightPos);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rightPos, 0.1f);

    }
}
