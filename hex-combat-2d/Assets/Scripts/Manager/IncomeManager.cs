using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeManager : MonoBehaviour {
    public static event Action<long> GoldAmountChanged;
    public static event Action<long> GoldIncomeChanged;
    IEnumerator timeCountEnumerator;
    private long goldAmount = 50;
    public long GoldAmount {
        get { return goldAmount; }
        set {
            goldAmount = value;
            GoldAmountChanged?.Invoke(goldAmount);
        }
    }
    public long goldIncome = 3;
    public long GoldIncome {
        get {
            return goldIncome;
        }
        set {
            goldIncome = value;
            GoldIncomeChanged?.Invoke(goldIncome);
        }
    }
    public static IncomeManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("Warning: multiple " + this + " in scene!");
        }
    }
    void Start() {
        timeCountEnumerator = RepeatWaitTimer();
        StartCoroutine(timeCountEnumerator);
        GoldAmountChanged?.Invoke(goldAmount);
        GoldIncomeChanged?.Invoke(goldIncome);
    }

    // Update is called once per frame
    void Update() {

    }

    private void AddIncome() {
        GoldAmount += 3;
    }

    IEnumerator RepeatWaitTimer() {
        while (true) {
            yield return new WaitForSeconds(5);
            AddIncome();
        }
    }
}
