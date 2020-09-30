using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵の移動
// 敵を生成
// 敵に弾が当たったら爆発する
// 敵とPlayerがぶつかったら爆発する
// --------
// 敵を左右に揺らす
// 敵を倒した時にスコアを上昇させる
// リスタートの実装

public class EnemyShip : MonoBehaviour
{
    // 爆破エフェクト
    public GameObject explosionPrefab;
    // 弾
    public GameObject enemyBulletPrefab;

    GameController gameController; // gameControllerをいれる変数
    
    float offset;

    void Start()
    {
        // 0~2πの間でランダムに位相をずらす
        offset = Random.Range(0, 2f * Mathf.PI);

        // Enemyが生成されて1秒後にShot関数を実行。その2秒ごとに実行する
        InvokeRepeating("shot", 2, 1);

        // ヒエラルキー上のGameControllerという名前のオブジェクトを取得 -> 全コンポーネントを一旦取得
        // GetComponent<GameController>() GameControllerというコンポーネントのみを取得
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // 敵に弾が当たったら爆発する
    // 当たり判定の基礎知識
    // 当たり判定を行うには、
    //   両者にColliderがついている
    //   少なくともどちらかにRigidbodyがついている
    //     rigidbodyはどちらにつけてもよいが、移動する側につける方が普通

    // isTriggerにチェックをつけた場合はこちらが実行される
    private void OnTriggerEnter2D(Collider2D collision)
    {
            // collisionにぶつかった相手の情報が入っている -> 今回はBulletの情報
            // collision.gameObjectでぶつかった相手を指定できる

            // Playerと敵が接触したとき
            if(collision.CompareTag("Player") == true)
            {
                // collision.transfor,.positionでプレイヤー側の位置を指定
                Instantiate(explosionPrefab, collision.transform.position, transform.rotation);
                gameController.GameOver();
                Destroy(collision);
            }
            // 弾と敵が接触したとき
            else if(collision.CompareTag("Bullet") == true)
            {
                gameController.AddScore();
                Destroy(collision);
            }
    }

    void Destroy(Collider2D collision)
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(collision.gameObject);
    }

    void shot()
    {
        Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
    }

    void Update()
    {
        transform.position -= new Vector3(
            // 三角関数cosで左右に揺らす
            Mathf.Cos(Time.frameCount * 0.01f + offset) * 0.01f,
            Time.deltaTime,
            0);

        if(transform.position.y < -3)
            {
                Destroy(gameObject);
            }
    }
}