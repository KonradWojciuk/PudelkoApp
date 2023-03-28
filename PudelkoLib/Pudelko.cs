namespace PudelkoLib
{
    public class Pudelko : IFormattable, IEquatable<Pudelko>
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private readonly UnitOfMeasure _measure;

        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure measure = UnitOfMeasure.meter)
        {
            if (a < 0 || b < 0 || c < 0)
                throw new ArgumentOutOfRangeException();

            if ((a > 10 || b > 10 || c > 10) && measure == UnitOfMeasure.meter)
                throw new ArgumentOutOfRangeException();

            _a = a;
            _b = b;
            _c = c;
            _measure = measure;
        }

        public double A => _a;
        public double B => _b;
        public double C => _c;

        public override string ToString() => $"{A:F3} m × {B:F3} m × {C:F3} m";

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return format switch
            {
                "m" => $"{A:F3} m × {B:F3} m × {C:F3} m",
                "cm" => $"{A * 100:F1} cm × {B * 100:F1} cm × {C * 100:F1} cm",
                "mm" => $"{A * 1000:F0} mm × {B * 1000:F0} mm × {C * 1000:F1} mm",
                _ => throw new FormatException(),
            };
        }

        public double Objetosc => Math.Round(A * B * C, 9);

        public double Pole => Math.Round(2 * A + 2 * B + 2 * C, 6);

        #region Equals(), GetHash() and overload operators: '==', '!=', '+'

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Pudelko)obj;

            return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        }

        public bool Equals(Pudelko other)
        {
            if (other == null)
                return false;

            double[] dimensions = { A, B, C };
            double[] otherDimensions = { other.A, other.B, other.C };

            if (_measure != UnitOfMeasure.meter)
            {
                if (_measure == UnitOfMeasure.centimiter)
                    dimensions = Array.ConvertAll(dimensions, x => x * 100);
                if (_measure == UnitOfMeasure.milimeter)
                    dimensions = Array.ConvertAll(dimensions, x => x * 1000);
            }

            if (other._measure != UnitOfMeasure.meter)
            {
                if (other._measure == UnitOfMeasure.centimiter)
                    dimensions = Array.ConvertAll(dimensions, x => x * 100);
                if (other._measure == UnitOfMeasure.milimeter)
                    dimensions = Array.ConvertAll(dimensions, x => x * 1000);
            }

            Array.Sort(dimensions);
            Array.Sort(otherDimensions);

            return dimensions.SequenceEqual(otherDimensions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C, _measure);
        }

        public static bool operator ==(Pudelko p1, Pudelko p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Pudelko p1, Pudelko p2)
        {
            return !p1.Equals(p2);
        }

        #endregion
    }
}