using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class RoamState : State
    {
        Vector3 lookDir;
        Vector3 rotation;
        Quaternion face;

        public RoamState(NPCScript script) : base(script)
        {
        }

        public override void Start()
        {
            _system.transform.position = _system.Waypoints[_system.index].transform.position;
        }

        public override void Update()
        {
            Move();
            if (_system.PanicState)
            {
                _system.SetState(new PanicState(_system));
            }
        }

        void Move()
        {
            _system.transform.position = Vector3.MoveTowards(_system.transform.position, _system.Waypoints[_system.index].transform.position, _system.Speed * Time.deltaTime);

            lookDir = _system.Waypoints[_system.index].transform.position - _system.transform.position;
            if (lookDir != Vector3.zero)
            {
                face = Quaternion.LookRotation(lookDir);
            }
            else
            {
                face = _system.gameObject.transform.rotation;
            }

            rotation = Quaternion.Lerp(_system.transform.rotation, face, Time.deltaTime * 5f).eulerAngles;
            _system.transform.rotation = Quaternion.Euler(0, rotation.y, 0);

            _system.anim.SetFloat("Speed", 2);


            if (_system.transform.position == _system.Waypoints[_system.index].transform.position)
            {
                _system.index += 1;
            }

            if (_system.index == _system.Waypoints.Length)
            {
                _system.index = 0;
            }
        }


    }
}
