using System.Collections.Generic;
using Assets.scripts.grammar;
using UnityEngine;

/// <summary>
/// Base class for an entire Grammar
/// </summary>

[CreateAssetMenu(fileName = "newGrammarAsset", menuName = "Grammar/New GrammarAsset")]
public class GrammarAsset : ScriptableObject
{
    
    public NonTerminal root;
    
    public List<AttributeDefinition> attributes = new List<AttributeDefinition>();
    //public List<Production> productions = new List<Production>();

}
