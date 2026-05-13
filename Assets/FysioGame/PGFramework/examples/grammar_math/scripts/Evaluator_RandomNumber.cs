using System.Collections.Generic;
using Assets.scripts.grammar;
using Assets.scripts.grammar.runtime;
using UnityEngine;

namespace grammar.formal.evaluators
{
    [CreateAssetMenu(fileName = "New attribute evaluator", menuName = "Math_grammar/Random Number evaluator")]
    public class Evaluator_RandomNumber: AttributeEvaluator
    {
        public int minInt = 1;
        public int maxInt = 10;

        public string attributeName = "value";
        
        
        public override void Evaluate(List<Node> targets, List<Node> source)
        {
            //source is not used here
            foreach (Node target in targets)
            {
                int randomInt = Random.Range(minInt, maxInt);
                SetAttribute(target, attributeName, randomInt);
            }
        }
    }
}