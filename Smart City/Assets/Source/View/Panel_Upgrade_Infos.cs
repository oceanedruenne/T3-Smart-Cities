using Source.Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Panel_Upgrade_Infos : MonoBehaviour
{
    GameHandler gh = null;

    //Tableau des infos
    TextMeshProUGUI[] infos = null;

    //Prix amélioration bâtiment
    uint upgradeCost;
    private TextMeshProUGUI upgradeText;

    //Revenus en + pour le bâtiment après amélioration (Gain + NvRevenus)
    uint newIncome = 0;
    private TextMeshProUGUI newIncomeText;

    private void Awake()
    {
        Debug.Log("Awake");
        initGameHandler();
        initInfos();
        updatePanelInfos();
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        initGameHandler();
        initInfos();
        updatePanelInfos();
    }

    private void initGameHandler()
    {
        if(gh == null)
        {
            gh = FindObjectOfType<GameHandler>();
        }
    }

    private void initInfos()
    {
        if(infos == null)
        {
            infos = GetComponentsInChildren<TextMeshProUGUI>();
            upgradeText = infos[0];
            newIncomeText = infos[1];
        }

        Debug.Log("X = " + gh.posx + " | Y = " + gh.posy);
        if(gh.posx >= 0 && gh.posy >= 0 && gh.map.getUnderMaxLevel((uint)gh.posx, (uint)gh.posy))
        {

            upgradeCost = gh.map.getUpgradeCostAt((uint)gh.posx, (uint)gh.posy);
            newIncome = gh.map.getIncomeAfterUpgradeAt((uint)gh.posx, (uint)gh.posy);

        }
        else
        {
            upgradeCost = 0;
            newIncome = 0;
        }
    }

    private void updatePanelInfos()
    {
        if(upgradeCost == 0 && newIncome == 0)
        {
            upgradeText.text = "Prix :  N/A";
            newIncomeText.text = "Gain :  N/A";
        }
        else
        {
            upgradeText.text = "Prix :  " + upgradeCost.ToString();
            newIncomeText.text = "Gain : +" + newIncome.ToString();
        }    
    }

    public void clickUpdate()
    {
        initInfos();
        updatePanelInfos();
    }
}
