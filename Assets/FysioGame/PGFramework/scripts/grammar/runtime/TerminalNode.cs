using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.grammar.runtime
{
    public class TerminalNode: Node
    {
        public TerminalNode(Symbol symbol, NonTerminalNode parentNode) : base(symbol, parentNode)
        {
        }
        
    }
}