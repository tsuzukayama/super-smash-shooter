using Assets.Scripts.Commands.Jump;
using Assets.Scripts.Commands.MoveAround;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private string horizontalInputName;
        [SerializeField] private string verticalInputName;
        [SerializeField] private float movementSpeed;

        [SerializeField] private AnimationCurve jumpFallOff;
        [SerializeField] private float jumpMultiplier;
        [SerializeField] private KeyCode jumpKey;

        private bool isJumping = false;

        private CharacterController charController;

        private MoveAround.CommandHandler moveAround;
        private Jump.CommandHandler jump;

        private void Awake()
        {
            charController = GetComponent<CharacterController>();

            moveAround = new MoveAround.CommandHandler();
            jump = new Jump.CommandHandler();
        }

        private void Update()
        {            
            moveAround.Handle(new MoveAround.Command
            {
                characterController = charController,
                horizontalInputName = horizontalInputName,
                verticalInputName = verticalInputName,
                movementSpeed = movementSpeed,
                transform = transform
            });
            if (Input.GetKeyDown(jumpKey) && !isJumping)
            {
                isJumping = true;
                StartCoroutine(jump.Execute(new Jump.Command
                {
                    characterController = charController,
                    jumpFallOff = jumpFallOff,
                    jumpMultiplier = jumpMultiplier,
                }));
                isJumping = false;
            }
        }
    }
}