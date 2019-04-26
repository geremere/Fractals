using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //это сделанно чтобы это сообщение вызывалось всего лишь раз
                MessageBox.Show("Стандартный цвет рисования черный\n" +
                    "При выборе дерева Пифaгора появляют нужные для обращения с ней интерфейс\n" +
                    "Ввод коэффициента прозводится через запятую в границах от 0,4 до 1,5\n" +
                    "Программа рисует фракталы 3-ёх видов, кторые вы найдете в самой программе\n" +
                    "Приятного использования)\n" +
                    "Надеюсь она не упадет =)", "Инструкции");
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
