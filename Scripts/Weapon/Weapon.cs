using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	private Animator anim;
	private AudioSource _AudioSource;

	int floorMask;

	public float range = 100f;
	public int bulletsPerMag = 30; //Bullets per each magazine
	public int bulletsLeft = 200; //Total bullets we have

	public int currentBullets; //The current bullets in our magazine

	public enum ShootMode {Auto, Semi}
	public ShootMode shootingMode;

	public Transform shootPoint;
	public GameObject hitParticles;
	public GameObject bulletImpact;

	public ParticleSystem muzzleFalsh;
	public AudioClip shootSound;

	public float fireRate = 0.1f;
	public float damage = 20f;

	float fireTimer;
	private bool isReloading;
	private bool isAiming;
	private bool shootInput;

	private Vector3 originalPosition;
	public Vector3 aimPosition;
	public float aodSpeed = 8f;

	// Use this for initialization
	void Start () {
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		_AudioSource = GetComponent<AudioSource>();

		currentBullets = bulletsPerMag;
		originalPosition = transform.localPosition;
	
	}
	
	// Update is called once per frame
	void Update () {

		switch (shootingMode) 
		{
		case ShootMode.Auto:
			shootInput = Input.GetButton ("Fire1");
			break;
		case ShootMode.Semi:
			shootInput = Input.GetButtonDown("Fire1");
			break;
		}
		if (shootInput) 
		{
			if (currentBullets > 0)
				Fire ();  //Execute the fire function if we press/hold the left mouse button
		
			else if(bulletsLeft > 0)
				DoReload ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			if (currentBullets < bulletsPerMag && bulletsLeft > 0)
				DoReload ();
		}
		if (fireTimer < fireRate)
			fireTimer += Time.deltaTime;    //Add into time counter

		AimDownSights ();
	}

	void FixedUpdate()
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);
		isReloading = info.IsName ("Reload");
		anim.SetBool ("Aim", isAiming);
	}

	private void AimDownSights()
	{
		if(Input.GetButton("Fire2") && !isReloading)
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition, aimPosition, Time.deltaTime * aodSpeed);
			isAiming = true;	
		}
		else
		{
			transform.localPosition = Vector3.Lerp (transform.localPosition, originalPosition, Time.deltaTime * aodSpeed);
			isAiming = false;
		}
	}

	private void Fire()
	{
		if (fireTimer < fireRate || currentBullets <= 0 || isReloading)
			return;
		
		RaycastHit hit;

		if (floorMask != 0 && Physics.Raycast (shootPoint.position, shootPoint.transform.forward, out hit, range)) 
		{
			Debug.Log (hit.transform.name + " found!");

			GameObject hitParticleEffect = Instantiate (hitParticles, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal));
			GameObject bulletHole = Instantiate (bulletImpact, hit.point, Quaternion.FromToRotation (Vector3.forward, hit.normal));

			Destroy (hitParticleEffect, 1f);
			Destroy (bulletHole, 2f);

			if (hit.transform.GetComponent<HealthController> ()) {
				hit.transform.GetComponent<HealthController> ().ApplyDamage (damage);
			}
		}

		anim.CrossFadeInFixedTime ("Fire", 0.01f);
		muzzleFalsh.Play ();
		PlayShootSound ();

		currentBullets--;
		fireTimer = 0.0f; //Reset fire timer
	
	}

	public void Reload()
	{
		if(bulletsLeft <= 0) return;

		int bulletsToLoad = bulletsPerMag - currentBullets;
		int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

		bulletsLeft -= bulletsToDeduct;
		currentBullets += bulletsToDeduct;
	}

	private void DoReload()
	{
		AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo (0);

		if (isReloading)
			return;
		anim.CrossFadeInFixedTime ("Reload", 0.01f);
	}

	private void PlayShootSound()
	{
		_AudioSource.PlayOneShot (shootSound);
		//_AudioSource.clip = shootSound;
		//_AudioSource.Play ();
	}
}
