namespace HS
{
    class StateManager
    {
        public static StateManager current;

        public RobbingBankState robbingBankState;
        public HavingGoodTimeState havingGoodTimeState;
        public FleeingState fleeingState;
        public LayingLowState layingLowState;
        public ImprisonedState imprisonedState;
        public FightingState fightingState;


        private BaseState CurrentState;

        public void Setup()
        {
            current = this;
            InitializeStates();
            ChangeState(robbingBankState);
        }
        public void Update()
        {
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
