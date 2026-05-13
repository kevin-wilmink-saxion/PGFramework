using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.grammar.runtime
{
    public class NonTerminalNode: Node
    {
       
        
        public NonTerminalNode(Symbol symbol, NonTerminalNode parentNode) : base(symbol, parentNode)
        {
        }
        
        
        
        
        public override void Evaluate()
        {
            base.Evaluate();
            if (DEBUG_EVALUATION_STEPS)
                Debug.Log("evaluate 0: (NT)node with symbol: " + this.symbol.symbolName);
            //base.Evaluate();
            //1. Select a Production
            //  1.1 Based on the Production generate rhs-Node's for each of the rhs Symbols
            Production production = ((NonTerminal)symbol).GetRandomAvailableProduction(this);
            if (production == null)
            {
                Debug.LogError("Non-terminal production " + this + " returned a null production");
                return;
            }
            if (DEBUG_EVALUATION_STEPS)
                Debug.Log("evaluate 1a: (NT)node with symbol: " + this.symbol.symbolName + " prod found: " + production.rhs);
            
            List<Node> childrenRHSNode = new List<Node>();
            
            if (DEBUG_EVALUATION_STEPS)
                Debug.Log("evaluate 1b: (NT)node with symbol: " + this.symbol.symbolName + "generate child nodes (not evaluated yet)");
            foreach (Symbol rhsSymbol in production.rhs)
            {
                
                //Node rhsNode = new Node(rhsSymbol, this);
                Node rhsNode = GenerateNode(rhsSymbol, this);
                childrenRHSNode.Add(rhsNode);
                //Debug.Log("evaluate: (NT)node with symbol: " + this.symbol.symbolName + " generate rhs node: " + rhsNode.symbol.symbolName);
            }
            
            
            
            //2. Compute the AttributeEquations for inherited values (computed BEFORE the children)
            //TODO
            if (DEBUG_EVALUATION_STEPS)
                Debug.Log("evaluate 2: (NT)node with symbol: " + this.symbol.symbolName + "inherited equations NOW");
            production.EvaluateInheritedAttributeEquations(this);
            
            //3. Evaluate the rhs-Node's
            if (childrenRHSNode.Count != this.children.Count)
            {
                Debug.LogError("When Evaluating a node, the parent-node already had children. This should not happen.");
                return;
            }

            if (DEBUG_EVALUATION_STEPS)
                Debug.Log("evaluate 3: (NT)node with symbol: " + this.symbol.symbolName + " evaluate children (rhs)");
            foreach (Node child in childrenRHSNode)
            {
                //Debug.Log("evaluate 3: (NT)node with symbol: " + this.symbol.symbolName + " evaluate child: " + child.symbol.symbolName);
                child.Evaluate();
            }
            
            
            //4. Compute the AttributeEquations for the synthesized values (computed AFTER the children)
            //TODO
            if (DEBUG_EVALUATION_STEPS)
                Debug.Log("evaluate 4: (NT)node with symbol: " + this.symbol.symbolName + " synthesized equations NOW");
            production.EvaluateSynthesizedAttributeEquations(this);
        }
        
        
        
        
        /*
        public void ProcessChildren()
        {
            // randomly select a new available production
            //TODO: add constrains/attributes
            Production production = ((NonTerminal)this.symbol).getRandomProduction();
        
            // do the production, pass a reference to the lhsNode (node version of this symbol)
            production.Produce(this);
        }*/
    }
}