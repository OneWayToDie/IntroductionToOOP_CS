using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
	class Fraction
	{
		int integer; 
		int numerator; 
		int denominator; 

		public int	GetInteger()
		{
			return integer;
		}
		public void setInteger(int integer)
		{
			this.integer = integer;
		}
		public int GetNumerator()
		{
			return numerator;
		}
		public void setNumerator(int numerator)
		{
			this.numerator = numerator;
		}
		public int GetDenominator()
		{
			return denominator;
		}
		public void setDenominator(int denominator)
		{
			this.denominator = denominator;
		}
		public Fraction()
		{
			this.integer = 0;
			this.numerator = 0;
			this.denominator = 1;
			Console.WriteLine($"DefaultConstructor:\t{this.GetHashCode()}");
		}
		public Fraction(double decimalValue)
		{
			decimalValue += 1e-10;
			integer = (int)decimalValue;
			decimalValue -= integer;
			denominator = 1000000000;
			numerator = (int)(decimalValue * denominator);
			Reduce();
			Console.WriteLine($"SingleArgumentConstructor:{this.GetHashCode()}");
		}
		public Fraction(int integer)
		{
			this.integer = integer;
			this.numerator = 0;
			this.denominator = 1;
			Console.WriteLine($"SingleArgumentConstructor:\t{this.GetHashCode()}");
		}
		public Fraction(int numerator, int denominator)
		{
			this.integer = 0;
			this.numerator = numerator;
			this.denominator = denominator;
			Console.WriteLine($"Constructor:\t\t{this.GetHashCode()}");
		}
		public Fraction(int integer, int numerator, int denominator)
		{
			this.integer = integer;
			this.numerator = numerator;
			this.denominator = denominator;
			Console.WriteLine($"Constructor:\t\t{this.GetHashCode()}");
		}
		public Fraction(Fraction other)
		{
			this.integer = other.integer;
			this.numerator = other.numerator;
			this.denominator = other.denominator;
			Console.WriteLine($"CopyConstructor:\t{this.GetHashCode()}");
		}
		public Fraction Assign(Fraction other)
		{
			this.integer = other.integer;
			this.numerator = other.numerator;
			this.denominator = other.denominator;
			Console.WriteLine($"CopyAssigment:\t{this.GetHashCode()}");
			return this;
		}
		~Fraction()
		{
			Console.WriteLine($"Destructor:\t\t{this.GetHashCode()}");
		}
		public static bool operator ==(Fraction left, Fraction right)
		{
			if (ReferenceEquals(left, right)) return true;
			if (left is null || right is null) return false;

			left.ToImproper();
			right.ToImproper();
			return left.numerator * right.denominator == right.numerator * left.denominator;
		}
		public static bool operator !=(Fraction left, Fraction right)
		{
			return !(left == right);
		}
		public static bool operator >(Fraction left, Fraction right)
		{
			left.ToImproper();
			right.ToImproper();
			return left.numerator * right.denominator > right.numerator * left.denominator;
		}
		public static bool operator <(Fraction left, Fraction right)
		{
			left.ToImproper();
			right.ToImproper();
			return left.numerator * right.denominator < right.numerator * left.denominator;
		}
		public static bool operator >=(Fraction left, Fraction right)
		{
			return !(left < right);
		}

		public static bool operator <=(Fraction left, Fraction right)
		{
			return !(left > right);
		}
		public Fraction ToImproper()
		{
			numerator += integer * denominator;
			integer = 0;
			return this;
		}

		public Fraction ToProper()
		{
			integer += numerator / denominator;
			numerator %= denominator;
			return this;
		}

		public Fraction Inverted()
		{
			Fraction inverted = new Fraction(this);
			inverted.ToImproper();
			(inverted.numerator, inverted.denominator) = (inverted.denominator, inverted.numerator);
			return inverted;
		}

		public Fraction Reduce()
		{
			int more, less, rest;
			if (numerator > denominator)
			{
				more = numerator;
				less = denominator;
			}
			else
			{
				less = numerator;
				more = denominator;
			}

			do
			{
				rest = more % less;
				more = less;
				less = rest;
			} while (rest != 0);

			int GCD = more;
			numerator /= GCD;
			denominator /= GCD;
			return this;
		}
		public void Print()
		{
			if (integer != 0) Console.Write(integer);
			if (numerator != 0)
			{
				if (integer != 0) Console.Write("(");
				Console.Write($"{numerator}/{denominator}");
				if (integer != 0) Console.Write(")");
			}
			else if (integer == 0) Console.Write(0);
			Console.WriteLine();
		}
		public static Fraction operator /(Fraction left, Fraction right)
		{
			return left * right.Inverted();
		}
		public static Fraction operator +(Fraction left, Fraction right)
		{
			left.ToImproper();
			right.ToImproper();
			return new Fraction(
				left.numerator * right.denominator + right.numerator * left.denominator,
				left.denominator * right.denominator
			).ToProper();
		}
		public static Fraction operator -(Fraction left, Fraction right)
		{
			left.ToImproper();
			right.ToImproper();
			return new Fraction(
				left.numerator * right.denominator - right.numerator * left.denominator,
				left.denominator * right.denominator
			).ToProper().Reduce();
		}
		public static Fraction operator *(Fraction left, Fraction right)
		{
			left.ToImproper();
			right.ToImproper();
			return new Fraction(
				left.numerator * right.numerator,
				left.denominator * right.denominator
			).ToProper().Reduce();
		}
	}
}
