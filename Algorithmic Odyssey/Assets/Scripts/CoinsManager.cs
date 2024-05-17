using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public const string Coins = "Coins";
    public static int coins = 0;
    public const string TreeLog = "TreeLog";
    public static int treelog = 0;
    public const string Iron = "Iron";
    public static int iron = 0;
    public const string Stone = "Stone";
    public static int stone = 0;
 
    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        treelog = PlayerPrefs.GetInt("TreeLog", 0);
        iron = PlayerPrefs.GetInt("Iron", 0);
        stone = PlayerPrefs.GetInt("Stone", 0);
        
    }
 
    // Update is called once per frame
    void Update()
    {
 
    }
    public static void UpdateCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
        coins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.Save();
    }
    public static void UpdateTreeLog()
    {
        PlayerPrefs.SetInt("TreeLog", treelog);
        treelog = PlayerPrefs.GetInt("TreeLog");
        PlayerPrefs.Save();
    }
    public static void UpdateIron()
    {
        PlayerPrefs.SetInt("Iron", iron);
        iron = PlayerPrefs.GetInt("Iron");
        PlayerPrefs.Save();
    }
    public static void UpdateStone()
    {
        PlayerPrefs.SetInt("Stone", stone);
        stone = PlayerPrefs.GetInt("Stone");
        PlayerPrefs.Save();
    }

    public static void RestartMats()
    {
        CoinsManager.iron = 0;
        PlayerPrefs.SetInt("Iron", CoinsManager.iron);
        PlayerPrefs.Save();
        CoinsManager.UpdateIron();
        CoinsManager.stone = 0;
        PlayerPrefs.SetInt("Stone", CoinsManager.stone);
        PlayerPrefs.Save();
        CoinsManager.UpdateStone();
        CoinsManager.treelog = 0;
        PlayerPrefs.SetInt("TreeLog", CoinsManager.treelog);
        PlayerPrefs.Save();
        CoinsManager.UpdateTreeLog();
        CoinsManager.coins = 0;
        PlayerPrefs.SetInt("Coins", CoinsManager.coins);
        PlayerPrefs.Save();
        CoinsManager.UpdateCoins();
    }
}
