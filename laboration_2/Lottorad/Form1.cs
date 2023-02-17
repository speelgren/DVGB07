using System.Diagnostics.Metrics;

namespace Lottorad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_start_clicked(object sender, EventArgs e)
        {
            Random r = new();

            int[] user_input = new int[7];
            int[] lotto_row = new int[7];
            int input_5_correct = 0;
            int input_6_correct = 0;
            int input_7_correct = 0;

            try
            {
                int counts = Int32.Parse(textBox_counts.Text);

                /* 
                 * Spara textBox_input.
                 */
                string[] textBoxInputs = new string[]
                {
                    textBox_input_1.Text,
                    textBox_input_2.Text,
                    textBox_input_3.Text,
                    textBox_input_4.Text,
                    textBox_input_5.Text,
                    textBox_input_6.Text,
                    textBox_input_7.Text
                };

                /*
                 * Check f�r att kolla s� att alla inputs �r unika.
                 * Kasta exception om det finns dubblett bland input.
                 */
                for (int i = 0; i < textBoxInputs.Length; i++)
                {
                    for (int j = i + 1; j < textBoxInputs.Length; j++)
                    {
                        if (textBoxInputs[i] == textBoxInputs[j])
                        {
                            throw new Exception("Input values must be unique");
                        }
                    }
                }

                /*
                 * correlate user_input till ett textBox_input.
                 */
                for (int i = 0; i < user_input.Length; i++)
                {
                    user_input[i] = Int32.Parse(textBoxInputs[i]);
                }

                /*
                 * Algoritm f�r att slumpa tal. 7 slumpade tal f�r varje antal (counts) anv�ndaren valt.
                 * Kollar sedan efter dubbletter bland de slumpade talen och byter ut dem mot ett nytt slumptal.
                 */
                for(int l = 0; l < counts; l++)
                {
                    int hit = 0;

                    for(int m = 0; m <7; m++)
                    {
                        lotto_row[m] = r.Next(1, 36);
                    }

                    for (int n = 0; n < 7; n++)
                    {
                        int temp = lotto_row[n];
                        int swapIndex = r.Next(n, 7);
                        lotto_row[n] = lotto_row[swapIndex];
                        lotto_row[swapIndex] = temp;
                    }

                    /*
                     * R�kna upp hit f�r varje tr�ff bland de slumpade talen.
                     */
                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            int lotto_num = lotto_row[j];
                            if (user_input[i] == lotto_num)
                            {
                                hit++;
                            }
                            while (lotto_num == lotto_row[j])
                            {
                                lotto_num = r.Next(1, 36);
                            }
                            lotto_row[j] = lotto_num;
                        }
                    }

                    /*
                     * Uppr�kning om user_input f�r tr�ff bland de slumpade talen.
                     */
                    if (hit == 5)
                    {
                        input_5_correct++;
                    }
                    if(hit == 6)
                    {
                        input_6_correct++;
                    }
                    if(hit == 7)
                    {
                        input_7_correct++;
                    }
                }

                /* 
                 * Uppdatera textBox med antalet tr�ffar.
                 */
                textBox_5_correct.Text = input_5_correct.ToString();
                textBox_6_correct.Text = input_6_correct.ToString();
                textBox_7_correct.Text = input_7_correct.ToString();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}