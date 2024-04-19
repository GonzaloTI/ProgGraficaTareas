using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas
{
    internal class Parte
    {

        Dictionary<string, Cara> vertices = new Dictionary<string, Cara>();

        Shader shader;

        String name;

        public Parte(Shader shader, String nombre)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Cara>();// cuando se crea el objeto solo con el chaser y el nombre
        }

        public Parte(Shader shader, String nombre, Dictionary<string, Cara> listcara)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Cara>();

            foreach (KeyValuePair<string, Cara> k in listcara)
            {
                this.vertices.Add(k.Key, k.Value);
            }
        }



        public void add(string key, Cara cara)
        {
            vertices.Add(key, cara);
        }
        public String ArrayverticeTostring()
        {

            String listcaras = "";
            foreach (KeyValuePair<string, Cara> k in vertices)
            {
                listcaras += k.Value.getName() + " ";
               
            }
            Console.WriteLine(listcaras);
            return listcaras;
        }




        public void dibujar()
        {
            
            foreach (KeyValuePair<string, Cara> k in vertices)
            {
                k.Value.dibujar();
            }
         
        }

    }
}
