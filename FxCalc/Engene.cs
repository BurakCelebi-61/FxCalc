using FxCalc.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCalc
{
    public class Engine
    {
        public string Formule { get; set; }
        public ReturnType returnType { get; set; }
        public InputType inputType { get; set; }

        /// <summary>
        /// İşlem Ağaçlandırma. Formulü parçalarına ayırıp işlem yapmaya gönderir.
        /// </summary>
        /// <param name="formule">işleme hazır değerleri yüklenmiş formül</param>
        public ProcessFile ProcessFiling(string formule,ProcessInputType İnputType,int startIndex)
        {
            string processedString = formule.Replace("*", "a").Replace("/", "b").Replace("+", "c").Replace("-", "d");
            var value = new ProcessFile() { StartIndex = startIndex};
            Recheck:
            var a = processedString.ToArray();
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '(')
                {
                    var rt1 = ProcessFiling(processedString.Substring(i+1, processedString.Length-1-i),ProcessInputType.SubFormul,i);
                    processedString = processedString.Substring(0, rt1.StartIndex) + rt1.Value + processedString.Substring(rt1.StartIndex+rt1.EndIndex + 2);
                    
                    goto Recheck;
                }
                else if (a[i] == ')')
                {
                    if (Transaction(value.Value) == 0)
                    {
                        //var sta = value.Value.Split('*');
                        value.EndIndex = i;
                        return value;
                    }
                    if (Transaction(value.Value) == 1)
                    {
                        if (value.Value.IndexOf("a") != -1)
                        {
                            var sta = value.Value.Split('a');
                            value.Value = OperationTypeSelection(sta[0], sta[1], ProcessType.Multiplication,inputType);
                            value.EndIndex = i;
                            return value;
                        }
                        else if (value.Value.IndexOf("b") != -1)
                        {
                            var sta = value.Value.Split('b');
                            value.Value = OperationTypeSelection(sta[0], sta[1], ProcessType.Division, inputType);
                            value.EndIndex = i;
                            return value;
                        }
                        else if (value.Value.IndexOf("c") != -1)
                        {
                            var sta = value.Value.Split('c');
                            value.Value = OperationTypeSelection(sta[0], sta[1], ProcessType.Addition, inputType);
                            value.EndIndex = i;
                            return value;
                        }
                        else if (value.Value.IndexOf("d") != -1)
                        {
                            var sta = value.Value.Split('d');
                            value.Value = OperationTypeSelection(sta[0], sta[1], ProcessType.Subtraction, inputType);
                            value.EndIndex = i;
                            return value;
                        }
                    }
                    else
                    {
                        string s = value.Value;
                    rh:
                        //String[] delimiters = { "a", "b", "c", "d" };
                        //String[] delimiters = { "*", "/", "+", "-" };
                        String[] delimiters = { "a", "b","c","d" };
                        String[] vl = s.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);//değerler
                        String[] oo = s.Split(vl, StringSplitOptions.RemoveEmptyEntries);//işlem operatorleri
                        if (vl.Length == 1)
                        {
                            value.Value = s;
                            value.EndIndex = i;
                            return value;
                        }
                        //ilkçarpan veya bölmeni bul
                        for (int oi = 0; oi < oo.Length; oi++)
                        {
                            if (oo[oi]=="a")
                            {
                                var s2 = OperationTypeSelection(vl[oi], vl[oi+1], ProcessType.Multiplication, inputType);
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
                        

                    }
                }
                else
                {
                    value.Value +=a[i];
                }
            }
            return value;
        }
        
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
        /// Çarpma Bölme İşlemlerini Say
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int MultiplicationDivideCount(string value)
        {
            var count = 0;
            foreach (var c in value)
            {
                if (c=='b' || c == 'a')
                {
                    count++;
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
            switch (returnType)
            {
                case InputType.Decimal:
                    switch (type)
                    {
                        case ProcessType.Multiplication:
                            return Multiplication(value1.ToDecimal(), value2.ToDecimal()).ToString();
                        case ProcessType.Division:
                            return Division(value1.ToDecimal(), value2.ToDecimal()).ToString();
                        case ProcessType.Addition:
                            return Addition(value1.ToDecimal(), value2.ToDecimal()).ToString();
                        case ProcessType.Subtraction:
                            return Subtraction(value1.ToDecimal(), value2.ToDecimal()).ToString();
                        default:
                            return 0.ToString();
                    }
                case InputType.Double:
                    switch (type)
                    {
                        case ProcessType.Multiplication:
                            return Multiplication(value1.ToDouble(), value2.ToDouble()).ToString();
                        case ProcessType.Division:
                            return Division(value1.ToDouble(), value2.ToDouble()).ToString();
                        case ProcessType.Addition:
                            return Addition(value1.ToDouble(), value2.ToDouble()).ToString();
                        case ProcessType.Subtraction:
                            return Subtraction(value1.ToDouble(), value2.ToDouble()).ToString();
                        default:
                            return 0.ToString();
                    }
            }

            return "0";
        }

    }
}
