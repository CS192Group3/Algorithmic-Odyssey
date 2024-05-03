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

        public GameObject Answering(Text questionText)
        {
            GameObject optionsHolder = GameObject.Find("Canvas/GameMenu/OptionsHolder");
            string optionName = null;
            if (questionText.text == "Give the Inorder Traversal sequence")
            {
                optionName = "4 - 8  - 2 - 5 - 1 - 6 - 3 - 9 - 7";
            }
            else if (questionText.text == "In this traversal method, the left subtree is visited first, then the root and later the right sub-tree.")
            {
                optionName = "Inorder Traversal";
            }
            else if (questionText.text == "In this traversal method, we traverse the left subtree, then the right subtree and finally the root node.")
            {
                optionName = "Postorder Traversal";
            }
            else if (questionText.text == "In this traversal method, the root node is visited first, then the left subtree and finally the right subtree.")
            {
                optionName = "Preorder Traversal";
            }
            else if (questionText.text == "Give the Postorder Traversal sequence")
            {
                optionName = "8 - 4 - 5 - 2 - 6 - 9 - 7 - 3 - 1";
            }

            Transform optionTransform = optionsHolder.transform.Find(optionName);
            return optionTransform.gameObject;
        }

        [UnityTest, Order(1)]
        public IEnumerator RewardSystem()
        {
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

            GameObject optionButton;
            Text questionText = GameObject.Find("Canvas/GameMenu/QuestionInfo/QuestionText").GetComponent<Text>();
            GameObject optionsHolder = GameObject.Find("Canvas/GameMenu/OptionsHolder");
            int initialTreeLog = PlayerPrefs.GetInt("treeLog");
            int initialStone = PlayerPrefs.GetInt("Iron");
            int initialIron = PlayerPrefs.GetInt("Stone");
            string sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo("Quiz1"));
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
            Assert.Greater(PlayerPrefs.GetInt("treeLog"), initialTreeLog);
            Assert.Greater(PlayerPrefs.GetInt("Iron"), initialIron);
            Assert.Greater(PlayerPrefs.GetInt("Stone"), initialStone);
        }

    }
}
