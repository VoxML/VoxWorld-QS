using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePart3D : ScalePart
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        Block block = col.gameObject.GetComponent<Block>();
        if (block != null)
        {
            block.scalePartSupport = this;
            scale.RegisterWeight(part, block);
        }
    }

    void OnTriggerExit(Collider col)
    {
        Block block = col.gameObject.GetComponent<Block>();
        if (block != null)
        {
            block.scalePartSupport = null;
            scale.RemoveWeight(part, block);
        }
    }

    void RegisterWeight(Block block)
    {
        scale.RegisterWeight(part, block);
    }

    void RemoveWeight(Block block)
    {
        scale.RemoveWeight(part, block);
    }

    void OnDestroy(){
        
    }
}