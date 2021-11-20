using System;

namespace HS
{
    class BaseState
    {
        protected StateManager sm;

        protected string stateMessage;

        public BaseState() => sm = StateManager.current;

        /// <summary>
        /// Behaviour when entering the state
        /// </summary>
        public void EnterState() 
        {
            SetStateMessage();
        }

        /// <summary>
        /// Behaviour of the state
        /// </summary>
        public void ExecuteState()
        {
            StateEffect();
            WelcomeMessage();
        }
        
        /// <summary>
        /// Displays the welcome message
        /// </summary>
        private void WelcomeMessage()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine(stateMessage);
            Console.WriteLine($"Wealth: {sm.wealth}, DistanceToCop: {sm.distanceToCop}, Strength: {sm.strength}");
        }

        /// <summary>
        /// Used to set the state message
        /// </summary>
        protected virtual void SetStateMessage() { }      

        /// <summary>
        /// Used to set the effect of the state
        /// </summary>
        protected virtual void StateEffect() { }
    }

    class RobbingBankState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "I'm robbing banks and getting loads of money! Pew pew!";
        }

        protected override void StateEffect()
        {
            sm.strength -= sm.random.Next(1, 3);
            sm.wealth += sm.random.Next(6, 10);
            sm.distanceToCop = sm.random.Next(0, 2);
        }
    }

    class HavingGoodTimeState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "I'm rich enough to have a good time";
        }

        protected override void StateEffect()
        {
            sm.strength -= sm.random.Next(1, 3);
            sm.wealth -= sm.random.Next(1, 3);
            sm.distanceToCop = sm.random.Next(0, 2);
        }
    }

    class FleeingState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "I see a cop, so I have to start running";
        }

        protected override void StateEffect()
        {
            sm.strength -= sm.random.Next(1, 3);
            sm.wealth -= sm.random.Next(1, 3);
        }
    }

    class LayingLowState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "I'm getting very tired, so I better lay low for a while";
        }

        protected override void StateEffect()
        {
            sm.strength += sm.random.Next(5, 8);
        }
    }

    class ImprisonedState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "They got me, better get out of here soon!";
        }

        protected override void StateEffect()
        {
            sm.wealth -= sm.random.Next(1, 3);
            sm.strength += sm.random.Next(2, 5);
        }
    }

    class FightingState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "Haha, they will never get me alive!";
        }

        protected override void StateEffect()
        {
            sm.strength -= sm.random.Next(2, 5);
        }
    }
}
