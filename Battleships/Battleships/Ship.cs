namespace Battleships
{
    public class Ship
    {
        public ShipCoordinates StartCoordinates { get; }
        public ShipCoordinates EndCoordinates { get; }

        public HitStatus GetHitStatus(Coordinates c)
        {
            return HitStatus.Miss;
        }
    }
}