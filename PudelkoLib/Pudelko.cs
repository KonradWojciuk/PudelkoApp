using System.Collections;

namespace PudelkoLib
{
    public class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
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


            switch (measure)
            {
                case UnitOfMeasure.meter:
                    _a = a;
                    _b = b;
                    _c = c;
                    break;
                case UnitOfMeasure.centimiter:
                    _a = a / 100;
                    _b = b / 100;
                    _c = c / 100;
                    break;
                case UnitOfMeasure.milimeter:
                    _a = a / 1000;
                    _b = b / 1000;
                    _c = c / 1000;
                    break;
            }

            _measure = measure;
        }

        public double A => _a;
        public double B => _b;
        public double C => _c;
        public UnitOfMeasure Measure => _measure;

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

        #endregion

        public static explicit operator double[](Pudelko p1)
        {
            switch (p1.Measure)
            {
                case UnitOfMeasure.meter:
                    return new double[] { p1.A, p1.B, p1.C };
                case UnitOfMeasure.centimiter:
                    return new double[] { p1.A / 100, p1.B / 100, p1.C / 100 };
                case UnitOfMeasure.milimeter:
                    return new double[] { p1.A / 1000, p1.B / 1000, p1.C / 1000 };
                default:
                    throw new ArgumentException();
            }
        }

        public static implicit operator Pudelko((int a, int b, int c) dimesions)
        {
            double a = dimesions.a;
            double b = dimesions.b;
            double c = dimesions.c;

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
            if (units[0] == "cm")
                unit = UnitOfMeasure.centimiter;
            if (units[0] == "mm")
                unit = UnitOfMeasure.milimeter;
            else
                throw new ArgumentException("Niepoprawna miara długości");

            return new Pudelko(valuse[0], valuse[1], valuse[2], unit);
        }
    }                                                     
}