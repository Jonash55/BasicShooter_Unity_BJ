using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //let camera follow target
    public class CameraFollow : MonoBehaviour
    {
        public float dampTime = 0.15f;
        private Vector3 velocity = Vector3.zero;
        public Transform target;

        // Update is called once per frame
        void LateUpdate()
        {
            if (target)
            {

                Vector3 point = Camera.main.WorldToViewportPoint(target.position);


                Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                Vector3 destination = transform.position + delta;


                transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

            }

        }

    }
}
