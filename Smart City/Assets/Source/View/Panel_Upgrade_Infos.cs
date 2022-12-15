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

    //Prix amelioration batiment
    uint upgradeCost;
    private TextMeshProUGUI upgradeText;

    //Revenus en + pour le batiment apres amelioration (Gain + NvRevenus)
    uint newIncome = 0;
    private TextMeshProUGUI newIncomeText;


    // CONSTRUCTEUR
    /// <summary>
    /// Constructeur
    /// </summary>
    private void Awake()
    {
        Debug.Log("Awake");
        initGameHandler();
        initInfos();
        updatePanelInfos();
    }


    // Activation
    /// <summary>
    /// Activation
    /// </summary>

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        initGameHandler();
        initInfos();
        updatePanelInfos();
    }

    /* initGameHandler : fonction 
     Permet d'initialiser le GameHandler 
    */
    /// <summary>
    /// Permet d'initialiser le GameHandler
    /// </summary>
    
    private void initGameHandler()
    {
        if(gh == null)
        {
            gh = FindObjectOfType<GameHandler>();
        }
    }

    /* initInfos : fonction 
    Permet d'initialiser les informations
    */
    /// <summary>
    /// Permet d'initialiser les informations
    /// </summary>
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

    /* updatePanelInfos : fonction 
    Permet de mettre à jour les informations du panel
    */
    /// <summary>
    /// Permet de mettre à jour les informations du panel
    /// </summary>
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


    /* clickUpdate : fonction 
    Permet de modifier les informations quand il y a un clic
    */
    /// <summary>
    /// Permet de modifier les informations quand il y a un clic
    /// </summary>
    public void clickUpdate()
    {
        initInfos();
        updatePanelInfos();
    }
}
