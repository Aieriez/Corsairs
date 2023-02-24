using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
   public void DestroyObj()
   {
        Destroy(gameObject);
   }
}
