using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public static class Utilities
    {
        
        public static void DestroyChildren(Transform parent)
        {
            foreach (Transform child in parent) {
                GameObject.Destroy(child.gameObject);
            }
        }



        public static GameObject InstantiatePrefab(string prefabName, Transform parent = null)
        {
            var prefab = Resources.Load<GameObject>(prefabName);
            
            if (prefab == null)
                return null;
            
            
            GameObject result = GameObject.Instantiate(prefab, parent, false);

            return result;
        }
        
        
        public static int[] CountPositivesPerColumn(List<int[]> arrays)
        {
            if (arrays == null || arrays.Count == 0)
                return new int[0];

            int length = arrays[0].Length;
            int[] counts = new int[length];

            foreach (var arr in arrays)
            {
                if (arr.Length != length)
                    throw new System.ArgumentException("All arrays must have the same length.");

                for (int i = 0; i < length; i++)
                {
                    if (arr[i] > 0)
                    {
                        counts[i]++;
                    }
                }
            }

            return counts;
        }

        
        public static int[] AddArrays(int[] a, int[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Arrays must have the same length");

            int[] result = new int[a.Length];

            for (int i = 0; i < a.Length; i++)
                result[i] = a[i] + b[i];

            return result;
        }
        
        public static int GetIndexOfMax(int[] array)
        {
            int maxIndex = 0;
            int maxValue = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                {
                    maxValue = array[i];
                    maxIndex = i;
                }
            }

            return maxIndex;
        }
        
        
        public static int[] AddAllArrays(List<int[]> arrays)
        {
            if (arrays == null || arrays.Count == 0)
                throw new ArgumentException("List cannot be empty");

            int length = arrays[0].Length;

            // Ensure all arrays have same length
            foreach (var arr in arrays)
            {
                if (arr.Length != length)
                    throw new ArgumentException("All arrays must be the same length");
            }

            int[] result = new int[length];

            foreach (var arr in arrays)
            {
                for (int i = 0; i < length; i++)
                {
                    result[i] += arr[i];
                }
            }

            return result;
        }

        
        
        
        
        public static List<T> GetAvailableItems<T>(List<T> originalList, List<int> indexesUnavailable)
        {
            // Handle null safely
            indexesUnavailable ??= new List<int>();

            List<T> available = new List<T>();

            for (int i = 0; i < originalList.Count; i++)
            {
                // Skip if index is unavailable
                if (!indexesUnavailable.Contains(i))
                {
                    available.Add(originalList[i]);
                }
            }

            return available;
        }
        
        
        public static List<int> GetListWithAvailableIndexes(int itemsInOriginalList, List<int> indexesUnavailable)
        {
            // Ensure unavailable list is not null
            indexesUnavailable ??= new List<int>(); //uses right hand if left hand is null

            List<int> available = new List<int>();

            for (int i = 0; i < itemsInOriginalList; i++)
            {
                if (!indexesUnavailable.Contains(i))
                {
                    available.Add(i);
                }
            }

            return available;
        }
        
        

    }
}