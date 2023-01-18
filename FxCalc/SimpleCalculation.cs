using FxCalc.Enums;

namespace FxCalc
{
    /// <summary>
    /// Basit şekilde sadece isim ve değer alarak formülü hesaplar.
    /// </summary>
    /// <typeparam name="T">Sadece Int,Decimal ve Double tiplerinde tanımlama yapılır.</typeparam>
    public class SimpleCalculation<T> where T : struct
    {
        /// <summary>
        /// Basit şekilde sadece isim ve değer alarak formülü hesaplar.
        /// </summary>
        public SimpleCalculation()
        {
            Formule  = string.Empty;
            eng = new Engine<T>();
            Varibles = new List<BasicVarible>();
        }
        /// <summary>
        /// Basit şekilde sadece isim ve değer alarak formülü hesaplar.
        /// </summary>
        /// <param name="type">Varible da değerlerin tipini seçer</param>
        public SimpleCalculation(InputType type)
        {
            Formule = string.Empty;
            eng = new Engine<T>(type);
            Varibles = new List<BasicVarible>();
        }
        private Engine<T> eng { get; set; }
        public string Formule { get; set; }
        public InputType InputType { get; set; }
        public T Result { get; set; }
        public List<BasicVarible> Varibles { get; private set; }
        public void SetVarible(string name,string value)
        {
            foreach (var v in Varibles)
            {
                if (v.Name == name)
                {
                    v.Value = value;
                    return;
                }
            }
            Varibles.Add(new BasicVarible { Name = name, Value = value });
        }
        public void SetVarible(BasicVarible varible)
        {
            foreach (var v in Varibles)
            {
                if (v.Name == varible.Name)
                {
                    v.Value = varible.Value;
                    return;
                }
            }
            Varibles.Add(varible);
        }
        public void SetVarible(List<BasicVarible> varibles)
        {
            foreach (var v in varibles)
            {
                SetVarible(v);
            }
        }
        public void Compute()
        {
            if (Formule != string.Empty)
            {
                var resfor = Formule;
                foreach (var v in Varibles)
                {
                    resfor = resfor.Replace(v.Name, v.Value);
                }
                Result = eng.Compute(resfor);
            }
            
        }
    }
}