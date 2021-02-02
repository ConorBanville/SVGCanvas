using System;
namespace Assignment04
{
    public struct Point
    {
		public double x { get; set; }
		public double y { get; set; }

		public Point(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public String toString()
		{
			return $"{x},{y}";
		}

		public string GetX()
        {
			return x + "";
        }

		public string GetY()
        {
			return y + "";
        }
	}
}
