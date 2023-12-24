using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreatManagerVarsContainer" )]
public class ManagerVars : ScriptableObject
{
   public static ManagerVars GetManagerVars()
   {
      return Resources.Load<ManagerVars>("ManagerVarsContainer");
   }
   public List<Sprite> bgThemeSpriteList = new List<Sprite>();
   public List<Sprite> plantformThemeSpriteList = new List<Sprite>();
   public GameObject normalPlatformPre;
   public List<GameObject> commonPlatformGroup = new List<GameObject>();
   public List<GameObject> grassPlatformGroup = new List<GameObject>();
   public List<GameObject> winterPlatformGroup = new List<GameObject>();
   public GameObject spikePlatformLeft;
   public GameObject spikePlatformRight;
   public GameObject deathEffect;
   public GameObject diamondPre;
   public float nextXPos = 0.554f, nextYPos = 0.645f;
   public GameObject characterpre;
}
