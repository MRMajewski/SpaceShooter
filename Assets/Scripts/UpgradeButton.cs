﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{

    private AudioSource AudioSource;

    [SerializeField]
    AudioClip BuyClip;

    IUpgradable Upgradable;
    GameManager GameManager;
    Button Button;

    [SerializeField]
    string Text;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>(); 
        AudioSource.clip = BuyClip;


        GameManager = FindObjectOfType<GameManager>();
        Button = GetComponent<Button>();

        var waveController = FindObjectOfType<AsteroidWaveController>();

        waveController.OnWaveStarted += _ => gameObject.SetActive(false);
        waveController.OnWaveEnded += _ => gameObject.SetActive(true);

        GameManager.OnMoneyChanged += _ => RefreshButton();
    }

    public void Configure(IUpgradable upgradable)
    {
        Upgradable = upgradable;
        Button.onClick.AddListener(Upgrade);
    }

    private void Upgrade()
    {
        if (!CanUpgrade())
            return;

        Upgradable.Upgrade();
        GameManager.Money -= Upgradable.UpgradeCost;
        AudioSource.Play();
        RefreshButton();
    }

    private void RefreshButton()
    {
        var canUpgrade = CanUpgrade();

        Button.enabled = canUpgrade;
        Button.GetComponent<Image>().color = canUpgrade ? Color.white : Color.gray;

        var textComponent = Button.GetComponentInChildren<Text>();

        textComponent.text = string.Format("{0} ({1})", Text, Upgradable.CurrentLevel);

        if (!IsMaximumLevel())
            textComponent.text += string.Format("\n{0}$", Upgradable.UpgradeCost);
    }

    private bool CanUpgrade()
    {
        return !IsMaximumLevel() && IsMoneyEnough();
    }

    private bool IsMaximumLevel()
    {
        return Upgradable.CurrentLevel >= Upgradable.MaxLevel;
    }

    private bool IsMoneyEnough()
    {
        return Upgradable.UpgradeCost <= GameManager.Money;
    }
}
