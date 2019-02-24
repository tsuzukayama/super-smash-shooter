using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Commands.Jump
{
    public partial class Jump
    {
        public class Command
        {
            public CharacterController characterController{ get; set; }
            public AnimationCurve jumpFallOff { get; set; }
            public float jumpMultiplier { get; set; }
        }
    }
}
