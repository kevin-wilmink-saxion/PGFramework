using System;
using grammar;
using UnityEngine;

namespace PGFramework.examples.grammar_fysio.scripts
{
    public class PGFysioInputManager : MonoBehaviour
    {
        private static PGFysioInputManager instance;

        public PatientProfile patientProfile;

        public enum InputTypes
        {
            BodyPart,
            Side,
            Task,
            Difficulty,
            TotalRepetitions
        }


        private void Awake()
        {
            instance = this;
        }

        public static PGFysioInputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogError("PGFysioInputManager is not initialized yet");
                    return null;
                }

                return instance;
            }
        }


        public int GetNextInput(InputTypes inputType)
        {
            switch (inputType)
            {
                case InputTypes.BodyPart:
                    return GetNextBodyPart();
                case InputTypes.Side:
                    return GetNextSide();
                case InputTypes.Task:
                    return GetNextTask();
                case InputTypes.Difficulty:
                    return GetNextDifficulty();
                case InputTypes.TotalRepetitions:
                    return GetNextTotalRepetitions();
                default:
                    throw new ArgumentException("Invalid input type");
            }
        }

        public int GetNextBodyPart()
        {
            return GrammarUtils.GetWeightedRandom(patientProfile.bodyPartFrequencies);
        }

        public int GetNextSide()
        {
            return GrammarUtils.GetWeightedRandom(patientProfile.sideFrequencies);
        }

        public int GetNextTask()
        {
            return GrammarUtils.GetWeightedRandom(patientProfile.taskFrequencies);
        }

        public int GetNextDifficulty()
        {
            return UnityEngine.Random.Range(patientProfile.difficultyMin,
                patientProfile.difficultyMax + 1);
        }

        public int GetNextTotalRepetitions()
        {
            return UnityEngine.Random.Range(patientProfile.numberOfRepetitions_min,
                patientProfile.numberOfRepetitions_max + 1);
        }

        public int GetNumberOfRepetitions()
        {
            return UnityEngine.Random.Range(patientProfile.numberOfRepetitions_min,
                patientProfile.numberOfRepetitions_max + 1);
        }
    }
}