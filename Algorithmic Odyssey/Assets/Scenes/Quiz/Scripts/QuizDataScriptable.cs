using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestionData", menuName = "Scenes/Quiz/QuestionData", order = 1)]
public class QuizDataScriptable : ScriptableObject 
{
    public List<Question> questions;
}
