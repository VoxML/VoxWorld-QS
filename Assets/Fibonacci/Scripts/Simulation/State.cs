using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class State : MonoBehaviour
{
    public GameObject blocks;
    public static State _instance;
    public HashSet<Block> allBlocks;
    public HashSet<Block> unassignedBlocks;
    public Scale scale;
    public Answer answer;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }

        allBlocks = new HashSet<Block>();
        foreach (Transform child in blocks.transform)
        {
            allBlocks.Add(child.gameObject.GetComponent<Block>());
        }
        unassignedBlocks = new HashSet<Block>(allBlocks);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckAnswers()
    {
        /**
        if (allBlocks.Intersect(answer.BlocksSubmitted()).Count() != allBlocks.Count)
        {
            Debug.Log("not all blocks submitted");
        }
        if (!answer.CheckAnswers())
        {
            Debug.Log("wrong answer");
        }
        **/

        //return allBlocks.Intersect(answer.BlocksSubmitted()).Count() == allBlocks.Count && answer.CheckAnswers();
        return answer.CheckAnswers();
    }
}
