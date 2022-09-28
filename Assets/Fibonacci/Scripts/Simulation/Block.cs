using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int weight;
    private Scale scale;
    public ScalePart scalePartSupport;
    public Block supportingBlock;

    // Start is called before the first frame update
    void Start()
    {
        scale = GameObject.Find("Scale").GetComponent<Scale>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Block otherBlock = collision.gameObject.GetComponent<Block>();
        if (otherBlock != null)
        {
            if (scalePartSupport != null && otherBlock.scalePartSupport == null)
            {
                otherBlock.scalePartSupport = scalePartSupport;
                scalePartSupport.RegisterSupportAdd(otherBlock);
                supportingBlock = otherBlock;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Block otherBlock = collision.gameObject.GetComponent<Block>();
        if (otherBlock != null && supportingBlock == otherBlock)
        {
            if (scalePartSupport != null)
            {
                scalePartSupport.RegisterSupportRemove(otherBlock);
                otherBlock.scalePartSupport = null;
                supportingBlock = null;
            }
        }
    }

    void OnDisable(){
        scale.RemoveWeight("L", this);
        scale.RemoveWeight("R", this);
    }
}
