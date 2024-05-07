using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SubMarineSelection : MonoBehaviour
{
    [Header ("Navigatiion Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Play/Buy Buttons")]
    [SerializeField] private Button play;
    [SerializeField] private Button buy;
    [SerializeField] private Text priceText;

    [Header("Car Attributes")]
    [SerializeField] private int[] carPrices;
    private int currentCar;

    private void Start()
    {
        currentCar = SaveManager.instance.currentCar;
        SelectCar(currentCar);
    }
    private void SelectCar(int _index)
    {
        previousButton.interactable = (_index !=0);
        nextButton.interactable = (_index != transform.childCount-1);
        for (int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i== _index);
        }

        UpdateUI(_index);
    }

    private void UpdateUI(int _index)
    {
        if (SaveManager.instance.carsUnlocked[_index])
        {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        else
        {
            play.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            priceText.text = carPrices[_index] + "$";

            
        }
    }

    private void Update()
    {
        if(buy.gameObject.activeInHierarchy)
        {
            //check if he has enough money
            buy.interactable = (SaveManager.instance.money >= carPrices[currentCar]);
        }
        
    }
    public void ChangeCar(int _change)
    {
        currentCar += _change;

        SaveManager.instance.currentCar = currentCar;
        SaveManager.instance.Save();
        SelectCar(currentCar);
    }

    public void BuyCar()
    {
        SaveManager.instance.money -= carPrices[currentCar];
        SaveManager.instance.carsUnlocked[currentCar] = true;
        SaveManager.instance.Save();
        UpdateUI(currentCar);

    }

    
}
