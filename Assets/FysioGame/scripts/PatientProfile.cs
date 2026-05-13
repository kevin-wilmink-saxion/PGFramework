using UnityEngine;

namespace PGFramework.examples.grammar_fysio.scripts
{
    [CreateAssetMenu(fileName = "New Patient Profile", menuName = "Fysio/Patient Profile")]
    public class PatientProfile : ScriptableObject
    {
        [Tooltip(
            "Relative frequency of repitions focused on a specific body part, " +
            "index 0=arms, 1=leggs, 2=chest. Give an item a higher number to increase the relative frequency.")]
        public int[] bodyPartFrequencies = new int[] {1, 1, 1};

        [Tooltip(
            "Relative frequency of repitions focused on a side of the body, " +
            "index 0=left, 1=right, 2=center. Give an item a higher number to increase the relative frequency.")]
        public int[] sideFrequencies = new int[] {1, 1, 1};

        [Tooltip(
            "Relative frequency an exercise type, " +
            "index 0=follow, 1=hit, 2=dodge. Give an item a higher number to increase the relative frequency.")]
        public int[] taskFrequencies = new int[] {1, 1, 1};

        [Tooltip("Relative minimal difficutly of the exercises.")]
        public int difficultyMin = 1;
        [Tooltip("Relative maximal difficutly of the exercises.")]
        public int difficultyMax = 100;

        
        [Tooltip("The total number of repetitions.")]
        public int numberOfRepetitions_min = 10;
        public int numberOfRepetitions_max = 10;
    }
}