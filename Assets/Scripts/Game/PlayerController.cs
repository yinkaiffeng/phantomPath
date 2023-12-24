using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    public Transform rayDown,rayLeft,rayRight;
    public LayerMask platformLayer,obstacleLayer;
    /// <summary>
    /// 是否向左移动
    /// </summary>
    private bool isMoveLeft = false;
    /// <summary>
    /// 是否跳跃
    /// </summary>
    private bool isJumping = false;

    private Vector3 nextPlatformLeft, nextPlatformRight;
    private ManagerVars _vars;
    private Rigidbody2D my_Body;
    private SpriteRenderer _spriteRenderer;
    private bool isMove = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _vars = ManagerVars.GetManagerVars();
        my_Body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Debug.DrawRay(rayDown.position,Vector2.down*0.5f,Color.red);
        Debug.DrawRay(rayLeft.position,Vector2.left*0.5f,Color.red);
        Debug.DrawRay(rayRight.position,Vector2.right*0.5f,Color.red);
        
        
        if (GameManager.Instance.IsGameStarted ==false ||GameManager.Instance.IsGameOver||GameManager.Instance.IsPause)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && isJumping==false )
        {
            if (isMove ==false)
            {
                EventCenter.Broadcast(EventDefine.PlayerMove);
                isMove = true;
            }
            EventCenter.Broadcast(EventDefine.DecidePach);
            isJumping = true;
            Vector3 mousePos = Input.mousePosition;
            //点击屏幕向左移动
            if (mousePos.x <= Screen.width/2)
            {
                isMoveLeft = true;
            }
            //点击屏幕向右移动
            else if (mousePos.x > Screen.width/2)
            {
                isMoveLeft = false;
            }
            Jump();
        }
  //游戏结束了
        if (my_Body.velocity.y<0&&isRayPlatform()==false&& GameManager.Instance.IsGameOver == false)
        {
            _spriteRenderer.sortingLayerName = "Default";
            GetComponent<BoxCollider2D>().enabled = false;
            GameManager.Instance.IsGameOver = true;
            print("游戏结束了isRayPlatform");
            //调用结束面板
        }

        if (isJumping &&IsRayObstacle()&&GameManager.Instance.IsGameOver ==false)
        {
            print("游戏结束了IsRayObstacle"); 
            GameObject go = ObjectPool.Instance.GetDeathEffect();
            go.SetActive(true);
            go.transform.position = transform.position;
            GameManager.Instance.IsGameOver = true;
            Destroy(gameObject);
        }

        if (transform.position.y-Camera.main.transform.position.y <-6&&GameManager.Instance.IsGameOver==false)
        {
            GameManager.Instance.IsGameOver = true;
            gameObject.SetActive(false);
            print("游戏结束了");
        }
    }

    private GameObject lastHitGo = null;
    private bool isRayPlatform()
    {
      RaycastHit2D hit =  Physics2D.Raycast(rayDown.position, Vector2.down,1f,platformLayer);
      if (hit.collider != null)
      {
          if (hit.collider.tag =="Platform")
          {
              if (lastHitGo!=hit.collider.gameObject)
              {
                  if (lastHitGo == null)
                  {
                      lastHitGo = hit.collider.gameObject;
                      return true;
                  }
                  EventCenter.Broadcast(EventDefine.AddScore);
                  lastHitGo = hit.collider.gameObject;
              }
              return true;
          }
      }
      return false;

    }

    /// <summary>
    /// 是否检测到障碍物
    /// <DDDDsummary> 
    private bool IsRayObstacle()
    {
      
        RaycastHit2D leftHit = Physics2D.Raycast(rayLeft.position, Vector2.left, 0.15f, obstacleLayer); 
        RaycastHit2D rightHit = Physics2D.Raycast(rayRight.position, Vector2.right, 0.15f, obstacleLayer);
        if (leftHit.collider != null)
        {
            if (leftHit.collider.tag == "Obstacle")
            {
                return true;
                
            }
        }

        if (rightHit.collider != null)
        {
            if (rightHit.collider.tag == "Obstacle")
            {
                return true;
            }
        }

        return false;
    }
    private void Jump()
    {
        if (isMoveLeft)
        {
            transform.localScale = new Vector3(-1,1,1);
            transform.DOMoveX(nextPlatformLeft.x, 0.2f);
            transform.DOMoveY(nextPlatformLeft.y + 0.8f, 0.15f);


        }
        else
        {
            transform.localScale = Vector3.one;
            transform.DOMoveX(nextPlatformRight.x, 0.2f);
            transform.DOMoveY(nextPlatformRight.y + 0.8f, 0.15f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag =="Platform")
        {
            isJumping = false;
            Vector3 currentPlatformPos = col.gameObject.transform.position;
            nextPlatformLeft = new Vector3(currentPlatformPos.x - _vars.nextXPos,
                currentPlatformPos.y + _vars.nextYPos, 0);
            
            nextPlatformRight = new Vector3(currentPlatformPos.x +_vars.nextXPos,
                currentPlatformPos.y + _vars.nextYPos, 0);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Pickup")
        {
            EventCenter.Broadcast(EventDefine.AddDiamond);
            //吃到钻石
            col.gameObject.SetActive(false);
        }
    }
}
