using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.IO;
using OpenTK.Windowing.GraphicsLibraryFramework; 
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using ProgGraficaTareas.animacion;

namespace ProgGraficaTareas
{
    public class Game : GameWindow
    {
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) {
            this.VSync = VSyncMode.On;
        }
     
        Shader shader;
        Thread animarthread;

        bool mythreadbool = false;
        Animacion directoranimacion;

        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;

        private Matrix4 projection;
        private Matrix4 view;
        private Matrix4 model;

        Televisor televisor;

        Escena escena1;

        Escena escenaDeserializada;
        Escena escenaDeserializada2;
        Escena escenaprueva;

       // Parte parterotar;

        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;

        private double _time;

        bool animated = false;
        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

            shader = new Shader("../../../Shaders/shader.vert", "../../../Shaders/shader.frag");

         
           projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(20.0f), Size.X / (float)Size.Y, 0.1f, 20.0f);
         
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
          // projection = Matrix4.CreateOrthographic(2.0f, 2.0f, 0.1f, 100.0f); // Tamaño de la proyección ortogonal en el plano XY
            model = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(0.0f));


            shader.SetMatrix4("model", model);
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);

            //     shader.Use();
            //     var move = Matrix4.Identity;
            //     move = move * Matrix4.CreateTranslation(0f, 0f, 0f);
            //      shader.SetMatrix4("origen", move);

            // TELEVISOR

            Punto centromasatv = new Punto(0.0f,0.0f,0.0f);

            Cara caratv = new Cara(shader, "Base tv", centromasatv);
            caratv.add("1", new Punto(-0.55f, -0.20f, -0.10f));
            caratv.add("2", new Punto(-0.45f, -0.20f, -0.10f));
            caratv.add("3", new Punto(-0.45f, 0.01f, -0.10f));
            caratv.add("4", new Punto(-0.55f, 0.01f, -0.10f));


            Cara caratv2 = new Cara(shader, "Base tv 2", centromasatv);
            caratv2.add("1", new Punto(-0.55f, -0.20f, -0.0f));
            caratv2.add("2", new Punto(-0.45f, -0.20f, -0.0f));
            caratv2.add("3", new Punto(-0.45f, 0.01f, -0.0f));
            caratv2.add("4", new Punto(-0.55f, 0.01f, -0.0f));

            Cara caratvsupp = new Cara(shader, "superior", centromasatv);
            caratvsupp.add("1", new Punto(-0.55f, 0.01f, -0.10f));
            caratvsupp.add("2", new Punto(-0.45f, 0.01f, -0.10f));
            caratvsupp.add("3", new Punto(-0.45f, 0.01f, -0.0f));
            caratvsupp.add("4", new Punto(-0.55f, 0.01f, -0.0f));

            Cara caratvinf = new Cara(shader, "inferior", centromasatv);
            caratvinf.add("1", new Punto(-0.55f, -0.20f, -0.10f));
            caratvinf.add("2", new Punto(-0.45f, -0.20f, -0.10f));
            caratvinf.add("4", new Punto(-0.45f, -0.20f, -0.0f));
            caratvinf.add("3", new Punto(-0.55f, -0.20f, -0.0f));


            //#################

            televisor = new Televisor(shader, new Vector3(0.0f, 0.0f, 0.0f));

            Punto punto1 = new Punto(0.15f, 0.10f, -0.10f);

            Punto origen = new Punto(0.0f, 0.0f, 0.0f);

            Cara baseFlorero = new Cara(shader, "Base Florero",origen);
            baseFlorero.add("1", new Punto(0.15f, 0.10f, -0.10f));
            baseFlorero.add("2", new Punto(0.17f, 0.20f, -0.10f));
            baseFlorero.add("3", new Punto(0.17f, 0.25f, -0.10f));
            baseFlorero.add("4", new Punto(0.10f, 0.25f, -0.10f));
            baseFlorero.add("5", new Punto(0.10f, 0.20f, -0.10f));
            baseFlorero.add("6", new Punto(0.13f, 0.10f, -0.10f));

           
            Cara baseFlorero2 = new Cara(shader, "Base Florero 2", origen);
            baseFlorero2.add("1", new Punto(0.15f, 0.10f, -0.13f));
            baseFlorero2.add("2", new Punto(0.17f, 0.20f, -0.13f));
            baseFlorero2.add("3", new Punto(0.17f, 0.25f, -0.13f));
            baseFlorero2.add("4", new Punto(0.10f, 0.25f, -0.13f));
            baseFlorero2.add("5", new Punto(0.10f, 0.20f, -0.13f));
            baseFlorero2.add("6", new Punto(0.13f, 0.10f, -0.13f));

            Cara basesup = new Cara(shader, "Base sup Florero", origen);
            basesup.add("1", new Punto(0.17f, 0.25f, -0.13f));
            basesup.add("2", new Punto(0.10f, 0.25f, -0.13f));
            basesup.add("3", new Punto(0.17f, 0.25f, -0.10f));
            basesup.add("4", new Punto(0.10f, 0.25f, -0.10f));

            Cara baseinf = new Cara(shader, "Base inf Florero", origen);
            baseinf.add("1", new Punto(0.15f, 0.10f, -0.10f));
            baseinf.add("2", new Punto(0.13f, 0.10f, -0.10f));
            baseinf.add("3", new Punto(0.15f, 0.10f, -0.13f));
            baseinf.add("4", new Punto(0.13f, 0.10f, -0.13f));

      

            Cara caraeq = new Cara(shader, "Base equipo", origen); 
            caraeq.add("1", new Punto(-0.55f, -0.20f, -0.10f));
            caraeq.add("2", new Punto(-0.45f, -0.20f, -0.10f));
            caraeq.add("3", new Punto(-0.45f, 0.01f, -0.10f));
            caraeq.add("4", new Punto(-0.55f, 0.01f, -0.10f));


            Cara caraeq2 = new Cara(shader, "Base equipo 2", origen);
            caraeq2.add("1", new Punto(-0.55f, -0.20f, -0.0f));
            caraeq2.add("2", new Punto(-0.45f, -0.20f, -0.0f));
            caraeq2.add("3", new Punto(-0.45f, 0.01f, -0.0f));
            caraeq2.add("4", new Punto(-0.55f, 0.01f, -0.0f));

            Cara carasupp = new Cara(shader, "superior", origen);
            carasupp.add("1", new Punto(-0.55f,  0.01f, -0.10f));
            carasupp.add("2", new Punto(-0.45f, 0.01f, -0.10f));
            carasupp.add("3", new Punto(-0.45f, 0.01f, -0.0f));
            carasupp.add("4", new Punto(-0.55f, 0.01f, -0.0f));

            Cara carainf = new Cara(shader, "inferior", origen);
            carainf.add("1", new Punto(-0.55f, -0.20f, -0.10f));
            carainf.add("2", new Punto(-0.45f, -0.20f, -0.10f));
            carainf.add("4", new Punto(-0.45f, -0.20f, -0.0f));
             carainf.add("3", new Punto(-0.55f, -0.20f, -0.0f));


            Cara caraeqdelante2 = new Cara(shader, "Base equipo", origen);
            caraeqdelante2.add("1", new Punto(0.55f, -0.20f, -0.10f));
            caraeqdelante2.add("2", new Punto(0.45f, -0.20f, -0.10f));
            caraeqdelante2.add("3", new Punto(0.45f, 0.01f, -0.10f));
            caraeqdelante2.add("4", new Punto(0.55f, 0.01f, -0.10f));


            Cara caraeqatras2 = new Cara(shader, "Base equipo 2", origen)  ;
            caraeqatras2.add("1", new Punto(0.55f, -0.20f, -0.0f));
            caraeqatras2.add("2", new Punto(0.45f, -0.20f, -0.0f));
            caraeqatras2.add("3", new Punto(0.45f, 0.01f, -0.0f));
            caraeqatras2.add("4", new Punto(0.55f, 0.01f, -0.0f));

            Cara carasupp2 = new Cara(shader, "superior", origen);
            carasupp2.add("1", new Punto(0.55f, 0.01f, -0.10f));
            carasupp2.add("2", new Punto(0.45f, 0.01f, -0.10f));
            carasupp2.add("3", new Punto(0.45f, 0.01f, -0.0f));
            carasupp2.add("4", new Punto(0.55f, 0.01f, -0.0f));

            Cara carainf2 = new Cara(shader, "inferior", origen);
            carainf2.add("1", new Punto(0.55f, -0.20f, -0.10f));
            carainf2.add("2", new Punto(0.45f, -0.20f, -0.10f));
            carainf2.add("4", new Punto(0.45f, -0.20f, -0.0f));
            carainf2.add("3", new Punto(0.55f, -0.20f, -0.0f));

            Parte parte1 = new Parte(shader,"parte1", origen);  //parlante 1
            parte1.add("1",carainf2);
            parte1.add("2", carasupp2);
            parte1.add("3", caraeqatras2);
            parte1.add("4", caraeqdelante2);

            //###########################################################################
            Punto origenescena = new Punto(0.0f, 0.0f, 0.0f);

            escenaprueva = new Escena(shader, "escenaprueva", origenescena); 
            
            Punto origenobjeto = new Punto(0.5f,0.0f,0.0f);

            Punto origenobjeto2 = new Punto(-0.5f, 0.0f, 0.0f);

            Punto origenobjeto3 = new Punto(0.0f, 0.0f, 0.0f);


            Objeto objetopractica = new Objeto(shader,"objetoparlante1", origenobjeto);

            Objeto objetopractica2 = new Objeto(shader, "objetoparlante2", origenobjeto2);

            Objeto objetopractica3 = new Objeto(shader, "objetoflorero", origenobjeto3);


            Punto origenparterota = new Punto(0.0f, 0.0f, 0.0f);

            Punto origenparterota2 = new Punto(0.0f, 0.0f, 0.0f);

            Punto origenparterotar3 = new Punto(0.15f, 0.10f, -0.10f);

            Punto origenparterotar4 = new Punto(0.0f, 0.1f, -0.05f);


            Parte parterotar = new Parte(shader, "parte123", origenparterota);  //parlante 1

            Parte parterotar2 = new Parte(shader, "parte145", origenparterota2);  //parlante 2

            Parte parterotar3 = new Parte(shader, "parte155", origenparterotar3);  //florero 1

            Parte parterotar4 = new Parte(shader, "parte177", origenparterotar4);  //florero 2 encima del parlante


            Punto origencara = new Punto(0.0f, 0.0f, 0.0f);

            Cara caraeqdelante2rota = new Cara(shader, "Base equipo", origencara);
            caraeqdelante2rota.add("1", new Punto(-0.05f, -0.1f, 0.0f));
            caraeqdelante2rota.add("2", new Punto(0.05f, -0.1f, 0.0f));
            caraeqdelante2rota.add("3", new Punto(0.05f, 0.1f, 0.0f));
            caraeqdelante2rota.add("4", new Punto(-0.05f, 0.1f, 0.0f));


            Cara caraeqatras2rota = new Cara(shader, "Base equipo 2", origencara);
            caraeqatras2rota.add("1", new Punto(-0.05f, -0.1f, -0.05f));
            caraeqatras2rota.add("2", new Punto(0.05f, -0.1f, -0.05f));
            caraeqatras2rota.add("3", new Punto(0.05f, 0.1f, -0.05f));
            caraeqatras2rota.add("4", new Punto(-0.05f, 0.1f, -0.05f));

            Cara carasupp2rota = new Cara(shader, "superior", origencara);
            carasupp2rota.add("1", new Punto(0.05f, 0.1f, 0.0f));
            carasupp2rota.add("2", new Punto(-0.05f, 0.1f, 0.0f));
            carasupp2rota.add("4", new Punto(-0.05f, 0.1f, -0.05f));
            carasupp2rota.add("3", new Punto(0.05f, 0.1f, -0.05f));


            Cara carainf2rota = new Cara(shader, "inferior", origencara);
            carainf2rota.add("1", new Punto(-0.05f, -0.1f, 0.0f));
            carainf2rota.add("2", new Punto(0.05f, -0.1f, 0.0f));
            carainf2rota.add("4", new Punto(0.05f, -0.1f, -0.05f));
            carainf2rota.add("3", new Punto(-0.05f, -0.1f, -0.05f));

            parterotar.add("11", carainf2rota);
            parterotar.add("22", carasupp2rota);
            parterotar.add("33", caraeqatras2rota);
            parterotar.add("44", caraeqdelante2rota);

            parterotar2.add("1", carainf2rota);
            parterotar2.add("2", carasupp2rota);
            parterotar2.add("3", caraeqatras2rota);
            parterotar2.add("4", caraeqdelante2rota);


            Punto centrodemasa = new Punto(0.0f, 0.0f, 0.0f);

            Cara baseFlorero3 = new Cara(shader, "Base Florero", centrodemasa);
            baseFlorero3.add("1", new Punto(0.01f, 0.0f, 0.01f));
            baseFlorero3.add("2", new Punto(0.03f, 0.10f, 0.01f));
            baseFlorero3.add("3", new Punto(0.03f, 0.15f, 0.01f));
            baseFlorero3.add("4", new Punto(-0.04f, 0.15f, 0.01f));
            baseFlorero3.add("5", new Punto(-0.04f, 0.10f, 0.01f));
            baseFlorero3.add("6", new Punto(-0.01f, 0.0f,  0.01f));


            Cara baseFlorero33 = new Cara(shader, "Base Florero 2", centrodemasa);
            baseFlorero33.add("1", new Punto(0.01f, 0.0f, -0.03f));
            baseFlorero33.add("2", new Punto(0.03f, 0.10f, -0.03f));
            baseFlorero33.add("3", new Punto(0.03f, 0.15f, -0.03f));
            baseFlorero33.add("4", new Punto(-0.04f, 0.15f, -0.03f));
            baseFlorero33.add("5", new Punto(-0.04f, 0.10f, -0.03f));
            baseFlorero33.add("6", new Punto(-0.01f, 0.0f, -0.03f));

            Cara basesup3 = new Cara(shader, "Base sup Florero", centrodemasa);
            basesup3.add("1", new Punto(0.03f, 0.15f, 0.01f));
            basesup3.add("2", new Punto(-0.04f, 0.15f, 0.01f));
            basesup3.add("3", new Punto(-0.04f, 0.15f, -0.03f));
            basesup3.add("4", new Punto(0.03f, 0.15f, -0.03f));
           

            Cara baseinf3 = new Cara(shader, "Base inf Florero", centrodemasa);
            baseinf3.add("1", new Punto(0.01f, 0.0f, 0.01f));
            baseinf3.add("2", new Punto(-0.01f, 0.0f, 0.01f));
            baseinf3.add("3", new Punto(-0.01f, 0.0f, -0.03f));
            baseinf3.add("4", new Punto(0.01f, 0.0f, -0.03f));
           

            parterotar3.add("1", baseFlorero3);
            parterotar3.add("2", baseFlorero33);
            parterotar3.add("3", basesup3);
            parterotar3.add("4", baseinf3);

            parterotar4.add("1", baseFlorero3);
            parterotar4.add("2", baseFlorero33);
            parterotar4.add("3", basesup3);
            parterotar4.add("4", baseinf3);



            objetopractica.add("1", parterotar);  //parlante

            objetopractica2.add("1", parterotar2); //parlante
            objetopractica2.add("2", parterotar4); //florero


            objetopractica3.add("1", parterotar3); //florero

            escenaprueva.add("1", objetopractica);
            escenaprueva.add("2", objetopractica2);
            escenaprueva.add("3", objetopractica3);


            Utilidades utils2 = new Utilidades();
            //serializarobjeto
            string rutaArchivo1 = "../../../escena55.json";
            utils2.saveJson(rutaArchivo1, escenaprueva);


            //Deserializar objeto
            string rutaArchivoDes1 = "../../../escena55.json"; // Ruta del archivo JSON

            this.escenaDeserializada2 = utils2.getObjectFromJson<Escena>(rutaArchivoDes1);


       //     escenaprueva.setShader(shader);

            escenaDeserializada2.setShader(shader);
            
            //###########################################################################

            Parte parte2 = new Parte(shader, "parte2", origen);  //parlante 2
            parte2.add("1", carasupp);
            parte2.add("2", carainf);
            parte2.add("3", caraeq2);
            parte2.add("4", caraeq);

          
            Parte parte3 = new Parte(shader, "parte3", origen);  //florero
            parte3.add("1", basesup);
            parte3.add("2", baseFlorero2);
            parte3.add("3", baseFlorero);
            parte3.add("4", baseinf);


            Objeto objetonew = new Objeto(shader, "obj1", origen);

            objetonew.add("part1", parte1);
            objetonew.add("part2", parte2);
            objetonew.add("part3", parte3);

            Punto origen2 = new Punto(0.0f, 0.0f, -0.1f);


            this.escena1 = new Escena(shader, "escena1", origen2);

            escena1.add("1", objetonew);

            Utilidades utils = new Utilidades();
            //serializarobjeto
            string rutaArchivo = "../../../escena111.json";
            utils.saveJson(rutaArchivo, escena1);


            //Deserializar objeto
            string rutaArchivoDes = "../../../escena1.json"; // Ruta del archivo JSON

            this.escenaDeserializada = utils.getObjectFromJson<Escena>(rutaArchivoDes);
            Console.WriteLine(escenaDeserializada);

           this.escenaDeserializada.setShader(shader);

            _camera = new Camera(Vector3.UnitZ, Size.X / (float)Size.Y);

            Accion acto1 = new Accion("act1", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));
            Accion acto2 = new Accion("act2", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));
            Accion acto3 = new Accion("act3", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));
            Accion acto4 = new Accion("act4", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));
            Accion acto5 = new Accion("act5", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));
            Accion acto6 = new Accion("act", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));
            Accion acto7 = new Accion("act7", "rotar", 20, new Punto(0.0f, 0.00f, 1.0f));
            Accion acto8 = new Accion("act8", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));
            Accion acto9 = new Accion("act9", "escalar", 5, new Punto(1.0f, 1.5f, 1.0f));
            Accion acto10 = new Accion("act10", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));
            Accion acto11 = new Accion("act11", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));
            Accion acto12 = new Accion("act12", "trasladar", 5, new Punto(0.01f, 0.01f, 0.01f));

            this.directoranimacion = new Animacion("ani1", 200f, 20f);
            this.directoranimacion.add("1", acto1);
            this.directoranimacion.add("2", acto2);
            this.directoranimacion.add("3", acto3);
            this.directoranimacion.add("4", acto4);
            this.directoranimacion.add("5", acto5);
            this.directoranimacion.add("6", acto6);
            this.directoranimacion.add("7", acto7);
            this.directoranimacion.add("8", acto8);
            this.directoranimacion.add("9", acto9);
            this.directoranimacion.add("10", acto10);
            this.directoranimacion.add("11", acto11);
            this.directoranimacion.add("12", acto12);
            this.directoranimacion.add("13", acto1);
            this.directoranimacion.add("14", acto2);
            this.directoranimacion.add("15", acto3);
            this.directoranimacion.add("16", acto4);
            this.directoranimacion.add("17", acto5);
            this.directoranimacion.add("18", acto6);
            this.directoranimacion.add("19", acto7);
            this.directoranimacion.add("20", acto8);
            this.directoranimacion.add("21", acto9);
            this.directoranimacion.add("22", acto10);
            this.directoranimacion.add("23", acto11);
            this.directoranimacion.add("24", acto12);
        }
        public void animacion(Escena escena)
        {
            int i = 0;
            Animacion ani = new Animacion("ani0", 200, 2f);
            try
            {
                while (mythreadbool == true && false)
                {
                    ani.Animar(escena);
                    Console.WriteLine("hilo corriendo");


                    Thread.Sleep(100);
                }

                foreach (KeyValuePair<string, Accion> k in this.directoranimacion.acciones)
                {
                    ani.AnimarByInstru2(escena.get("1").get("1"), k.Value);
                    Thread.Sleep((int)this.directoranimacion.time);
                    Console.WriteLine("hilo corriendo desde acctions");
                }

                //con un while para poder detenerlo
                var enumerator = this.directoranimacion.acciones.GetEnumerator();

              /*  while (true)
                {
                    if (enumerator.MoveNext() )
                    {
                        var keyValue = enumerator.Current;
                        var clave = keyValue.Key;
                        var accion = keyValue.Value;

                        // Tu lógica aquí
                        ani.AnimarByInstru(escena, accion);
                        Thread.Sleep((int)this.directoranimacion.time);
                        Console.WriteLine("hilo corriendo desde acciones while normal ");
                    }
                    else { break; }
                }*/


            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Hilo terminado.");
            }
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
          base.OnRenderFrame(args);
            _time += 0.0 * args.Time;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.BindVertexArray(vertexArrayObject);
            shader.Use();


           var model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));
           shader.SetMatrix4("model", model);
           shader.SetMatrix4("view", _camera.GetViewMatrix());
           shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
          


            televisor.dibujar();
            // this.objeto1.Dibujar();
              // this.objeto2.Dibujar();
            // this.objeto3.Dibujar();

           //    this.escena1.dibujar(); 
        //   this.escenaDeserializada.dibujar();

            // this.parterotar.dibujar(new Punto(0.0f, 0.0f, 0.0f));

           // escenaprueva.dibujar();
            escenaDeserializada2.dibujar();


            Context.SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
           

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0); 
            base.OnUnload();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
           
            if (!IsFocused) // Check to see if the window is focused
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 1.0f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time;

              // escenaprueva.escalar(1.0f, 1.5f, 1.0f);
                //  Parte newp = escenaDeserializada.vertices.First().Value.vertices.First().Value;


              //  escenaDeserializada2.trasladar(0.0f, 1.5f, 0.0f);

              //  escenaDeserializada2.rotar(60f,0.0f, 0.0f, 1.0f);

                Parte newp2 = escenaDeserializada2.vertices.First().Value.vertices.First().Value;
                //  parterotar.trasladar(-1.5f, 0.0f, 0.0f);
             //  newp2.rotar(60f,0.0f, 0.0f, 1.0f);

                //    newp2.escalar(1.0f, 2.0f, 1.0f);
                //    newp2.trasladar(-1.0f, 0.0f, 0.0f);

               Objeto newOb24 = escenaDeserializada2.vertices.First().Value;
            //    newOb24.rotar(60f,0.0f,0.0f,1.0f);

            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.T) && !mythreadbool)
            {
                if (!mythreadbool)
                {
                    this.animarthread = new Thread(() => animacion(escenaDeserializada2));
                    this.animarthread.Start();
                    mythreadbool = true;
                }

            }
            if (input.IsKeyDown(Keys.N) && input.IsKeyDown(Keys.Y))
            {
                escenaDeserializada2.get("2").get("1").rotar(5, 1.0f, 0.0f, 0.0f);
            }
            if (input.IsKeyDown(Keys.N) && input.IsKeyDown(Keys.H))
            {
                escenaDeserializada2.get("2").get("1").rotar(5, 0.0f, 1.0f, 0.0f);
            }
            if (input.IsKeyDown(Keys.N) && input.IsKeyDown(Keys.J))
            {
                escenaDeserializada2.get("2").get("1").rotar(5, 0.0f, 0.0f, 1.0f);
            }

            if (input.IsKeyDown(Keys.B) && input.IsKeyDown(Keys.Y))
            {
                escenaDeserializada2.rotar(5, 1.0f, 0.0f, 0.0f);
            }
            if (input.IsKeyDown(Keys.B) && input.IsKeyDown(Keys.H))
            {
                escenaDeserializada2.rotar(5, 0.0f, 1.0f, 0.0f);
            }
            if (input.IsKeyDown(Keys.B) && input.IsKeyDown(Keys.J))
            {
                escenaDeserializada2.rotar(5, 0.0f, 0.0f, 1.0f);
            }

            if (input.IsKeyDown(Keys.M) && input.IsKeyDown(Keys.Y))
            {
                escenaDeserializada2.get("2").rotar(5, 1.0f, 0.0f, 0.0f);
            }
            if (input.IsKeyDown(Keys.M) && input.IsKeyDown(Keys.H))
            {
                escenaDeserializada2.get("2").rotar(5, 0.0f, 1.0f, 0.0f);
            }
            if (input.IsKeyDown(Keys.M) && input.IsKeyDown(Keys.J))
            {
                escenaDeserializada2.get("2").rotar(5, 0.0f, 0.0f, 1.0f);
            }



            if (input.IsKeyDown(Keys.O))
            {
                escenaDeserializada2.get("1").get("2").rotar(5, 0.0f, 0.0f, 1.0f);
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }

            // Get the mouse state
            var mouse = MouseState;

            if (_firstMove) // This bool variable is initially set to true.
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
              //  _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
        }
    }
}
