using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePart : MonoBehaviour
{
    public string part;
    public Scale scale;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Block block = col.gameObject.GetComponent<Block>();
        if (block != null)
        {
            scale.RegisterWeight(part, block);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Block block = col.gameObject.GetComponent<Block>();
        if (block != null)
        {
            scale.RemoveWeight(part, block);
        }
    }

    public void RegisterSupportAdd(Block block)
    {
        scale.RegisterSupportAdd(part, block);
    }

    public void RegisterSupportRemove(Block block)
    {
        scale.RegisterSupportRemove(part, block);
    }
}