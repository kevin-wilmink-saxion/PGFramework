using System;
using System.Collections.Generic;
using Assets.scripts.grammar;
using Assets.scripts.grammar.runtime;
using grammar;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GrammarGenerator : MonoBehaviour
{
    
    [Tooltip("called after the generating is done")] public UnityEvent<Node> onGenerateTree = new UnityEvent<Node>();

    public Node root;
    
    
    public void StartNewGeneration(GrammarAsset grammarAsset)
    {
        //Node grammarTree = ProcessSymbol(grammarAsset.root, null);
        //create node tree
        root = Node.GenerateNode(grammarAsset.root, null);
        root.Evaluate();

        CleanSyntheticChildNodes(root);
        
        //Node grammarTree = grammarAsset.root.Process(null);
        onGenerateTree.Invoke(root);
    }



    public void CleanSyntheticChildNodes(Node node)
    {
        //Keep replacing children until all children are no longer synthetic
        for (int i = 0; i < node.children.Count; i++)
        {
            var child = node.children[i];
            
            if (child.IsSynthetic())
            {
                var grandChildren = child.children;
                node.children.RemoveAt(i);
                node.children.InsertRange(i, grandChildren);
                i--; //recheck this position again
            }
        }
        
        //Go into the recursion for the children
        foreach (var child in node.children)
        {
            CleanSyntheticChildNodes(child);
        }
    }
}
