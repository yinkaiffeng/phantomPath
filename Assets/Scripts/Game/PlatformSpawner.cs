using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlatformGroupType
{
    Grass,
    winter,
}

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    public Vector3 startSpawnPos;
    public int milestoneCount = 10;
    public float fallTime;
    public float minFallTime;
    public float multiple;
    
    private int spawnplatformCount;
    private ManagerVars _vars;
    private Vector3 _platSpawnPosition;
    private bool _isLeftSpawn ;
  /// <summary>
  /// 选择平台图
  /// </summary>
    private Sprite selectPlatformSprite;
    /// <summary>
    /// 组合平台的类型
    /// </summary>
    private PlatformGroupType _groupType;

    /// <summary>
    /// 钉子组合平台是否生成在左边,反之右边
    /// </summary>
    private bool spikeSpawnLeft = false;
    //钉子方向平台的位置
    private Vector3 spikeDirPlatformPos;
    //需要在钉子方向生成的平台数量
    private int afterSpawnSpikeSpikeSpawnCount;
    private bool isSpawnSpike;


    private void Awake()
    {
        EventCenter.AddListener(EventDefine.DecidePach,Decidepath);
        _vars = ManagerVars.GetManagerVars();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.DecidePach,Decidepath);
    }

    private void Start()
    {
       
        _platSpawnPosition = startSpawnPos;
       
        RandomPlatformTheme();
        //生成平台
        for (int i = 0; i < 5; i++)
        {
            spawnplatformCount = 5;
            Decidepath();
        }
        //生成人物
      GameObject go =  Instantiate(_vars.characterpre);
      go.transform.position = new Vector3(0, -1.8f, 0);
      

    }

    private void Update()
    {
        if (GameManager.Instance.IsGameStarted && GameManager.Instance.IsGameOver == false)
        {
            UpdateFallTime();
        }
    }

    /// <summary>
    /// 更新平台掉落时间
    /// </summary>
    private void UpdateFallTime()
    {
        if (GameManager.Instance.GetGameScore()>milestoneCount)
        {
            milestoneCount = 2;
            fallTime *= multiple;
            if (fallTime<minFallTime)
            {
                fallTime = minFallTime;
            }
        }
     
    }

    /// <summary>
    /// 随机平台主题
    /// </summary>
    private void RandomPlatformTheme()
    {
        int ran = Random.Range(0, _vars.plantformThemeSpriteList.Count);
        selectPlatformSprite = _vars.plantformThemeSpriteList[ran];
        if (ran == 2)
        {
            _groupType =PlatformGroupType.winter;
        }
        else
        {
            _groupType = PlatformGroupType.Grass;
        }
        
    }
/// <summary>
/// 确定路径
/// </summary>
    private void Decidepath()
    {
        if (isSpawnSpike)
        {
            AfterSpawnSpike();
            return;
        }
        if (spawnplatformCount > 0)
        {
            spawnplatformCount--;
            SpawnPlatform();
        }
        else
        {
            _isLeftSpawn = !_isLeftSpawn;
            spawnplatformCount = Random.Range(1, 4);
            SpawnPlatform();
        }
    }
    /// <summary>
    /// 生成平台方法
    /// </summary>
    private void SpawnPlatform()
    {
        int ranObstacleDir = Random.Range(0, 2);
        // 生成普通平添
        if (spawnplatformCount > 0)
        {
            SpawnNormalPlatform(ranObstacleDir);
        }
        else if(spawnplatformCount == 0)
        {
            int ran = Random.Range(0, 3);
            //生成通用组合平台
            if (ran==0)
            {
                SpawnCommonPlatform(ranObstacleDir);
            }
            //生成主题平台
            else if (ran==1)
            {
                switch (_groupType)
                {
                    case PlatformGroupType.Grass:
                        SpawnGrassPlatform(ranObstacleDir);
                        break;
                    case PlatformGroupType.winter:
                        SpawnWinterPlatform(ranObstacleDir);
                        break;
                    default:
                       break;
                }
            }
            //生成钉子平台
            else
            {
                int value = -1;
                if (_isLeftSpawn)
                {
                 
                    value = 0;//生成右边钉子
                }
                else
                {
                    value = 1;//生成左边钉子
                }

                SpawnSpikePlatform(value);
                afterSpawnSpikeSpikeSpawnCount = 4;
                isSpawnSpike = true;
                if (spikeSpawnLeft)//钉子在左边
                {
                    spikeDirPlatformPos = new Vector3(_platSpawnPosition.x - 1.65f,
                        _platSpawnPosition.y + _vars.nextYPos, 0);
                }
                else
                {
                    spikeDirPlatformPos = new Vector3(_platSpawnPosition.x + 1.65f,
                        _platSpawnPosition.y + _vars.nextYPos, 0);
                }
            }

        }

        int ranSpawnDiamond = Random.Range(0, 8);
        if (ranSpawnDiamond >= 6 && GameManager.Instance.PlayerIsMove)
        {
            GameObject go = ObjectPool.Instance.GetDiamond();
            go.transform.position = new Vector3(_platSpawnPosition.x, _platSpawnPosition.y + 0.5f, 0);
            go.SetActive(true);
        }
      
      if (_isLeftSpawn) // 向左生成
      {
          _platSpawnPosition.x -= _vars.nextXPos;
          _platSpawnPosition.y += _vars.nextYPos;
      }
      else
      {
          _platSpawnPosition.x += _vars.nextXPos;
          _platSpawnPosition.y += _vars.nextYPos;
      }

    }
//生成普通平台
    private void SpawnNormalPlatform(int ranObstancleDir)
    {
        GameObject go = ObjectPool.Instance.GetNormalPlatform();
        go.transform.position = _platSpawnPosition;
        go.GetComponent<PlatformScirpt>().Init(selectPlatformSprite,fallTime, ranObstancleDir); 
        go.SetActive(true);
    }
    //生成通用组合平台
    private void SpawnCommonPlatform(int ranObstancleDir)
    {
      
        GameObject go = ObjectPool.Instance.GetCommonplatform();
        go.transform.position = _platSpawnPosition;
        go.GetComponent<PlatformScirpt>().Init(selectPlatformSprite,fallTime, ranObstancleDir);
        go.SetActive(true);
    }
    //生成草地组合平台
    private void SpawnGrassPlatform(int ranObstancleDir)
    {
       
        GameObject go = ObjectPool.Instance.GetGrassPlatform();
        go.transform.position = _platSpawnPosition;
        go.GetComponent<PlatformScirpt>().Init(selectPlatformSprite,fallTime, ranObstancleDir);
        go.SetActive(true);
    }
    //生成冬季组合平台
    private void SpawnWinterPlatform(int ranObstancleDir)
    {
       
        GameObject go = ObjectPool.Instance.GetWinterPlatform();
        go.transform.position = _platSpawnPosition;
        go.GetComponent<PlatformScirpt>().Init(selectPlatformSprite,fallTime, ranObstancleDir);
        go.SetActive(true);
    }
    private void SpawnSpikePlatform(int dir)
    {
        GameObject temp = null;
        if (dir == 0)
        {
            spikeSpawnLeft = false;
            temp = ObjectPool.Instance.GetRightSpikePlatform();

        }
        else

        {
            spikeSpawnLeft = true;
            temp = ObjectPool.Instance.GetLeftSpikePlatform();
          
        }
        temp.transform.position = _platSpawnPosition;
        temp.GetComponent<PlatformScirpt>().Init(selectPlatformSprite,fallTime, dir);
        temp.SetActive(true);
    }

    private void AfterSpawnSpike()
    {
        if (afterSpawnSpikeSpikeSpawnCount>0)
        {
            afterSpawnSpikeSpikeSpawnCount--;
            for (int i = 0; i < 2; i++)
            {
                GameObject temp = ObjectPool.Instance.GetNormalPlatform();
                if (i==0)//生成原来方向
                {
                    temp.transform.position = _platSpawnPosition;
                    if (spikeSpawnLeft)
                    {
                        _platSpawnPosition = new Vector3(_platSpawnPosition.x + _vars.nextXPos,
                            _platSpawnPosition.y + _vars.nextYPos, 0);
                    }
                    else
                    {
                        _platSpawnPosition = new Vector3(_platSpawnPosition.x - _vars.nextXPos,
                            _platSpawnPosition.y + _vars.nextYPos, 0);
                    }
                }
                else//生成钉子方向的平台
                {
                    temp.transform.position = spikeDirPlatformPos;
                    if (spikeSpawnLeft)
                    {
                        spikeDirPlatformPos = new Vector3(spikeDirPlatformPos.x - _vars.nextXPos,
                            spikeDirPlatformPos.y + _vars.nextYPos, 0);
                    }
                    else
                    {
                        
                        spikeDirPlatformPos = new Vector3(spikeDirPlatformPos.x + _vars.nextXPos,
                            spikeDirPlatformPos.y + _vars.nextYPos, 0);
                    }
                }
                temp.GetComponent<PlatformScirpt>().Init(selectPlatformSprite,fallTime,1);
                temp.SetActive(true);
                
            }
        }
        else
        {
            isSpawnSpike = false;
            Decidepath();
        }
    }
    
    
}
