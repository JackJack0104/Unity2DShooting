using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    //弾を打ち出すまでの遅延
    public float delay = 1f;
    //爆破エフェクト
    public GameObject explosion;
    //生存、発射時間
    private int shot_time;
    //弾幕の弾
    public GameObject BossEnemyBullet1;
    public GameObject BossEnemyBullet2;
    private float rad;    //弾用角度
    private float rad2;   //弾用角度2
    private float rad3;   //弾用角度3
    //GameControllerのAddScoreメソッドを使用するため入れ物を用意
    GameController gameController;
    //BossEnemyのhp
    public int hp = 20;

    public GameObject Player;

    void Start()
    {
        //GameObject.Find("")でカッコ内のオブジェクトを取得し、GetComponentでそのオブジェクトの指定した部品を取得してくる
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        rad = 0f;//弾用角度初期設定
        rad2 = 0f;//弾用角度2初期設定
        rad3 = 0f;//弾用角度2初期設定

        //コルーチン開始
        StartCoroutine(CPU());
    }
    //Bossの制御
    IEnumerator CPU()
    {
        //Moveの処理
        yield return Move();
        while (true)
        {
            yield return Shot8();//弾幕1
            yield return new WaitForSeconds(2f);//2秒の遅延 
            yield return OneShotfleam();//弾幕2
            yield return new WaitForSeconds(3f);//3秒の遅延
            yield return trackingshot();//弾幕3
            yield return new WaitForSeconds(3f);//3秒の遅延
           

            //角度とshot_timeを初期化
            shot_time = 18;
            rad = 0f;//弾用角度初期設定
            rad2 = 0f;//弾用角度2初期設定
            rad3 = 0f;//弾用角度2初期設定
        }
    }
    IEnumerator Move()
    {
        while (shot_time < 18)
        {
            yield return null;
            //画面上部まで移動
            transform.Translate(0, -0.07f, 0);
            //shot_timeの数値でBossの動作管理を行う
            shot_time++;
        }
    }
    IEnumerator Shot8()
    {
        while (shot_time < 160)
        {
            yield return null;
            if (shot_time > 100)
            {
                //弾幕1
                if (shot_time % 16 == 0)
                {
                    //弾発射角度が6.28になるまでループ【6.28で一回転(3.14の2倍)】
                    for (rad = 0f; rad < 6.28f; rad += 0.8f)
                    {
                        //敵弾1の生成
                        GameObject shot = Instantiate(BossEnemyBullet1, transform.position, transform.rotation) as GameObject;
                        //BossEnemyBulletからスクリプトを取得する
                        BossEnemyBullet s = shot.GetComponent<BossEnemyBullet>();
                        //関数を通じて弾の初期情報を渡す(発射角度、スピード)
                        s.Add(rad + rad2, 0.01f);
                    }
                    //1フレームごとに発射角度プラス
                    rad2 += 0.1f;
                    //弾発射角度が6.28になるまでループ【6.28で一回転(3.14の2倍)】
                    for (rad = 0f; rad < 6.28f; rad += 0.8f)
                    {
                        //敵弾2の生成
                        GameObject shot = Instantiate(BossEnemyBullet2, transform.position, transform.rotation) as GameObject;
                        //BossEnemyBulletからスクリプトを取得する
                        BossEnemyBullet s = shot.GetComponent<BossEnemyBullet>();
                        //関数を通じて弾の初期情報を渡す(発射角度、スピード)
                        s.Add(rad + rad3, 0.01f);
                    }
                    //1フレームごとに発射角度マイナス
                    rad3 -= 0.1f;
                }
            }
            //shot_timeの数値でBossの動作管理を行う
            shot_time++;
        }
    }
    IEnumerator OneShotfleam()
    {
        while (shot_time < 500)
        {
            yield return null;
            if (shot_time > 200)
            {
                //回転弾幕
                if (shot_time % 4 == 0)
                {
                    GameObject shot = Instantiate(BossEnemyBullet1, transform.position, transform.rotation) as GameObject;//敵弾生成
                    BossEnemyBullet s = shot.GetComponent<BossEnemyBullet>();//bulletスクリプトを取得
                    s.Add(rad, 0.01f);//関数に弾の初期情報を渡す
                    rad += 0.2f;//発射角度プラス
                }
                if (shot_time % 4 == 0)
                {
                    GameObject shot = Instantiate(BossEnemyBullet2, transform.position, transform.rotation) as GameObject;//敵弾生成
                    BossEnemyBullet s = shot.GetComponent<BossEnemyBullet>();//bulletスクリプトを取得
                    s.Add(rad2, 0.01f);//関数に弾の初期情報を渡す
                    rad2 -= 0.2f;//発射角度マイナス
                }
            }
            //shot_timeの数値でBossの動作管理を行う
            shot_time++;
        }
    }
    IEnumerator trackingshot()
    {
        while (shot_time < 800)
        {
            yield return null;
            if (shot_time > 500)
            {
                //追尾
                if (shot_time % 45 == 0)
                {
                    Vector2 e0 = transform.position;//自機と敵の角度計算
                    float etx = Player.transform.position.x - e0.x;//自機と敵の角度計算
                    float ety = Player.transform.position.y - e0.y;//自機と敵の角度計算
                    rad = Mathf.Atan2(ety, etx);//自機と敵の角度計算

                    for (float ang = -7.8f; ang <= -5.4f; ang += 0.4f)//自機狙い6方向弾幕(1フレームに6発発射)
                    {
                        GameObject shot = Instantiate(BossEnemyBullet1, transform.position, transform.rotation) as GameObject;//敵弾生成
                        BossEnemyBullet s = shot.GetComponent<BossEnemyBullet>();//bulletスクリプトを取得
                        s.Add(rad + ang + 0.1f, 0.03f);//関数に弾の初期情報を渡す
                    }
                }
            }
            //shot_timeの数値でBossの動作管理を行う
            shot_time++;
        }
    }
    //接触判定があった場合(collisonには接触したオブジェクトの情報が入っている)
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //playerと敵が接触した時
        if (collision.CompareTag("Player") == true)
        {
            //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
            Instantiate(explosion, collision.transform.position, transform.rotation);
            gameController.GameOver();
        }
        //BulletとBossEnemyが接触した時
        else if (collision.CompareTag("Bullet") == true)
        {
            //HPを-1
            hp--;
            //collisionはぶつかった相手の情報が入っている。この場合は弾
            Destroy(collision.gameObject);
            if (hp <= 0)
            {
                //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
                Instantiate(explosion, transform.position, transform.rotation);
                //enemyの機体を破壊
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("EnemyBullet") == true)
        {
            return;
        }
    }
}