using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BgTheme : MonoBehaviour
{
    private SpriteRenderer m_SprintRenderer;
    private ManagerVars vars;

    private void Awake()
    {
        m_SprintRenderer = GetComponent<SpriteRenderer>();
        vars = ManagerVars.GetManagerVars();
        int ranValue = Random.Range(0, vars.bgThemeSpriteList.Count);
        m_SprintRenderer.sprite = vars.bgThemeSpriteList[ranValue];

    }
}
