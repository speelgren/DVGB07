using System.Drawing.Text;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Minir�knare
{
    public partial class Kalkylator : Form
    {

        private string input_calculation = "";

        public Kalkylator()
        {
            InitializeComponent();
        }

        private void button_clicked(object sender, EventArgs e)
        {
            input_calculation += (sender as Button).Text;

            textBox_output.Text = input_calculation;
        }

        private void button_equals_clicked(object sender, EventArgs e)
        {
            /* Ville l�sa uppgiften utan att beh�va skapa en function f�r varje individuell knapp och ge den ett v�rde,
             * utan ist�llet ta v�rdet av det som redan finns i knappens text-content. Se functionen "button_clicked".
             * Detta ledde mig ner i detta "tr�sk".
             */
            /* https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.split?redirectedfrom=MSDN&view=net-7.0#System_Text_RegularExpressions_Regex_Split_System_String_ */
            Regex Regex = new Regex("([+-/*])");
            string[] input_to_string = Regex.Split(input_calculation, "([0-9]+|[/+]+|[-]+|[//]+|[*])");

            /* Kollar om input_to_string inneh�ller tomma str�ngar.
             * Sparar resultatet i input_to_array. 
             */
            var input_to_array = input_to_string.ToArray().Where(item => item.Length > 0);
            string[] input_to_join = String.Join(" ", input_to_array).Split(" ");
            int nr;

            /* Om n�got fel uppst�r. Om f�rsta input_to_join inte �r en int */
            if (!Int32.TryParse(input_to_join[0], out nr))
            {
                textBox_output.Text = "Input valid calculation.";
                return;
            }

            for (int i = 1; i < input_to_join.Length; i++)
            {
                /* ang. "[^0-9]" tog jag hj�lp av:
                 * https://www.c-sharpcorner.com/UploadFile/puranindia/regular-expressions-in-C-Sharp/ */
                if (Regex.Replace(input_to_join[i], "[^0-9]", "").Length > 0)
                {
                    /* Sparar operator */
                    string input_operator = input_to_join[i - 1];
                    int result;

                    /* Kolla om input �r int. 
                     * Fixar �ven overflow n�r resultatet blir st�rre �n int. */
                    if (!Int32.TryParse(input_to_join[i], out result))
                    {
                        textBox_output.Text = "Input valid calculation.";
                        return;
                    }

                    /* Switch f�r varje typ av operator. 
                     * Kan definitivt l�sas p� ett b�ttre s�tt. 
                     */
                    switch (input_operator)
                    {
                        case "/":
                            if (result == 0)
                            {
                                textBox_output.Text = "No division by zero allowed.";
                                return;
                            }
                            nr /= result;
                            break;
                        case "*":
                            nr *= result;
                            break;
                        case "-":
                            nr -= result;
                            break;
                        case "+":
                            nr += result;
                            break;
                        default:
                            textBox_output.Text = "Input valid calculation.";
                            return;
                    }
                }
            }

            textBox_output.Text = nr.ToString();
        }

        private void button_clear_clicked(object sender, EventArgs e)
        {
            /* 
             * t�mmer rutan n�r anv�ndaren klickar p� C-knappen.
             */
            input_calculation = "";
            textBox_output.Clear();
        }

    }
}