using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// スコアの実装
//   UIの作成
//   UIの更新
//   敵と弾がぶつかったときに更新
//   敵にぶつかった時にスコア加算+更新
//   Playerと弾の差別化: Tag 目印

// ゲームオーバーの実装
//   敵とPlayerがぶつかったときにUIを表示
//   リトライの実装
//      Spaceを押したらシーンを再読み込み

// 音
//   スピーカー : AudioSource
//   音の素材 : AudioClip
// 敵が左右に移動する
// Playerの移動範囲制限
// 弾と敵の表示範囲制限

public class GameController : MonoBehaviour
{
    public GameObject gameOverText;

    public Text scoreText;
    int score = 0;

    void Start()
    {
        gameOverText.SetActive(false);
        scoreText.text = "SCORE:" + score;
    }

    private void Update()
    {
        // gameOverTextが表示されているときスペースキーでリトライ
        // そうでないときは弾を撃つ
        if(gameOverText.activeSelf == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    // スコア加算
    public void AddScore()
    {
        score += 100;
        scoreText.text = "SCORE:" + score;
    }

    // ゲームオーバー
    public void GameOver()
    {
        gameOverText.SetActive(true);
    }
}