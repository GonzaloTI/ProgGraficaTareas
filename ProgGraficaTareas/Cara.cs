using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Audio.OpenAL;

using System.Text.Json;

using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace ProgGraficaTareas
{
   
    public class Cara
    {
        public Shader shader;

       public String name { get; set; }

        public Punto origen { get; set; }
        public Dictionary<string, Punto> vertices { get; set; }

        public Matrix4 transform = Matrix4.Identity;

        public Matrix4 origencara = Matrix4.Identity;

        public Matrix4 models = Matrix4.Identity;
        public Cara(Shader shader, String nombre ,Punto origenne) { 
           // this.shader = shader;
            this.name = nombre;
            this.origen = origenne;
        //    transform = transform * Matrix4.CreateTranslation(this.origen.x, this.origen.y, this.origen.z);
            this.vertices = new Dictionary<string, Punto>();// cuando se crea el objeto solo con el chaser y el nombre
        }

        public Cara() {
            this.name = "";
            this.vertices = new Dictionary<string, Punto>();
        }

       /* public Cara(Shader shader, String nombre , Dictionary<string, Punto> listpunto)
        {
            this.shader = shader;
            this.name = nombre;
            this.vertices = new Dictionary<string, Punto>();

            foreach (KeyValuePair<string, Punto> k in listpunto)
            {
                this.vertices.Add(k.Key, k.Value);
            }
        }*/

        public String getName()
        {
           return this.name;
        }

        public void setShader(Shader shader)
        {
            this.shader = shader;
        }

        public void add(string key, Punto punto)
        {
            vertices.Add(key, punto);
        }
        public float[] Arrayvertice()
        {
            float[] result = new float[vertices.Count * 3];
            int pos = 0;
            foreach (KeyValuePair<string, Punto> k in vertices)
            {
                result[pos] = k.Value.x;//  +this.origen.x;
                pos++;
                result[pos] = k.Value.y;// +this.origen.y;
                pos++;
                result[pos] = k.Value.z;// + this.origen.z;
                pos++;
            }
            return result;

        }

        public void dibujar(Punto origen, Matrix4 modell) {
            shader.Use();
            //this.origen = origen;

            //  shader.SetMatrix4("origen", origencara);

            // Aplicar las transformaciones locales a la matriz de modelo

            Matrix4 model =  transform * Matrix4.CreateTranslation(this.origen.x, this.origen.y, this.origen.z) *  modell ;

            //Matrix4 model = transform * modell;
            shader.SetMatrix4("model", model);
            float[] array = Arrayvertice();
            shader.SetVector3("objectColor", new Vector3(0.0f, 0.0f, 0.0f));
            GL.BufferData(BufferTarget.ArrayBuffer, array.Length * sizeof(float), array, BufferUsageHint.StaticDraw);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, vertices.Count);
        }
        public void rotar(float a,float x, float y, float z)
        {
            Console.WriteLine( this.origen.x.ToString() +"x , " +this.origen.y.ToString() + "y , " + this.origen.z.ToString() + "z , " );

            if (x > 0) transform = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(a));

            if (y > 0) transform = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(a));

            if (z > 0) transform = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(a));

          //  transform =  transform * Matrix4.CreateTranslation(this.origen.x, this.origen.y, this.origen.z);
           
            //var move = Matrix4.Identity;
            //   move = move * Matrix4.CreateTranslation(this.origen.x, this.origen.y, this.origen.z);
            //  transform = transform * move;
           // funciona //
        }

        public void trasladar(float x, float y, float z)
        {
        
            // transform =  Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(50f));
            //transform = transform * Matrix4.CreateTranslation(x,y,z);
            transform =  Matrix4.CreateTranslation(x, y, z);

        }
        public void escalar(float x, float y, float z)
        {
            // Crear una matriz de escala
            transform =  Matrix4.CreateScale(x,y,z);

        }
    }
}
