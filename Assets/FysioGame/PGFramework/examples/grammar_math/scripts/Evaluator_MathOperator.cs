using System;
using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using UnityEngine;

namespace Assets.scripts.grammar.evaluators
{
    [CreateAssetMenu(fileName = "New attribute evaluator", menuName = "Math_grammar/Math Operator evaluator")]
    public class Evaluator_MathOperator : AttributeEvaluator
    {
        public enum MathOperators { Addition, Subtraction, Multiplication, Division };
        
        public string attributeName = "value";
        
        public MathOperators mathOperator = MathOperators.Addition;
        
        
        //TODO: use more generic params?
        public override void Evaluate(List<Node> targets, List<Node> source)
        {
            foreach (Node target in targets)
            {
                //check if attribute is set
                if (target.GetAttribute(attributeName) == null)
                {
                    Debug.LogError(target + " is missing " + attributeName + " attribute");
                    return;
                }
    
    
                //sum from all value's in source
             
                int result = target.GetAttribute(attributeName).value;
                int before = result;
                for (int i = 0; i < source.Count; i++)
                {
                    Node sourceNode = source[i];
                    if (mathOperator == MathOperators.Addition)
                        result += sourceNode.GetAttribute(attributeName).value;
                    else if (mathOperator == MathOperators.Subtraction)
                        result -= sourceNode.GetAttribute(attributeName).value;
                    
                    else if (mathOperator == MathOperators.Multiplication || mathOperator == MathOperators.Division)
                    {
                        /*
                        if (i == 0)
                        {
                            result += sourceNode.GetAttribute(attributeName).value;
                        }
                        else
                        {*/
                            if (mathOperator == MathOperators.Multiplication)
                                result *= sourceNode.GetAttribute(attributeName).value;
                            if (mathOperator == MathOperators.Division)
                                result /= sourceNode.GetAttribute(attributeName).value;
                        //}
                    }
                        
                }
    
                //add this sum to target
                //target.GetAttribute(attributeName).value = sum;
                
                SetAttribute(target, attributeName, result);
            }
            
        }
    }
}