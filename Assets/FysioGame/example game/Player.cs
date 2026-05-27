using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace FysioGame.example_game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        void Update()
        {
            float horizontalInput = 0f;
            float verticalInput = 0f;

            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed)
                {
                    verticalInput += 1f;
                }
                if (Keyboard.current.sKey.isPressed)
                {
                    verticalInput -= 1f;
                }
                if (Keyboard.current.aKey.isPressed)
                {
                    horizontalInput -= 1f;
                }
                if (Keyboard.current.dKey.isPressed)
                {
                    horizontalInput += 1f;
                }
            }

            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
            transform.position += movement * speed * Time.deltaTime;
        }
        
        
        
        void OnTriggerEnter2D(Collider2D other)
        {
            InteractableObject interactable = other.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                if (interactable.dealsDamage)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else
                {
                    Debug.Log("coin collected: gain points?");
                    Destroy(other.gameObject);
                }
            }
        }
    }
}