using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class State
    {
        protected readonly NPCScript _system;

        public State(NPCScript system)
        {
            _system = system;
        }

        public virtual void Start()
        {
        }
        
        public virtual void Update()
        {

        }
    }
}
