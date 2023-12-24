using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   /// <summary>
   /// 游戏是否开始
   /// </summary>
   public bool IsGameStarted { get; set; }
   /// <summary>
   /// 游戏是否结束
   /// </summary>
   public bool IsGameOver { get; set; }
   public bool IsPause { get; set; }
   public bool PlayerIsMove {get;set; }
  //游戏成绩
  private int gameScore;
  private int gameDiamond;
   private void Awake()
   {
      Instance = this;
      EventCenter.AddListener(EventDefine.AddScore,AddGameScore);
      EventCenter.AddListener(EventDefine.PlayerMove,PlayerMove);
      EventCenter.AddListener(EventDefine.AddDiamond,AddGameDiamond);
   }

   private void OnDestroy()
   {
       EventCenter.RemoveListener(EventDefine.AddScore,AddGameScore);
       EventCenter.RemoveListener(EventDefine.PlayerMove,PlayerMove);
       EventCenter.RemoveListener(EventDefine.AddDiamond,AddGameDiamond);
   }

   private void AddGameScore()
   {
       if (IsGameStarted==false|| IsGameOver || IsPause)
       {
           return;
       }
       gameScore++;
       EventCenter.Broadcast(EventDefine.UpdateScoreText,gameScore);
   }
//玩家移动会调用此方法
   private void PlayerMove()
   {
       PlayerIsMove = true;
   }

   public int  GetGameScore()
   {
       return gameScore;
   }
 //更新游戏钻石
   private void AddGameDiamond()
   {
       gameDiamond++;
       EventCenter.Broadcast(EventDefine.UpdateDiamondText,gameDiamond);
   }
}
