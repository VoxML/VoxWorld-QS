using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerArea3D : AnswerArea
{

    void OnTriggerEnter(Collider col)
    {
        Block block = col.gameObject.GetComponent<Block>();
        if (block != null)
        {
            blocks.Add(block);
        }
        State._instance.unassignedBlocks.Remove(block);
    }

    void OnTriggerExit(Collider col)
    {
        Block block = col.gameObject.GetComponent<Block>();
        if (block != null)
        {
            blocks.Remove(block);
        }
        State._instance.unassignedBlocks.Add(block);
    }

    public override bool CheckAnswer(int answer)
    {
        Debug.Log("Checking weight of " + answer + "...");
        foreach (Block b in blocks)
        {
            Debug.Log(b.weight);
            if (b.weight != answer)
            {
                Debug.Log(false);
                return false;
            }
        }
        Debug.Log(true);
        return true;
    }
}
