using System.Collections.Generic;
using Assets.scripts.grammar;
using Assets.scripts.grammar.runtime;
using UnityEngine;

namespace Assets.scripts.grammar
{
    
    [CreateAssetMenu(fileName = "new pass attribute evaluator", menuName = "Grammar/evaluators/pass attribute evaluator")]
    public class EVAL_pass_attribute: AttributeEvaluator //TODO: rename to: Evaluator_pass_attribute
    {
        public string sourceAttributeName = "";
        public string targetAttributeName = "";
        public override void Evaluate(List<Node> targets, List<Node> source)
        {
            //is target set?
            foreach (var target in targets)
            {
                if (target == null)
                {
                    Debug.LogWarning("Target node is null");
                    return;
                }
                
                
                var targetAttribute = target.GetAttribute(targetAttributeName);
                            
                //check if target has the attribute
                if (targetAttribute == null)
                {
                    Debug.LogWarning("Target node " + target + " has no " + targetAttributeName);
                    return;
                }
    
                //set the attribute to the node in the rhs
                //use the value in the first sourceNode

                var sourceVal = source[0].GetAttribute(sourceAttributeName).value;
                if(Node.DEBUG_EVALUATION_STEPS)
                    Debug.Log("target: node with symbol: "+ target.symbol.symbolName + " attr: " + targetAttributeName +" pass value: "+sourceVal);
                target.GetAttribute(targetAttributeName).value = sourceVal;
            }
            
        }


        //override for more specific behaviour
        public void SetSourceAttribute(AttributeReference<int> fromAttribute, AttributeReference<int> toAttribute)
        {
            toAttribute.value = fromAttribute.value;
        }

    }
}