using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class PanicState : State
    {
        public PanicState(NPCScript script) : base(script)
        {

        }
        public override void Update()
        {
            PanicMode();
            if (!_system.PanicState)
            {
                _system.SetState(new RoamState(_system));
            }
        }

        void PanicMode()
        {
            //int layerMask = 1 << 8; // do layer 8 ,, == bitwise
            //layerMask = ~layerMask; // ~ == reverses it so it does all but 8

            RaycastHit hit;
            if (Physics.Raycast(_system.Raypos.position, _system.transform.TransformDirection(Vector3.forward), out hit, _system.RayRange))
            {
                Debug.DrawRay(_system.Raypos.position, _system.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                Quaternion newRotation;
                newRotation = UnityEngine.Random.rotation;
                _system.transform.rotation = Quaternion.Euler(0f, newRotation.eulerAngles.y, 0f);
            }
            else
            {
                Debug.DrawRay(_system.Raypos.position, _system.transform.TransformDirection(Vector3.forward) * _system.RayRange, Color.white);
                //move to the end of the ray
                _system.transform.position += _system.transform.forward * Time.deltaTime * _system.RunSpeed;
                _system.anim.SetFloat("Speed", 3);
            }
        }

    }
}
