using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Settings : NetworkBehaviour
{
    
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
