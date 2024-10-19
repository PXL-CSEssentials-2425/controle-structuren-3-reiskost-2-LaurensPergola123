using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reiskost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
           
                // Variabelen declareren
                string destination = destinationTextBox.Text;
                float baseFlight = float.Parse(baseFlightTextBox.Text);
                int flightClass = int.Parse(flightClassTextBox.Text);
                float basePrice = float.Parse(basePriceTextBox.Text);
                int numberOfDays = int.Parse(numberOfDaysTextBox.Text);
                int numberOfPersons = int.Parse(numberOfPersonsTextBox.Text);
                float reductionPercentage = float.Parse(reductionPercentageTextBox.Text);

                // Vluchtklasse aanpassingen
                switch (flightClass)
                {
                    case 1: // Businessclass
                        baseFlight *= 1.30f; // 30% verhoging
                        break;
                    case 3: // Charter
                        baseFlight *= 0.80f; // 20% korting
                        break;
                    case 2: // Standaard geen aanpassing
                    default:
                        break;
                }

                // Totale vluchtkosten
                float totalFlightCost = baseFlight * numberOfPersons;

                // Verblijfsprijs met kortingen voor meerdere personen
                float totalStayingCost = 0;
                for (int i = 1; i <= numberOfPersons; i++)
                {
                    if (i == 1 || i == 2)
                    {
                        totalStayingCost += basePrice * numberOfDays; // Eerste 2 personen betalen volledig
                    }
                    else if (i == 3)
                    {
                        totalStayingCost += (basePrice * numberOfDays) * 0.50f; // 3e persoon krijgt 50% korting
                    }
                    else
                    {
                        totalStayingCost += (basePrice * numberOfDays) * 0.30f; // 4e en volgende personen krijgen 70% korting
                    }
                }

                // Totale reisprijs
                float totalCost = totalFlightCost + totalStayingCost;

                // Korting toepassen
                float discount = totalCost * (reductionPercentage / 100);
                float totalCostPlusDiscount = totalCost - discount;

                // Resultaten tonen
                resultTextBox.Text = $"REISKOST VOLGENS BESTELLING NAAR {destination}\r\n\r\n" +
                                     $"Totale vluchtprijs: {totalFlightCost:c}\r\n" +
                                     $"Totale Verblijfsprijs: {totalStayingCost:c}\r\n" +
                                     $"Totale reisprijs: {totalCost:c}\r\n" +
                                     $"Korting: {discount:c}\r\n\r\n" +
                                     $"Te betalen: {totalCostPlusDiscount:c}";
           
      


        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            //Alles Wissen
            destinationTextBox.Clear();
            baseFlightTextBox.Clear();
            flightClassTextBox.Clear();
            basePriceTextBox.Clear();
            numberOfDaysTextBox.Clear();
            numberOfPersonsTextBox.Clear();
            reductionPercentageTextBox.Clear();
            resultTextBox.Clear();

            destinationTextBox.Focus();

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //Afsluiten
            this.Close();
        }

        private void flightClassTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            infoTextBox.Text = $"1=Businessclass\r\n" + $"2=Standaard lijnvlucht\r\n" + $"3=Charter\r\n";
        }

        private void flightClassTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            infoTextBox.Clear();
        }
    }
}