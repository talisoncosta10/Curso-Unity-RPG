using System.Collections.Generic;
using UnityEngine;

public class AudioSteps : MonoBehaviour
{
    public AudioSource sourceSteps;
    private AudioClip clips;

    public List<AudioClip> floorSteps;
    public List<AudioClip> waterSteps;

    public List<AudioClip> currentList;

    public void Steps()
    {
        clips = currentList[Random.Range(0, currentList.Count)];
        sourceSteps.PlayOneShot(clips);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Floor"))
        {
            currentList = floorSteps;
        }

        if (hit.gameObject.CompareTag("Water"))
        {
            currentList = waterSteps;
        }
    }


}
