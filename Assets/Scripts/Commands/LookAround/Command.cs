using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Commands
{
    public partial class LookAround
    {
        public class Command
        {
            public string mouseXInputName { get; set; }
            public string mouseYInputName { get; set; }
            public float mouseSensitivity { get; set; }
            public Transform transform { get; set; }
            public Transform playerBody { get; set; }


        }
    }
}
