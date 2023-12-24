using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScirpt : MonoBehaviour
{
    public SpriteRenderer[] SpriteRenderers;
    public GameObject obstacles;
    private bool startTimer;
    private float fallTime;
    private Rigidbody2D my_Body;

    private void Awake()
    {
        my_Body = GetComponent<Rigidbody2D>();
        
    }

    public void Init(Sprite sprite,float fallTime, int obstaclesDir)
    {
        my_Body.bodyType = RigidbodyType2D.Static;
        startTimer = true;
        for (int i = 0; i < SpriteRenderers.Length; i++)
        {
            this.fallTime = fallTime;
            SpriteRenderers[i].sprite = sprite;
        }

        if (obstaclesDir ==0) //朝右边
        {
            if (obstacles != null)
            {
                obstacles.transform.localPosition = new Vector3(-obstacles.transform.localPosition.x,
                    obstacles.transform.localPosition.y, 0);
            }
        }
    }

    private void Update()
    {
        
        if (GameManager.Instance.IsGameStarted == false||GameManager.Instance.PlayerIsMove ==false)
        {
            return;
        }
        if (startTimer)
        {
            fallTime -= Time.deltaTime;
            if (fallTime<0)//倒计时结束
            {
                //掉落
                startTimer = false;
                if (my_Body.bodyType != RigidbodyType2D.Dynamic)
                {
                    my_Body.bodyType = RigidbodyType2D.Dynamic;
                    StartCoroutine(DealyHide());
                }
            }
        }

        if (transform.position.y-Camera.main.transform.position.y<-6)
        {
            StartCoroutine(DealyHide());
        }
     
    }

    private IEnumerator DealyHide()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
