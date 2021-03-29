using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedDrops
{
    [SerializeField, Tooltip("What is being dropped")]
    public GameObject itemToDrop;

    [SerializeField, Tooltip("How often it drops")]
    public int weight;
}

public class Drops : MonoBehaviour
{
    public List<WeightedDrops> drops;
    private List<int> CDFArray;

    // Start is called before the first frame update
    private void Start()
    {
        CDFArray = new List<int>();//create the array

        for (int i = 0; i < drops.Count; i++)
        {
            if (i == 0)//only the first object
            {
                CDFArray.Add(drops[i].weight);//add item to array with weight
            }
            else//not the first item
            {
                CDFArray.Add(drops[i].weight + CDFArray[i - 1]);//add weight plus weight of last item
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void RandomDrop()
    {
        //get random number less than the value of the last cdf item(total cumulative value)
        int rnd = Random.Range(0, CDFArray[CDFArray.Count - 1]);

        int randomSelection = System.Array.BinarySearch(CDFArray.ToArray(), rnd);//use BST to find the index of our random number

        if (randomSelection < 0)//bst will return 0 if the random number lands exactly at the beginning of a weighted index
                                //                     Otherwise we get a negative number
        {
            randomSelection = ~randomSelection;//flip the binary value of the number

            //create drop based on the weighted random number
            Instantiate(drops[randomSelection].itemToDrop, transform.position, transform.rotation);
        }
    }
}