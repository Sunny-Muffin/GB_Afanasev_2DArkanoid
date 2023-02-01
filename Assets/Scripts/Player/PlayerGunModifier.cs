namespace Arkanoid
{
    internal sealed class PlayerGunModifier : PlayerModifier
    {
        private readonly string _newGun;
        public PlayerGunModifier(Player player, string newGun) : base(player)
        {
            _newGun = newGun;
        }

        public override void Handle()
        {
            _player._gunName = _newGun;
            base.Handle();
        }
    }
}
