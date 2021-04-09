namespace Outrun
{
    public class BrokenCar : Obstacle
    {
        public BrokenCar(float x, float y, string mode) : base(x, y, $"Art/{mode}_obstacle_000.png", false, true, 0.05f)
        {
        }
    }
}