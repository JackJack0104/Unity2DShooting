using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, 3f, 0) * Time.deltaTime;

        if(transform.position.y > 3)
        {
            Destroy(gameObject);
        }
    }
}