namespace destructive_code.PlayerCodeBase.Standard
{
    public sealed class StandardStateFactory<TState> : PlayerStateFactory
        where TState : PlayerState, new()
    {
        public override PlayerState GetState()
        {
            return new TState();
        }
    }
}