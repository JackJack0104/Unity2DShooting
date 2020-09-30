using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip boomSE;

    // 爆発するエフェクトのプレハブが生成された0.5秒後に消滅する関数
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 0.5f);
        audioSource.PlayOneShot(boomSE);
    }
}