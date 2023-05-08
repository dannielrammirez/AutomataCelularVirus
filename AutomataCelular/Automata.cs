using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        private static int seed = Environment.TickCount;

        private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref seed))
        );

        public static Random GetThreadRandom()
        {
            return randomWrapper.Value;
        }

        public Automata()
        {
            _formAutomata = FormAutomata.Instance;
            _objPersonas = _formAutomata.Objpersonas;
            _arrayPersonas = _formAutomata.arrayPersonas;
            _longitud = _formAutomata.longitud;
            _random = new Random();
        }

        public Persona[,] Movimiento()
        {
            Persona[][] response;

            _clonObjPersonas = GetClonArrayObject(_objPersonas);

            MoverIndividuo();

            return _clonObjPersonas;
        }

        public Persona[,] Process()
        {
            Persona[][] response;

            _clonObjPersonas = GetClonArrayObject(_objPersonas);

            for (int x = 0; x < _longitud; x++)
            {
                for (int y = 0; y < _longitud; y++)
                {
                    if (_clonObjPersonas[x, y].Estado != EnumEstado.VACIO)
                        ReglaEvolucion(x, y);
                }
            }

            return _clonObjPersonas;
        }

        public void MoverIndividuo()
        {
            for (int x = 0; x < _longitud; x++)
            {
                for (int y = 0; y < _longitud; y++)
                {
                    ProcessMovimiento(x, y);
                }
            }
        }

        private void ProcessMovimiento(int x, int y)
        {
            _random = GetThreadRandom();

            if (_objPersonas[x, y].Estado != EnumEstado.FALLECIDO && _objPersonas[x, y].Estado != EnumEstado.UCI)
            {
                if (_random.Next(0, 100) > FormAutomata.Instance._probabilidadMovimiento)
                {
                    var posDisponibles = AnalizarVecinas(x, y).ToArray();
                    posDisponibles = posDisponibles.Where(pd => pd.Estado != EnumEstado.FALLECIDO && pd.Estado != EnumEstado.UCI).ToArray();

                    if (posDisponibles.Length > 0)
                    {
                        int indiceRandom = _random.Next(0, posDisponibles.Length);
                        var aleaPosDis = posDisponibles[indiceRandom];
                        var currentClon = GetClonObject(_clonObjPersonas[x, y]);
                        var aleaClon = GetClonObject(aleaPosDis);

                        if (_objPersonas[aleaClon.EjeX, aleaClon.EjeY].Estado != EnumEstado.VACIO)
                        {
                            if (!_clonObjPersonas[aleaClon.EjeX, aleaClon.EjeY].IsModified)
                            {
                                _clonObjPersonas[x, y].Estado = aleaClon.Estado;
                                _clonObjPersonas[x, y].NumDiasContagiado = aleaClon.NumDiasContagiado;
                                _clonObjPersonas[x, y].NumDiasFallecido = aleaClon.NumDiasFallecido;
                                _clonObjPersonas[x, y].NumDiasSano = aleaClon.NumDiasSano;
                                _clonObjPersonas[x, y].NumDiasUCI = aleaClon.NumDiasUCI;
                                _clonObjPersonas[x, y].IsModified = true;

                                _clonObjPersonas[aleaClon.EjeX, aleaClon.EjeY].Estado = currentClon.Estado;
                                _clonObjPersonas[aleaClon.EjeX, aleaClon.EjeY].NumDiasContagiado = currentClon.NumDiasContagiado;
                                _clonObjPersonas[aleaClon.EjeX, aleaClon.EjeY].NumDiasFallecido = currentClon.NumDiasFallecido;
                                _clonObjPersonas[aleaClon.EjeX, aleaClon.EjeY].NumDiasSano = currentClon.NumDiasSano;
                                _clonObjPersonas[aleaClon.EjeX, aleaClon.EjeY].NumDiasUCI = currentClon.NumDiasUCI;
                                _clonObjPersonas[aleaClon.EjeX, aleaClon.EjeY].IsModified = true;
                            }
                        }
                    }
                }
            }
        }

        private Persona[,] GetClonArrayObject(Persona[,] arrayOriginal)
        {
            Persona[,] arrayClonado = new Persona[_longitud, _longitud];

            for (int i = 0; i < _longitud; i++)
            {
                for (int j = 0; j < _longitud; j++)
                {
                    arrayClonado[i, j] = (Persona)((ICloneable)arrayOriginal[i, j]).Clone();
                }
            }

            return arrayClonado;
        }

        private Persona GetClonObject(Persona objOriginal)
        {
            Persona objClonado;

            objClonado = (Persona)((ICloneable)objOriginal).Clone();

            return objClonado;
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

        private List<Persona> AnalizarVecinas(int x, int y)
        {
            var listResponse = new List<Persona>();

            //vecina 1
            if (x > 0 && y > 0)
            {
                int ejeX = x - 1, ejeY = y - 1;
                listResponse.Add(_objPersonas[ejeX, ejeY]);
            }

            //vecina 2
            if (y > 0)
            {
                int ejeX = x, ejeY = y - 1;
                listResponse.Add(_objPersonas[ejeX, ejeY]);
            }

            //vecina 3
            if (x < _longitud - 1 && y > 0)
            {
                int ejeX = x + 1, ejeY = y - 1;
                listResponse.Add(_objPersonas[ejeX, ejeY]);
            }

            //vecina 4
            if (x > 0)
            {
                int ejeX = x - 1, ejeY = y;
                listResponse.Add(_objPersonas[ejeX, ejeY]);
            }

            //vecina 5
            if (x < _longitud - 1)
            {
                int ejeX = x + 1, ejeY = y;
                listResponse.Add(_objPersonas[ejeX, ejeY]);
            }

            //vecina 6
            if (x > 0 && y < _longitud - 1)
            {
                int ejeX = x - 1, ejeY = y + 1;
                listResponse.Add(_objPersonas[ejeX, ejeY]);
            }

            //vecina 7
            if (y < _longitud - 1)
            {
                int ejeX = x, ejeY = y + 1;
                listResponse.Add(_objPersonas[ejeX, ejeY]);
            }

            //vecina 8
            if (x < _longitud - 1 && y < _longitud - 1)
            {
                int ejeX = x + 1, ejeY = y + 1;
                listResponse.Add(_objPersonas[ejeX, ejeY]);
            }

            return listResponse;
        }

        private void ReglaEvolucion(int x, int y)
        {
            EnumEstado newStatus;
            var randomUCI = _random.Next(0, 100);
            var randomMorir = _random.Next(0, 100);
            var randomInfeccion = _random.Next(0, 100);

            if (_objPersonas[x, y].Estado != EnumEstado.INMUNE)
            {
                if (_objPersonas[x, y].Estado == EnumEstado.CONTAGIADO)
                {
                    _clonObjPersonas[x, y].NumDiasContagiado++;
                    newStatus = _objPersonas[x, y].Estado;
                    var vlrProbabilidadUCI = double.Parse(randomUCI.ToString());


                    if (_objPersonas[x, y].NumDiasContagiado >= 7)
                    {
                        var formProbabilidadHospitalizacion = FormAutomata.Instance._probabilidadHospitalizacion;

                        if (vlrProbabilidadUCI <= formProbabilidadHospitalizacion)
                            newStatus = EnumEstado.UCI;
                    }
                    else
                    {
                        if (_objPersonas[x, y].NumDiasContagiado >= FormAutomata.Instance._diasEvolucionVirus)
                        {
                            _clonObjPersonas[x, y].NumDiasContagiado = 0;
                            newStatus = EnumEstado.INMUNE;
                        }
                    }

                }
                else if (_objPersonas[x, y].Estado == EnumEstado.UCI)
                {
                    _clonObjPersonas[x, y].NumDiasContagiado++;
                    var vlrProbabilidadMorir = double.Parse(randomMorir.ToString());

                    if (vlrProbabilidadMorir < _formAutomata._probabilidadMorir)
                    {
                        newStatus = EnumEstado.FALLECIDO;
                        randomMorir = _random.Next(0, 100);
                    }
                    else
                    {
                        if (_objPersonas[x, y].NumDiasContagiado >= FormAutomata.Instance._diasEvolucionVirus)
                        {
                            _clonObjPersonas[x, y].NumDiasContagiado = 0;
                            newStatus = EnumEstado.INMUNE;
                        }
                        else
                            newStatus = EnumEstado.UCI;
                    }
                }
                else if (_objPersonas[x, y].Estado == EnumEstado.FALLECIDO)
                {
                    newStatus = EnumEstado.FALLECIDO;
                }
                else if (_objPersonas[x, y].Estado == EnumEstado.VACIO)
                {
                    newStatus = EnumEstado.VACIO;
                }
                else
                {
                    int vecinasContagiadas = AnalizarVecinasPorEstado(x, y, EnumEstado.CONTAGIADO);

                    var formProbabilidadInfeccion = FormAutomata.Instance._probabilidadInfeccion;
                    var formVecinasParaInfeccion = FormAutomata.Instance._VecinasNecesariasParaInfeccion;

                    if (vecinasContagiadas >= formVecinasParaInfeccion && randomInfeccion < formProbabilidadInfeccion)
                        newStatus = EnumEstado.CONTAGIADO;
                    else
                        newStatus = EnumEstado.SANO;
                }
            }
            else
                newStatus = EnumEstado.INMUNE;

            _clonObjPersonas[x, y].Estado = newStatus;
        }
    }
}