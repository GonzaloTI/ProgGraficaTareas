using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.IO;

namespace ProgGraficaTareas
{
    public class Game : GameWindow
    {
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }

        Shader shader;

        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;

        private Matrix4 projection;
        private Matrix4 view;
        private Matrix4 model;

        Televisor televisor;
 
        Objeto objeto1;
        Objeto objeto2;
        Objeto objeto3;

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

         
           projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(20.0f), Size.X / (float)Size.Y, 0.1f, 60.0f);
         
            view = Matrix4.CreateTranslation(0.0f, 0.0f, -4.0f);
          // projection = Matrix4.CreateOrthographic(2.0f, 2.0f, 0.1f, 100.0f); // Tamaño de la proyección ortogonal en el plano XY
            model = Matrix4.Identity * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(30.0f));


            shader.SetMatrix4("model", model);
            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);

            shader.Use();
            var move = Matrix4.Identity;
            move = move * Matrix4.CreateTranslation(0f, 0f, 0f);
            shader.SetMatrix4("origen", move);

            televisor = new Televisor(shader, new Vector3(0.0f, 0.0f, 0.0f));


            Cara baseFlorero = new Cara(shader, "Base Florero");
            baseFlorero.add("1", new Punto(0.15f, 0.10f, -0.10f));
            baseFlorero.add("2", new Punto(0.17f, 0.20f, -0.10f));
            baseFlorero.add("3", new Punto(0.17f, 0.25f, -0.10f));
            baseFlorero.add("4", new Punto(0.10f, 0.25f, -0.10f));
            baseFlorero.add("5", new Punto(0.10f, 0.20f, -0.10f));
            baseFlorero.add("6", new Punto(0.13f, 0.10f, -0.10f));

            Cara baseFlorero2 = new Cara(shader, "Base Florero 2");
            baseFlorero2.add("1", new Punto(0.15f, 0.10f, -0.13f));
            baseFlorero2.add("2", new Punto(0.17f, 0.20f, -0.13f));
            baseFlorero2.add("3", new Punto(0.17f, 0.25f, -0.13f));
            baseFlorero2.add("4", new Punto(0.10f, 0.25f, -0.13f));
            baseFlorero2.add("5", new Punto(0.10f, 0.20f, -0.13f));
            baseFlorero2.add("6", new Punto(0.13f, 0.10f, -0.13f));

            Cara basesup = new Cara(shader, "Base sup Florero");
            basesup.add("1", new Punto(0.17f, 0.25f, -0.13f));
            basesup.add("2", new Punto(0.10f, 0.25f, -0.13f));
            basesup.add("3", new Punto(0.17f, 0.25f, -0.10f));
            basesup.add("4", new Punto(0.10f, 0.25f, -0.10f));

            Cara baseinf = new Cara(shader, "Base inf Florero");
            baseinf.add("1", new Punto(0.15f, 0.10f, -0.10f));
            baseinf.add("2", new Punto(0.13f, 0.10f, -0.10f));
            baseinf.add("3", new Punto(0.15f, 0.10f, -0.13f));
            baseinf.add("4", new Punto(0.13f, 0.10f, -0.13f));

            // Crea un objeto para contener todas las caras del florero
            this.objeto1 = new Objeto("Florero");

            // Añade cada cara al objeto del florero
            objeto1.Add(baseFlorero);
            objeto1.Add(baseFlorero2);
            objeto1.Add(basesup);

            this.objeto2 = new Objeto("equiposemusica");

            Cara caraeq = new Cara(shader, "Base equipo"); 
            caraeq.add("1", new Punto(-0.55f, -0.20f, -0.10f));
            caraeq.add("2", new Punto(-0.45f, -0.20f, -0.10f));
            caraeq.add("3", new Punto(-0.45f, 0.01f, -0.10f));
            caraeq.add("4", new Punto(-0.55f, 0.01f, -0.10f));


            Cara caraeq2 = new Cara(shader, "Base equipo 2");
            caraeq2.add("1", new Punto(-0.55f, -0.20f, -0.0f));
            caraeq2.add("2", new Punto(-0.45f, -0.20f, -0.0f));
            caraeq2.add("3", new Punto(-0.45f, 0.01f, -0.0f));
            caraeq2.add("4", new Punto(-0.55f, 0.01f, -0.0f));

         Cara carasupp = new Cara(shader, "superior");
            carasupp.add("1", new Punto(-0.55f,  0.01f, -0.10f));
            carasupp.add("2", new Punto(-0.45f, 0.01f, -0.10f));
            carasupp.add("3", new Punto(-0.45f, 0.01f, -0.0f));
            carasupp.add("4", new Punto(-0.55f, 0.01f, -0.0f));

            Cara carainf = new Cara(shader, "inferior");
            carainf.add("1", new Punto(-0.55f, -0.20f, -0.10f));
            carainf.add("2", new Punto(-0.45f, -0.20f, -0.10f));
            carainf.add("4", new Punto(-0.45f, -0.20f, -0.0f));
             carainf.add("3", new Punto(-0.55f, -0.20f, -0.0f));

             this.objeto2.Add(caraeq);
            this.objeto2.Add(caraeq2);
            this.objeto2.Add(carainf);
            this.objeto2.Add(carasupp);

            this.objeto3 = new Objeto("equiposemusica 2");

            Cara caraeqdelante2 = new Cara(shader, "Base equipo");
            caraeqdelante2.add("1", new Punto(0.55f, -0.20f, -0.10f));
            caraeqdelante2.add("2", new Punto(0.45f, -0.20f, -0.10f));
            caraeqdelante2.add("3", new Punto(0.45f, 0.01f, -0.10f));
            caraeqdelante2.add("4", new Punto(0.55f, 0.01f, -0.10f));


            Cara caraeqatras2 = new Cara(shader, "Base equipo 2");
            caraeqatras2.add("1", new Punto(0.55f, -0.20f, -0.0f));
            caraeqatras2.add("2", new Punto(0.45f, -0.20f, -0.0f));
            caraeqatras2.add("3", new Punto(0.45f, 0.01f, -0.0f));
            caraeqatras2.add("4", new Punto(0.55f, 0.01f, -0.0f));

            Cara carasupp2 = new Cara(shader, "superior");
            carasupp2.add("1", new Punto(0.55f, 0.01f, -0.10f));
            carasupp2.add("2", new Punto(0.45f, 0.01f, -0.10f));
            carasupp2.add("3", new Punto(0.45f, 0.01f, -0.0f));
            carasupp2.add("4", new Punto(0.55f, 0.01f, -0.0f));

            Cara carainf2 = new Cara(shader, "inferior");
            carainf2.add("1", new Punto(0.55f, -0.20f, -0.10f));
            carainf2.add("2", new Punto(0.45f, -0.20f, -0.10f));
            carainf2.add("4", new Punto(0.45f, -0.20f, -0.0f));
            carainf2.add("3", new Punto(0.55f, -0.20f, -0.0f));

            this.objeto3.Add(caraeqdelante2);
            this.objeto3.Add(caraeqatras2);
            this.objeto3.Add(carasupp2);
            this.objeto3.Add(carainf2);





        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);


            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.BindVertexArray(vertexArrayObject);
            shader.Use();

             televisor.dibujar();


           this.objeto1.Dibujar();
            this.objeto2.Dibujar();
            this.objeto3.Dibujar();

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

    }
}
