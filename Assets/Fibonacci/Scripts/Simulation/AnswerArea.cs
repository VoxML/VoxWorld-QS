using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerArea : MonoBehaviour
{
    public HashSet<Block> blocks = new HashSet<Block>();
    void OnTriggerEnter2D(Collider2D col)
    {
        Block block = col.gameObject.GetComponent<Block>();
        if (block != null)
        {
            blocks.Add(block);
        }
        State._instance.unassignedBlocks.Remove(block);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Block block = col.gameObject.GetComponent<Block>();
        if (block != null)
        {
            blocks.Remove(block);
        }
        State._instance.unassignedBlocks.Add(block);
    }

    public virtual bool CheckAnswer(int answer)
    {
        foreach (Block b in blocks)
        {
            if (b.weight != answer)
            {
                return false;
            }
        }
        return true;
    }
}
