using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerShipを方向キーで動かす
//   方向キーの入力を受け取る
//   Playerの位置を変更する

// 弾を撃つ
//    弾を作る
//    弾の動きをつくる
//    発射ポイントをつくる
//    ボタンを押した時に弾を生成する

public class PlayerShip : MonoBehaviour
{
    public Transform firePoint; // 弾を発射する位置
    public GameObject bulletPrefab;

    AudioSource audioSource;
    public AudioClip shotSE;

    public GameObject explosionPrefab;
    GameController gameController;

    private void Start()
    {
        // 自身のComponentを取得する
        audioSource = GetComponent<AudioSource>();

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // 約0.02秒に1回実行される
    void Update()
    {
        Shot();
        Move();
    }

    void Shot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            audioSource.PlayOneShot(shotSE);
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        // GetAxisRawとGetAxisの違い
        // Debug.Log(x);
        // Debug.Log(y);
        // GetAxisは-1~1の間で細かく判定される
        // GetAxisRawは-1と1のどちらかで判定される

        Vector3 nextPosition = transform.position + new Vector3(x, y, 0) * Time.deltaTime * 4f;
        // 移動範囲の制限
        //   x(-2.9~2.9)、y(-2~2)の範囲で収めたい
        nextPosition = new Vector3(
            // Mathf.Clamp(float value, float min, float MAX);
            Mathf.Clamp(nextPosition.x, -2.9f, 2.9f),
            Mathf.Clamp(nextPosition.y, -2.0f, 2.0f),
            nextPosition.z
        );

        // Time.deltaTimeを使うとどのフレーム速度でも同じになる
        transform.position = nextPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet"))
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            gameController.GameOver();
        }
    }
}