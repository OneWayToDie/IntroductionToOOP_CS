//#define CONSTRUCTORS_CHECK
#define ARITHMETICAL_OPERATORS_CHECK

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
	internal class Program
	{
		static void Main(string[] args)
		{
#if CONSTRUCTORS_CHECK
			Fraction A = new Fraction(); 
			A.Print();

			Fraction B = new Fraction(5); 
			B.Print();

			Fraction C = new Fraction(1, 2);
			C.Print();

			Fraction D = new Fraction(2, 3, 4);
			D.Print();
#endif
#if ARITHMETICAL_OPERATORS_CHECK
			Fraction A = new Fraction(2, 3, 4); 
			A.Print();

			Fraction B = new Fraction(3, 4, 5); 
			B.Print();

			Fraction C = new Fraction(A * B);
			C.Print();

			C = A / B;
			C.Print();

			A *= B;
			A.Print();

			A /= B;
			A.Print();

			C = A + B;
			C.Print();

			C = B - A;
			C.Print();

			A += B;
			A.Print();

			A -= B;
			A.Print();
#endif

		}
	}
}
