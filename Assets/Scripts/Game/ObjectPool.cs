using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
   public static ObjectPool Instance;
   public int initSpawnCount = 5;
   private List<GameObject> normalPlatformList = new List<GameObject>();
   private List<GameObject> commonPlatformList = new List<GameObject>();
   private List<GameObject> grassPlatformList = new List<GameObject>();
   private List<GameObject> winterPlatformList = new List<GameObject>();
   private List<GameObject> spikePlatformLeftList = new List<GameObject>();
   private List<GameObject> spikePlatformRightList = new List<GameObject>();
   private List<GameObject> deathEffectList = new List<GameObject>();
   private List<GameObject> diamondList = new List<GameObject>();
   private ManagerVars _vars;

   private void Awake()
   {
      Instance = this;
      _vars = ManagerVars.GetManagerVars();
      Init();
   }

   private void Init()
   {
      for (int i = 0; i < initSpawnCount; i++)
      {
         InstaniateObject(_vars.normalPlatformPre, ref normalPlatformList);
      }

      for (int i = 0; i < initSpawnCount; i++)
      {
         for (int j = 0; j < _vars.commonPlatformGroup.Count; j++)
         {
            InstaniateObject(_vars.commonPlatformGroup[j], ref commonPlatformList);
         }
      }
      for (int i = 0; i < initSpawnCount; i++)
      {
         for (int j = 0; j < _vars.grassPlatformGroup.Count; j++)
         {
            InstaniateObject(_vars.grassPlatformGroup[j], ref grassPlatformList);
         }
      }
      for (int i = 0; i < initSpawnCount; i++)
      {
         for (int j = 0; j < _vars.winterPlatformGroup.Count; j++)
         {
            InstaniateObject(_vars.winterPlatformGroup[j], ref winterPlatformList);
         }
      }
      for (int i = 0; i < initSpawnCount; i++)
      {
         InstaniateObject(_vars.spikePlatformLeft, ref spikePlatformLeftList);
      }
      for (int i = 0; i < initSpawnCount; i++)
      {
         InstaniateObject(_vars.spikePlatformRight, ref spikePlatformRightList);
      }

      for (int i = 0; i < initSpawnCount; i++)
      {
         InstaniateObject(_vars.deathEffect, ref deathEffectList);
      }

      for (int i = 0; i < initSpawnCount; i++)
      {
         InstaniateObject(_vars.diamondPre, ref diamondList);
      }
      
   }

   private GameObject InstaniateObject(GameObject prefab, ref List<GameObject> addList)
   {
      GameObject go = Instantiate(prefab, transform);
      go.SetActive(false);
      addList.Add(go);
      return go;
   }
      /// <summary>
      /// 获取单个平台
      /// </summary>
      /// <returns></returns>
   public GameObject GetNormalPlatform()
   {
      for (int i = 0; i < normalPlatformList.Count; i++)
      {
         if (normalPlatformList[i].activeInHierarchy == false)
         {
            return normalPlatformList[i];
         }
         
      }
      return InstaniateObject(_vars.normalPlatformPre, ref normalPlatformList);         
   }
      /// <summary>                                                                    
      /// 获取通用平台                                                                       
      /// </summary>                                                                   
      /// <returns></returns>                                                          
      public GameObject GetCommonplatform()                                               
      {                                                                                   
         for (int i = 0; i < commonPlatformList.Count; i++)                               
         {                                                                                
            if (commonPlatformList[i].activeInHierarchy == false)                         
            {                                                                             
               return commonPlatformList[i];                                              
            }                                                                             
                                                                                      
         }

         int ran = Random.Range(0, _vars.commonPlatformGroup.Count);
         return InstaniateObject(_vars.commonPlatformGroup[ran], ref commonPlatformList);        
      } 
      /// <summary>                                                                    
      /// 获取草地平台                                                                       
      /// </summary>                                                                   
      /// <returns></returns>                                                          
      public GameObject GetGrassPlatform()                                               
      {                                                                                   
         for (int i = 0; i < grassPlatformList.Count; i++)                               
         {                                                                                
            if (grassPlatformList[i].activeInHierarchy == false)                         
            {                                                                             
               return grassPlatformList[i];                                              
            }                                                                             
                                                                                      
         }
         int ran = Random.Range(0, _vars.grassPlatformGroup.Count);
         return InstaniateObject(_vars.grassPlatformGroup[ran], ref grassPlatformList);        
      }         
      /// <summary>                                                                    
      /// 获取冬季平台                                                                       
      /// </summary>                                                                   
      /// <returns></returns>                                                          
      public GameObject GetWinterPlatform()                                               
      {                                                                                   
         for (int i = 0; i < winterPlatformList.Count; i++)                               
         {                                                                                
            if (winterPlatformList[i].activeInHierarchy == false)                         
            {                                                                             
               return winterPlatformList[i];                                              
            }                                                                             
                                                                                      
         }
         int ran = Random.Range(0, _vars.winterPlatformGroup.Count);
         return InstaniateObject(_vars.winterPlatformGroup[ran], ref winterPlatformList);        
      }  
      
      /// <summary>                                                                    
      /// 获取左边钉子平台                                                                       
      /// </summary>                                                                   
      /// <returns></returns>                                                          
      public GameObject GetLeftSpikePlatform()                                               
      {                                                                                   
         for (int i = 0; i < spikePlatformLeftList.Count; i++)                               
         {                                                                                
            if (spikePlatformLeftList[i].activeInHierarchy == false)                         
            {                                                                             
               return spikePlatformLeftList[i];                                              
            }                                                                             
                                                                                      
         }
         return InstaniateObject(_vars.spikePlatformLeft, ref spikePlatformLeftList);        
      }  
      /// <summary>                                                                    
      /// 获取右边钉子平台                                                                       
      /// </summary>                                                                   
      /// <returns></returns>                                                          
      public GameObject GetRightSpikePlatform()                                               
      {                                                                                   
         for (int i = 0; i < spikePlatformRightList.Count; i++)                               
         {                                                                                
            if (spikePlatformRightList[i].activeInHierarchy == false)                         
            {                                                                             
               return spikePlatformRightList[i];                                              
            }                                                                             
                                                                                      
         }
         return InstaniateObject(_vars.spikePlatformRight, ref spikePlatformRightList);        
      }  
      /// <summary>                                                                    
      /// 获取死亡特效                                                                       
      /// </summary>                                                                   
      /// <returns></returns>                                                          
      public GameObject GetDeathEffect()                                               
      {                                                                                   
         for (int i = 0; i < deathEffectList.Count; i++)                               
         {                                                                                
            if (deathEffectList[i].activeInHierarchy == false)                         
            {                                                                             
               return deathEffectList[i];                                              
            }                                                                             
                                                                                      
         }
         return InstaniateObject(_vars.deathEffect, ref deathEffectList);        
      }  
      
      //获取钻石
      public GameObject GetDiamond()
      {
         for (int i = 0; i < diamondList.Count; i++)                               
         {                                                                                
            if (diamondList[i].activeInHierarchy == false)                         
            {                                                                             
               return diamondList[i];                                              
            }                                                                             
                                                                                      
         }
         return InstaniateObject(_vars.diamondPre, ref diamondList);       
      }

}
