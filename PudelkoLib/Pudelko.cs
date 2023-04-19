using System.Collections;

namespace PudelkoLibrary
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        public const int PRECISION = 3;
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private readonly UnitOfMeasure _unit;

        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        { 
            if ((a > 10 || b > 10 || c > 10) && unit == UnitOfMeasure.meter)
                throw new ArgumentOutOfRangeException();

            if ((a > 1000 || b > 1000 || c > 1000) && unit == UnitOfMeasure.centimeter)
                throw new ArgumentOutOfRangeException();

            if ((a > 10000 || b > 10000 || c > 10000) && unit == UnitOfMeasure.milimeter)
                throw new ArgumentOutOfRangeException();

            switch (unit)
            {
                case UnitOfMeasure.meter:
                    _a = Math.Floor(a * 1000) / 1000;
                    _b = Math.Floor(b * 1000) / 1000;
                    _c = Math.Floor(c * 1000) / 1000;
                    break;
                case UnitOfMeasure.centimeter:
                    _a = Math.Round(a / 100, PRECISION);
                    _b = Math.Round(b / 100, PRECISION);
                    _c = Math.Round(c / 100, PRECISION);
                    break;
                case UnitOfMeasure.milimeter:
                    _a = Math.Round(a / 1000, PRECISION);
                    _b = Math.Round(b / 1000, PRECISION);
                    _c = Math.Round(c / 1000, PRECISION);
                    break;
            }

            if (_a <= 0 || _b <= 0 || _c <= 0)
                throw new ArgumentOutOfRangeException();

            _unit = unit;
        }

        public double A => _a;
        public double B => _b;
        public double C => _c;
        public UnitOfMeasure Measure => _unit;

        public override string ToString() => $"{A:F3} m × {B:F3} m × {C:F3} m";

        public string ToString(string? format, IFormatProvider? formatProvider = null)
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

        public double Pole => Math.Round(2 * A * B + 2 * A * C + 2 * B * C, 6);

        #region Equals(), GetHash() and overload operators: '==', '!=', '+'

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Pudelko)obj;

            return A == other.A && B == other.B && C == other.C;
        }

        public bool Equals(Pudelko other)
        {
            if (other is null)
                return false;

            double[] dimensions = { A, B, C };
            double[] otherDimensions = { other.A, other.B, other.C };

            Array.Sort(dimensions);
            Array.Sort(otherDimensions);

            return dimensions.SequenceEqual(otherDimensions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C, Measure);
        }

        public static bool operator ==(Pudelko p1, Pudelko p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Pudelko p1, Pudelko p2)
        {
            return !p1.Equals(p2);
        }

        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            double a = p1.A + p2.A;
            double b = Math.Max(p1.B, p2.B);
            double c = Math.Max(p1.C, p2.C);

            return new Pudelko(a, b, c);
        }

        #endregion

        public static explicit operator double[](Pudelko p1)
        {
            switch (p1.Measure)
            {
                case UnitOfMeasure.meter:
                    return new double[] { p1.A, p1.B, p1.C };
                case UnitOfMeasure.centimeter:
                    return new double[] { p1.A / 100, p1.B / 100, p1.C / 100 };
                case UnitOfMeasure.milimeter:
                    return new double[] { p1.A / 1000, p1.B / 1000, p1.C / 1000 };
                default:
                    throw new ArgumentException();
            }
        }

        public static implicit operator Pudelko((int a, int b, int c) dimesions)
        {
            int a = dimesions.a;
            int b = dimesions.b;
            int c = dimesions.c;

            return new Pudelko(a, b, c, UnitOfMeasure.milimeter);
        }

        public double this[int side]
        {
            get
            {
                switch (side)
                {
                    case 0:
                        return A;
                    case 1:
                        return B;
                    case 2:
                        return C;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Pudelko PudelkoDimensionsParse(string data)
        {
            string[] dimensions = data.Split('×');
            dimensions = dimensions.Select(x => x.Trim()).ToArray();

            double[] valuse = new double[dimensions.Length];
            string[] units = new string[dimensions.Length];

            for (int i = 0; i < dimensions.Length; i++)
            {
                string[] parts = dimensions[i].Split(' ');
                valuse[i] = double.Parse(parts[0]);
                units[i] = parts[1];
            }

            UnitOfMeasure unit = new();

            if (units[0] == "m")
                unit = UnitOfMeasure.meter;
            else if (units[0] == "cm")
                unit = UnitOfMeasure.centimeter;
            else if (units[0] == "mm")
                unit = UnitOfMeasure.milimeter;
            else
                throw new ArgumentException("Niepoprawna miara długości");

            return new Pudelko(valuse[0], valuse[1], valuse[2], unit);
        }
    }                                                     
}