using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseSee : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            
            other.transform.localScale = new Vector3(-other.transform.localScale.x, other.transform.localScale.y,
                other.transform.localScale.z);
        }
    }
}
