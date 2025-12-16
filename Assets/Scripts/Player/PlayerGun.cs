using System;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 1.0f;

    [SerializeField]
    private GameObject bulletOrigin;

    [SerializeField]
    private float shootingCooldown = 1.0f;

    private float shootingCooldownTimer = 0.0f;

    private void Start()
    {
        shootingCooldown = 0;
            //RhythmManager.Instance.beatDureationMs;
    }

    private void Update()
    {
        // track cooldown between shots
        shootingCooldownTimer -= Time.deltaTime;

        // shoot if pressing button and shooting not on cooldown
        if (Input.GetKeyDown(KeyCode.Space) && shootingCooldownTimer <= 0 && GameManager.Instance.isOnBeat)
        {
            GameManager.Instance.playerPressedButton();
            Shoot();
        }
    }
    private void Shoot()
    {
        // create bullet at bullet origin's location and rotation, and launch with speed
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(0,0,bulletSpeed);

        AudioManager.Instance.playSFX(AudioManager.Instance.shootAudio);

        // reset shooting cooldown
        shootingCooldownTimer = shootingCooldown;
    }
}
