using System;
using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using FysioGame.scripts;
using UnityEngine;


public class PGFysioTester : MonoBehaviour
{
    public GrammarGenerator grammarGenerator;
    public GrammarAsset grammarAsset;
    
   public IProceduralGenerationConsumer pgConsumer;
    
    
    public bool debug_printTree = true;
    public bool debug_printNodes = true;
    
    void Awake()
    {
        grammarGenerator.onGenerateTree.AddListener(OnGrammarGenerated);
    }
    

    void Start()
    {
        //Debug.Log("Starting grammar generation");
        TestGeneration();
    }



    public void TestGeneration()
    {
        grammarGenerator.StartNewGeneration(grammarAsset);
    }
    
    
    private void OnGrammarGenerated(Node root)
    {
        if (debug_printTree)
            Debug.Log("Grammar generated: " + root.TreeToString());
        
        // call the event that the generation is done. The root is a data tree (contains all PG output data)
        pgConsumer?.OnProceduralGenerationFinished(root);
        
        //Examples:
        
        //PrintExamples(root); //shows how to access the data from the output
        TraverseTree(root); //loop over all the elements in after the PG. (breath first)
    }


    private void PrintExamples(Node root)
    {
        //NOTE: use the structure set by the root and its children to start building your game/app!
        //  in this example the root is a Workout,
        // workout has 1 property: rep
        // workout has children of the type Region
        //      region has 2 properties: bodyPart and side
        //      region has children of the type task
        //          task has 2 properties: task and difficulty
        if (root == null) return;
        Debug.Log("In the root (workout) print reps: " + root.GetAttributeValue("rep"));
        
        if (root.children.Count == 0) return;
        Debug.Log("In the first region print bodyPart: " + root.GetChild(0).GetAttributeValue("bodyPart"));
        Debug.Log("In the first region print side: " + root.GetChild(0).GetAttributeValue("side"));
        
        if (root.GetChild(0).children.Count == 0) return;
        Debug.Log("In the first region in the task print task: " + root.GetChild(0).GetChild(0).GetAttributeValue("task"));
        Debug.Log("In the first region in the task print difficulty: " + root.GetChild(0).GetChild(0).GetAttributeValue("difficulty"));
    }

   

    
    /// <summary>
    /// Used as an example of how to traverse the tree
    /// </summary>
    /// <param name="node"></param>
    private void TraverseTree(Node node)
    {
        if (node == null) return;

        OnNodeVisited(node);

        foreach (var child in node.children)
        {
            TraverseTree(child);
        }
    }
    
    
    /// <summary>
    /// Used as an example of how to get the attributes of a node
    /// </summary>
    /// <param name="node"></param>
    private void OnNodeVisited(Node node)
    {
        if (debug_printNodes)
        {
            String result = "node: " + node.symbol.symbolName;
            result += ", attributes: ";
            List<string>
                attributes =
                    node.GetAllAttributeNames(); //use node.GetAllAttributeNames() to get all the attribute names

            foreach (string attributeName in attributes)
            {
                result += attributeName + ": " + node.GetAttributeValue(attributeName) +
                          ", "; //use node.GetAttributeValue(..) to get the value of an attribute
            }

            result += "\n";
            Debug.Log(result);
        }
        
        pgConsumer?.OnNodeVisited(node);
    }

}
