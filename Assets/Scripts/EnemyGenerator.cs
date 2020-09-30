using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        // 繰り返し関数を実行する
        // InvokeRepeating(string methodName, float time, float repeatRate);
        // Spawn関数を2秒後から０.5秒毎にリピートする
        InvokeRepeating("Spawn", 2f, 0.5f);
    }

    // 生成する関数
    void Spawn()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-2.5f, 2.5f),  // x座標は-2.5~2.5の間でランダムに決まる
            transform.position.y,
            transform.position.z
        );

        Instantiate(
            enemyPrefab, // 生成するオブジェクト
            spawnPosition, // 生成位置
            transform.rotation // 生成時の向き
            );
    }

    void Update()
    {
        
    }
}
