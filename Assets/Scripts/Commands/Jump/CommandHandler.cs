using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Commands.Jump
{
    public partial class Jump
    {
        public class CommandHandler
        {

            public IEnumerator Execute(Command command)
            {
                command.characterController.slopeLimit = 90.0f;
                float timeInAir = 0.0f;

                do
                {
                    float jumpForce = command.jumpFallOff.Evaluate(timeInAir);
                    command.characterController.Move(Vector3.up * jumpForce * command.jumpMultiplier * Time.deltaTime);
                    timeInAir += Time.deltaTime;
                    yield return null;
                } while (!command.characterController.isGrounded && command.characterController.collisionFlags != CollisionFlags.Above);

                command.characterController.slopeLimit = 45.0f;
            }
        }
    }
}
