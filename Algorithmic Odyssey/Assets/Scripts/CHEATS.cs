using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CHEATS : MonoBehaviour
{
    public bool playerIsClose;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && playerIsClose)
        {
            CoinsManager.iron += 100;
            PlayerPrefs.SetInt("Iron", CoinsManager.iron);
            PlayerPrefs.Save();
            CoinsManager.UpdateIron();
            CoinsManager.stone += 100;
            PlayerPrefs.SetInt("Stone", CoinsManager.stone);
            PlayerPrefs.Save();
            CoinsManager.UpdateStone();
            CoinsManager.treelog += 100;
            PlayerPrefs.SetInt("treeLog", CoinsManager.treelog);
            PlayerPrefs.Save();
            CoinsManager.UpdateTreeLog();
        }
        if (Keyboard.current.qKey.wasPressedThisFrame && playerIsClose)
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
            PlayerPrefs.SetInt("treeLog", CoinsManager.treelog);
            PlayerPrefs.Save();
            CoinsManager.UpdateTreeLog();
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
        }
    }
}
