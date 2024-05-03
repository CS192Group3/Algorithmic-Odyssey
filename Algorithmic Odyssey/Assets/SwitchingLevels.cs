using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingLevels : MonoBehaviour {

    // Storing different levels'
    public GameObject[] levels;
    // Counting current level
    int current_level = 0;
    public GameObject NotEnoughPanel;
    public GameObject dialoguePanel;
    // int numberOfTreeLog = CoinsManager.treelog;

    public void Upgrade(){
        int numberOfTreeLog = CoinsManager.treelog;
        int numberOfIron = CoinsManager.iron;
        int numberOfStone = CoinsManager.stone;
        
        if (numberOfTreeLog < 10 || numberOfIron < 10 || numberOfStone < 10){
            Debug.Log("Not enough coins");
            Debug.Log("You have " + numberOfTreeLog + " coins");
            Debug.Log("You have " + numberOfIron + " coins");
            Debug.Log("You have " + numberOfStone + " coins");
            NotEnoughPanel.SetActive(true);
            dialoguePanel.SetActive(false);
            return;
        }

        Debug.Log("You have " + numberOfTreeLog + " coins");
        // Check if we're safe to upgrade (We haven't reached the last level)
        if(current_level < levels.Length - 1){
            // Increase current level
            current_level++;
            // Switch to the updated level
            SwitchObject(current_level);
            // Decrease the number of logs
            CoinsManager.treelog -= 10;
            CoinsManager.UpdateTreeLog();
            // Decrease the number of iron
            CoinsManager.iron -= 10;
            CoinsManager.UpdateIron();
            // Decrease the number of stone
            CoinsManager.stone -= 10;
            CoinsManager.UpdateStone();
        }
    }

    void SwitchObject(int lvl){
        // Count from zero the last level in our array
        for (int i = 0; i < levels.Length; i++){
            // Check if we're in the desired level, then activate
            if (i == lvl)
                levels[i].SetActive(true);
            else
                //Otherwise, hdie it
                levels[i].SetActive(false);
        }
    }
}