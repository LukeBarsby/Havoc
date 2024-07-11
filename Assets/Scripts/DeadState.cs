using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class DeadState : State
    {
        float speed;
        public DeadState(NPCScript script) : base(script)
        {
        }
        public override void Start()
        {
            Die();
        }

        void Die()
        {
            _system.anim.Play("Dead");
            _system.col.enabled = false;
            _system.rb.isKinematic = true;
            //Play effect
        }
    }
}
