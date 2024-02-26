using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeManager : MonoBehaviour {
    public event Action<long> GoldAmountChanged;
    public event Action<long> GoldIncomeChanged;
    IEnumerator timeCountEnumerator;
    [SerializeField]
    private long goldAmount = 50;
    
    public long GoldAmount {
        get { return goldAmount; }
        set {
            goldAmount = value;
            // Debug.Log("Change gold from "+GetComponentInParent<Player>().playerTag+" amount to: "+value);
            GoldAmountChanged?.Invoke(goldAmount);
        }
    }
    [SerializeField]
    private long goldIncome = 3;
    public long GoldIncome {
        get {
            return goldIncome;
        }
        set {
            goldIncome = value;
            GoldIncomeChanged?.Invoke(goldIncome);
        }
    }
    // public static IncomeManager Instance { get; private set; }
    public int interval = 4;
    void Awake() {
        // if (Instance == null) {
        //     Instance = this;
        // } else {
        //     Debug.Log("Warning: multiple " + this + " in scene!");
        // }
    }
    void Start() {
        timeCountEnumerator = RepeatWaitTimer();
        StartCoroutine(timeCountEnumerator);
        GoldAmountChanged?.Invoke(goldAmount);
        GoldIncomeChanged?.Invoke(goldIncome);
    }

    private void AddIncome() {
        GoldAmount += goldIncome;
    }

    IEnumerator RepeatWaitTimer() {
        while (true) {
            yield return new WaitForSeconds(interval);
            AddIncome();
        }
    }
}
