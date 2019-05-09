using System.Runtime.InteropServices.ComTypes;

namespace RESTNødopkald
{
    public class Sensor
    {
        public string Dato { get; set; }
        public string Tid { get; set; }
        public string Motion { get; set; }

        public Sensor() { }

        public Sensor(string dato, string tid, string motion)
        {
            Dato = dato;
            Tid = tid;
            Motion = motion;
        }

        public override string ToString()
        {
            return $"{nameof(Dato)}: {Dato}, {nameof(Tid)}: {Tid}, {nameof(Motion)}: {Motion}";
        }
    }
}