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
            SceneManager.LoadScene("Scenes/Quiz/Quiz2");
            mouse = InputSystem.AddDevice<Mouse>();
        }
        public void ClickAction(GameObject uiElement)
        {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector2 screenPos = uiElement.GetComponent<RectTransform>().position;
            Set(mouse.position, screenPos);
            Click(mouse.leftButton);
        }

        public GameObject Answering(Text questionText)
        {
            GameObject optionsHolder = GameObject.Find("Canvas/GameMenu/OptionsHolder");
            string optionName = null;
            if (questionText.text == "What is the time complexity of Heap Sort?")
            {
                optionName = "O(n log n)";
            }
            else if (questionText.text == "In Heap Sort, what is the role of \"heapify\" operation?")
            {
                optionName = "Rebuilding the heap after removing the root element";
            }
            else if (questionText.text == "After applying Heap Sort on the array [30, 20, 15, 25, 10], which element will be at the third position from the beginning in the sorted array?")
            {
                optionName = "20";
            }
            else if (questionText.text == "In the sorted array obtained after Heap Sort is applied to [30, 20, 15, 25, 10], which element will be at the last position?")
            {
                optionName = "30";
            }
            else if (questionText.text == "Consider the array [30, 20, 15, 25, 10]. After performing Heap Sort, which element will be at the root of the heap (first element in the sorted array)?")
            {
                optionName = "10";
            }

            Transform optionTransform = optionsHolder.transform.Find(optionName);
            return optionTransform.gameObject;
        }

        [UnityTest, Order(1)]
        public IEnumerator Quiz2()
        {
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Quiz2");
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
            yield return new WaitForSeconds(2f);

            GameObject optionButton;
            Text questionText = GameObject.Find("Canvas/GameMenu/QuestionInfo/QuestionText").GetComponent<Text>();
            GameObject optionsHolder = GameObject.Find("Canvas/GameMenu/OptionsHolder");
            int initialTreeLog = PlayerPrefs.GetInt("TreeLog");
            int initialStone = PlayerPrefs.GetInt("Iron");
            int initialIron = PlayerPrefs.GetInt("Stone");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Quiz2"));
            yield return new WaitForSeconds(2f);

            string prevText = questionText.text;
            optionButton = Answering(questionText);
            ClickAction(optionButton);
            yield return new WaitUntil(() => questionText.text != prevText);
            yield return new WaitForSeconds(2f);

            optionButton = Answering(questionText);
            ClickAction(optionButton);
            yield return new WaitUntil(() => questionText.text != prevText);
            yield return new WaitForSeconds(2f);

            optionButton = Answering(questionText);
            ClickAction(optionButton);
            yield return new WaitUntil(() => questionText.text != prevText);
            yield return new WaitForSeconds(2f);

            optionButton = Answering(questionText);
            ClickAction(optionButton);
            yield return new WaitUntil(() => questionText.text != prevText);
            yield return new WaitForSeconds(2f);

            optionButton = Answering(questionText);
            ClickAction(optionButton);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "ReviewScene");
            yield return new WaitForSeconds(2f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("ReviewScene"));
            Assert.Greater(PlayerPrefs.GetInt("TreeLog"), initialTreeLog);
            Assert.Greater(PlayerPrefs.GetInt("Iron"), initialIron);
            Assert.Greater(PlayerPrefs.GetInt("Stone"), initialStone);

            
        }


        [UnityTest, Order(2)]
        public IEnumerator Quiz3()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Scenes/Quiz/Quiz3");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Quiz3");
            yield return new WaitForSeconds(2f);
            GameObject optionsHolder = GameObject.Find("Canvas/GameMenu/OptionsHolder");
            Text questionText = GameObject.Find("Canvas/GameMenu/QuestionInfo/QuestionText").GetComponent<Text>();

            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Quiz3"));

            string prevText = questionText.text;
            GameObject Option1 = optionsHolder.transform.GetChild(0).gameObject;
            ClickAction(Option1);
            yield return new WaitUntil(() => questionText.text != prevText);
            yield return new WaitForSeconds(2f);

            GameObject Option2 = optionsHolder.transform.GetChild(1).gameObject;
            ClickAction(Option2);
            yield return new WaitUntil(() => questionText.text != prevText);
            yield return new WaitForSeconds(2f);

            GameObject Option3 = optionsHolder.transform.GetChild(2).gameObject;
            ClickAction(Option3);
            yield return new WaitUntil(() => questionText.text != prevText);
            yield return new WaitForSeconds(2f);

            GameObject Option4 = optionsHolder.transform.GetChild(3).gameObject;
            ClickAction(Option4);
            yield return new WaitUntil(() => questionText.text != prevText);
            yield return new WaitForSeconds(2f);

            ClickAction(Option4);
            yield return new WaitUntil(() => questionText.text != prevText);
            yield return new WaitForSeconds(2f);

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("ReviewScene"));
        }
    }
}
