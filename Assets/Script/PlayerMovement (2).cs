using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocità di movimento
    public float gravity = -9.81f; // Gravità applicata al personaggio
    public AudioClip footstepSound; // Suono dei passi
    public float footstepInterval = 0.5f; // Intervallo tra i suoni dei passi

    private CharacterController controller; // Riferimento al controller del personaggio
    private Vector3 velocity; // Velocità verticale per la gravità
    private AudioSource audioSource; // Componente AudioSource
    private float footstepTimer; // Timer per i passi

    void Start()
    {
        // Ottenere il componente CharacterController
        controller = GetComponent<CharacterController>();

        // Aggiungere un AudioSource al GameObject se non esiste
                audioSource = GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }

                // Configurare l'AudioSource
                audioSource.clip = footstepSound;
                audioSource.loop = false;
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

        // Suono dei passi
                if (controller.isGrounded && move.magnitude > 0)
                {
                    footstepTimer -= Time.deltaTime;
                    if (footstepTimer <= 0)
                    {
                        PlayFootstep();
                        footstepTimer = footstepInterval;
                    }
                }
                else
                {
                    footstepTimer = 0; // Resetta il timer quando il personaggio si ferma
                }
    }

    private void PlayFootstep()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
}
