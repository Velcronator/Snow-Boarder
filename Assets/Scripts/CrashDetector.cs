using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private float reloadDelay = 2.0f;
    [SerializeField] private ParticleSystem particleEffect;

    [SerializeField] private GameObject[] spriteParts; // Assign the sprite parts in the inspector
    [SerializeField] private float separationForce = 5f; // Force applied to each part

    [SerializeField] private SpriteRenderer srTurnOffShadow; // Turn off the sprite renderer of the shadow.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            TriggerParticleEffect();
            SeparateParts();
            Invoke("ReloadScene", reloadDelay);
        }
    }

    private void TriggerParticleEffect()
    {
        // stop the transform of the object and play the particle effect
        GetComponent<Rigidbody2D>().simulated = false;
        particleEffect.Play();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SeparateParts()
    {
        // if a shadow is present, turn it off
        if (srTurnOffShadow != null)
        {
            srTurnOffShadow.enabled = false;
        }

        foreach (GameObject part in spriteParts)
        {
            part.transform.SetParent(null); // Detach from the parent
            Rigidbody2D rb = part.GetComponent<Rigidbody2D>();

            if (rb == null)
            {
                rb = part.AddComponent<Rigidbody2D>(); // Add Rigidbody2D if not already present
            }

            rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * separationForce, ForceMode2D.Impulse);
        }
    }
}
