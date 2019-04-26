using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp2
{
    class Triangular:Fractal
    {
        bool flag = true;//флажок чтобы был
        public Triangular(Color startColor, Color lastColor, int depthmax, float leng) : base(startColor, lastColor, depthmax, leng)
        {
        }

        public override void Draw(double x, double y, ref Graphics gr, List<Color> colorList, Pen pn)
        {
            pn.Color = colorList[depthzero];//сразу инициализируем ручку

            Treedots(pn, x, y, colorList,ref gr);//тк передаю точко центр надо сначала найти вершины треугольника

        }

        /// <summary>
        /// вершины треугольника
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="colorList"></param>
        /// <param name="gr"></param>
        private void Treedots(Pen pn, double x, double y, List<Color> colorList, ref Graphics gr)
        {
            PointF A = new PointF((float)x,(float)(y-leng));
            PointF B = new PointF((float)(x + leng * Math.Cos(Math.PI / 6)), (float)(y + leng /2));
            PointF C = new PointF((float)(x - leng * Math.Cos(Math.PI / 6)), (float)(y + leng /2));
        
            gr.DrawLine(pn, A.X, A.Y, B.X, B.Y);//рисуем первый треугольник
            gr.DrawLine(pn, B.X, B.Y, C.X, C.Y);//рисуем первый треугольник
            gr.DrawLine(pn, A.X, A.Y, C.X, C.Y);//рисуем первый треугольник

            Drawing(pn, A , B , C , ref gr, colorList);//переходим к рисованию самого фрактала

        }

        /// <summary>
        /// собсвеннно сам фрактал
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="gr"></param>
        /// <param name="colorList"></param>
        private void Drawing(Pen pn, PointF A, PointF B, PointF C, ref Graphics gr, List<Color> colorList)
        {
            if (depthzero == depthmax) return;

            PointF D = new PointF();   //точка флоат)                                     
            
            depthzero++;

            pn.Color = colorList[depthzero];

            D.X = (A.X + B.X + C.X)/3; // находим координаты центра     
            D.Y = (A.Y + B.Y + C.Y)/3; // находим координаты центра     

            try
            {
                gr.DrawLine(pn, A.X, A.Y, D.X, D.Y); //рисуем линии  
                gr.DrawLine(pn, B.X, B.Y, D.X, D.Y); //рисуем линии
                gr.DrawLine(pn, C.X, C.Y, D.X, D.Y); //рисуем линии
            }
            catch (OverflowException e)//вроде не должно выпадать но на всякий случай))
            {
                if (flag)
                {
                    MessageBox.Show(e.Message, "Exception");
                    flag = false;
                }
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так!?", "Exception");
            }

            //depthzero++;

            Drawing(pn, A, B, D, ref gr, colorList);  //заходим в рекурсию  
            Drawing(pn, B, C, D, ref gr, colorList);  //заходим в рекурсию  
            Drawing(pn, A, C, D, ref gr, colorList);  //заходим в рекурсию

            depthzero--;
        }

    }

}
