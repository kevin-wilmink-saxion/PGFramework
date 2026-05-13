using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using UnityEngine;

namespace Assets.scripts.grammar
{
    [CreateAssetMenu(fileName = "newNonTerminal", menuName = "Grammar/New Non Terminal")]
    public class NonTerminal: Symbol
    {
        public List<Production> productions = new List<Production>();
        
       
        
        public Production GetRandomAvailableProduction(NonTerminalNode lhsNode)
        {
            var availableProductions = GetAvailableProductions(lhsNode);

            if (availableProductions.Count == 0)
            {
                Debug.LogError("No productions available in non-terminal: " + name);
                return null;
            }
            
            //Return a random one
            return availableProductions[Random.Range(0, availableProductions.Count)];
        }


        public List<Production> GetAvailableProductions(NonTerminalNode lhsNode)
        {
            List<Production> result = new List<Production>();
            foreach (Production production in productions)
            {
                if (production.AreConstraintsSatisfied(lhsNode))
                {
                    result.Add(production);
                }
            }
            
            return result;
        }



        
    }
}