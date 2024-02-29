using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace GameUITests
{
    public class GameUISuites : InputTestFixture
    {

        Mouse mouse;
        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("Scenes/GameScene");
            mouse = InputSystem.AddDevice<Mouse>();
        }
        public void ClickAction(GameObject uiElement)
        {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector2 screenPos = uiElement.GetComponent<RectTransform>().position;
            Debug.Log("Screen Position: " + screenPos);
            Set(mouse.position, screenPos);
            Click(mouse.leftButton);
        }


        [UnityTest]
        public IEnumerator TestPause()
        {
            GameObject pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
            GameObject pauseButton = GameObject.Find("Canvas/PauseButton");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("GameScene"));
            Assert.IsFalse(pauseMenu.activeSelf);

            ClickAction(pauseButton);
            yield return new WaitUntil(() => pauseMenu.activeSelf);

            Assert.IsTrue(pauseMenu.activeSelf);

            GameObject resumeButton = GameObject.Find("Canvas/PauseMenu/Button Container/ResumeButton");
            
            ClickAction(resumeButton);
            yield return new WaitUntil(() => !pauseMenu.activeSelf);

            Assert.IsFalse(pauseMenu.activeSelf);
        }

        [UnityTest]
        public IEnumerator TestPauseSettings()
        {
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

        }

        [UnityTest]
        public IEnumerator TestMainMenu()
        {
            GameObject pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
            GameObject pauseButton = GameObject.Find("Canvas/PauseButton");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("GameScene"));
            Assert.IsFalse(pauseMenu.activeSelf);

            ClickAction(pauseButton);
            yield return new WaitUntil(() => pauseMenu.activeSelf);

            Assert.IsTrue(pauseMenu.activeSelf);

            GameObject menuButton = GameObject.Find("Canvas/PauseMenu/Button Container/MainMenu");

            ClickAction(menuButton);
            yield return new WaitForSeconds(1f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("MainMenu"));

        }
    }
}
