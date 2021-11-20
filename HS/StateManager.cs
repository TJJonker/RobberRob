using System;
using System.Diagnostics;

namespace HS
{
    class StateManager
    {
        public static StateManager current;

        public Random random = new Random();

        private Stopwatch stopwatch = new Stopwatch();

        public RobbingBankState robbingBankState;
        public HavingGoodTimeState havingGoodTimeState;
        public FleeingState fleeingState;
        public LayingLowState layingLowState;
        public ImprisonedState imprisonedState;
        public FightingState fightingState;

        private BaseState CurrentState;

        public int distanceToCop;
        public int strength;
        public int wealth;

        public void Setup()
        {
            current = this;
            random = new Random();
            InitializeStates();
            ChangeState(robbingBankState);
            stopwatch.Start();
        }
        public void Update()
        {
            if (stopwatch.Elapsed.TotalSeconds < 1) return;
            stopwatch.Restart();
            CurrentState.ExecuteState();
        }
        public void ChangeState(BaseState state)
        {
            CurrentState = state;
            CurrentState.EnterState();
        }
        private void InitializeStates()
        {
            robbingBankState = new RobbingBankState();
            havingGoodTimeState = new HavingGoodTimeState();
            fleeingState = new FleeingState();
            layingLowState = new LayingLowState();
            imprisonedState = new ImprisonedState();
            fightingState = new FightingState();
        }

    }
}
