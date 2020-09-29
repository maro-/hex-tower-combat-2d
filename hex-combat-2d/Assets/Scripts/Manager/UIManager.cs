using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text goldAmountText;
    public Text goldIncomeText;
    public Button buildButton;

    public static UIManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("Warning: multiple " + this + " in scene!");
        }
        IncomeManager.GoldAmountChanged += OnGoldAmountChanged;
        IncomeManager.GoldIncomeChanged += OnGoldIncomeChanged;
    }

    public void OnGoldAmountChanged(long gold) {
        goldAmountText.text = "Gold: " + gold;
    }
    public void OnGoldIncomeChanged(long goldIncome) {
        goldIncomeText.text = "Income: " + goldIncome;
    }

    void OnDestroy() {
        IncomeManager.GoldAmountChanged -= OnGoldAmountChanged;
        IncomeManager.GoldIncomeChanged -= OnGoldIncomeChanged;
    }
}
