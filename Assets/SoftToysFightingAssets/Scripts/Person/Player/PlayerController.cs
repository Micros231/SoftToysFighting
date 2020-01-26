using UnityEngine;

namespace Com.SoftToysFighting.Person.Player
{
    public class PlayerController : PersonController
    {
        private  PlayerParameters PlayerParameters { get; set; }
        private  PersonInput PlayerInput { get; set; }

        protected override void InitPersonSystems()
        {
            base.InitPersonSystems();
            PlayerParameters = GetComponent<PlayerParameters>();
            PlayerInput = GetComponent<PersonInput>();
        }
        protected override void Attack()
        {
            bool attackSuper = PlayerParameters.EnergyParameter.Value >= PlayerParameters.EnergyParameter.MaxValue 
                               && PlayerInput.AttackSuper() 
                               && !PersonAnimator.IsAttack;
            if (attackSuper)
            {
                PlayerParameters.EnergyParameter.Value = 0;
            }

            PersonAttacker.Attack(PlayerInput.AttackHand(), PlayerInput.AttackLeg(), attackSuper);
        }
    }
}
