using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scale : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public HashSet<Block> weightsL = new HashSet<Block>();
    public HashSet<Block> weightsR = new HashSet<Block>();
    public HashSet<Block> supportedWeightsL = new HashSet<Block>();
    public HashSet<Block> supportedWeightsR = new HashSet<Block>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegisterWeight(string part, Block weight)
    {
        if (part == "L")
        {
            weightsL.Add(weight);
        }
        else if (part == "R")
        {
            weightsR.Add(weight);
        }
        State._instance.unassignedBlocks.Remove(weight);
    }

    public void RegisterSupportAdd(string part, Block weight)
    {
        if (part == "L")
        {
            supportedWeightsL.Add(weight);
            supportedWeightsL.ExceptWith(weightsL);
        }
        else if (part == "R")
        {
            supportedWeightsR.Add(weight);
            supportedWeightsR.ExceptWith(weightsR);
        }
    }

    public void RegisterSupportRemove(string part, Block weight)
    {
        if (part == "L")
        {
            supportedWeightsL.Remove(weight);
        }
        else if (part == "R")
        {
            supportedWeightsR.Remove(weight);
        }
    }

    public void RemoveWeight(string part, Block weight)
    {
        RegisterSupportRemove(part, weight);
        if (part == "L")
        {
            weightsL.Remove(weight);
        }
        else if (part == "R")
        {
            weightsR.Remove(weight);
        }
        State._instance.unassignedBlocks.Add(weight);
    }

    public int Tip()
    {
        int totalWeightL = weightsL.Sum(block => block.weight) + supportedWeightsL.Sum(block => block.weight);
        int totalWeightR = weightsR.Sum(block => block.weight) + supportedWeightsR.Sum(block => block.weight); ;

        if (totalWeightL > totalWeightR)
        {
            return -1;
        }
        if (totalWeightL < totalWeightR)
        {
            return 1;
        }
        return 0;
    }
}
