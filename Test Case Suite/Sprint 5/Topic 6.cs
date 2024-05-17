using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Topic6
{
    public class TestSuite : InputTestFixture
    {
        Mouse mouse;
        private Book book;
        public override void Setup()
        {
            Time.timeScale = 1f;
            base.Setup();
            SceneManager.LoadScene("Scenes/ReviewScene");
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
        public IEnumerator TestTopic()
        {
            GameObject topicButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Wood/Button Container/Topic 6");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("ReviewScene"));

            ClickAction(topicButton);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Topic6");
            yield return new WaitForSeconds(2f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Topic6"));

            GameObject backButton = GameObject.Find("Canvas/Settings Container/Back/Button");

            ClickAction(backButton);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "ReviewScene");
            yield return new WaitForSeconds(2f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("ReviewScene"));
        }


        [UnityTest, Order(3)]
        public IEnumerator TestPages()
        {
            GameObject topicButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Wood/Button Container/Topic 6");
            ClickAction(topicButton);
            yield return new WaitUntil(() => GameObject.Find("Canvas/skipMenu/Button Container/skipContinue") != null);
            yield return new WaitForSeconds(2f);

            GameObject contButton = GameObject.Find("Canvas/skipMenu/Button Container/skipContinue");
            ClickAction(contButton);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Topic6");
            yield return new WaitForSeconds(2f);

            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Topic6"));

            book = GameObject.FindObjectOfType<Book>();
            GameObject nextButton = GameObject.Find("Canvas/next");
            int initialPage = book.currentPage;
            ClickAction(nextButton);
            yield return new WaitUntil(() => book.currentPage != initialPage);
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(initialPage + 2, book.currentPage);

            GameObject prevButton = GameObject.Find("Canvas/prev");
            book.currentPage = 2;
            initialPage = book.currentPage;
            ClickAction(prevButton);
            yield return new WaitUntil(() => book.currentPage != initialPage);
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(initialPage - 2, book.currentPage);

        }

    }
}
