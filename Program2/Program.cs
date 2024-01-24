Console.WriteLine ("ComplexNumber Class Implementation");

public class Complex {
   #region Constructors ---------------------------------------------
   public Complex (double real, double imaginary) => (mReal, mImaginary) = (real, imaginary);
   #endregion

   #region Properties -----------------------------------------------
   public double Norm { get => Math.Sqrt (Math.Pow (mReal, 2) + Math.Pow (mImaginary, 2)); }
   public double Real { get => mReal; }
   public double Imaginary { get => mImaginary; }
   #endregion

   #region Methods --------------------------------------------------
   public static Complex operator + (Complex a, Complex b) => new (a.Real + b.Real, a.Imaginary + b.Imaginary);
   public static Complex operator - (Complex a, Complex b) => new (a.Real - b.Real, a.Imaginary - b.Imaginary);
   public override string ToString () => $"Real:\t{mReal} Imaginary:\t{mImaginary}";
   #endregion

   #region Private Data ---------------------------------------------
   readonly double mReal, mImaginary;
   #endregion
}