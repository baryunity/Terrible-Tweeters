using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private static bool oneShot = true;
    private float lifeTime = 3f;

    private void Awake()
    {
        if (oneShot)
        {
            Destroy(gameObject, lifeTime);
            oneShot = false;
        }
        else
            Destroy(gameObject);
    }

}
