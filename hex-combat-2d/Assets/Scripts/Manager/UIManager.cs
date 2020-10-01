using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Text goldAmountText;
    public Text goldIncomeText;
    public Button buildButton;
    private IncomeManager incomeManager;

    public static UIManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("Warning: multiple " + this + " in scene!");
        }
    }

    void Start(){
        incomeManager = GameManager.Instance.GetComponent<PlayerController>().player.incomeManager;
        incomeManager.GoldAmountChanged += OnGoldAmountChanged;
        incomeManager.GoldIncomeChanged += OnGoldIncomeChanged;
    }
    public void OnGoldAmountChanged(long gold) {
        goldAmountText.text = "Gold: " + gold;
    }
    public void OnGoldIncomeChanged(long goldIncome) {
        goldIncomeText.text = "Income: " + goldIncome;
    }

    void OnDestroy() {
        incomeManager.GoldAmountChanged -= OnGoldAmountChanged;
        incomeManager.GoldIncomeChanged -= OnGoldIncomeChanged;
    }
}
