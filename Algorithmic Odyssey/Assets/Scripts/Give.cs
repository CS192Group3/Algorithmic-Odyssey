using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Give : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Press()
    {
        CoinsManager.coins += 710;
        CoinsManager.UpdateCoins();
    }
    public void logs()
    {
        CoinsManager.treelog += 10;
        CoinsManager.UpdateTreeLog();
    }
    public void iron()
    {
        CoinsManager.iron += 20;
        CoinsManager.UpdateIron();
    }
    public void stone()
    {
        CoinsManager.stone += 30;
        CoinsManager.UpdateStone();
    }
}
