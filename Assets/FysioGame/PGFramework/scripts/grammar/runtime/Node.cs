using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.grammar.runtime
{
    public abstract class Node
    {
        public static bool DEBUG_EVALUATION_STEPS = false;
        public static int FAIL_SAFE_ENDLESS_LOOP_EXCEPTION = 1000; //when evaluate is called more then this, the loop will end with a error
        public static int FAIL_SAFE_ENDLESS_LOOP_SHOW_LATEST = 20; //when an exception is thrown, show the latest X nodes that where being evaluated
        public static List<Node> nodeCallStack = new List<Node>();
        
        
        
        public List<Node> children = new List<Node>();
        public Node parent;

        //public List<AttributeDefinition> values = new List<AttributeDefinition>();
        public Symbol symbol;

        
        //set at runtime and can change
        public List<AttributeReference<int>> attributes = new List<AttributeReference<int>>();



        public List<string> GetAllAttributeNames()
        {
            List<string> result = new List<string>();
            foreach (AttributeReference<int> attribute in attributes)
            {
                result.Add(attribute.attributeDefinition.name);
            }
            return result;
        }
        

        public Node GetChild(int index)
        {
            return children[index];
        }
        

        public static void ResetFailSafeEndlessLoop()
        {
            nodeCallStack = new List<Node>();
        }


        public bool IsSynthetic()
        {
            return symbol.isSynthetic;
        }
        
        
        public virtual void Evaluate()
        {
            //fail safe:
            FailSafeToPreventEndlessLoop();
        }


        private void FailSafeToPreventEndlessLoop()
        {
            if (nodeCallStack.Count > FAIL_SAFE_ENDLESS_LOOP_EXCEPTION)
            {

                string latestFromNodeStack = GetDumpOfLatestNodesInCallStack(FAIL_SAFE_ENDLESS_LOOP_SHOW_LATEST);
                throw new Exception("The evaluate has already called NonTerminalNode.Evaluate more then " + FAIL_SAFE_ENDLESS_LOOP_EXCEPTION + " times. Probably it is stuck in an endless loop or the grammar is very large. Latest " + FAIL_SAFE_ENDLESS_LOOP_SHOW_LATEST + " entries in the node call stack are: " + latestFromNodeStack);
            }
            nodeCallStack.Add(this);
        }


        public static string GetDumpOfLatestNodesInCallStack(int latest)
        {
            string result = "";
            for (int i = Math.Max(0,nodeCallStack.Count - latest); i < nodeCallStack.Count; i++)
            {
                result += nodeCallStack[i].ToString() + ", ";
            }
            
            return result;
        }


        public override string ToString()
        {
            return "node (" + symbol.symbolName+")";
        }

        

        public int GetAttributeValue(string name)
        {
            var attribute = GetAttribute(name);
            if (attribute == null)
            {
                Debug.LogWarning("Could not find attribute: " + name);
                return -1;
            }

            return GetAttribute(name).value;
        }


        public AttributeReference<int> GetAttribute(string name)
        {
            foreach (AttributeReference<int> attributeReference in attributes)
            {
                if (attributeReference.attributeDefinition.name == name)
                {
                    return attributeReference;
                }
            }
            
            throw new Exception("node (" + this +") attribute missing: " + name+". latest nodes in call stack: " + Node.GetDumpOfLatestNodesInCallStack(20));
        }  
        
        public void SetAttribute(string name, int value)
        {
            AttributeReference<int> attributeReference = GetAttribute(name);
            if (attributeReference == null)
            {
                Debug.LogError($"Could not find attribute {name}");
                return;
            }

            attributeReference.value = value;
        }  
        
        
        public Node(Symbol symbol, NonTerminalNode parentNode)
        {
            
            //Setup all references for this new node
            this.symbol = symbol;
    
            if (parentNode != null){
                this.parent = parentNode;
                parentNode.children.Add(this);
            }
            
            
            //TODO: setup the attributes based on the attributeDefintions
            foreach (AttributeDefinition attributeDefinition in symbol.attributeDefintions)
            {
                AttributeReference<int> attributeReference = new AttributeReference<int>(attributeDefinition, 0);
                attributes.Add(attributeReference);
            }
            
            
            
            //run the evaluators on init:
            //if(symbol.evaluatorsOnInit != null) 
            //    symbol.evaluatorsOnInit.Evaluate(this, new List<Node>());

            //run the evaluators
            if (symbol.evaluatorsOnInit == null)
            {
                Debug.LogError("Node with symbol: " + symbol.name + " has not set: evaluatorsOnInit");
                return;
            }
            
            //run all the evaluators on init
            foreach (var evaluator in symbol.evaluatorsOnInit)
            {
                if (evaluator == null)
                {
                    Debug.LogError("Node with symbol: " + symbol.name + " has a null in the evaluatorsOnInit");
                }
                //quick solution
                var targets = new List<Node>();
                targets.Add(this);
                evaluator.Evaluate(targets, new List<Node>());
            }
        }


      


        public static Node GenerateNode(Symbol symbol, NonTerminalNode parentNode)
        {
            Node nwNode;
            if (symbol is Terminal)
            {
                nwNode = new TerminalNode(symbol, parentNode);
            }
            else if (symbol is NonTerminal)
            {
                nwNode = new NonTerminalNode(symbol, parentNode);
            }
            else
            {
                Debug.LogError("symbol should be terminal or non-terminal");
                return null;
            }
            
            return nwNode;
            
        }
        
        

        //TODO: use string builder
        public String TreeToString(int level = 0)
        {
            Node node = this;
            String result = "";

            //add tabs
            for (int i = 0; i < level; i++)
            {
                result += "\t";
            }

            result += node.symbol.symbolName;
            
            
            //add attributes
            //foreach (AttributeReference<int> attribute in attributes)
            //{
            //   result += attribute.ToString()+"";
            //}

            result += " {";
            for (int i = 0; i < attributes.Count; i++)
            {
                result += attributes[i].ToString();
                if (i < attributes.Count - 1)
                {
                    result += ",";
                }
            }

            result += "}";
            

            foreach (Node child in node.children)
            {
                result += "\n" + child.TreeToString(level + 1);
            }

            return result;
        }
        
    }
}