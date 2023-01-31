namespace Arkanoid
{
    internal sealed class PlayerHealthModifier : PlayerModifier
    {
        private readonly float _addHP;
        public PlayerHealthModifier(Player player, float addHP) : base(player)
        {
            _addHP = addHP;
        }

        public override void Handle()
        {
            _player._maxHp += _addHP;
            base.Handle();
        }
    }
}
