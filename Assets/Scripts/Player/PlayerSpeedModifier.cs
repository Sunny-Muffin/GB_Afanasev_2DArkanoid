namespace Arkanoid
{
    internal sealed class PlayerSpeedModifier : PlayerModifier
    {
        private readonly float _newSpeed;
        public PlayerSpeedModifier(Player player, float newSpeed) : base(player)
        {
            _newSpeed = newSpeed;
        }

        public override void Handle()
        {
            _player._speed = _newSpeed;
            base.Handle();
        }
    }
}
