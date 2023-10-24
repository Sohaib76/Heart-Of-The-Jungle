using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a token.
    /// </summary>
    /// <typeparam name="PlayerCollision"></typeparam>
    public class PlayerTokenCollision : Simulation.Event<PlayerTokenCollision>
    {

        public PlayerController player;
        public TokenInstance token;



        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        private int count = 0;




        public override void Execute()
        {
            AudioSource.PlayClipAtPoint(token.tokenCollectAudio, token.transform.position);

 
            count = count + 1;
            GameController.projectileCount++;

            
            

           




            Debug.Log("Number of Tokens" + count);
        }
    }
}