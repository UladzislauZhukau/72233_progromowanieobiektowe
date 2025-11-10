using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Programm
{
    
    
    class ComplexNumber : ICloneable , IEquatable<ComplexNumber> , Imodular
    {
        private double re;
        private double im;

        public double Re
        {
            get { return re; }
            set { re = value; }
        }
        public double Im
        {
            get { return im; }
            set { im = value; }
        }
        public ComplexNumber(double re, double im)
        {
            this.re = re;  
            this.im = im;
        }
        public override string ToString()
        {
            string sign = im >= 0 ? "+" : "-";
            return $"{re} {sign} {Math.Abs(im)}i";
        }
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
             return new ComplexNumber(a.re + b.re , a.im + b.im);
        }
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.re - b.re, a.im - b.im);
        }


        public object Clone() { return new ComplexNumber(this.re, this.im); }        
    }
}