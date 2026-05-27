

using Assets.scripts.grammar.runtime;

namespace FysioGame.scripts
{
    public interface IProceduralGenerationConsumer
    {
        public void OnProceduralGenerationFinished(Node node);
        public void OnNodeVisited(Node node);

    }
}