using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace MenuTests
{
    public class MenuSuite : InputTestFixture
    {
        Mouse mouse;
        private string debugLog;
        public override void Setup()
        {
            Time.timeScale = 1f;
            base.Setup();
            SceneManager.LoadScene("Scenes/MainMenu");
            mouse = InputSystem.AddDevice<Mouse>();
        }
        public void ClickAction(GameObject uiElement)
        {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector2 screenPos = uiElement.GetComponent<RectTransform>().position;
            Set(mouse.position, screenPos);
            Click(mouse.leftButton);
        }


        [UnityTest]
        public IEnumerator TestReview()
        {
            GameObject reviewButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Wood/Button Container/Review Button");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("MainMenu"));

            ClickAction(reviewButton);
            yield return new WaitForSeconds(1f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("ReviewScene"));

            GameObject backButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Settings Container/Return/Button");
            
            ClickAction(backButton);
            yield return new WaitForSeconds(1f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("MainMenu"));
        }

        [UnityTest]
        public IEnumerator TestGameStart()
        {
            GameObject playButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Wood/Button Container/Play Button");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("MainMenu"));

            ClickAction(playButton);
            yield return new WaitForSeconds(1f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("GameScene"));
        }

        [UnityTest]
        public IEnumerator TestSettings()
        {
            GameObject settingsButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Settings Container/Settings/Button");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("MainMenu"));

            ClickAction(settingsButton);
            yield return new WaitForSeconds(1f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("SettingsScene"));

            GameObject backButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Settings Container/Back/Button");

            ClickAction(backButton);
            yield return new WaitForSeconds(1f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("MainMenu"));
        }

        private void LogMessage(string condition, string stackTrace, LogType type)
        {
            debugLog = condition;
        }

        [UnityTest]
        public IEnumerator TestQuit()
        {
            Application.logMessageReceived += LogMessage;

            GameObject quitButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Wood/Button Container/Quit Button");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("MainMenu"));

            ClickAction(quitButton);
            yield return null;

            Assert.That(debugLog, Is.EqualTo("Quit"));
            Application.logMessageReceived -= LogMessage;
        }


    }

    
}
