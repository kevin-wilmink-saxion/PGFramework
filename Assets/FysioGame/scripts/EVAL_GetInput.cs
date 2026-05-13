using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using PGFramework.examples.grammar_fysio.scripts;
using UnityEngine;

namespace Assets.scripts.grammar.evaluators
{
    [CreateAssetMenu(fileName = "Get a inputm value", menuName = "Fysio/Add get input evaluator")]
    public class Evaluator_GetInput : AttributeEvaluator
    {
        public string attributeName = "value";
        
        public PGFysioInputManager.InputTypes inputType;
        
        public override void Evaluate(List<Node> targets, List<Node> source)
        {
            foreach (Node target in targets)
            {
                int value = PGFysioInputManager.Instance.GetNextInput(inputType);
                SetAttribute(target, attributeName, value);
            }
        }
    }
}