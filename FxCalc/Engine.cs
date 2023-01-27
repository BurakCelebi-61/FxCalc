using FxCalc.Enums;
using System;
using System.Linq;

namespace FxCalc
{
    public class Engine<T> where T : struct
    {
        public string Formule { get; set; }
        public ReturnType returnType { get; set; }
        public InputType inputType { get; set; }
        /// <summary>
        /// Formülü işleyip sonuç döndüren çekirdek yapı.
        /// </summary>
        public Engine()
        {
            T it = new T();
            if (it.GetType() == typeof(decimal))
            {
                inputType = InputType.Decimal;
                returnType = ReturnType.Decimal;
            }
            else if (it.GetType() == typeof(double))
            {
                inputType = InputType.Double;
                returnType = ReturnType.Double;
            }
            else
            {
                inputType = InputType.Decimal;
                returnType = ReturnType.Int;
            }
        }
        /// <summary>
        /// Formülü işleyip sonuç döndüren çekirdek yapı.
        /// </summary>
        /// <param name="_inputType">Değerleri decimal mi double olarak işleyecek ?</param>
        public Engine(InputType _inputType)
        {
            inputType = _inputType;
            T it = new T();
            if (it.GetType() == typeof(decimal))
            {
                returnType = ReturnType.Decimal;
            }
            else if (it.GetType() == typeof(double))
            {
                returnType = ReturnType.Double;
            }
            else
            {
                returnType = ReturnType.Int;
            }

        }
        public T Compute(string formule)
        {
            ///(5*((-56*4)/-4))
            string s1 = $"{formule}";


        rh1:
            string s3 = s1.Replace("*", "a").Replace("/", "b").Replace("+", "c");
            var b = s3.ToArray();
            if (b[0] == '-')
            {
                b[0] = 'e';
            }
            for (int i = 1; i < b.Length; i++)
            {
                if (b[i - 1] == '(' || b[i - 1] == 'a' || b[i - 1] == 'b' || b[i - 1] == 'c')
                {
                    if (b[i] == '-')
                    {
                        b[i] = 'e';
                    }
                    if (b[i] == 'd')
                    {
                        b[i] = 'e';
                    }

                }
            }
            s1 = new string(b).Replace("-", "d");
            string s2 = "";
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] == '(')
                {
                    s2 = "";
                }
                else if (s1[i] == ')')
                {
                    var ss = Clac(s2);
                    s1 = s1.Replace("(" + s2 + ")", ss);
                    goto rh1;
                }
                else
                {
                    s2 += s1[i];
                }
            }
            var ss2 = Clac(s2);
            switch (returnType)
            {
                case ReturnType.Int:
                    return (T)((object)ss2.ToInt());
                case ReturnType.Decimal:
                    return (T)((object)ss2.ToDecimal());
                case ReturnType.Double:
                    return (T)((object)ss2.ToDouble());
                default:
                    return (T)((object)ss2.ToInt());
            }

        }
        public string Clac(string select)
        {
            var ret = "";
            if (Transaction(select) == 0)
            {

                return select;
            }
            if (Transaction(select) == 1)
            {
                if (select.IndexOf("a") != -1)
                {
                    var sta = select.Split('a');
                    return OperationTypeSelection(sta[0], sta[1], ProcessType.Multiplication, inputType);
                }
                else if (select.IndexOf("b") != -1)
                {
                    var sta = select.Split('b');
                    return OperationTypeSelection(sta[0], sta[1], ProcessType.Division, inputType);
                }
                else if (select.IndexOf("c") != -1)
                {
                    var sta = select.Split('c');

                    return OperationTypeSelection(sta[0], sta[1], ProcessType.Addition, inputType);

                }
                else if (select.IndexOf("d") != -1)
                {
                    var sta = select.Split('d');
                    return OperationTypeSelection(sta[0], sta[1], ProcessType.Subtraction, inputType);

                }
            }
            else
            {
                string s = select;
            rh:
                //String[] delimiters = { "a", "b", "c", "d" };
                //String[] delimiters = { "*", "/", "+", "-" };
                var g = s.ToArray();
                for (int z = 0; z < g.Length; z++)
                {
                    if (z != 0)
                    {
                        if (g[z] == 'e')
                        {
                            if (g[z - 1] != 'a' || g[z - 1] != 'b' || g[z - 1] != 'c')
                            {
                                g[z] = 'e';
                            }
                        }
                    }
                }
                s = new string(g);
                String[] delimiters = { "a", "b", "c", "d" };
                String[] vl = s.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);//değerler
                String[] oo = s.Split(vl, StringSplitOptions.RemoveEmptyEntries);//işlem operatorleri
                if (vl.Length == 1)
                {
                    return s;
                }
                //ilkçarpan veya bölmeni bul
                for (int oi = 0; oi < oo.Length; oi++)
                {
                    if (oo[oi] == "a")
                    {
                        var s2 = OperationTypeSelection(vl[oi], vl[oi + 1], ProcessType.Multiplication, inputType);
                        string r = vl[oi] + "a" + vl[oi + 1];
                        s = s.Replace(r, s2);
                        goto rh;
                    }
                    else if (oo[oi] == "b")
                    {
                        var s2 = OperationTypeSelection(vl[oi], vl[oi + 1], ProcessType.Division, inputType);
                        s = s.Replace(vl[oi] + "b" + vl[oi + 1], s2);
                        goto rh;
                    }
                }
                //topla çıkartma işlemi
                for (int oi = 0; oi < oo.Length; oi++)
                {
                    if (oo[oi] == "c")
                    {
                        var s2 = OperationTypeSelection(vl[oi], vl[oi + 1], ProcessType.Addition, inputType);
                        s = s.Replace(vl[oi] + "c" + vl[oi + 1], s2);
                        goto rh;
                    }
                    else if (oo[oi] == "d")
                    {
                        var s2 = OperationTypeSelection(vl[oi], vl[oi + 1], ProcessType.Subtraction, inputType);
                        s = s.Replace(vl[oi] + "d" + vl[oi + 1], s2);
                        goto rh;
                    }
                }
                ret = s;
            }
            return ret;
        }
        //public List<object> FormuleDirecy(string chars)
        //{
        //    char[] chars2 = chars.ToArray();
        //    string root = "";
        //    List<char> dz = new List<char>();
        //    List<FormulaDirectory> formulas = new List<FormulaDirectory>();
        //    for (int i = 0; i < chars.Length; i++)
        //    {
        //        if (chars[i] == '(')
        //        {

        //            formulas.Add(FormuleDirecy(root));

        //        }
        //        else if (chars[i] == ')')
        //        {

        //            return ret;
        //        }
        //        else
        //        {
        //            root += chars[i];
        //            dz.Add(chars[i]);
        //        }
        //    }
        //    return ret;
        //}
        private int Transaction(string value)
        {
            var count = 0;
            foreach (var c in value)
            {
                switch (c)
                {
                    case 'a':
                        count++;
                        break;
                    case 'b':
                        count++;
                        break;
                    case 'c':
                        count++;
                        break;
                    case 'd':
                        count++;
                        break;
                    default:
                        break;
                }
            }
            return count;
        }
        /// <summary>
        /// Çarpma İşlemi
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private decimal Multiplication(decimal value1, decimal value2)
        {
            return value1 * value2;
        }
        private double Multiplication(double value1, double value2)
        {
            return value1 * value2;
        }
        /// <summary>
        /// Bölme İşlemi
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private decimal Division(decimal value1, decimal value2)
        {
            return value1 / value2;
        }
        private double Division(double value1, double value2)
        {
            return value1 / value2;
        }
        /// <summary>
        /// Toplama İşlemi
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private decimal Addition(decimal value1, decimal value2)
        {
            return value1 + value2;
        }
        private double Addition(double value1, double value2)
        {
            return value1 + value2;
        }
        /// <summary>
        /// Çıkartma İşlemi
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private decimal Subtraction(decimal value1, decimal value2)
        {
            return value1 - value2;
        }
        private double Subtraction(double value1, double value2)
        {
            return value1 - value2;
        }

        private string OperationTypeSelection(string value1, string value2, ProcessType type, InputType returnType)
        {
            var v1 = value1.Replace("e", "-");
            var v2 = value2.Replace("e", "-");
            switch (returnType)
            {
                case InputType.Decimal:
                    switch (type)
                    {
                        case ProcessType.Multiplication:
                            return Multiplication(v1.ToDecimal(), v2.ToDecimal()).ToString();
                        case ProcessType.Division:
                            return Division(v1.ToDecimal(), v2.ToDecimal()).ToString();
                        case ProcessType.Addition:
                            return Addition(v1.ToDecimal(), v2.ToDecimal()).ToString();
                        case ProcessType.Subtraction:
                            return Subtraction(v1.ToDecimal(), v2.ToDecimal()).ToString();
                        default:
                            return 0.ToString();
                    }
                case InputType.Double:
                    switch (type)
                    {
                        case ProcessType.Multiplication:
                            return Multiplication(v1.ToDouble(), v2.ToDouble()).ToString();
                        case ProcessType.Division:
                            return Division(v1.ToDouble(), v2.ToDouble()).ToString();
                        case ProcessType.Addition:
                            return Addition(v1.ToDouble(), v2.ToDouble()).ToString();
                        case ProcessType.Subtraction:
                            return Subtraction(v1.ToDouble(), v2.ToDouble()).ToString();
                        default:
                            return 0.ToString();
                    }
            }

            return "0";
        }

    }
}
