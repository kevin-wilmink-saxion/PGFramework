using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using UnityEngine;

namespace Assets.scripts.grammar.evaluators
{
    [CreateAssetMenu(fileName = "New add value evaluator", menuName = "Math_grammar/Add value evaluator")]
    public class Evaluator_AddValue : AttributeEvaluator
    {
        public string attributeName = "value";
        public int addedValue = 1;
        public override void Evaluate(List<Node> target, List<Node> source)
        {
            foreach (Node n in target)
            {
                var attribute = n.GetAttribute(attributeName);
                if (attribute == null)
                {
                    throw new System.Exception("Could not find attribute: " + attributeName);
                }
                
                
                attribute.value += addedValue;
            }
            
        }
    }
}