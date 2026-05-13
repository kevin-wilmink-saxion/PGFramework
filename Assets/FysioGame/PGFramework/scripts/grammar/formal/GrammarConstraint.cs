using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using UnityEngine;

namespace Assets.scripts.grammar
{
    public abstract class GrammarConstraint: ScriptableObject
    {
        public abstract bool IsSatisfied(NonTerminalNode lhs, List<Symbol> rhsProduction);
    }
}