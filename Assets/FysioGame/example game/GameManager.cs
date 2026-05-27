using System.Collections.Generic;
using UnityEngine;
using FysioGame.scripts;
using Assets.scripts.grammar.runtime;
using FysioGame.example_game;

public class GameManager : MonoBehaviour, IProceduralGenerationConsumer
{
    
    public PGFysioTester pgFysioTester;

    public InteractableObjectsSpawner spawnableObjectsSpawner;
   
    void Awake()
    {
        //register this class as a consumer of procedural generation events
        //  pgFysioTester will now call:
        //      > OnProceduralGenerationFinished (after a new generation)
        //      > OnNodeVisited (after a new generation, each node in the output will be visted once in a breath-first aproach.
        //          the data in these nodes are used to fill the queueWithSpawnables)
        pgFysioTester.pgConsumer = this;
    }

   
    //called by: PGFysioTester -> after a new generation
    public void OnProceduralGenerationFinished(Node node)
    {
        Debug.Log("New Game is being generated!");
        spawnableObjectsSpawner.Reset();
    }

    
    //called by: PGFysioTester -> after a new generation PGFysioTester traverses the tree, each node will call this method
    public void OnNodeVisited(Node node)
    {
        //only use the regions
        if (node.symbol.symbolName == "Region")
        {
            //get the data from the node
            int bodyPart = node.GetAttributeValue("bodyPart");
            int side = node.GetAttributeValue("side");
            
            //use child(0), this is the task that belongs to this region
            int taskNr = node.GetChild(0).GetAttributeValue("task");
            int difficulty = node.GetChild(0).GetAttributeValue("difficulty");
            
            //use this data to add an object to the queue
            spawnableObjectsSpawner.AddSpawnableToQueue(taskNr, bodyPart, side, difficulty);
        }
    }
    
    
}
