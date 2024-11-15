using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocità di movimento
    public float gravity = -9.81f; // Gravità applicata al personaggio

    private CharacterController controller; // Riferimento al controller del personaggio
    private Vector3 velocity; // Velocità verticale per la gravità

    void Start()
    {
        // Ottenere il componente CharacterController
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimento del giocatore con i tasti WASD
        float moveX = Input.GetAxis("Horizontal"); // Tasti A e D
        float moveZ = Input.GetAxis("Vertical"); // Tasti W e S

        // Movimento in avanti, indietro e laterale
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Applicare la gravità al personaggio
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Resettare la velocità verticale se il giocatore è a terra
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
