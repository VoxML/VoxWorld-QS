using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    public List<AnswerArea> answerAreas;
    public GameObject checkButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckAnswers()
    {
        for (int i = 0; i < answerAreas.Count; i++)
        {
            if (!answerAreas[i].CheckAnswer(i + 1)) { return false; }
        }
        return true;
    }

    public HashSet<Block> BlocksSubmitted()
    {
        HashSet<Block> res = new HashSet<Block>();
        foreach (AnswerArea area in answerAreas)
        {
            res.UnionWith(area.blocks);
        }
        return res;
    }
}
