using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace QuizTest
{
    public class TestSuite : InputTestFixture
    {
        Mouse mouse;
        public override void Setup()
        {
            Time.timeScale = 1f;
            base.Setup();
            SceneManager.LoadScene("Scenes/Quiz/Quiz1");
            mouse = InputSystem.AddDevice<Mouse>();
        }
        public void ClickAction(GameObject uiElement)
        {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector2 screenPos = uiElement.GetComponent<RectTransform>().position;
            Set(mouse.position, screenPos);
            Click(mouse.leftButton);
        }


        [UnityTest, Order(1)]
        public IEnumerator QuizAnswers()
        {
            GameObject optionsHolder = GameObject.Find("Canvas/GameMenu/OptionsHolder");

            GameObject Option1 = optionsHolder.transform.GetChild(0).gameObject;
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Quiz1"));

            ClickAction(Option1);
            yield return new WaitForSeconds(2f);

            GameObject Option2 = optionsHolder.transform.GetChild(1).gameObject;
            ClickAction(Option2);
            yield return new WaitForSeconds(2f);

            GameObject Option3 = optionsHolder.transform.GetChild(2).gameObject;
            ClickAction(Option3);
            yield return new WaitForSeconds(2f);

            GameObject Option4 = optionsHolder.transform.GetChild(3).gameObject;
            ClickAction(Option4);
            yield return new WaitForSeconds(2f);

            ClickAction(Option4);
            yield return new WaitForSeconds(2f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("ReviewScene"));
        }

    }
}
