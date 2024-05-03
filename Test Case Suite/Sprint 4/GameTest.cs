using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Interactions;

namespace UpgradeTest
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
        public IEnumerator UpgradeBuilding()
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

            yield return new WaitForSeconds(2f);
            GameObject player = GameObject.Find("Player");
            GameObject mailbox = GameObject.Find("MAILBOX 1");

            player.transform.position = mailbox.transform.position;

            yield return new WaitUntil(() => player.transform.position == mailbox.transform.position);
            yield return new WaitForSeconds(2f);

            Assert.AreEqual(player.transform.position, mailbox.transform.position);

            Press(keyboard[Key.E]);
            yield return null;
            Release(keyboard[Key.E]);
            yield return null;

            yield return new WaitUntil(() => GameObject.Find("Canvas/MailboxPanel 1/DialogueChoices/Yes") != null);
            yield return new WaitForSeconds(8f);

            GameObject sendButton = GameObject.Find("Canvas/MailboxPanel 1/DialogueChoices/Yes");

            ClickAction(sendButton);
            yield return new WaitUntil(() => GameObject.Find("Grid/Building 1/Level 2") != null);

            Assert.IsNotNull(GameObject.Find("Grid/Building 1/Level 2"));

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
            PlayerPrefs.SetInt("treeLog", CoinsManager.treelog);
            PlayerPrefs.Save();
            CoinsManager.UpdateTreeLog();

            Press(keyboard[Key.E]);
            yield return null;
            Release(keyboard[Key.E]);
            yield return null;

            yield return new WaitUntil(() => GameObject.Find("Canvas/MailboxPanel 1/DialogueChoices/Yes") != null);
            yield return new WaitForSeconds(8f);
            sendButton = GameObject.Find("Canvas/MailboxPanel 1/DialogueChoices/Yes");

            ClickAction(sendButton);
            yield return new WaitForSeconds(4f);

            yield return new WaitUntil(() => GameObject.Find("Canvas/NotEnough Panel") != null);

            Assert.IsNotNull(GameObject.Find("Canvas/NotEnough Panel"));

            yield return new WaitForSeconds(2f);
        }

    }
    }
