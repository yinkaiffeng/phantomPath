using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    private Button btn_Pause;
    private Button btn_Play;
    private Text txt_Score;
    private Text txt_DiamondCount;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowGamePanel,Show);
        EventCenter.AddListener<int>(EventDefine.UpdateScoreText,UpdateScoreText);
        EventCenter.AddListener<int>(EventDefine.UpdateDiamondText,UpdateDiamondText);
        Init();
       
    }

    private void Init()
    {
        btn_Pause = transform.Find("btn_Pause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OnPauseButtonClick);
        btn_Play = transform.Find("btn_Play").GetComponent<Button>();
        btn_Play.onClick.AddListener(OnPlayButtonClick);
        txt_Score = transform.Find("txt_Score").GetComponent<Text>();
        txt_DiamondCount = transform.Find("Diamond/txt_DiamondCount").GetComponent<Text>();
        btn_Play.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGamePanel,Show);
        EventCenter.RemoveListener<int>(EventDefine.UpdateScoreText,UpdateScoreText);
        EventCenter.RemoveListener<int>(EventDefine.UpdateDiamondText,UpdateDiamondText);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
   //更新显示分数
    private void UpdateScoreText(int score)
    {
        txt_Score.text = score.ToString();
    }
   //更新钻石数量
    private void UpdateDiamondText(int diamond)
    {
        txt_DiamondCount.text = diamond.ToString();
    }
    /// <summary>
    /// 暂停按钮点击
    /// </summary>
    private void OnPauseButtonClick()
    {
       btn_Pause.gameObject.SetActive(false);
       btn_Play.gameObject.SetActive(true);
       // 游戏暂停
       Time.timeScale = 0;
       GameManager.Instance.IsPause = true;
    }
    /// <summary>
    /// play按钮点击
    /// </summary>
    private void OnPlayButtonClick()
    {
        btn_Play.gameObject.SetActive(false);
        btn_Pause.gameObject.SetActive(true);
        //继续游戏
        Time.timeScale = 1;
        GameManager.Instance.IsPause = false;
    }
}
