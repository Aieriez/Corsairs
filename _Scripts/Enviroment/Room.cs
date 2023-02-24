using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public event Action<Room> OnRoomActivated;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent<Ship>(out var ship))
        {
            OnRoomActivated?.Invoke(this);
            Debug.Log("colisor." + gameObject.name);
        }
   }

   public void DisableCamera()
   {   
       virtualCamera.enabled = false;
   }

   public void EnableCamera(Transform target)
   {
       virtualCamera.enabled = true;
       virtualCamera.Follow = target;
   }
}
