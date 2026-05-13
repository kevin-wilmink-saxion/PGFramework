using UnityEngine;

namespace Assets.scripts.grammar
{

    [System.Serializable]
    public class AttributeDefinition
    {
        public enum AttributeKind
        {
            Synthesized,
            Inherited
        }


        public string name;
        //[Tooltip("NYI: always used as an int")]public string dataType = "int"; //todo: implement this functionality (for now always treat as an int)

        ///[Tooltip("NYI: (might not be needed?) attribute equations define the flow")]public AttributeKind kind;


        override
            public string ToString()
        {
            return name;
        }
    }
}
