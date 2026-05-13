using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using grammar;
using UnityEngine;

namespace Assets.scripts.grammar
{
    [CreateAssetMenu(fileName = "new constraint (values)", menuName = "Grammar/constraints/value constraints")]
    [System.Serializable]
    public class GrammarConstraintValues: GrammarConstraint
    {
        public string lhsAttributeName = "";
        public int comparandValue = 0;
        
        public GrammarUtils.ComparisonOperator comparisonOperator = GrammarUtils.ComparisonOperator.Equal;
        
        
        public override bool IsSatisfied(NonTerminalNode lhs, List<Symbol> rhsProduction)
        {
            var attributeReference = lhs.GetAttribute(lhsAttributeName);
            if (attributeReference == null)
            {
                Debug.LogWarning($"Missing attribute {lhsAttributeName} on {lhs}");
                return false;
            }

            int lhsValue = attributeReference.value;
            return GrammarUtils.CompareIntegers(lhsValue, comparandValue, comparisonOperator);
        }
    }
}