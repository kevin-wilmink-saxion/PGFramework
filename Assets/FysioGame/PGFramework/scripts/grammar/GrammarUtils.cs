using System;
using System.Collections.Generic;
using System.IO;
using Assets.scripts.grammar.runtime;
using UnityEngine;

namespace grammar
{
    public class GrammarUtils
    {
        
        public enum ComparisonOperator
        {
            Greater,
            GreaterOrEqual,
            Less,
            LessOrEqual,
            Equal,
            NotEqual
        }

        
        
        public static bool CompareIntegers(int a, int b, ComparisonOperator comparisonOperator) =>
            comparisonOperator switch
            {
                ComparisonOperator.Greater         => a > b,
                ComparisonOperator.GreaterOrEqual  => a >= b,
                ComparisonOperator.Less            => a < b,
                ComparisonOperator.LessOrEqual     => a <= b,
                ComparisonOperator.Equal           => a == b,
                ComparisonOperator.NotEqual        => a != b,
                _ => throw new ArgumentOutOfRangeException(nameof(comparisonOperator), comparisonOperator, null)
            };

        
        
        
        
        //TODO: find better solution then brute force copying all
        public static Dictionary<string, int> CopyAttributes(Dictionary<string, int> source)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (KeyValuePair<string,int> item in source)
            {
                result.Add(item.Key, item.Value);
            }

            return result;
        }


        /// <summary>
        /// Returns the node based on nodeIndex, after production
        /// </summary>
        /// <param name="nodeIndex">0=lhs (parentNode itself), 1-N = rhs, childnodes, where 1= the first child</param>
        public static Node GetNode(Node parentNode, int nodeIndex)
        {
            if (parentNode == null)
            {
                Debug.LogWarning("Parent node is null");
                return null;
            }
            
            if (nodeIndex == 0)
                return parentNode;


            if (nodeIndex > parentNode.children.Count)
            {
                Debug.LogWarning("Parent node index is out of range");
                return null;
            }
            
            return parentNode.children[nodeIndex-1];
        }
        
        public static void PrintToLog(string log)
        {
            Debug.Log("log written to log.txt (see the folder above the /Assets folder");
            File.WriteAllText("log.txt", log);
        }
        
        
/// <summary>
        /// Performs weighted random selection based on frequency values.
        /// This method selects a random index from the frequencies array where indices with higher frequency values
        /// have a proportionally higher chance of being selected.
        /// </summary>
        /// <param name="frequencies">
        /// Array of frequency values (weights) for each option. 
        /// Each element represents the relative probability weight for that index.
        /// Higher values indicate higher selection probability.
        /// </param>
        /// <returns>
        /// return the selected index
        /// </returns>
  
        public static int GetWeightedRandom(int[] frequencies)
        {
            int totalFrequency = 0;
            foreach (int freq in frequencies)
            {
                totalFrequency += freq;
            }
            
            int randomValue = UnityEngine.Random.Range(0, totalFrequency);
            
            int cumulativeFrequency = 0;
            for (int i = 0; i < frequencies.Length; i++)
            {
                cumulativeFrequency += frequencies[i];
                if (randomValue < cumulativeFrequency)
                {
                    return i; // Return 1-based index
                }
            }
            
            return frequencies.Length; // Fallback
        }
        
    }
}