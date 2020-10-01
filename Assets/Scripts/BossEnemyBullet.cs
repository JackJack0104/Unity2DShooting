using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBullet : MonoBehaviour
{
    // 弾の移動量
    public float dx;
    public float dy;

    public void Add(float direction, float speed)
    {
        dx = Mathf.Cos(direction) * speed;
        dy = Mathf.Sin(direction) * speed;
    }

    void Update()
    {
        Vector2 nextPoint = transform.position;
        nextPoint.x = nextPoint.x + dx;
        nextPoint.y = nextPoint.y + dy;

        transform.position = nextPoint;

        if(transform.position.x < -3 || transform.position.x > 3 ||
            transform.position.y < -3 || transform.position.y >3)
            {
                Destroy(gameObject);
            }
    }
}
