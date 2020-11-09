using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3? translate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (translate.HasValue)
        {
            transform.Translate(translate.Value);
            translate = null;
        }
    }

    internal void ApplyTranslation(Vector3 vector3)
    {
        translate = vector3;
    }

}
