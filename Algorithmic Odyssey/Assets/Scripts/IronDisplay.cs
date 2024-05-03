using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class IronDisplay : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }
 
    // Update is called once per frame
    void Update()
    {
    //    string[] temp = text.text.Split('$');
       text.text = "Iron: " + CoinsManager.iron;
    }
}