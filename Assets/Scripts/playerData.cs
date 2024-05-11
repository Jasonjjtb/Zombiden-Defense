using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerdata : MonoBehaviour
{
    public float startingHealth;
    public float startingMoney;
    [System.NonSerialized]
    public float currHealth;
    [System.NonSerialized]
    public float currMoney;
    private float maxHealth;
    public TextMeshProUGUI healthGUI;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = startingHealth;
        currHealth = startingHealth;
        currMoney = startingMoney;
    }

    // Update is called once per frame
    void Update()
    {
        healthGUI.text = "Health: " + currHealth.ToString() + "/" + maxHealth.ToString() + "\n" + "Money: $" + currMoney;
    }
}
