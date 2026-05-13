using Assets.scripts.grammar.runtime;
using grammar;
using UnityEngine;


public class PGTester : MonoBehaviour
{
    public GrammarGenerator grammarGenerator;
    public GrammarAsset grammarAsset;


    void Awake()
    {
        grammarGenerator.onGenerateTree.AddListener(OnGrammarGenerated);
    }
    

    void Start()
    {
        TestGeneration();
    }



    public void TestGeneration()
    {
        grammarGenerator.StartNewGeneration(grammarAsset);
    }
    
    
    private void OnGrammarGenerated(Node root)
    {
        Debug.Log("Grammar generated: " + root.TreeToString());
    }

}
