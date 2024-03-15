using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ReviewTests
{
    public class ReviewSuite : InputTestFixture
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
            GameObject topicButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Wood/Button Container/Topic 1");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("ReviewScene"));

            ClickAction(topicButton);
            yield return new WaitForSeconds(1f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Topic1"));

            GameObject backButton = GameObject.Find("Canvas/Settings Container/Back/Button");

            ClickAction(backButton);
            yield return new WaitForSeconds(1f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("ReviewScene"));
        }



        [UnityTest, Order(2)]
        public IEnumerator TestSkipCheck()
        {
            GameObject skipMenu = GameObject.Find("Canvas").transform.Find("skipMenu").gameObject;
            GameObject topicButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Wood/Button Container/Topic 1");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("ReviewScene"));
            Assert.IsFalse(skipMenu.activeSelf);

            ClickAction(topicButton);
            yield return new WaitForSeconds(1f);

            Assert.IsTrue(skipMenu.activeSelf);
            GameObject contButton = GameObject.Find("Canvas/skipMenu/Button Container/skipContinue");

            ClickAction(contButton);
            yield return new WaitForSeconds(1f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Topic1"));
        }


        [UnityTest, Order(3)]
        public IEnumerator TestPages()
        {
            GameObject topicButton = GameObject.Find("Canvas/Background/Main Panel - Shadow/Wood/Button Container/Topic 1");
            ClickAction(topicButton);
            yield return new WaitForSeconds(1f);

            GameObject contButton = GameObject.Find("Canvas/skipMenu/Button Container/skipContinue");
            ClickAction(contButton);
            yield return new WaitForSeconds(1f);

            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Topic1"));

            book = GameObject.FindObjectOfType<Book>();
            GameObject nextButton = GameObject.Find("Canvas/next");
            int initialPage = book.currentPage;
            ClickAction(nextButton);
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(initialPage + 2, book.currentPage);

            GameObject prevButton = GameObject.Find("Canvas/prev");
            book.currentPage = 2;
            initialPage = book.currentPage;
            ClickAction(prevButton);
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(initialPage - 2, book.currentPage);

        }

    }
}
