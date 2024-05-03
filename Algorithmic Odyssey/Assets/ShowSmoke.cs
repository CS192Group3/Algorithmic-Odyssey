using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class ShowSmoke : MonoBehaviour
{
    public GameObject Smoke;
    public float delay = 0.5f;

    private void Start()
    {
        // Hide the object initially
        Smoke.SetActive(false);
    }

    public void OnButtonClick()
    {
        int numberOfTreeLog = CoinsManager.treelog;
        int numberOfIron = CoinsManager.iron;
        int numberOfStone = CoinsManager.stone;
        if (numberOfTreeLog < 10 || numberOfIron < 10 || numberOfStone < 10){
            Debug.Log("Not enough coins");
            Debug.Log("You have " + numberOfTreeLog + " coins");
        }
        else {
        // Set the object to be visible
        Smoke.SetActive(true);

        // Start a coroutine to hide the object after a delay
        StartCoroutine(HideObjectAfterDelay());
        }
    }

    private IEnumerator HideObjectAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Hide the object
        Smoke.SetActive(false);
    }
}
