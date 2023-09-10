using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private readonly float jumpForce = 555f;
    private readonly float gravityModifier = 1.25f;
    private int score;
    [NonSerialized] public bool gameOver;
    private bool isOnGround;
    
    private Rigidbody playerRigidbody;
    private Animator anim;
    [SerializeField] private ParticleSystem explosionVFX;
    [SerializeField] private ParticleSystem runningVFX;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private TextMeshProUGUI scoreTMP;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        InvokeRepeating(nameof(IncreaseScore),0f,1f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump_trig");
            _audioSource.PlayOneShot(jumpSound);
            runningVFX.Stop();
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Ground"))
        {
            isOnGround = true;
            runningVFX.Play();
        }
        else if (other.transform.CompareTag("Obstacle"))
        {
            gameOver = true;
            runningVFX.Stop();
            _audioSource.PlayOneShot(crashSound);
            explosionVFX.Play();
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int",1);

            CancelInvoke();
            Invoke(nameof(RestartGame),1.5f);
            
            print("Game Over");
        }
    }

    void IncreaseScore()
    {
        if (!gameOver)
        {
            score++;
            scoreTMP.text = score.ToString();
        }
    }

    private void RestartGame()
    {
        score = 0;
        InvokeRepeating(nameof(IncreaseScore),0f,1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
