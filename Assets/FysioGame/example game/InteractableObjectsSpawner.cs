using System.Collections.Generic;
using UnityEngine;

namespace FysioGame.example_game
{
    public class InteractableObjectsSpawner: MonoBehaviour
    {
        public GameObject enemyPrefab;
        public GameObject coinPrefab;
        public List<SpawnableObject> queueWithSpawnables = new List<SpawnableObject>();

        
        public float MAX_SPEED = 15f;
        private float TIME_BETWEEN_SPAWN = 1f;
    

        private int indexNextSpawn = -1; //pause while at -1
        private float timeSinceLastSpawn = 0f;


        public void Reset()
        {
            indexNextSpawn = 0;
            timeSinceLastSpawn = 0;
        }
        
        
        //based on the input from the PG, add an enemy or coin to the queue with spawnables
        public void AddSpawnableToQueue(int taskNr, int bodyPart, float side, int difficulty)
        {
            //use the values above to spawn a new InteractableObject
            SpawnableObject spawnableObject = new SpawnableObject();

            //if task==2(dodge) spawn an enemy, else spawn a coin
            if (taskNr == 2)
            {
                spawnableObject.prefab = enemyPrefab;
            }
            else
            {
                spawnableObject.prefab = coinPrefab;
            }


            //use body part to set the y position:
            if (bodyPart == 0)//arms = top
                spawnableObject.position.y = Random.Range(1, 6);
            if (bodyPart == 1) //leggs = bottom
                spawnableObject.position.y = -Random.Range(1, 6);
            if (bodyPart == 2) //chest = middle
                spawnableObject.position.y = Random.Range(-3, 3);


            //use side to set the x position:
            if (side == 0) //left
                spawnableObject.position.x = -9;
            if (side == 1) //right
                spawnableObject.position.x = 9;
            if (side == 2) //center
                if (Random.Range(0, 1) == 0) //50% chance left or right
                    spawnableObject.position.x = -9;
                else
                    spawnableObject.position.x = -9;
        

        
            //use difficulty to set the speed of the object
            float relativeDifficulty = difficulty / 100f; //returns value between 0 and 1
            spawnableObject.speed = MAX_SPEED * relativeDifficulty;
        
            //add the object to the queue, so it can be spawned later
            queueWithSpawnables.Add(spawnableObject);
        }
        
        
        
        void Update()
        {
            UpdateNextSpawn();
        }

        
        
        
        

        /// <summary>
        /// Loop through the queue and spawn objects
        /// </summary>
        private void UpdateNextSpawn()
        {
            //if done or not started, return now
            if (indexNextSpawn == -1 || indexNextSpawn >= queueWithSpawnables.Count)
                return;
        
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn < TIME_BETWEEN_SPAWN)
                return;
        
            SpawnableObject spawnableObject = queueWithSpawnables[indexNextSpawn];
            GameObject newSpawn = Instantiate(spawnableObject.prefab);
            newSpawn.transform.position = spawnableObject.position;
            indexNextSpawn++;
            timeSinceLastSpawn = 0f;
        }



        [System.Serializable]
        public class SpawnableObject
        {
            public Vector2 position;
            public float speed;
            public GameObject prefab;        
        }
    }
}