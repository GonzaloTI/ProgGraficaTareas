using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas
{


    internal class Objeto
    {
        private String nombre;
        List<Cara> Listcaras;

        public Objeto(string nombre)
        {
            this.nombre = nombre;
  
            Listcaras = new List<Cara>();
        }

        public void Add(Cara cara)
        {
            Listcaras.Add(cara);
        }

        public void Dibujar()
        {
            foreach (Cara k in Listcaras)
            {
                k.dibujar();
            }
        }

    }
}
