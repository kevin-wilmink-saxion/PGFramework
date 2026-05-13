using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using grammar;
using NUnit.Framework;
using UnityEngine;

namespace Assets.scripts.grammar
{
    [System.Serializable]
    public class AttributeEquation
    {
        
        //set on which Symbols in the production the evaluation will take place
        
        //0=lhs, 1-N = rhs
        public List<int> targets = new List<int>();
        public List<int> source= new List<int>();
        
        public AttributeEvaluator evaluator; //set in constructor
        
        


        //OVERIDE this methode in your implementation


        public bool IsSynthesized()
        {
            return (targets.Count == 1 && targets[0] == 0);
        }
        

        public void EvaluateEquation(List<Node> targetNode, List<Node> sourceNodes)
        {
            evaluator.Evaluate(targetNode, sourceNodes);
        }
        
        
        public void Evaluate(Node parentNode)
        {
            //Node target, List<Node> source
            //TODO: override this and implement specific rules
            // target.someAttribute = source.someAttributes computation (fe: target.x = s1.x + s2.y)
            //  if lhs in prod is a synthesized attribute: target = lhs, source = rhs
            //  if lhs in prod is an inherited attribute: target = for each rhs Compute is called:
            //      target=rhs_element_x,  source = lhs
            
            List<Node> targetNodes = new List<Node>();
            foreach (int index in targets)
            {
                targetNodes.Add(GrammarUtils.GetNode(parentNode, index));
            }
            
            List<Node> sourceNodes = new List<Node>();
            foreach (int index in source)
            {
                sourceNodes.Add(GrammarUtils.GetNode(parentNode, index));
            }
            
            
            


            //Node targetNode = parentNode;
            //if (target > 0)
            //{
            //    targetNode = parentNode.children[target-1];
            //}
            
            
            // quick solution
            //List<Node> targetNodes = new List<Node>();
            //targetNodes.Add(targetNode);
            evaluator.Evaluate(targetNodes, sourceNodes);
        }

        /*
        public void Evaluate(Node parentNode)
        {
            //Node target, List<Node> source
            //TODO: override this and implement specific rules
            // target.someAttribute = source.someAttributes computation (fe: target.x = s1.x + s2.y)
            //  if lhs in prod is a synthesized attribute: target = lhs, source = rhs
            //  if lhs in prod is an inherited attribute: target = for each rhs Compute is called:
            //      target=rhs_element_x,  source = lhs
            
            List<Node> sourceNodes = new List<Node>();
            foreach (int nodeSourceIndex in source)
            {
                sourceNodes.Add(parentNode.children[nodeSourceIndex-1]);
            }


            Node targetNode = parentNode;
            if (target > 0)
            {
                targetNode = parentNode.children[target-1];
            }
            
            
            // quick solution
            List<Node> targetNodes = new List<Node>();
            targetNodes.Add(targetNode);
            evaluator.Evaluate(targetNodes, sourceNodes);
        }*/
        
    }
}