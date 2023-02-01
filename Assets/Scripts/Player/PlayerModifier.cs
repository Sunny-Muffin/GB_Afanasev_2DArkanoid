namespace Arkanoid
{
    internal class PlayerModifier
    {
        protected Player _player;
        protected PlayerModifier Next;
        
        public PlayerModifier (Player player)
        {
            _player = player;
        }

        public void Add(PlayerModifier playerModifier)
        {
            if (Next != null)
            {
                Next.Add(playerModifier);
            }
            else
            {
                Next = playerModifier;
            }
        }

        public virtual void Handle() => Next?.Handle();
    }
}
