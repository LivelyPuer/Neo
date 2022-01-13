using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.Tanks;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public List<GameObject> skins = new List<GameObject>();
    public int current = 0;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (current >= skins.Count)
        {
            current = 0;
        }

        foreach (GameObject skin in skins)
        {
            skin.SetActive(false);
        }

        skins[current].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit;
            Vector2 tmp_now_pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(tmp_now_pos, Vector2.one * 0.01f, 0.01f);
            if (hit)
            {
                GameObject hitObj = hit.collider.gameObject;
                // if (hitObj.GetComponent<Character>())
                // {
                //     hitObj.GetComponent<Character>().Click();
                // }
            }
        }
    }
}