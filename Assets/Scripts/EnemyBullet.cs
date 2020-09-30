using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void Update()
    {
        // Update()ごとに下に3f分移動する
        transform.position += new Vector3(0, -3f, 0) * Time.deltaTime;

        // 指定範囲外に出たら消滅する
        if(transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }
}