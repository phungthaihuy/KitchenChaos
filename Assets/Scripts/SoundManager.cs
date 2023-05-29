using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioRefsSO audioRefsSO;

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        Playerr.Instance.OnPlayerPickedUp += Playerr_OnPlayerPickedUp;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnDropSth += BaseCounter_OnDropSth;
        TrashCounter.OnTrash += TrashCounter_OnTrash;
        StoveCounter.OnStoveSizzleSoundLoop += StoveCounter_OnStoveSizzleSoundLoop;
        PlayerFootstepSound.OnFootStep += PlayerFootstepSound_OnFootStep;
    }

    private void PlayerFootstepSound_OnFootStep(object sender, System.EventArgs e)
    {
        PlayerFootstepSound playerFootstepSound = sender as PlayerFootstepSound;
        PlayAudio(audioRefsSO.footStep, playerFootstepSound.transform.position);
    }

    private void StoveCounter_OnStoveSizzleSoundLoop(object sender, System.EventArgs e)
    {
        StoveCounter stoveCounter = sender as StoveCounter;
        PlayAudio(audioRefsSO.panSizzleLoop, stoveCounter.transform.position);
    }

    private void TrashCounter_OnTrash(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlayAudio(audioRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnDropSth(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlayAudio(audioRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Playerr_OnPlayerPickedUp(object sender, System.EventArgs e)
    {
        Playerr playerr = Playerr.Instance;
        PlayAudio(audioRefsSO.objectPickUp, playerr.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlayAudio(audioRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayAudio(audioRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlayAudio(audioRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlayAudio(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    private void PlayAudio(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
}
