using System;
using System.Collections.Generic;

namespace HS
{
    class BaseState
    {
        protected StateManager sm;

        protected string stateMessage;
        private string inputMessage;

        protected const string getRich = "get rich";
        protected const string spotCop = "spot cop";
        protected const string getTired = "get tired";
        protected const string feelSafe = "feel safe";
        protected const string getCaught = "get caught";
        protected const string escape = "escape";
        protected const string rebel = "rebel";
        protected const string getOverpowered = "get overpowered";
        protected const string feelScared = "feel scared";

        protected Dictionary<string, BaseState> triggers;

        public BaseState() => sm = StateManager.current;

        /// <summary>
        /// Behaviour when entering the state
        /// </summary>
        public void EnterState() 
        {
            if(triggers == null)
            {
                triggers = new Dictionary<string, BaseState>();
                SetInputOptions();
                SetInputMessage();
            }
            SetStateMessage();            
            WelcomeMessage();

        }

        /// <summary>
        /// Behaviour of the state
        /// </summary>
        public void ExecuteState()
        {            
            Console.Write("> ");
            var input = Console.ReadLine().ToLower();
            HandleInput(input);
        }

        /// <summary>
        /// Checks and changes state based on the given input
        /// </summary>
        /// <param name="input"> The input in a lower-cased string </param>
        private void HandleInput(string input) 
        {
            BaseState output;
            triggers.TryGetValue(input, out output);
            if (output != null) StateManager.current.ChangeState(output);
            else
            {
                Console.WriteLine("Not a valid input.");
                ExecuteState();
            }
        }
        
        /// <summary>
        /// Displays the welcome message
        /// </summary>
        private void WelcomeMessage()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine(stateMessage);
            Console.WriteLine(inputMessage);
        }

        /// <summary>
        /// Creates a string with the input options based on the input dictionary
        /// </summary>
        private void SetInputMessage()
        {
            inputMessage = "Input options are: ";
            foreach (KeyValuePair<string, BaseState> entry in triggers)
                inputMessage += entry.Key + ", ";
        }

        /// <summary>
        /// Used to add a string and BaseState pair to the input dictionary
        /// </summary>
        /// <param name="key"> string which will be compared to the input string </param>
        /// <param name="value"> State to swtich to when input is the same as the key </param>
        protected void AddToInputOptions(string key, BaseState value)
            => triggers.Add(key, value);

        /// <summary>
        /// Used to set the state message
        /// </summary>
        protected virtual void SetStateMessage() { }

        /// <summary>
        /// Used to set the input options  
        /// </summary>
        protected virtual void SetInputOptions() { }        

    }

    class RobbingBankState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "I'm robbing banks and getting loads of money! Pew pew!";
        }

        protected override void SetInputOptions()
        {
            AddToInputOptions(getRich, sm.havingGoodTimeState);
            AddToInputOptions(spotCop, sm.fleeingState);
        }

    }

    class HavingGoodTimeState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "I'm rich enough to have a good time";
        }

        protected override void SetInputOptions()
        {
            AddToInputOptions(getTired, sm.layingLowState);
            AddToInputOptions(spotCop, sm.fleeingState);
        }
    }

    class FleeingState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "I see a cop, so I have to start running";
        }

        protected override void SetInputOptions()
        {
            AddToInputOptions(feelSafe, sm.robbingBankState);
            AddToInputOptions(getTired, sm.layingLowState);
            AddToInputOptions(getCaught, sm.imprisonedState);
        }
    }

    class LayingLowState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "I'm getting very tired, so I better lay low for a while";
        }

        protected override void SetInputOptions()
        {
            AddToInputOptions(feelSafe, sm.robbingBankState);
        }
    }

    class ImprisonedState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "They got me, better get out of here soon!";
        }

        protected override void SetInputOptions()
        {
            AddToInputOptions(rebel, sm.fightingState);
            AddToInputOptions(escape, sm.fleeingState);
        }
    }

    class FightingState : BaseState
    {
        protected override void SetStateMessage()
        {
            stateMessage = "Haha, they will never get me alive!";
        }

        protected override void SetInputOptions()
        {
            AddToInputOptions(feelScared, sm.fleeingState);
            AddToInputOptions(getOverpowered, sm.imprisonedState);
        }
    }
}
