using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Interactions;

namespace DialogueTest
{
    public class TestSuite : InputTestFixture
    {
        GameObject player;
        Mouse mouse;
        Keyboard keyboard;
        public Vector2 initialPosition;

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
        public IEnumerator DialoguetoQuiz()
        {
            player = GameObject.Find("Player");
            var playerMovement = player.GetComponent<PlayerMovement>();

            playerMovement.isTestingMovement = true;

            playerMovement.testMovementDirection = new Vector2(-1, -1);
            initialPosition = player.transform.position;
            yield return new WaitForSeconds(0.77f);

            playerMovement.testMovementDirection = new Vector2(-1, 0);
            initialPosition = player.transform.position;
            yield return new WaitForSeconds(7.3f);

            playerMovement.testMovementDirection = new Vector2(0, 1);
            initialPosition = player.transform.position;
            yield return new WaitForSeconds(0.51f);

            playerMovement.isTestingMovement = false;
            playerMovement.testMovementDirection = Vector2.zero;

            Press(keyboard[Key.E]);
            yield return null;
            Release(keyboard[Key.E]);
            yield return null;

            yield return new WaitForSeconds(2.3f);

            GameObject continueButton = GameObject.Find("Canvas/DialoguePanel 1/ContinueButton");

            ClickAction(continueButton);
            yield return new WaitForSeconds(8.7f);

            ClickAction(continueButton);
            yield return new WaitForSeconds(2.3f);

            GameObject YesButton = GameObject.Find("Canvas/DialoguePanel 1/DialogueChoices/Yes");
            ClickAction(YesButton);
            yield return new WaitForSeconds(1f);

            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Quiz1"));
        }

    }
    }
