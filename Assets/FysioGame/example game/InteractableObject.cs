using UnityEngine;

namespace FysioGame.example_game
{
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Vector3 moveDirection;


        //set to false if the player needs to pick this up instead
        public bool dealsDamage = true;

        void Start()
        {
            // Determine movement direction based on spawn position
            if (transform.position.x < 0)
            {
                moveDirection = Vector3.right; // Move right
            }
            else if (transform.position.x > 0)
            {
                moveDirection = Vector3.left; // Move left
            }
            else // x == 0
            {
                if (transform.position.y > 0)
                {
                    moveDirection = Vector3.down; // Move down
                }
                else // y < 0
                {
                    moveDirection = Vector3.up; // Move up
                }
            }
        }

        void Update()
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }
        
    }
}