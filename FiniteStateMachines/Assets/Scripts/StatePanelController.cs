using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePanelController : MonoBehaviour
{
    private PlayerController_FSM player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController_FSM>();
    }

    // Update is called once per frame
    void Update()
    {
    }

}
