using System;
using System.IO;
using System.Windows;

namespace FileStream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string path_to_file = "";
        /// <summary>
        /// Конструктор
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Открывает какой то текстовый файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                path_to_file = Path_.Text;
                StreamReader reader = new StreamReader(path_to_file);
                MainText.Text = reader.ReadToEnd();
                reader.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Incorrect path to file");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}");
            }
        }
        /// <summary>
        /// Для сохранения измененного файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path_to_file);
                writer.WriteAsync(MainText.Text);
                writer.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}");
            }
        }
    }
}
