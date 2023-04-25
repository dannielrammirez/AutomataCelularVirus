using System;

namespace AutomataCelular
{
    public class Automata
    {
        FormAutomata _formAutomata;
        public Persona[,] _objPersonas;
        private Persona[,] _clonObjPersonas;

        public int[,] _arrayPersonas;
        public int _longitud;
        Random _random;

        public Automata()
        {
            _formAutomata = FormAutomata.Instance;
            _objPersonas = _formAutomata.Objpersonas;
            _arrayPersonas = _formAutomata.arrayPersonas;
            _longitud = _formAutomata.longitud;
            _random = new Random();
        }

        public Persona[,] Process()
        {
            Persona[][] response;

            _clonObjPersonas = GetClonObject(_objPersonas);

            for (int x = 0; x < _longitud; x++)
            {
                for (int y = 0; y < _longitud; y++)
                {
                    ReglaEvolucion(x, y);
                }
            }

            return _clonObjPersonas;
        }

        private Persona[,] GetClonObject(Persona[,] arrayOriginal)
        {
            Persona[,] arrayClonado = new Persona[_longitud, _longitud];

            for (int i = 0; i < _longitud; i++)
            {
                for (int j = 0; j < _longitud; j++)
                {
                    arrayClonado[i,j] = (Persona)((ICloneable)arrayOriginal[i,j]).Clone();
                }
            }

            return arrayClonado;
        }

        private int AnalizarVecinasPorEstado(int x, int y, EnumEstado prmEstado)
        {
            int contadorPorEstado = 0;

            //vecina 1
            if (x > 0 && y > 0)
            {
                int ejeX = x - 1, ejeY = y - 1;
                if (_objPersonas[ejeX, ejeY].Estado == prmEstado)
                    contadorPorEstado++;
            }

            //vecina 2
            if (y > 0)
            {
                int ejeX = x, ejeY = y - 1;
                if (_objPersonas[ejeX, ejeY].Estado == prmEstado)
                    contadorPorEstado++;
            }

            //vecina 3
            if (x < _longitud - 1 && y > 0)
            {
                int ejeX = x + 1, ejeY = y - 1;
                if (_objPersonas[ejeX, ejeY].Estado == prmEstado)
                    contadorPorEstado++;
            }

            //vecina 4
            if (x > 0)
            {
                int ejeX = x - 1, ejeY = y;
                if (_objPersonas[ejeX, ejeY].Estado == prmEstado)
                    contadorPorEstado++;
            }

            //vecina 5
            if (x < _longitud - 1)
            {
                int ejeX = x + 1, ejeY = y;
                if (_objPersonas[ejeX, ejeY].Estado == prmEstado)
                    contadorPorEstado++;
            }

            //vecina 6
            if (x > 0 && y < _longitud - 1)
            {
                int ejeX = x - 1, ejeY = y + 1;
                if (_objPersonas[ejeX, ejeY].Estado == prmEstado)
                    contadorPorEstado++;
            }

            //vecina 7
            if (y < _longitud - 1)
            {
                int ejeX = x, ejeY = y + 1;
                if (_objPersonas[ejeX, ejeY].Estado == prmEstado)
                    contadorPorEstado++;
            }


            //vecina 8
            if (x < _longitud - 1 && y < _longitud - 1)
            {
                int ejeX = x + 1, ejeY = y + 1;
                if (_objPersonas[ejeX, ejeY].Estado == prmEstado)
                    contadorPorEstado++;
            }

            return contadorPorEstado;
        }

        private void ReglaEvolucion(int x, int y)
        {
            EnumEstado newStatus;

            if (_objPersonas[x, y].Estado != EnumEstado.INMUNE)
            {
                if (_objPersonas[x, y].Estado == EnumEstado.CONTAGIADO)
                {
                    _clonObjPersonas[x, y].NumDiasContagiado++;
                    newStatus = _objPersonas[x, y].Estado;
                    var randomUCI = _random.Next(0, 100);
                    var vlrProbabilidadUCI = double.Parse(randomUCI.ToString());


                    if (_objPersonas[x, y].NumDiasContagiado >= 8)
                    {
                        if (vlrProbabilidadUCI <= 30)
                        {
                            newStatus = EnumEstado.UCI;
                        }
                    }
                    else
                    {
                        if (_objPersonas[x, y].NumDiasContagiado >= 14)
                        {
                            _clonObjPersonas[x, y].NumDiasContagiado = 0;
                            newStatus = EnumEstado.INMUNE;
                        }
                    }

                }
                else if (_objPersonas[x, y].Estado == EnumEstado.UCI)
                {
                    _clonObjPersonas[x, y].NumDiasContagiado++;
                    var randomMorir = _random.Next(0, 100);
                    var vlrProbabilidadMorir = double.Parse(randomMorir.ToString());

                    if (vlrProbabilidadMorir < _formAutomata._probabilidadMorir)
                    {
                        newStatus = EnumEstado.FALLECIDO;
                    }
                    else
                    {
                        if (_objPersonas[x, y].NumDiasContagiado >= 14)
                        {
                            _clonObjPersonas[x, y].NumDiasContagiado = 0;
                            newStatus = EnumEstado.INMUNE;
                        }
                        else
                        {
                            newStatus = EnumEstado.UCI;
                        }
                    }
                }
                else if (_objPersonas[x, y].Estado == EnumEstado.FALLECIDO)
                {
                    newStatus = EnumEstado.FALLECIDO;
                }
                else
                {
                    int vecinasVacias = AnalizarVecinasPorEstado(x, y, EnumEstado.VACIO);
                    int vecinasSanas = AnalizarVecinasPorEstado(x, y, EnumEstado.SANO);
                    int vecinasContagiadas = AnalizarVecinasPorEstado(x, y, EnumEstado.CONTAGIADO);
                    int vecinasAsintomaticas = AnalizarVecinasPorEstado(x, y, EnumEstado.ASINTOMATICO);
                    int vecinasInmunes = AnalizarVecinasPorEstado(x, y, EnumEstado.INMUNE);
                    int vecinasUCI = AnalizarVecinasPorEstado(x, y, EnumEstado.UCI);
                    int vecinasFallecidas = AnalizarVecinasPorEstado(x, y, EnumEstado.FALLECIDO);

                    if (vecinasContagiadas >= 3)
                        newStatus = EnumEstado.CONTAGIADO;
                    else
                        newStatus = EnumEstado.SANO;

                    // FALTA CONFIGURAR LAS DEMAS REGLAS
                }
            }
            else
                newStatus = EnumEstado.INMUNE;

            _clonObjPersonas[x, y].Estado = newStatus;
        }
    }
}