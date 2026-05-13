using System.Collections.Generic;
using Assets.scripts.grammar.runtime;
using UnityEngine;


//TODO: override with more functionality

namespace Assets.scripts.grammar
{
    
    public abstract class AttributeEvaluator: ScriptableObject
    {
        
        
        /// <summary>
        /// target.someAttribute = source.someAttributes computation (fe: target.x = s1.x + s2.y)
        //  if lhs in prod is a synthesized attribute: target = lhs, source = rhs
        //  if lhs in prod is an inherited attribute: target = for each rhs Compute is called:
        //      target=rhs_element_x,  source = lhs
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        public abstract void Evaluate(List<Node> target, List<Node> source);


        public int GetAttributeValue(Node node, string attributeName)
        {
            return node.GetAttribute(attributeName).value;
        }


        public void SetAttribute(Node node, string attributeName, int attributeValue)
        {
            node.GetAttribute(attributeName).value = attributeValue;
            
            //TODO use node.SetAttribute(..)
        }

    }
}