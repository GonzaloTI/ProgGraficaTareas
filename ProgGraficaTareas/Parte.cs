﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgGraficaTareas
{
    public class Parte
    {


        public Shader shader;

        public String name { get; set; }

        public Punto origen { get; set; }
        public Dictionary<string, Cara> vertices { get; set; }

        public Parte(Shader shader, String nombre, Punto origen)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Cara>();// cuando se crea el objeto solo con el shader y el nombre
            this.origen = origen;
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

        public Parte()
        {
            this.name = "";
            this.vertices = new Dictionary<string, Cara>();// cuando se crea el objeto solo con el chaser y el nombre
        }

        public void add(string key, Cara cara)
        {
            vertices.Add(key, cara);
        }

        public String getName() {
            return this.name;
        }
        public void setShader(Shader shader)
        {
            this.shader = shader;
            foreach (KeyValuePair<string, Cara> k in this.vertices)
            {
                k.Value.setShader(shader);
            }
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




        public void dibujar(Punto origen)
        {
            Punto origennew = origen.sum(origen, this.origen);
            foreach (KeyValuePair<string, Cara> k in vertices)
            {
                k.Value.dibujar(origennew);
            }
         
        }

    }
}
