using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckButton : MonoBehaviour
{

    public TextMeshProUGUI resultText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckAnswers()
    {
        if (State._instance.CheckAnswers())
        {
            resultText.text = "blocks correct";
        }
        else
        {
            resultText.text = "blocks invalid";
        }
    }
}
