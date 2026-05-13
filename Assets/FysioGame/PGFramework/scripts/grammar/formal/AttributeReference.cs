namespace Assets.scripts.grammar
{
    [System.Serializable]
    public class AttributeReference<T>
    {
        public AttributeDefinition attributeDefinition;
        public T value; //todo: extend further. For now every attribute uses an int as value)


        public AttributeReference(AttributeDefinition attributeDefinition, T originalValue)
        {
            this.attributeDefinition = attributeDefinition;
        }


        override 
        public string ToString()
        {
            return attributeDefinition.ToString() +": "+ value;
        }
    }
}