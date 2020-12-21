using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temperature_Converter
{
    /// <summary>
    /// Represents a temperature form.
    /// </summary>
    public partial class Temp : Form
    {
        /// <summary>
        /// Initializes an instance of the Temp form class.
        /// </summary>
        public Temp()
        {
            InitializeComponent();

            this.Load += Temp_Load;
            this.btnConvert.Click += BtnConvert_Click;
            this.radFahrenheit.CheckedChanged += RadFahrenheit_CheckedChanged;
        }

        /// <summary>
        /// Handles the CheckedChanged event of radFahrenheit button.
        /// </summary>
        private void RadFahrenheit_CheckedChanged(object sender, EventArgs e)
        {
            // once the button changes to Celsius, change the text of the temperature symbol next to Conversion output
            this.lblFOrC.Text = "°F";
            this.lblInputFOrC.Text = "°C";
            this.lblOutput.Text = "";
            this.txtInput.Focus();

            if (!this.radFahrenheit.Checked)
            {
                this.lblFOrC.Text = "°C";
                this.lblInputFOrC.Text = "°F";
            }
        }

        /// <summary>
        /// Handles the click event of BtnConvert.
        /// </summary>
        private void BtnConvert_Click(object sender, EventArgs e)
        {
            string errorMessage = String.Empty;
            decimal input;
            decimal result;

            // once clicked, validate the input to ensure it's a numeric value.
            // if not numeric, display an error icon beside the textbox, clear output 
            // set focus to textbox and select all its text
            try
            {
                input = Decimal.Parse(this.txtInput.Text);
            }
            catch
            {
                errorMessage = "Temperature cannot contain letters or special characters.";
                this.errorProvider.SetError(this.txtInput, errorMessage);

                this.lblOutput.Text = "";

                // set focus to temperature textbox and select all its text
                this.txtInput.Focus();
                this.txtInput.SelectAll();
            }

            if (errorMessage.Equals(String.Empty) && this.radFahrenheit.Checked)
            {
                this.errorProvider.SetError(this.txtInput, String.Empty);

                input = Decimal.Parse(this.txtInput.Text);

                result = Decimal.Multiply(input, 1.8M) + 32; 

                this.lblOutput.Text = Math.Round(result, 1).ToString();
            }
            if (errorMessage.Equals(String.Empty) && this.radCelsius.Checked)
            {
                this.errorProvider.SetError(this.txtInput, String.Empty);

                input = Decimal.Parse(this.txtInput.Text);

                result = Decimal.Multiply((input - 32), 0.5556M);

                this.lblOutput.Text = Math.Round(result, 1).ToString();
            }
        }

        /// <summary>
        /// Handles the load event of the Temp form.
        /// </summary>
        private void Temp_Load(object sender, EventArgs e)
        {
            // temperature textbox has focus
            this.txtInput.Focus();

            // fahrenheit is selected by default
            this.radFahrenheit.Checked = true;
        }
    }
}
