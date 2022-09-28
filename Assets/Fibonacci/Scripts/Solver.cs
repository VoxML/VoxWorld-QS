using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System.Linq;
using System;
using TMPro;

public class Solver : MonoBehaviour
{
    //references
    public GameObject blocks;
    public Scale scale;
    public TextMeshProUGUI displayText;

    //data structures
    List<Block> unsortedBlocks = new List<Block>();
    List<List<Block>> sortingOrderBuckets = new List<List<Block>>(); //each list has blocks of the same weight, lists in ascending order
    int[] weightDeterminations = null;

    //memory
    Block currentBlock = null; //block to sort or determine the weight of
    int bucketToCheck = 0;

    int bucketToDetermine = 1;
    int weightToCheck = 2;
    List<Block> additionSet;

    private void Awake()
    {
        foreach (Transform child in blocks.transform)
        {
            unsortedBlocks.Add(child.gameObject.GetComponent<Block>());
        }
    }

    public void DisplaySuggestion()
    {
        displayText.text = SuggestAction();
    }

    string SuggestAction()
    {

        //try to sort a block into sortingOrderBuckets
        if (unsortedBlocks.Count > 0)
        {
            return SuggestInsertionSortStep();
        }

        //initialize weightDeterminations (assume weights start at 1)

        if (weightDeterminations == null)
        {
            weightDeterminations = new int[sortingOrderBuckets.Count];
            if (weightDeterminations.Count() > 0)
            {
                weightDeterminations[0] = 1;
            }
        }

        if (weightDeterminations.Count() == 0)
        {
            return "No blocks found.";
        }

        if (bucketToDetermine >= weightDeterminations.Count())
        {
            return "All done. \n" + "Buckets: " + String.Join(",", sortingOrderBuckets.Select(list => "[" + String.Join(",", list.Select(block => block.name)) + "]")) + "\n" + "Weight determinations: " + String.Join(",", weightDeterminations);
        }

        //if all sorted, try to probe incrementing inequalities to find all weights

        return SuggestWeightDeterminationStep();
    }

    private string SuggestInsertionSortStep()
    {
        string res;

        if (currentBlock == null)
        {
            //extract a block from unsorted
            currentBlock = unsortedBlocks.First();
        }

        //first block
        if (sortingOrderBuckets.Count == 0)
        {
            res = currentBlock.name + " is our first comparison.";
            AddSortedBlockWithBucketToIndex(0);
            return res;
        }

        //heaviest block
        if (bucketToCheck >= sortingOrderBuckets.Count)
        {
            res = currentBlock.name + " is the heaviest so far.";
            AddSortedBlockWithBucketToIndex(sortingOrderBuckets.Count);
            return res;
        }

        //next bucket
        List<Block> currentBucket = sortingOrderBuckets[bucketToCheck];

        bool scaleMatch = ScaleMatchesComparison(currentBlock, currentBucket.First());
        if (scaleMatch)
        {
            switch (scale.Tip())
            {
                case -1:
                    //if heavier, check next bucket
                    res = currentBlock.name + " is heavier than " + currentBucket.First().name + "!";
                    bucketToCheck += 1;
                    break;
                case 0:
                    //if equal, add to existing bucket
                    res = currentBlock.name + " is the same weight as " + currentBucket.First().name + "!";
                    unsortedBlocks.Remove(currentBlock);
                    sortingOrderBuckets[bucketToCheck].Add(currentBlock);
                    currentBlock = null;
                    bucketToCheck = 0;
                    break;
                case 1:
                    //if lighter, add to a new preceding bucket
                    res = currentBlock.name + " is lighter than " + currentBucket.First().name + "!";
                    AddSortedBlockWithBucketToIndex(bucketToCheck);
                    break;
                default:
                    res = "";
                    break;
            }
            return res;
        }

        res = "Please compare " + currentBlock.name + " to " + currentBucket.First().name + ".";
        return res;
    }

    private string SuggestWeightDeterminationStep()
    {
        string res;
        if (currentBlock == null)
        {
            //extract a block from the next bucket
            currentBlock = sortingOrderBuckets[bucketToDetermine].First();
        }

        if (additionSet != null)
        {
            bool scaleMatch = ScaleMatchesComparison(currentBlock, additionSet);
            if (scaleMatch)
            {
                switch (scale.Tip())
                {
                    case -1:
                        //if heavier, check the next weight
                        res = currentBlock.name + " is heavier than " + String.Join(",", additionSet.Select(p => p.name)) + "!";
                        weightToCheck += 1;
                        additionSet = null;
                        break;
                    case 0:
                        //if equal, update weightDeterminations and get new block
                        res = currentBlock.name + " is equal to " + String.Join(",", additionSet.Select(p => p.name)) + "!";
                        weightDeterminations[bucketToDetermine] = weightToCheck;
                        currentBlock = null;
                        additionSet = null;
                        weightToCheck += 1;
                        bucketToDetermine += 1;
                        break;
                    default:
                        res = "";
                        break;
                }
            }
            else
            {
                res = "Please compare " + currentBlock.name + " to " + String.Join(",", additionSet.Select(p => p.name)) + ".";
            }
            return res;
        }

        additionSet = FindAdditionSet();
        return "Please compare " + currentBlock.name + " to " + String.Join(",", additionSet.Select(p => p.name)) + ".";
    }

    private List<Block> FindAdditionSet()
    {
        //fetch heaviest blocks first, like counting change
        List<Block> result = new List<Block>();
        int targetWeight = weightToCheck;
        int heaviestRemainingBucket = bucketToDetermine - 1;
        while (targetWeight > 0)
        {
            for (int i = 0; i < sortingOrderBuckets[heaviestRemainingBucket].Count && targetWeight > 0; i++)
            {
                Block heaviestRemainingWeight = sortingOrderBuckets[heaviestRemainingBucket][i];
                result.Add(heaviestRemainingWeight);
                targetWeight -= weightDeterminations[heaviestRemainingBucket];
            }
            heaviestRemainingBucket -= 1;
        }
        return result;
    }

    private void AddSortedBlockWithBucketToIndex(int i)
    {
        unsortedBlocks.Remove(currentBlock);
        List<Block> newBucket = new List<Block>();
        newBucket.Add(currentBlock);
        if (i < sortingOrderBuckets.Count)
        {
            sortingOrderBuckets.Insert(i, newBucket);
        } else
        {
            sortingOrderBuckets.Add(newBucket);
        }
        currentBlock = null;
        bucketToCheck = 0;
    }

    private bool ScaleMatchesComparison(Block block1, Block block2)
    {
        if (scale.weightsL.Count == 1 && scale.supportedWeightsL.Count == 0 && scale.weightsR.Count == 1 && scale.supportedWeightsR.Count == 0 &&
           (scale.weightsL.Contains(block1) && scale.weightsR.Contains(block2)))
        {
            return true;
        }
        return false;
    }

    private bool ScaleMatchesComparison(Block block1, List<Block> blockList)
    {
        if (scale.weightsL.Count == 1 && scale.supportedWeightsL.Count == 0 && scale.weightsR.Count + scale.supportedWeightsR.Count == blockList.Count() &&
           (scale.weightsL.Contains(block1)))
        {
            //if all blocks in list match
            foreach (Block b in blockList)
            {
                if (!(scale.weightsR.Contains(b) || scale.supportedWeightsR.Contains(b)))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}
