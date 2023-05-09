using System;

namespace AutomataCelular
{
    public class Persona : ICloneable
    {
        public Persona(int ejex, int ejeY)
        {
            EjeX = ejex;
            EjeY = ejeY;
            Estado = EnumEstado.VACIO;
            NumDiasSano = 0;
            NumDiasContagiado = 0;
            NumDiasUCI = 0;
            NumDiasFallecido = 0;
            IsModified = false;
            IsPaint = false;
        }
        public int EjeX { get; set; }
        public int EjeY { get; set; }
        public EnumEstado Estado { get; set; }
        public int NumDiasSano { get; set; }
        public int NumDiasContagiado { get; set; }
        public int NumDiasUCI { get; set; }
        public int NumDiasFallecido { get; set; }
        public bool IsModified { get; set; }
        public bool IsPaint { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}