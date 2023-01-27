namespace FxCalc
{
    public class ConditionalCalculation<T> : UnitaryCalculation<T> where T : struct
    {
        public ConditionalCalculation()
        {
            Formules = new List<ConditionalFormula>();
        }
        public List<ConditionalFormula> Formules { get; set; }
        public ConditionalFormula SelectFormule { get; set; }
        public override T Compute(string? newFormule = null)
        {
            ConditionAssessment();
            if (SelectFormule != null)
            {
                return base.Compute(SelectFormule.Formula);
            }
            return new T();
        }
        private void ConditionAssessment()
        {
            foreach (var f in Formules)
            {
                string[] split = f.Conditional.Split(new Char[] { '|', '&', ' ' },
                                 StringSplitOptions.RemoveEmptyEntries);
                string tr = f.Conditional.Replace(" ", "");
                foreach (var s in split)
                {
                    int ee = s.IndexOf("==");
                    int de = s.IndexOf("!=");
                    int be = s.IndexOf(">=");
                    int ke = s.IndexOf("<=");
                    int b = s.IndexOf(">");
                    int k = s.IndexOf("<");
                    if (ee != -1)
                    {
                        var c = s.Replace("==", "?").Split('?');
                        var v1 = c[0].Replace(c[0], Varibles.FirstOrDefault(x => x.Name == c[0]).Value.ToString());
                        var v2 = c[1];
                        if (v1 == v2)
                        {
                            tr = tr.Replace(s, "TRUE");
                        }
                        else
                        {
                            tr = tr.Replace(s, "FALSE");
                        }
                    }
                    else if (de != -1)
                    {
                        var c = s.Replace("!=", "?").Split('?');
                        var v1 = c[0].Replace(c[0], Varibles.FirstOrDefault(x => x.Name == c[0]).Value.ToString());
                        var v2 = c[1];
                        if (v1 != v2)
                        {
                            tr = tr.Replace(s, "TRUE");
                        }
                        else
                        {
                            tr.Replace(s, "FALSE");
                        }
                    }
                    else if (be != -1)
                    {
                        var c = s.Replace(">=", "?").Split('?');
                        var v1 = c[0].Replace(c[0], Varibles.FirstOrDefault(x => x.Name == c[0]).Value.ToString());
                        var v2 = c[1];
                        if (this.InputType == Enums.InputType.Double)
                        {
                            if (v1.ToDouble() >= v2.ToDouble())
                            {
                                tr = tr.Replace(s, "TRUE");
                            }
                            else
                            {
                                tr.Replace(s, "FALSE");
                            }
                        }
                        else
                        {
                            if (v1.ToDecimal() >= v2.ToDecimal())
                            {
                                tr = tr.Replace(s, "TRUE");
                            }
                            else
                            {
                                tr.Replace(s, "FALSE");
                            }
                        }
                    }
                    else if (ke != -1)
                    {
                        var c = s.Replace(">=", "?").Split('?');
                        var v1 = c[0].Replace(c[0], Varibles.FirstOrDefault(x => x.Name == c[0]).Value.ToString());
                        var v2 = c[1];
                        if (this.InputType == Enums.InputType.Double)
                        {
                            if (v1.ToDouble() <= v2.ToDouble())
                            {
                                tr = tr.Replace(s, "TRUE");
                            }
                            else
                            {
                                tr.Replace(s, "FALSE");
                            }
                        }
                        else
                        {
                            if (v1.ToDecimal() <= v2.ToDecimal())
                            {
                                tr = tr.Replace(s, "TRUE");
                            }
                            else
                            {
                                tr.Replace(s, "FALSE");
                            }
                        }
                    }
                    else if (b != -1)
                    {
                        var c = s.Replace(">", "?").Split('?');
                        var v1 = c[0].Replace(c[0], Varibles.FirstOrDefault(x => x.Name == c[0]).Value.ToString());
                        var v2 = c[1];
                        if (this.InputType == Enums.InputType.Double)
                        {
                            if (v1.ToDouble() > v2.ToDouble())
                            {
                                tr = tr.Replace(s, "TRUE");
                            }
                            else
                            {
                                tr.Replace(s, "FALSE");
                            }
                        }
                        else
                        {
                            if (v1.ToDecimal() > v2.ToDecimal())
                            {
                                tr = tr.Replace(s, "TRUE");
                            }
                            else
                            {
                                tr.Replace(s, "FALSE");
                            }
                        }
                    }
                    else if (k != -1)
                    {
                        var c = s.Replace("<", "?").Split('?');
                        var v1 = c[0].Replace(c[0], Varibles.FirstOrDefault(x => x.Name == c[0]).Value.ToString());
                        var v2 = c[1];
                        if (this.InputType == Enums.InputType.Double)
                        {
                            if (v1.ToDouble() < v2.ToDouble())
                            {
                                tr = tr.Replace(s, "TRUE");
                            }
                            else
                            {
                                tr.Replace(s, "FALSE");
                            }
                        }
                        else
                        {
                            if (v1.ToDecimal() < v2.ToDecimal())
                            {
                                tr = tr.Replace(s, "TRUE");
                            }
                            else
                            {
                                tr.Replace(s, "FALSE");
                            }
                        }
                    }
                }
            Resplit:
                string[] split2 = tr.Split(new Char[] { '|', '&', ' ' },
                                 StringSplitOptions.RemoveEmptyEntries);

                if (split2.Count() == 1)
                {
                    if (split2[0] == "TRUE")
                    {
                        SelectFormule = f;
                    }
                }
                else if (split2.Count() > 1)
                {
                    var sorgu = tr.Substring(split2[0].Length, 2);
                    if (sorgu == "&&")//ve
                    {
                        //dogru ve dogru
                        if (split2[0] == "TRUE" && split2[1] == "TRUE")
                        {
                            tr = tr.Replace($"{split2[0]}&&{split2[1]}", "TRUE");
                        }
                        else
                        {
                            tr = tr.Replace($"{split2[0]}&&{split2[1]}", "FALSE");
                        }
                    }
                    else if (sorgu == "||")//veya
                    {
                        if (split2[0] == "TRUE" || split2[1] == "TRUE")
                        {
                            var s = $"{split2[0]}||{split2[1]}";
                            tr = tr.Replace(s, "TRUE");
                        }
                        else
                        {
                            var s = $"{split2[0]}||{split2[1]}";
                            tr.Replace(s, "TRUE");
                        }
                    }
                    goto Resplit;
                }

            }
        }
    }
}
