using DataStructures;

namespace ShipParts
{
	public interface IShipPart
	{
		ShipData InitialiseShipData(ShipData shipData);
		ShipData UpdateShipData(ShipData shipData, ShipInputData inputData);
	}
}
