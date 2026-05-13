using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using UnityEngine;
using UnityEngine.Serialization;


namespace Assets.scripts.grammar
{
    /// <summary>
    /// a single production that belongs to an non-terminal
    ///     (TODO: remove this class and store rhs directly on an NonTerminal?)
    /// </summary>
    
    [System.Serializable]
    public class Production
    {
        //public NonTerminal lhs; (this is stored at an NonTerminal, so that nt already acts as the lhs)
        [Tooltip("the rhs that are produced, leave empty to make it produce nothing")]public List<Symbol> rhs = new List<Symbol>();
        //public List<AttributeEquation> attributeEquation;
        public List<GrammarConstraint> constraints = new List<GrammarConstraint>();

       
        [Tooltip("happens BEFORE evaluating the children, but after generating the child nodes")] public List<AttributeEquation> inheritedEquations = new List<AttributeEquation>();
        [Tooltip("happens AFTER evaluating the children")]public List<AttributeEquation> synthezisedEquations = new List<AttributeEquation>();
       

        public bool AreConstraintsSatisfied(NonTerminalNode lhsNode)
        {
            foreach (var constraint in constraints)
            {
                if (!constraint.IsSatisfied(lhsNode, rhs))
                {
                    return false;
                }
            }
            return true;
        }
        
        

        public void EvaluateInheritedAttributeEquations(Node parentNode) //productionNodes = A->BC [A,B,C]
        {
            foreach (AttributeEquation equation in inheritedEquations)
            {
                equation.Evaluate(parentNode);
                /*
                if (!equation.IsSynthesized())
                {
                    equation.Evaluate(parentNode);
                }*/
            }
        }
        
        
        
        public void EvaluateSynthesizedAttributeEquations(Node parentNode)
        {
            foreach (AttributeEquation equation in synthezisedEquations)
            {
                equation.Evaluate(parentNode);
                /*
                //if there is only 1 target and this is the first element, it is a synthesized equation
                if (equation.IsSynthesized())
                {
                    equation.Evaluate(parentNode);
                }*/
            }
        }
        /*
        public void Produce(NonTerminalNode lhsNode)
        {
            //check if there is a valid rhs setup for this production
            if (rhs == null)
            {
                Debug.LogWarning("A production in " + this + " has a production with a null rhs");
                return;
            }
            
            //build a list with nodes based on the symbols in the rhs
            List<Node> nwRhsNodes = new List<Node>();
            
            //process all the Symbols in the rhs, add them to the rhs nodes for this production
            foreach (Symbol rhsSymbol in rhs)
            {
                Node rhsNode = rhsSymbol.Process(lhsNode);
                nwRhsNodes.Add(rhsNode);
            }
            
            //synthesized (after the children, the rhs nodes are the children)
            //attributeEquation.Produce(lhsNode, nwRhsNodes);
            
            lhsNode.ProcessChildren();
            
            
            
            //do the rule
            //if (rule == null)
            //    return;
            
            //rule.Produce(lhsNode, nwRhsNodes);
        }*/
    }
    
}