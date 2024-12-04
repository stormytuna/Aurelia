using DefaultNamespace;

namespace DataStructures
{
	public record ShipData
	{
		public float Mass = 0f;
		public ShipStat Health = new();
		public ShipStat Thrust = new();
		public ShipStat RotationSpeed = new();
		public ShipStat ShootSpeed = new();
		public ShipStat MaxShootDelay = new();
		public ShipStat Damage = new();
	}
}
