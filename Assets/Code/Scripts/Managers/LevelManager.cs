using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int startingMoney;
    [SerializeField] private int startingHealth;
    [SerializeField] private int startingCorruption; // Added corruption

    [Header("UI Bars")]
    [SerializeField] private Slider currentHPBar;
    [SerializeField] private Slider delayedHPBar;
    [SerializeField] private Slider corruptionBar; // New Corruption bar
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("Enemy Paths")]
    [SerializeField] public List<WaypointList> paths = new List<WaypointList>();
    public static LevelManager main;

    [Header("Current Stats")]
    public Transform startPoint;
    public int money;
    public int health;
    public int maxHealth;
    public int corruptionPercentage;


    private void Awake()
    {
        main = this;
        maxHealth = startingHealth;
    }

    void Start()
    {
        money = startingMoney;
        health = startingHealth;
        UpdateHealthUI();
        UpdateCorruptionUI();
        UpdateGoldUI();

    }
    private void UpdateGoldUI()
    {
        goldText.text = money.ToString();
    }
    public List<Transform> GetPath(int pathIndex)
    {
        if (paths != null && pathIndex >= 0 && pathIndex < paths.Count)
        {
            return paths[pathIndex].waypoints;
        }
        return new List<Transform>();
    }
    public bool isPause = false;
    public void Pause(){
        if (!isPause) 
        {
            Time.timeScale = 0f;
            isPause = true;
        }
        else 
        {
            Time.timeScale = 1f;
            isPause = false;
        }
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        corruptionPercentage += 1;
        
        Debug.Log(health);
        if (health <= 0){
            Debug.Log("Game Over");
            FindObjectOfType<GameOverUi>().ShowGameOver();
        }
        // Update instant HP bar
        UpdateHealthUI();
        UpdateCorruptionUI();
    }

    public bool SpendCurrency(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        Debug.Log("Not Enough Money");
        return false;
    }
    public void IncreaseCurrency(int amount)
    {
        money += amount;
        UpdateGoldUI();
    }
    public void IncreaseCorruption(int amount)
    {
        corruptionPercentage += amount;
        if (corruptionPercentage > 100) corruptionPercentage = 100;

        UpdateCorruptionUI();
    }

    public void DecreaseCorruption(int amount)
    {
        corruptionPercentage -= amount;
        if (corruptionPercentage < 0) corruptionPercentage = 0;

        UpdateCorruptionUI();
    }

    private void UpdateHealthUI()
    {
        float healthPercent = (float)health / maxHealth;
        currentHPBar.value = healthPercent;
    }

    private void UpdateCorruptionUI()
    {
        float corruptionPercent = (float)corruptionPercentage / 100f;
        corruptionBar.value = corruptionPercent;
    }
}
[System.Serializable]
public class WaypointList
{
    public List<Transform> waypoints;
}