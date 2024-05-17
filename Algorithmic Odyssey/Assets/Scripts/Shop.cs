using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject notEnough;

    public GameObject dialoguePanel;
    public GameObject shopPanel;

    private bool playerIsClose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialoguePanel != null && shopPanel.activeSelf)
        {
            dialoguePanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (shopPanel != null && shopPanel.activeSelf)
            {
                shopPanel.SetActive(false);
                if (dialoguePanel != null)
                {
                    dialoguePanel.SetActive(false);
                }
            }
        }
    }


    // buy materials
    public void BuyMaterial(string materialType, int amount, int cost)
    {
        // coin check
        if (CoinsManager.coins >= cost)
        {
            CoinsManager.coins -= cost;
            PlayerPrefs.SetInt("Coins", CoinsManager.coins);
            PlayerPrefs.Save();
            CoinsManager.UpdateCoins();

            switch (materialType)
            {
                case "Iron":
                    CoinsManager.iron += amount;
                    PlayerPrefs.SetInt("Iron", CoinsManager.iron);
                    PlayerPrefs.Save();
                    CoinsManager.UpdateIron();
                    break;
                case "TreeLog":
                    CoinsManager.treelog += amount;
                    PlayerPrefs.SetInt("TreeLog", CoinsManager.treelog);
                    PlayerPrefs.Save();
                    CoinsManager.UpdateTreeLog();
                    break;
                case "Stone":
                    CoinsManager.stone += amount;
                    PlayerPrefs.SetInt("Stone", CoinsManager.stone);
                    PlayerPrefs.Save();
                    CoinsManager.UpdateStone();
                    break;
            }

        }
        else
        {
            StartCoroutine(ShowNotEnoughCoinsPanel());
        }
    }

    private IEnumerator ShowNotEnoughCoinsPanel()
    {
        if (notEnough != null)
        {
            notEnough.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            notEnough.SetActive(false);
        }
    }


    public void Buy5Iron()
    {
        BuyMaterial("Iron", 5, 5);
    }

    public void Buy10Iron()
    {
        BuyMaterial("Iron", 10, 10);
    }

    public void Buy5Logs()
    {
        BuyMaterial("TreeLog", 5, 5);
    }

    public void Buy10Logs()
    {
        BuyMaterial("TreeLog", 10, 10);
    }

    public void Buy5Stone()
    {
        BuyMaterial("Stone", 5, 5);
    }

    public void Buy10Stone()
    {
        BuyMaterial("Stone", 10, 10);
    }

    public void OpenShop()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
        }
    }

    public void CloseShop()
    {

        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            CloseShop();
        }
    }
}
