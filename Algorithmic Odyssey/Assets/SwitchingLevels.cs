using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingManager
{
    public class SwitchingLevels : MonoBehaviour
    {

        // Storing different levels'
        public GameObject[] levels;
        // Counting current level
        int current_level = 0;
        public GameObject NotEnoughPanel;
        public GameObject dialoguePanel;
        public GameObject maxUpgradePanel;
        // int numberOfTreeLog = CoinsManager.treelog;

        private string CurrentLevelKey => gameObject.name + "_CurrentLevel";

        private void Start()
        {
            // Load the current level from PlayerPrefs
            current_level = PlayerPrefs.GetInt(CurrentLevelKey, 0); // Default to 0 if not set
            // Switch to the saved level
            SwitchObject(current_level);
        }

        public void Upgrade()
        {
            int numberOfTreeLog = CoinsManager.treelog;
            int numberOfIron = CoinsManager.iron;
            int numberOfStone = CoinsManager.stone;

            if (numberOfTreeLog < 10 || numberOfIron < 10 || numberOfStone < 10)
            {
                Debug.Log("Not enough coins");
                Debug.Log("You have " + numberOfTreeLog + " coins");
                Debug.Log("You have " + numberOfIron + " coins");
                Debug.Log("You have " + numberOfStone + " coins");
                NotEnoughPanel.SetActive(true);
                dialoguePanel.SetActive(false);
                return;
            }

            Debug.Log("You have " + numberOfTreeLog + " coins");

            // Check if we cannot upgrade anymore (We've reached the last level)
            if (current_level == levels.Length - 1)
            {
                maxUpgradePanel.SetActive(true);
                dialoguePanel.SetActive(false);
                return;
            }
            // Check if we're safe to upgrade (We haven't reached the last level)
            if (current_level < levels.Length - 1)
            {
                // Increase current level
                current_level++;
                // Save the new current level to PlayerPrefs
                PlayerPrefs.SetInt(CurrentLevelKey, current_level);
                PlayerPrefs.Save();
                // Switch to the updated level
                SwitchObject(current_level);
                // Hide the mailbox panel
                dialoguePanel.SetActive(false);
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

        public void RestartGame()
        {
            // Reset current level to 0 and clear saved level for all objects with SwitchingLevels script
            var objectsWithSwitchingLevels = FindObjectsOfType<SwitchingLevels>();
            foreach (var obj in objectsWithSwitchingLevels)
            {
                obj.current_level = 0;
                PlayerPrefs.DeleteKey(obj.CurrentLevelKey);
                obj.SwitchObject(0);
            }

            // Switch to level 0 for this object
            current_level = 0;
            PlayerPrefs.DeleteKey(CurrentLevelKey);
            SwitchObject(0);
        }

        void SwitchObject(int lvl)
        {
            // Activate the desired level and deactivate others
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].SetActive(i == lvl);
            }
        }
    }
}