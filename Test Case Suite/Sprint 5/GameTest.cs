using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Interactions;

namespace GameTest
{
    public class TestSuite : InputTestFixture
    {
        Mouse mouse;
        Keyboard keyboard;

        public override void Setup()
        {
            Time.timeScale = 1f;
            base.Setup();
            SceneManager.LoadScene("Scenes/GameScene");
            mouse = InputSystem.AddDevice<Mouse>();
            keyboard = InputSystem.AddDevice<Keyboard>();
        }

        public void ClickAction(GameObject uiElement)
        {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector2 screenPos = uiElement.GetComponent<RectTransform>().position;
            Set(mouse.position, screenPos);
            Click(mouse.leftButton);
        }

        [UnityTest, Order(1)]
        public IEnumerator RestartTest()
        {
            yield return new WaitForSeconds(2f);
            GameObject pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
            GameObject settingsPanel = GameObject.Find("Canvas").transform.Find("SettingsPanel").gameObject;
            GameObject pauseButton = GameObject.Find("Canvas/PauseButton");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("GameScene"));
            Assert.IsFalse(pauseMenu.activeSelf);
            Assert.IsFalse(settingsPanel.activeSelf);

            ClickAction(pauseButton);
            yield return new WaitUntil(() => pauseMenu.activeSelf);

            Assert.IsTrue(pauseMenu.activeSelf);
            Assert.IsFalse(settingsPanel.activeSelf);

            GameObject settingsButton = GameObject.Find("Canvas/PauseMenu/Button Container/SettingsButton");

            ClickAction(settingsButton);
            yield return new WaitUntil(() => !pauseMenu.activeSelf);

            Assert.IsFalse(pauseMenu.activeSelf);
            Assert.IsTrue(settingsPanel.activeSelf);

            GameObject restartButton = GameObject.Find("Canvas/SettingsPanel/Button Container/Restart Game");

            ClickAction(restartButton);
            yield return new WaitUntil(() => !settingsPanel.activeSelf);
            yield return new WaitForSeconds(1f);

            Assert.AreEqual(0, PlayerPrefs.GetInt("Iron"));
            Assert.AreEqual(0, PlayerPrefs.GetInt("TreeLog"));
            Assert.AreEqual(0, PlayerPrefs.GetInt("Stone"));

            yield return new WaitForSeconds(1f);

            GameObject building3 = GameObject.Find("Grid/BUILDINGS/Building 3");

            yield return new WaitForSeconds(1f);

            GameObject level1 = GameObject.Find("Grid/BUILDINGS/Building 3/Level 1");
            GameObject level2 = GameObject.Find("Grid/BUILDINGS/Building 3/Level 2");
            GameObject level3 = GameObject.Find("Grid/BUILDINGS/Building 3/Level 3");

            Assert.IsTrue(level1.activeSelf);
            Assert.IsFalse(level2.activeSelf);
            Assert.IsFalse(level3.activeSelf);
        }

        [UnityTest, Order(2)]
        public IEnumerator BuildingUpgrades()
        {
            yield return new WaitForSeconds(1f);

            string building2Key = "Building 2_CurrentLevel";
            PlayerPrefs.SetInt(building2Key, 0);
            PlayerPrefs.Save();

            yield return new WaitForSeconds(1f);

            GameObject building2 = GameObject.Find("Grid/BUILDINGS/Building 2");

            yield return new WaitForSeconds(1f);

            Transform level1 = building2.transform.Find("Level 1");
            Transform level2 = building2.transform.Find("Level 2");
            Transform level3 = building2.transform.Find("Level 3");

            level1.gameObject.SetActive(true);
            level2.gameObject.SetActive(false);
            level3.gameObject.SetActive(false);

            yield return new WaitForSeconds(1f);
            CoinsManager.iron += 100;
            PlayerPrefs.SetInt("Iron", CoinsManager.iron);
            PlayerPrefs.Save();
            CoinsManager.UpdateIron();
            CoinsManager.stone += 100;
            PlayerPrefs.SetInt("Stone", CoinsManager.stone);
            PlayerPrefs.Save();
            CoinsManager.UpdateStone();
            CoinsManager.treelog += 100;
            PlayerPrefs.SetInt("TreeLog", CoinsManager.treelog);
            PlayerPrefs.Save();
            CoinsManager.UpdateTreeLog();

            yield return new WaitForSeconds(2f);
            GameObject player = GameObject.Find("Player");
            GameObject mailbox = GameObject.Find("MAILBOX 2");

            player.transform.position = mailbox.transform.position;

            yield return new WaitUntil(() => player.transform.position == mailbox.transform.position);
            yield return new WaitForSeconds(2f);

            Assert.AreEqual(player.transform.position, mailbox.transform.position);

            Press(keyboard[Key.E]);
            yield return null;
            Release(keyboard[Key.E]);
            yield return null;

            yield return new WaitUntil(() => GameObject.Find("Canvas/MailboxPanel 2/DialogueChoices/Yes") != null);
            yield return new WaitForSeconds(7f);

            GameObject sendButton = GameObject.Find("Canvas/MailboxPanel 2/DialogueChoices/Yes");

            ClickAction(sendButton);
            yield return new WaitUntil(() => GameObject.Find("Grid/BUILDINGS/Building 2/Level 2") != null);

            Assert.IsNotNull(GameObject.Find("Grid/BUILDINGS/Building 2/Level 2"));

            yield return new WaitForSeconds(2f);

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

            Press(keyboard[Key.E]);
            yield return null;
            Release(keyboard[Key.E]);
            yield return null;

            yield return new WaitUntil(() => GameObject.Find("Canvas/MailboxPanel 2/DialogueChoices/Yes") != null);
            yield return new WaitForSeconds(7f);
            sendButton = GameObject.Find("Canvas/MailboxPanel 2/DialogueChoices/Yes");

            ClickAction(sendButton);

            yield return new WaitUntil(() => GameObject.Find("Canvas/NotEnough Panel") != null);

            Assert.IsNotNull(GameObject.Find("Canvas/NotEnough Panel"));

            yield return new WaitForSeconds(2f);

            string building3Key = "Building 3_CurrentLevel";
            PlayerPrefs.SetInt(building3Key, 0);
            PlayerPrefs.Save();

            yield return new WaitForSeconds(1f);

            GameObject building3 = GameObject.Find("Grid/BUILDINGS/Building 3");

            yield return new WaitForSeconds(1f);

            level1 = building3.transform.Find("Level 1");
            level2 = building3.transform.Find("Level 2");
            level3 = building3.transform.Find("Level 3");

            level1.gameObject.SetActive(true);
            level2.gameObject.SetActive(false);
            level3.gameObject.SetActive(false);

            yield return new WaitForSeconds(1f);
            CoinsManager.iron += 100;
            PlayerPrefs.SetInt("Iron", CoinsManager.iron);
            PlayerPrefs.Save();
            CoinsManager.UpdateIron();
            CoinsManager.stone += 100;
            PlayerPrefs.SetInt("Stone", CoinsManager.stone);
            PlayerPrefs.Save();
            CoinsManager.UpdateStone();
            CoinsManager.treelog += 100;
            PlayerPrefs.SetInt("TreeLog", CoinsManager.treelog);
            PlayerPrefs.Save();
            CoinsManager.UpdateTreeLog();

            yield return new WaitForSeconds(2f);
            mailbox = GameObject.Find("MAILBOX 3");

            player.transform.position = mailbox.transform.position;

            yield return new WaitUntil(() => player.transform.position == mailbox.transform.position);
            yield return new WaitForSeconds(2f);

            Assert.AreEqual(player.transform.position, mailbox.transform.position);

            Press(keyboard[Key.E]);
            yield return null;
            Release(keyboard[Key.E]);
            yield return null;

            yield return new WaitUntil(() => GameObject.Find("Canvas/MailboxPanel 3/DialogueChoices/Yes") != null);
            yield return new WaitForSeconds(7f);

            sendButton = GameObject.Find("Canvas/MailboxPanel 3/DialogueChoices/Yes");

            ClickAction(sendButton);
            yield return new WaitUntil(() => GameObject.Find("Grid/BUILDINGS/Building 3/Level 2") != null);

            Assert.IsNotNull(GameObject.Find("Grid/BUILDINGS/Building 3/Level 2"));

            yield return new WaitForSeconds(2f);

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

            Press(keyboard[Key.E]);
            yield return null;
            Release(keyboard[Key.E]);
            yield return null;

            yield return new WaitUntil(() => GameObject.Find("Canvas/MailboxPanel 3/DialogueChoices/Yes") != null);
            yield return new WaitForSeconds(7f);
            sendButton = GameObject.Find("Canvas/MailboxPanel 3/DialogueChoices/Yes");

            ClickAction(sendButton);

            yield return new WaitUntil(() => GameObject.Find("Canvas/NotEnough Panel") != null);

            Assert.IsNotNull(GameObject.Find("Canvas/NotEnough Panel"));

            yield return new WaitForSeconds(2f);
        }

        [UnityTest, Order(3)]
        public IEnumerator ShopTest()
        {
            yield return new WaitForSeconds(2f);

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
            CoinsManager.coins += 100;
            PlayerPrefs.SetInt("Coins", CoinsManager.coins);
            PlayerPrefs.Save();
            CoinsManager.UpdateCoins();

            yield return new WaitForSeconds(2f);
            GameObject player = GameObject.Find("Player");
            GameObject shop = GameObject.Find("SHOP NPC");

            player.transform.position = shop.transform.position;

            yield return new WaitForSeconds(2f);

            Press(keyboard[Key.E]);
            yield return null;
            Release(keyboard[Key.E]);
            yield return null;

            yield return new WaitUntil(() => GameObject.Find("Canvas/DialoguePanel 7/DialogueChoices/Yes") != null);
            yield return new WaitForSeconds(3f);

            GameObject openShop = GameObject.Find("Canvas/DialoguePanel 7/DialogueChoices/Yes");

            ClickAction(openShop);
            yield return new WaitUntil(() => GameObject.Find("Canvas/ShopPanel") != null);

            Assert.IsNotNull(GameObject.Find("Canvas/ShopPanel"));

            yield return new WaitForSeconds(1f);

            int initialTreeLog = PlayerPrefs.GetInt("TreeLog");
            int initialStone = PlayerPrefs.GetInt("Iron");
            int initialIron = PlayerPrefs.GetInt("Stone");

            GameObject BuyIron = GameObject.Find("Canvas/ShopPanel/DialogueChoices/5 Iron");
            GameObject BuyLog = GameObject.Find("Canvas/ShopPanel/DialogueChoices/5 Log");
            GameObject BuyStone = GameObject.Find("Canvas/ShopPanel/DialogueChoices/5 Stone");
            yield return new WaitForSeconds(1f);
            ClickAction(BuyIron);
            yield return new WaitForSeconds(1f);
            ClickAction(BuyLog);
            yield return new WaitForSeconds(1f);
            ClickAction(BuyStone);
            yield return new WaitForSeconds(1f);
            Assert.Greater(PlayerPrefs.GetInt("TreeLog"), initialTreeLog);
            Assert.Greater(PlayerPrefs.GetInt("Iron"), initialIron);
            Assert.Greater(PlayerPrefs.GetInt("Stone"), initialStone);
            yield return new WaitForSeconds(1f);

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

            yield return new WaitForSeconds(2f);
            ClickAction(BuyIron);
            yield return new WaitForSeconds(2f);
            ClickAction(BuyLog);
            yield return new WaitForSeconds(2f);
            ClickAction(BuyStone);
            yield return new WaitForSeconds(2f);

            Assert.AreEqual(initialIron, PlayerPrefs.GetInt("Iron"));
            Assert.AreEqual(initialTreeLog, PlayerPrefs.GetInt("TreeLog"));
            Assert.AreEqual(initialStone, PlayerPrefs.GetInt("Stone"));

            GameObject Close = GameObject.Find("Canvas/ShopPanel/DialogueChoices/Close");
            yield return new WaitForSeconds(1f);
            ClickAction(Close);
            yield return new WaitForSeconds(2f);

            yield return new WaitForSeconds(2f);
            GameObject pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
            GameObject settingsPanel = GameObject.Find("Canvas").transform.Find("SettingsPanel").gameObject;
            GameObject pauseButton = GameObject.Find("Canvas/PauseButton");
            string sceneName = SceneManager.GetActiveScene().name;

            ClickAction(pauseButton);
            yield return new WaitUntil(() => pauseMenu.activeSelf);

            GameObject settingsButton = GameObject.Find("Canvas/PauseMenu/Button Container/SettingsButton");

            ClickAction(settingsButton);
            yield return new WaitUntil(() => !pauseMenu.activeSelf);

            GameObject restartButton = GameObject.Find("Canvas/SettingsPanel/Button Container/Restart Game");

            ClickAction(restartButton);
            yield return new WaitUntil(() => !settingsPanel.activeSelf);
            yield return new WaitForSeconds(1f);
        }

        [TearDown]
        public override void TearDown()
        {
            // Clean up any static or persistent data
            LogAssert.NoUnexpectedReceived();
            base.TearDown();
        }
    }
}
