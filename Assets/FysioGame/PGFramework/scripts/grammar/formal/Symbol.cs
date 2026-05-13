using System;
using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.scripts.grammar
{
    [System.Serializable]
    public class Symbol: ScriptableObject
    {
        public string symbolName;


        [Tooltip("if true the node will be cleaned after generating the tree")]public bool isSynthetic = false;
        
        public List<AttributeEvaluator> evaluatorsOnInit = new List<AttributeEvaluator>();
        
        
        //starting attributes
        public List<AttributeDefinition> attributeDefintions = new List<AttributeDefinition>();




        
        
        
        /*
        public Node Process(NonTerminalNode parentNode)
        {
    
            // make (Terminal|NonTerminal)Node version based on this symbol
            Node nwNode = Node.GenerateNode(this, parentNode);

            //if this node was a nonTerminal then recursively process the children
            if (nwNode is NonTerminalNode node)
            {
                node.ProcessChildren();
            }
            
            
            
            return nwNode;
        }*/



        
        
        
    }
    
}