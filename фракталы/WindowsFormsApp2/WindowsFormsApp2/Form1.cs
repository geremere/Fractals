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
    public partial class Form1 : Form
    {
        PointF mousePos;// точка, в которой щапонимается знчени мыши
        PointF center;//месро откуда начинает рисоваться фрактал
        PointF nowcenter;
        public Graphics gr; //Графика
        public Bitmap mp; //Битмап
        public Pen pn; //Ручка
        int depth;//глубина фрактала
        float defaultleng;//минимальная длина елемента фрактала
        Color defaultPen;// цвет ручки при ее не инициализации
        string selectFractal;
        bool flaginp;
        bool flaginpkoef;
        int type;
        protected double ang1;
        protected double ang2;
        protected double koef;
        List<Color> colorList;
        List<double> Koeflist;



        /// <summary>
        /// Конструктор
        /// </summary>
        public Form1()
        {
            MinimumSize = new Size(Screen.PrimaryScreen.Bounds.Size.Width/2, Screen.PrimaryScreen.Bounds.Size.Height/2);
            InitializeComponent();
            center = new PointF(pictureBox.Width / 2, pictureBox.Height / 2);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            MouseWheel += new MouseEventHandler(This_MouseWheel);
            mp = new Bitmap(pictureBox.Width, pictureBox.Height);
            gr = Graphics.FromImage(mp);
            pn = new Pen(defaultPen);
            defaultPen = Color.Black;
            ang1 = Math.PI / 4;
            ang2 = Math.PI / 6;
            koef = 1;
           
        }


        /// <summary>
        /// even click on mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {

            mousePos = MousePosition;
            nowcenter = center;
        }

        /// <summary>
        /// movind picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if ((selectFractal != null) || (depth != 0))
            {

                if (e.Button == MouseButtons.Left)
                {

                    DoubleBuffered = true;                  

                    center.X = nowcenter.X - mousePos.X + MousePosition.X;
                    center.Y = nowcenter.Y - mousePos.Y + MousePosition.Y;
                    Choise();

                }
            }

        }

        /// <summary>
        /// Clear pictureBox
        /// </summary>
        void Clear()
        {
            gr.Clear(Color.White);
            pictureBox.Image = mp;
        }

        /// <summary>
        /// ZOOM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void This_MouseWheel(object sender, MouseEventArgs e)
        {
            if((selectFractal!=null)||(depth!=0))
            {
                if (e.Delta != 0)
                {
                    if (e.Delta <= 0)
                    {

                        defaultleng /= e.Delta / 60 * (-1);//уменьшает изображение в 2 раза
                        if (defaultleng < 1 || double.IsInfinity(defaultleng))
                            defaultleng = 1;

                        Choise();

                        //pictureBoxOne(sender, e);

                        return;
                    }
                    else
                    {

                        defaultleng += e.Delta / 24;//увеличивает в на 5 т.е в 2 3 4 5 6 7 8 и далее раз
                        if (defaultleng > 100 || double.IsInfinity(defaultleng))
                            defaultleng = 100;

                        Choise();
                        //pictureBoxOne(sender, e);

                        return;
                    }
                }
            }
        }

        /// <summary>
        /// выбор отрисовка фрактала(необходимый метод для правильной работы приувеличении и перемещении картинки1
        /// </summary>
        private void Choise()
        {
            switch (type)
            {
                case (3):
                    Triangular();
                    break;
                case (2):
                    H_Fractal();
                    break;
                case (1):
                    Piftree();
                    break;
            }

        }

        /// <summary>
        /// depth input
        /// </summary>
        private void InputInspect()
        {
            try
            {
                depth = int.Parse(textBox1.Text);
                if (int.Parse(textBox1.Text) <= 0)
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Некоррректный ввод глубины фрактала", "Exception from depth");
                flaginp = true;
                return;
            }
            catch (FormatException ext)
            {
                MessageBox.Show("Некоррректный ввод глубины фрактала", "Exception from depth");
                flaginp = true;
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так!?", "Exception");
                flaginp = true;
                return;
            }

             if (selectFractal == null) MessageBox.Show("Выберите фрактал", "Exception");//просто чтобы выкинуть окошко
        }

        /// <summary>
        /// проверка ввода коэффиценциента
        /// </summary>
        private void InputInspect2()
        {
            try
            {
                if (textBox2.Text == "") return;
                 koef = double.Parse(textBox2.Text);
                if (double.Parse(textBox2.Text) <= 0)
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Exception");
                flaginpkoef = true;
                return;
            }

            catch (FormatException ext)
            {
                MessageBox.Show(ext.Message, "Exception");
                flaginpkoef = true;
                return;
            }
            try
            {
                if ((koef < 0.4) || (koef > 1.5))
                {
                    throw new ArgumentException();
                }
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Нужно ввести число от 0,4 до 1,5", "Hint");
                flaginpkoef = true;
                koef = 1;
                return;
            }

        }

        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxOne(object sender, EventArgs e)
        {

            switch (selectFractal)
            {
                case "Дерево Пифагора":
                    type = 1;
                    if ((depth > 11) && (depth <=20)) MessageBox.Show("Не гарантируется плавная работа", "Примечание");
                    if (depth > 20) { MessageBox.Show("Слишком глубока рекурсия (введите depth<20)", "Окошко"); return; }
                    Piftree();
                    return;

                case "Н-фрактал":
                    type = 2;
                    if ((depth > 5) && (depth <= 10)) MessageBox.Show("Не гарантируется плавная работа", "Примечание");
                    if (depth > 10) { MessageBox.Show("Слишком глубока рекурсия (введите depth<10)", "Окошко"); return; }
                    H_Fractal();
                    return;

                case "Центр масс треугольника":
                    type = 3;
                    if ((depth > 7) && (depth <= 10)) MessageBox.Show("Не гарантируется плавная работа", "Примечание");
                    if (depth > 10) { MessageBox.Show("Слишком глубока рекурсия (введите depth<10)", "Окошко"); return; }
                    Triangular();
                    return;

                case "":
                    MessageBox.Show("Выберите фрактал", "Input Exception");
                    return;

            }

            pictureBox.Image = mp;

        }

        /// <summary>
        /// вызов треугольника центра масс
        /// </summary>
        /// <param name="colorList"></param>
        private void Triangular()
        {
            Clear();
            Fractal frac1 = new Triangular(Color.Black, Color.Black, depth, defaultleng * 30);
            frac1.Draw(center.X, center.Y, ref gr, colorList, pn);
        }

        /// <summary>
        /// вызов Н-фрактала
        /// </summary>
        /// <param name="colorList"></param>
        private void H_Fractal()
        {
            Clear();
            Fractal frac1 = new H_fractal(Color.Black, Color.Black, depth, defaultleng*30);
            frac1.Draw(center.X, center.Y, ref gr, colorList, pn);
        }

        /// <summary>
        /// вызов дерева пифагора
        /// </summary>
        /// <param name="colorList"></param>
        private void Piftree()
        {
            Clear();            
            Fractal frac = new Piftree(Color.Black, Color.Black, depth, defaultleng, Koeflist, ang1, ang2);
            frac.Draw(center.X, center.Y, ref gr, colorList, pn);
        }

        /// <summary>
        /// изменение коэффицента
        /// </summary>
        /// <param name="Koef"></param>
        private List<double> Koef1()
        {
            List<double> Koefls = new List<double>();

            if (textBox2.Text != "")
            {
                double k=1;
                //if (koef > 1) k = koef / depth;
                //else k = koef / -depth;

                for (int i = 0; i < depth; i++)
                {
                    k *= koef;
                    Koefls.Add(k);
                }
            }
            else
            {
                for (int i = 0; i < depth; i++)
                {
                    Koefls.Add(1);
                }
            }

            return Koefls;
        }

        /// <summary>
        /// видимость тракбаров для измения угла дерева пифагора
        /// </summary>
        private void Vision()
        {
            if(selectFractal== "Дерево Пифагора")
            {
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                trackBar1.Visible = true;
                trackBar2.Visible = true;
                textBox2.Visible = true;
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                trackBar1.Visible = false;
                trackBar2.Visible = false;
                textBox2.Visible = false;
            }
        }

        /// <summary>
        /// Color gradient
        /// </summary>
        /// <param name="colorList"></param>
        private List<Color> Gradient()
        {
            List<Color> CL= new List<Color>();
            int rMin = colorDialog1.Color.R;
            int gMin = colorDialog1.Color.G;
            int bMin = colorDialog1.Color.B;
            int argMin = colorDialog1.Color.ToArgb();

            if (argMin == 0)
            {
                argMin = defaultPen.ToArgb();
            }

            int rMax = colorDialog2.Color.R;
            int gMax = colorDialog2.Color.G;
            int bMax = colorDialog2.Color.B;
            int argMax = colorDialog2.Color.ToArgb();

            if (argMax == 0)
            {
                argMax = defaultPen.ToArgb();
            }

            for (int i = 0; i <= depth; i++)
            {
                var rAverage = rMin + ((rMax - rMin) * i / depth);
                var gAverage = gMin + ((gMax - gMin) * i / depth);
                var bAverage = bMin + ((bMax - bMin) * i / depth);
                CL.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }
            return CL;
        }

        /// <summary>
        /// SAVE
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.ShowDialog();
            string filename = save.FileName + ".jpg";
            mp.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
        }        

        /// <summary>
        /// button start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void draw_Click(object sender, EventArgs e)
        {
            center = new PointF(pictureBox.Width / 2, pictureBox.Height / 2);
            defaultleng = 5;//каждый раз при нажатии кнопки старт размер фрактала становится стандартным
            InputInspect();
            if (flaginp) { flaginp = false; return; }//флажок для того чтобы не вылетаало окно ошибки по миллион раз
            colorList = Gradient();
            if (selectFractal == "Дерево Пифагора")
            {
                InputInspect2();
                if (flaginpkoef) { flaginpkoef = false; return; }//флажок для того чтобы не вылетаало окно ошибки по миллион раз
                Koeflist = Koef1();
            }
            pictureBoxOne(sender, e);
        }

        /// <summary>
        /// Измение размера pictureBox и перерисовка фрактала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
           
            try
            {
                mp = new Bitmap(pictureBox.Width, pictureBox.Height);
                gr = Graphics.FromImage(mp);
            }
            catch(ArgumentException)//ловит ошибку но не выдает сообщения т.к. это не зависит от пользователя
            {
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так!?", "Exception");
            }

            if ((selectFractal != null) || (depth != 0))
                Choise();




        }

        /// <summary>
        /// ColorDialog(startcolor)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button3.BackColor = colorDialog1.Color;//красим кнопку
            if (button3.BackColor == Color.Black) button3.ForeColor = Color.White;//красим буквы
            if (button3.BackColor != Color.Black) button3.ForeColor = Color.Black;//красим буквы


        }

        /// <summary>
        /// ColorDialog2(lastcolor)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog2.ShowDialog();
            button4.BackColor = colorDialog2.Color;//красим кнопку
            if (button4.BackColor == Color.Black) button4.ForeColor = Color.White;//красим буквы
            if (button4.BackColor != Color.Black) button4.ForeColor = Color.Black;//красим буквы



        }

        /// <summary>
        /// делает красивыую анимацию обдувания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (type == 1)
            {
                ang1 = Math.PI / trackBar1.Value;
                Piftree();
            }
        }

        /// <summary>
        /// делает красивую аимацию обдувания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (type == 1)
            {
                ang2 = Math.PI / trackBar2.Value;
                Piftree();
            }
        }

        /// <summary>
        /// метод для выбора фрактала из комбобокса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                selectFractal = comboBox1.SelectedItem.ToString();
                Vision();
                if (selectFractal == null) throw new NullReferenceException();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Выберите фрактал", "Exception");
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так!?", "Exception");
            }
        }
    }
    
}