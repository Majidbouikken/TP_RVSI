using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_animator.GetBool("character_nearby"))
        {
            _animator.SetBool("character_nearby", true);
            PlaySound();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && _animator.GetBool("character_nearby"))
        {
            _animator.SetBool("character_nearby", false);
            PlaySound();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject.name.StartsWith("door_3"))
                {
                    _animator.SetBool("character_nearby", !_animator.GetBool("character_nearby"));
                    PlaySound();
                }
            }
        }
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}
