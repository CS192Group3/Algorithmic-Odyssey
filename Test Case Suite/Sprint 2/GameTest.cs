using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

namespace GameplayTests
{
    public class GameplaySuites : InputTestFixture
    {
        public GameObject player;
        public Vector2 initialPosition;

        public override void Setup()
        {
            base.Setup();
            SceneManager.LoadScene("Scenes/GameScene");
            
        }

        [UnityTest]
        public IEnumerator PlayerMovesUp()
        {
            player = GameObject.Find("Player");
            var playerMovement = player.GetComponent<PlayerMovement>();

            playerMovement.isTestingMovement = true;
            playerMovement.testMovementDirection = new Vector2(0, 1);

            initialPosition = player.transform.position;

            float moveDuration = 1f; 
            yield return new WaitForSeconds(moveDuration);

            playerMovement.isTestingMovement = false;
            playerMovement.testMovementDirection = Vector2.zero;

            Vector2 newPosition = player.transform.position;
            Assert.Greater(newPosition.y, initialPosition.y);

            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerMovesDown()
        {
            player = GameObject.Find("Player");
            var playerMovement = player.GetComponent<PlayerMovement>();

            playerMovement.isTestingMovement = true;
            playerMovement.testMovementDirection = new Vector2(0, -1);

            initialPosition = player.transform.position;

            float moveDuration = 1f;
            yield return new WaitForSeconds(moveDuration);

            playerMovement.isTestingMovement = false;
            playerMovement.testMovementDirection = Vector2.zero;

            Vector2 newPosition = player.transform.position;
            Assert.Less(newPosition.y, initialPosition.y);

            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerMovesRight()
        {
            player = GameObject.Find("Player");
            var playerMovement = player.GetComponent<PlayerMovement>();

            playerMovement.isTestingMovement = true;
            playerMovement.testMovementDirection = new Vector2(1, 0);

            initialPosition = player.transform.position;

            float moveDuration = 1f;
            yield return new WaitForSeconds(moveDuration);

            playerMovement.isTestingMovement = false;
            playerMovement.testMovementDirection = Vector2.zero;

            Vector2 newPosition = player.transform.position;
            Assert.Greater(newPosition.x, initialPosition.x);

            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerMovesLeft()
        {
            player = GameObject.Find("Player");
            var playerMovement = player.GetComponent<PlayerMovement>();

            playerMovement.isTestingMovement = true;
            playerMovement.testMovementDirection = new Vector2(-1, 0);

            initialPosition = player.transform.position;

            float moveDuration = 1f;
            yield return new WaitForSeconds(moveDuration);

            playerMovement.isTestingMovement = false;
            playerMovement.testMovementDirection = Vector2.zero;

            Vector2 newPosition = player.transform.position;
            Assert.Less(newPosition.x, initialPosition.x);

            yield return null;
        }

    }
    }
