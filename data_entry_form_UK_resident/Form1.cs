using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace data_entry_form_UK_resident
{
    public partial class Form1 : Form
    {
        //Auxiliary symbol and letter lists that help with input validation.
        List<string> symbolsList = new List<string> { "`", "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+", "[", "{", "]", "}",
                                                      ";", ":", "'", "\"", "\\", "|", ",", "<", ".", ">", "/", "?", " " };
        List<string> numbersList = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        List<string> lettersList = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                                      "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        Color yellow = Color.FromArgb(255, 196, 107);
        Color blue = Color.FromArgb(14, 73, 117);
        Color green = Color.FromArgb(114, 194, 81);
        Color red = Color.FromArgb(219, 56, 62);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeProgram();
        }

        //The removed symbols are allowed to be used, when entering a name or surname.
        private void textBoxFullName_TextChanged(object sender, EventArgs e)
        {
            symbolsList.Remove("-");
            symbolsList.Remove("'");
            symbolsList.Remove(".");
            symbolsList.Remove(" ");

            CheckInput(symbolsList, numbersList, textBoxFullName, labelFullNameErrorMessage);   //If any character included in the symbols and numbers lists exists in the Full Name field, an error is displayed.
        }

        //When the field loses focus, the removed symbols are re-added to the list, so that it is complete and ready to be used in another field.
        private void textBoxFullName_Leave(object sender, EventArgs e)
        {
            if (!symbolsList.Contains("-") && !symbolsList.Contains("'") && !symbolsList.Contains(".") && !symbolsList.Contains(" "))   //"if" used to prevent re-adding those elements, if they already exist,
            {                                                                                                                           //in case the user just entered and left the text box without inputting anything.
                symbolsList.Add("-");
                symbolsList.Add("'");
                symbolsList.Add(".");
                symbolsList.Add(" ");
            }
        }

        private void comboBoxDayOfBirth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMonthOfBirth.SelectedIndex > 0 && comboBoxDayOfBirth.SelectedIndex > 0 && comboBoxYearOfBirth.SelectedIndex > 0)
                CheckDateOfBirthValidity();
        }

        private void comboBoxMonthOfBirth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMonthOfBirth.SelectedIndex > 0 && comboBoxDayOfBirth.SelectedIndex > 0 && comboBoxYearOfBirth.SelectedIndex > 0)
                CheckDateOfBirthValidity();
        }

        private void comboBoxYearOfBirth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMonthOfBirth.SelectedIndex > 0 && comboBoxDayOfBirth.SelectedIndex > 0 && comboBoxYearOfBirth.SelectedIndex > 0)
                CheckDateOfBirthValidity();
        }

        private void comboBoxCountryOfBirth_TextUpdate(object sender, EventArgs e)
        {
            symbolsList.Remove(" ");
            symbolsList.Remove(".");
            symbolsList.Remove("'");
            symbolsList.Remove("-");
            symbolsList.Remove("(");
            symbolsList.Remove(")");

            CheckInput(symbolsList, numbersList, comboBoxCountryOfBirth, labelCountryOfBirthErrorMessage);
        }

        private void comboBoxCountryOfBirth_Leave(object sender, EventArgs e)
        {
            if (!symbolsList.Contains(" ") && !symbolsList.Contains(".") && !symbolsList.Contains("'") && !symbolsList.Contains("-") && !symbolsList.Contains("(") && !symbolsList.Contains(")"))
            {
                symbolsList.Add(" ");
                symbolsList.Add(".");
                symbolsList.Add("'");
                symbolsList.Add("-");
                symbolsList.Add("(");
                symbolsList.Add(")");
            }
        }

        private void textBoxPassport_TextChanged(object sender, EventArgs e)
        {
            CheckInput(symbolsList, textBoxPassport, labelPassportErrorMessage);
        }

        private void textBoxMobileNumber_TextChanged(object sender, EventArgs e)
        {
            symbolsList.Remove("+");
            symbolsList.Remove("(");
            symbolsList.Remove(")");
            symbolsList.Remove(" ");

            CheckInput(symbolsList, lettersList, textBoxMobileNumber, labelMobileNmbErrorMessage);
        }

        private void textBoxMobileNumber_Leave(object sender, EventArgs e)
        {
            if (!symbolsList.Contains("+") && !symbolsList.Contains("(") && !symbolsList.Contains(")") && !symbolsList.Contains(" "))
            {
                symbolsList.Add("+");
                symbolsList.Add("(");
                symbolsList.Add(")");
                symbolsList.Add(" ");
            }
        }

        //Only one preferred contact method can be selected.
        private void checkBoxPreferredMobile_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPreferredMobile.Checked == true)
            {
                checkBoxPreferredEmail.Enabled = false;
                checkBoxPreferredLandline.Enabled = false;
                checkBoxPreferredRoyalMail.Enabled = false;
            }
            else
            {
                checkBoxPreferredEmail.Enabled = true;
                checkBoxPreferredLandline.Enabled = true;
                checkBoxPreferredRoyalMail.Enabled = true;
            }
        }

        private void textBoxLandlineNumber_TextChanged(object sender, EventArgs e)
        {
            symbolsList.Remove("(");
            symbolsList.Remove(")");
            symbolsList.Remove(" ");

            CheckLandlineNumberFormat(symbolsList, lettersList, textBoxLandlineNumber);
        }

        private void textBoxLandlineNumber_Leave(object sender, EventArgs e)
        {
            if (!symbolsList.Contains("(") && !symbolsList.Contains(")") && !symbolsList.Contains(" "))
            {
                symbolsList.Add("(");
                symbolsList.Add(")");
                symbolsList.Add(" ");
            }
        }

        private void checkBoxPreferredLandline_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPreferredLandline.Checked == true)
            {
                checkBoxPreferredEmail.Enabled = false;
                checkBoxPreferredMobile.Enabled = false;
                checkBoxPreferredRoyalMail.Enabled = false;
            }
            else
            {
                checkBoxPreferredEmail.Enabled = true;
                checkBoxPreferredMobile.Enabled = true;
                checkBoxPreferredRoyalMail.Enabled = true;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            labelHouseNumberDisplay.Text = trackBar1.Value.ToString() + comboBoxHouseNumber.Text;       //Displays the track bar's (selected) number and the combo box's letter. Used for entering an address.
        }

        private void comboBoxHouseNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelHouseNumberDisplay.Text = trackBar1.Value.ToString() + comboBoxHouseNumber.Text;
        }

        private void textBoxStreetName_TextChanged(object sender, EventArgs e)
        {
            symbolsList.Remove(" ");

            CheckInput(symbolsList, numbersList, textBoxStreetName, labelStreetNameErrorMessage);
        }

        private void textBoxStreetName_Leave(object sender, EventArgs e)
        {
            if (!symbolsList.Contains(" "))
            {
                symbolsList.Add(" ");
            }
        }

        //Both the outcode and incode combo boxes have two events that check their validity, because the user can either type a value or select one from the drop down menu.
        private void comboBoxOutcode_TextUpdate(object sender, EventArgs e)
        {
            CheckOutcodeFormat(comboBoxOutcode);

            if (comboBoxOutcode.Text.Length == 4)       //If the outcode field is filled, the cursor moves to the next one.
                comboBoxIncode.Focus();
        }

        private void comboBoxOutcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckOutcodeFormat(comboBoxOutcode);
        }

        private void comboBoxIncode_TextUpdate(object sender, EventArgs e)
        {
            CheckIncodeFormat(comboBoxIncode);
        }

        private void comboBoxIncode_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckIncodeFormat(comboBoxIncode);
        }

        private void checkBoxPreferredRoyalMail_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPreferredRoyalMail.Checked == true)
            {
                checkBoxPreferredEmail.Enabled = false;
                checkBoxPreferredLandline.Enabled = false;
                checkBoxPreferredMobile.Enabled = false;
            }
            else
            {
                checkBoxPreferredEmail.Enabled = true;
                checkBoxPreferredLandline.Enabled = true;
                checkBoxPreferredMobile.Enabled = true;
            }
        }

        private void textBoxEmailDomain_TextChanged(object sender, EventArgs e)
        {
            symbolsList.Remove("-");
            symbolsList.Remove(".");

            CheckInput(symbolsList, textBoxEmailDomain, labelEmailErrorMessage);
        }

        private void textBoxEmailDomain_Leave(object sender, EventArgs e)
        {
            if (!symbolsList.Contains("-") && !symbolsList.Contains("."))
            {
                symbolsList.Add("-");
                symbolsList.Add(".");
            }
        }

        private void checkBoxPreferredEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPreferredEmail.Checked == true)
            {
                checkBoxPreferredMobile.Enabled = false;
                checkBoxPreferredLandline.Enabled = false;
                checkBoxPreferredRoyalMail.Enabled = false;
            }
            else
            {
                checkBoxPreferredMobile.Enabled = true;
                checkBoxPreferredLandline.Enabled = true;
                checkBoxPreferredRoyalMail.Enabled = true;
            }
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            CheckUsernameFormat();
        }

        //The labels that indicate whether the password is correct only become visible when the cursor enters the text box.
        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            labelPasswordLengthErrorMessage.Visible = true;
            labelPasswordLowerCaseErrorMessage.Visible = true;
            labelPasswordUpperCaseErrorMessage.Visible = true;
            labelPasswordNumberErrorMessage.Visible = true;
        }

        //As soon as the user starts typing, a real-time password validity check is performed.
        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                labelPasswordLengthErrorMessage.Enabled = false;
                labelPasswordLowerCaseErrorMessage.Enabled = false;
                labelPasswordUpperCaseErrorMessage.Enabled = false;
                labelPasswordNumberErrorMessage.Enabled = false;
            }
            else
            {
                labelPasswordLengthErrorMessage.Enabled = true;
                labelPasswordLowerCaseErrorMessage.Enabled = true;
                labelPasswordUpperCaseErrorMessage.Enabled = true;
                labelPasswordNumberErrorMessage.Enabled = true;
            }
            CheckPasswordFormat(textBoxPassword);

            if (!string.IsNullOrEmpty(textBoxConfirmPassword.Text))     //Checks if the Confirm Password field is already filled. If the user has already entered a password in the Password and Confirm Password fields and later decides
            {                                                           //to change his password, the message of the Confirm Password field has to change, since they no longer match.
                labelConfirmPasswordErrorMessage.ForeColor = yellow;
                CheckPasswordConfirmation(); 
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                labelPasswordLengthErrorMessage.Enabled = false;        //If the user erases all text and the control also loses focus, the field's error message labels disappear.
                labelPasswordLowerCaseErrorMessage.Enabled = false;
                labelPasswordUpperCaseErrorMessage.Enabled = false;
                labelPasswordNumberErrorMessage.Enabled = false;
                labelPasswordLengthErrorMessage.Visible = false;
                labelPasswordLowerCaseErrorMessage.Visible = false;
                labelPasswordUpperCaseErrorMessage.Visible = false;
                labelPasswordNumberErrorMessage.Visible = false;
            }
        }

        private void checkBoxRevealPasswordInput_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxRevealPasswordInput.Checked)
                textBoxPassword.UseSystemPasswordChar = false;
            else
                textBoxPassword.UseSystemPasswordChar = true;

            textBoxPassword.Focus();
        }

        private void textBoxConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            CheckPasswordConfirmation();
        }

        private void checkBoxRevealConfirmPasswordInput_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxRevealConfirmPasswordInput.Checked)
                textBoxConfirmPassword.UseSystemPasswordChar = false;
            else
                textBoxConfirmPassword.UseSystemPasswordChar = true;

            textBoxConfirmPassword.Focus();
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            ProcessData();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Form2 previousForm = new Form2();
            this.Hide();
            previousForm.ShowDialog();
            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessData();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 previousForm = new Form2();
            this.Hide();
            previousForm.ShowDialog();
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Helper Methods

        private void InitializeProgram()
        {
            StreamReader streamReader = new StreamReader("outcodes.txt");       //Loads all UK ZIP codes from an external file.
            string streamContents;

            try
            {
                streamContents = streamReader.ReadLine();

                while (streamContents != null)
                {
                    comboBoxOutcode.Items.Add(streamContents);
                    streamContents = streamReader.ReadLine();
                }
            }
            finally
            {
                if (streamReader != null)
                    streamReader.Close();
            }

            streamReader = new StreamReader("incodes.txt");                     //As above.

            try
            {
                streamContents = streamReader.ReadLine();

                while (streamContents != null)
                {
                    comboBoxIncode.Items.Add(streamContents);
                    streamContents = streamReader.ReadLine();
                }
            }
            finally
            {
                if (streamReader != null)
                    streamReader.Close();
            }

            streamReader = new StreamReader("countries list.txt");

            try
            {
                streamContents = streamReader.ReadLine();

                while (streamContents != null)
                {
                    comboBoxCountryOfBirth.Items.Add(streamContents);
                    streamContents = streamReader.ReadLine();
                }
            }
            finally
            {
                if (streamReader != null)
                    streamReader.Close();
            }

            comboBoxYearOfBirth.Items.Add(" ");                         //Populates the combo box with values from 1920 to the present year.
            for (int i = 1920; i <= DateTime.Now.Year; i += 1)
                comboBoxYearOfBirth.Items.Add(i);

            comboBoxYearsResidentInUK.Items.Add(" ");                   //Populates the combo box with values.
            for (int i = 1; i <= 130; i += 1)
                comboBoxYearsResidentInUK.Items.Add(i);

            trackBar1.Value = 0;

            this.BackColor = blue;
            this.ForeColor = yellow;

            foreach (Control control in this.Controls)
            {
                control.ForeColor = yellow;
                control.BackColor = blue;
            }
            exitToolStripMenuItem.BackColor = blue;
            processToolStripMenuItem.BackColor = blue;
            resetToolStripMenuItem.BackColor = blue;
            cancelToolStripMenuItem.BackColor = blue;
            exitToolStripMenuItem.ForeColor = yellow;
            processToolStripMenuItem.ForeColor = yellow;
            resetToolStripMenuItem.ForeColor = yellow;
            cancelToolStripMenuItem.ForeColor = yellow;

            comboBoxYearsResidentInUK.SelectedIndex = 0;
            comboBoxDayOfBirth.SelectedIndex = 0;
            comboBoxMonthOfBirth.SelectedIndex = 0;
            comboBoxYearOfBirth.SelectedIndex = 0;
            comboBoxYearsResidentInUK.SelectedIndex = 0;

            this.ActiveControl = textBoxFullName;
        }

        //Checks if the inputted date of birth is in the future.
        void CheckDateOfBirthValidity()
        {
            if (comboBoxDayOfBirth.SelectedIndex >= 0 && comboBoxMonthOfBirth.SelectedIndex >= 0 && comboBoxYearOfBirth.SelectedIndex > 0)
            {
                try
                {
                    DateTime currentDate = DateTime.Today;
                    DateTime inputtedDateOfBirth = new DateTime((int)comboBoxYearOfBirth.SelectedItem, comboBoxMonthOfBirth.SelectedIndex, comboBoxDayOfBirth.SelectedIndex);

                    if (inputtedDateOfBirth > currentDate)
                        labelDateOfBirthErrorMessage.Visible = true;
                    else
                        labelDateOfBirthErrorMessage.Visible = false;
                }
                catch
                {
                    labelDateOfBirthErrorMessage.Visible = true;
                }
            }
            else
                labelDateOfBirthErrorMessage.Visible = false;
        }

        //Checks if the inputted UK landline number is correct.
        private void CheckLandlineNumberFormat(List<string> charactersToCheck, List<string> moreCharactersToCheck, Control controlName)
        {
            int parsedCharacters = textBoxLandlineNumber.Text.Count(input => int.TryParse(input.ToString(), out int parsedInput));

            if (CheckInput(charactersToCheck, moreCharactersToCheck, controlName) || parsedCharacters > 11)             //Checks if the text box contains more than 11 digits or something other than numbers.
            {
                if (parsedCharacters > 11)
                {
                    labelLandlineErrorMessage.Text = "Please use the correct UK phone number format (maximum 11 digits)";
                    labelLandlineErrorMessage.Visible = true;
                }
                else
                    labelLandlineErrorMessage.Text = "Please enter only numbers!";

                labelLandlineErrorMessage.Visible = true;
            }
            else
                labelLandlineErrorMessage.Visible = false;
        }

        //Checks if the inputted UK ZIP code outcode part is correct.
        private void CheckOutcodeFormat(Control controlName)
        {
            for (int i = 0; i < numbersList.Count; i += 1)
            {
                if (controlName.Text.StartsWith(numbersList[i]) || CheckInput(symbolsList, controlName))        //Checks if the outcode starts with a number or symbol.
                {
                    labelPostcodeErrorMessage.Visible = true;
                    break;
                }
                else
                    labelPostcodeErrorMessage.Visible = false;
            }
        }

        //Checks if the inputted UK ZIP code incode part is correct.
        private void CheckIncodeFormat(Control controlName)
        {
            for (int i = 0; i < lettersList.Count; i += 1)
            {
                if (controlName.Text.StartsWith(lettersList[i]) || CheckInput(symbolsList, controlName) || controlName.Text.Length > 3)     //Checks if the incode starts with a letter or symbol or is more than 3 characters long.
                {
                    labelPostcodeErrorMessage.Visible = true;
                    break;
                }
                else
                    labelPostcodeErrorMessage.Visible = false;
            }
        }

        //Checks if the username contains spaces or has already been registered.
        private void CheckUsernameFormat()
        {
            for (int i = 0; i <= textBoxUsername.Text.Length; i += 1)
            {
                if (textBoxUsername.Text.Contains(" "))
                {
                    labelUsernameErrorMessage.Text = "Please do not use spaces!";
                    labelUsernameErrorMessage.Visible = true;
                    break;
                }
                else
                    labelUsernameErrorMessage.Visible = false;

                string[] dummyUsernamesArray = new string[4] { "edwin34", "mike1976", "AmandaP", "AGFerg" };

                if (dummyUsernamesArray.Contains(textBoxUsername.Text))
                {
                    labelUsernameErrorMessage.Text = "This username is already in use!";
                    labelUsernameErrorMessage.Visible = true;
                }
                else
                    labelUsernameErrorMessage.Visible = false;
            }
        }

        //Passwords must fulfill 4 conditions (at least: 8 letters long, containing one upper and one lower case character and one number). When fulfilled, the respective text becomes green, otherwise it is red.
        private void CheckPasswordFormat(Control controlName)
        {
            if (controlName.Text.Length > 7)
                labelPasswordLengthErrorMessage.ForeColor = green;
            else
                labelPasswordLengthErrorMessage.ForeColor = red;

            for (int i = 0; i <= 25; i += 1)                                    //The first 25 letters of the list are upper case letters. [Should have created two lists]
            {
                if (controlName.Text.IndexOf(lettersList[i]) >= 0)              //Checks for upper case letters.
                {
                    labelPasswordUpperCaseErrorMessage.ForeColor = green;
                    break;
                }
                else
                    labelPasswordUpperCaseErrorMessage.ForeColor = red;
            }

            for (int i = 26; i < lettersList.Count; i += 1)
            {
                if (controlName.Text.IndexOf(lettersList[i]) >= 0)              //Checks for lower case letters.
                {
                    labelPasswordLowerCaseErrorMessage.ForeColor = green;
                    break;
                }
                else
                    labelPasswordLowerCaseErrorMessage.ForeColor = red;
            }

            if (CheckInput(numbersList, controlName))                           //Checks for numbers.
                labelPasswordNumberErrorMessage.ForeColor = green;
            else
                labelPasswordNumberErrorMessage.ForeColor = red;
        }

        //Checks if the password entered in the Confirm Password text box matches the original.
        private void CheckPasswordConfirmation()
        {
            if (!string.IsNullOrWhiteSpace(textBoxConfirmPassword.Text))
            { 
                if (labelPasswordLengthErrorMessage.ForeColor != green || labelPasswordUpperCaseErrorMessage.ForeColor != green || labelPasswordLowerCaseErrorMessage.ForeColor != green ||
                    labelPasswordNumberErrorMessage.ForeColor != green)
                {
                    labelConfirmPasswordErrorMessage.Text = "Please enter a valid password first!";
                    labelConfirmPasswordErrorMessage.Visible = true;
                }
                else
                {
                    labelConfirmPasswordErrorMessage.ForeColor = yellow;
                    labelConfirmPasswordErrorMessage.Visible = false;
                }

                if (labelPasswordLengthErrorMessage.ForeColor == green && labelPasswordUpperCaseErrorMessage.ForeColor == green && labelPasswordLowerCaseErrorMessage.ForeColor == green &&
                    labelPasswordNumberErrorMessage.ForeColor == green)
                {
                    labelConfirmPasswordErrorMessage.Visible = false;                   //In case the error message was already displayed, because the user had not entered a correct password. (If the first nested "if" was true)
                    labelConfirmPasswordErrorMessage.Text = "The passwords match";

                    if (textBoxConfirmPassword.Text != textBoxPassword.Text)
                    {
                        labelConfirmPasswordErrorMessage.ForeColor = red;
                        labelConfirmPasswordErrorMessage.Visible = true;
                    }
                    else
                    {
                        labelConfirmPasswordErrorMessage.ForeColor = green;             //If the passwords match, the message is displayed in green.
                        labelConfirmPasswordErrorMessage.Visible = true;
                    }
                }
            }
            else
                labelConfirmPasswordErrorMessage.Visible = false;
        }

        //Checks input validity and if needed, displays a label.
        private void CheckInput(List<string> charactersToCheck, Control controlName, Label labelName)
        {
            for (int i = 0; i < charactersToCheck.Count; i += 1)
            {
                if (controlName.Text.IndexOf(charactersToCheck[i]) >= 0)
                {
                    labelName.Visible = true;
                    break;
                }
                else
                    labelName.Visible = false;
            }
        }

        //Checks input validity and returns true, if the text is invalid.
        private bool CheckInput(List<string> charactersToCheck, Control controlName)
        {
            bool containsWrongValue = false;

            for (int i = 0; i < charactersToCheck.Count; i += 1)
            {
                if (controlName.Text.IndexOf(charactersToCheck[i]) >= 0)
                {
                    containsWrongValue = true;
                    break;
                }
            }
            return containsWrongValue;
        }

        //Checks input validity and: Displays a label and returns true, if the text is invalid.
        private void CheckInput(List<string> charactersToCheck, List<string> moreCharactersToCheck, Control controlName, Label labelName)
        {
            for (int i = 0; i < charactersToCheck.Count; i += 1)
            {
                if (controlName.Text.IndexOf(charactersToCheck[i]) >= 0)
                {
                    labelName.Visible = true;
                    break;
                }
                else
                    labelName.Visible = false;
            }

            if (!labelName.Visible)
            {
                for (int i = 0; i < moreCharactersToCheck.Count; i += 1)
                {
                    if (controlName.Text.IndexOf(moreCharactersToCheck[i]) >= 0)
                    {
                        labelName.Visible = true;
                        break;
                    }
                    else
                        labelName.Visible = false;
                }
            }
        }

        //Checks input validity and returns true, if the text is invalid.
        private bool CheckInput(List<string> charactersToCheck, List<string> moreCharactersToCheck, Control controlName)
        {
            bool containsWrongValue = false;

            for (int i = 0; i < charactersToCheck.Count; i += 1)
            {
                if (controlName.Text.IndexOf(charactersToCheck[i]) >= 0)
                {
                    containsWrongValue = true;
                    break;
                }
            }

            if (!containsWrongValue)
            {
                for (int i = 0; i < moreCharactersToCheck.Count; i += 1)
                {
                    if (controlName.Text.IndexOf(moreCharactersToCheck[i]) >= 0)
                    {
                        containsWrongValue = true;
                        break;
                    }
                }
            }
            return containsWrongValue;
        }
        
        //Tool strip menu button for resetting the fields. Used when the user has filled all the fields correctly and pressed the Process button, so that he can enter new data.
        private void ResetFields()
        {
            List<TextBox> textBoxList = new List<TextBox> { textBoxFullName, textBoxPassport, textBoxMobileNumber, textBoxLandlineNumber, textBoxStreetName,
                                                            textBoxEmailLocalPart, textBoxEmailDomain, textBoxUsername, textBoxPassword, textBoxConfirmPassword, textBoxComments};
            List<CheckBox> checkBoxList = new List<CheckBox> { checkBoxPreferredMobile, checkBoxPreferredLandline, checkBoxPreferredRoyalMail, checkBoxPreferredEmail, checkBoxRevealPasswordInput,
                                                               checkBoxRevealConfirmPasswordInput };
            List<Control> resetColourList = new List<Control> {labelFullName, labelDateOfBirth, labelCountryOfBirth, labelYearsResidentInUK, labelPassportNumber, labelMobileNumber, labelLandlineNumber,
                                                               labelHouseNumber, labelStreetName, labelPostcode, labelEmailAddress, labelUsername, labelPassword, labelConfirmPassword, checkBoxPreferredMobile,
                                                               checkBoxPreferredLandline, checkBoxPreferredRoyalMail, checkBoxPreferredEmail};
            List<ComboBox> comboBoxList = new List<ComboBox> { comboBoxDayOfBirth, comboBoxMonthOfBirth, comboBoxYearOfBirth, comboBoxCountryOfBirth, comboBoxYearsResidentInUK, comboBoxOutcode,
                                                               comboBoxIncode, comboBoxHouseNumber};

            foreach (TextBox textBox in textBoxList)
            {
                textBox.Text = "";
                textBox.Enabled = true;
            }
            foreach (CheckBox checkBox in checkBoxList)
            {
                checkBox.Checked = false;
                checkBox.Enabled = true;
            }
            foreach (Control control in resetColourList)
                control.ForeColor = yellow;

            foreach (ComboBox comboBox in comboBoxList)
            {
                comboBox.Enabled = true;
                comboBox.SelectedIndex = 0;
            }

            trackBar1.Enabled = true;
            trackBar1.Value = 0;
            labelHouseNumberDisplay.Text = "0";
            labelProcessingResult.Text = "Your personal info has been registered!";
            labelProcessingResult.Visible = false;
            labelPasswordLengthErrorMessage.Visible = false;
            labelPasswordLowerCaseErrorMessage.Visible = false;
            labelPasswordNumberErrorMessage.Visible = false;
            labelPasswordUpperCaseErrorMessage.Visible = false;
            labelDateOfBirthErrorMessage.Visible = false;
            labelCountryOfBirthErrorMessage.Visible = false;
            textBoxFullName.Focus();
            buttonProcess.Enabled = true;
            buttonCancel.Enabled = true;
            processToolStripMenuItem.Enabled = true;
            cancelToolStripMenuItem.Enabled = true;
        }

        //Prepares the data (trims it) for the validation Scheck.
        private void ProcessData()
        {
            textBoxFullName.Text = textBoxFullName.Text.Trim();
            textBoxPassport.Text = textBoxPassport.Text.Trim();
            textBoxMobileNumber.Text = textBoxMobileNumber.Text.Trim();
            textBoxLandlineNumber.Text = textBoxLandlineNumber.Text.Trim();
            textBoxStreetName.Text = textBoxStreetName.Text.Trim();
            textBoxEmailLocalPart.Text = textBoxEmailLocalPart.Text.Trim();
            textBoxUsername.Text = textBoxUsername.Text.Trim();
            comboBoxCountryOfBirth.Text = comboBoxCountryOfBirth.Text.Trim();
            comboBoxOutcode.Text = comboBoxOutcode.Text.Trim();
            comboBoxIncode.Text = comboBoxIncode.Text.Trim();

            DataValidityCheckForProcessButton();
        }

        //Checks every field's validity, after the user has pressed the process button. If there are wrong values, their label becomes red (e.g. "Full name") and an error message is also displayed.
        //(The user could have pressed the Process without having filled all fields or having done so correctly)
        private void DataValidityCheckForProcessButton()
        {
            List<CheckBox> checkBoxList = new List<CheckBox> { checkBoxPreferredMobile, checkBoxPreferredLandline, checkBoxPreferredRoyalMail, checkBoxPreferredEmail, checkBoxRevealPasswordInput,
                                                               checkBoxRevealConfirmPasswordInput};
            List<TextBox> textBoxList = new List<TextBox> { textBoxFullName, textBoxPassport, textBoxMobileNumber, textBoxLandlineNumber, textBoxStreetName, textBoxEmailLocalPart, textBoxEmailDomain,
                                                            textBoxUsername, textBoxPassword, textBoxConfirmPassword, textBoxComments};
            List<ComboBox> comboBoxList = new List<ComboBox> { comboBoxDayOfBirth, comboBoxMonthOfBirth, comboBoxYearOfBirth, comboBoxCountryOfBirth, comboBoxYearsResidentInUK, comboBoxOutcode,
                                                               comboBoxIncode, comboBoxHouseNumber };

            if (string.IsNullOrEmpty(textBoxFullName.Text) || labelFullNameErrorMessage.Visible == true)
                labelFullName.ForeColor = red;
            else
                labelFullName.ForeColor = yellow;

            if (comboBoxDayOfBirth.SelectedIndex == 0 || comboBoxMonthOfBirth.SelectedIndex == 0 || comboBoxYearOfBirth.SelectedIndex == 0 || labelDateOfBirthErrorMessage.Visible == true)
                labelDateOfBirth.ForeColor = red;
            else
                labelDateOfBirth.ForeColor = yellow;

            if (string.IsNullOrEmpty(comboBoxCountryOfBirth.Text) || labelCountryOfBirthErrorMessage.Visible == true)
                labelCountryOfBirth.ForeColor = red;
            else
                labelCountryOfBirth.ForeColor = yellow;

            if (comboBoxYearsResidentInUK.SelectedIndex == 0)
                labelYearsResidentInUK.ForeColor = red;
            else
                labelYearsResidentInUK.ForeColor = yellow;

            if (string.IsNullOrEmpty(textBoxPassport.Text) || labelPassportErrorMessage.Visible == true)
                labelPassportNumber.ForeColor = red;
            else
                labelPassportNumber.ForeColor = yellow;

            if (string.IsNullOrEmpty(textBoxMobileNumber.Text) || labelMobileNmbErrorMessage.Visible == true)
                labelMobileNumber.ForeColor = red;
            else
                labelMobileNumber.ForeColor = yellow;

            if (string.IsNullOrEmpty(textBoxLandlineNumber.Text) || labelLandlineErrorMessage.Visible == true)
                labelLandlineNumber.ForeColor = red;
            else
                labelLandlineNumber.ForeColor = yellow;

            if (trackBar1.Value == 0)
                labelHouseNumber.ForeColor = red;
            else
                labelHouseNumber.ForeColor = yellow;

            if (string.IsNullOrEmpty(textBoxStreetName.Text) || labelStreetNameErrorMessage.Visible == true)
                labelStreetName.ForeColor = red;
            else
                labelStreetName.ForeColor = yellow;

            if (string.IsNullOrEmpty(comboBoxOutcode.Text) || string.IsNullOrEmpty(comboBoxIncode.Text) || labelPostcodeErrorMessage.Visible == true)
                labelPostcode.ForeColor = red;
            else
                labelPostcode.ForeColor = yellow;

            if (string.IsNullOrEmpty(textBoxEmailLocalPart.Text) || string.IsNullOrEmpty(textBoxEmailDomain.Text) || labelEmailErrorMessage.Visible == true)
                labelEmailAddress.ForeColor = red;
            else
                labelEmailAddress.ForeColor = yellow;

            if (string.IsNullOrEmpty(textBoxUsername.Text) || labelUsernameErrorMessage.Visible == true)
                labelUsername.ForeColor = red;
            else
                labelUsername.ForeColor = yellow;

            if (string.IsNullOrEmpty(textBoxPassword.Text) || labelPasswordLengthErrorMessage.ForeColor == red || labelPasswordLowerCaseErrorMessage.ForeColor == red
                                                           || labelPasswordNumberErrorMessage.ForeColor == red || labelPasswordUpperCaseErrorMessage.ForeColor == red)
                labelPassword.ForeColor = red;
            else
                labelPassword.ForeColor = yellow;

            if (string.IsNullOrEmpty(textBoxConfirmPassword.Text) || labelConfirmPasswordErrorMessage.ForeColor != green)
                labelConfirmPassword.ForeColor = red;
            else
                labelConfirmPassword.ForeColor = yellow;

            if (checkBoxPreferredMobile.Checked == false && checkBoxPreferredLandline.Checked == false && checkBoxPreferredRoyalMail.Checked == false && checkBoxPreferredEmail.Checked == false)
            {
                checkBoxPreferredMobile.ForeColor = red;
                checkBoxPreferredLandline.ForeColor = red;
                checkBoxPreferredRoyalMail.ForeColor = red;
                checkBoxPreferredEmail.ForeColor = red;
            }
            else
            {
                checkBoxPreferredMobile.ForeColor = yellow;
                checkBoxPreferredLandline.ForeColor = yellow;
                checkBoxPreferredRoyalMail.ForeColor = yellow;
                checkBoxPreferredEmail.ForeColor = yellow;
            }

            bool usernameAndPasswordCorrect = labelUsername.ForeColor == yellow && labelPassword.ForeColor == yellow && labelConfirmPassword.ForeColor == yellow;
            bool restFieldsCorrect = labelFullName.ForeColor == yellow && labelDateOfBirth.ForeColor == yellow && labelCountryOfBirth.ForeColor == yellow &&
                                 labelYearsResidentInUK.ForeColor == yellow && labelPassportNumber.ForeColor == yellow && labelMobileNumber.ForeColor == yellow &&
                                 labelLandlineNumber.ForeColor == yellow && labelHouseNumber.ForeColor == yellow && labelStreetName.ForeColor == yellow &&
                                 labelPostcode.ForeColor == yellow && labelEmailAddress.ForeColor == yellow && checkBoxPreferredEmail.ForeColor == yellow
                                 && checkBoxPreferredLandline.ForeColor == yellow && checkBoxPreferredMobile.ForeColor == yellow && checkBoxPreferredRoyalMail.ForeColor == yellow;

            if (usernameAndPasswordCorrect && restFieldsCorrect)
            {
                labelProcessingResult.Text = "Your personal info has been registered!";
                labelProcessingResult.Visible = true;
                buttonProcess.Enabled = false;
                buttonCancel.Enabled = false;
                processToolStripMenuItem.Enabled = false;
                cancelToolStripMenuItem.Enabled = false;
                foreach (TextBox textBox in textBoxList)
                    textBox.Enabled = false;
                foreach (ComboBox comboBox in comboBoxList)
                    comboBox.Enabled = false;
                foreach (CheckBox checkBox in checkBoxList)
                    checkBox.Enabled = false;
                trackBar1.Enabled = false;
                System.Media.SoundPlayer playSound = new System.Media.SoundPlayer("Success sound.wav");
                playSound.Play();
            }
            if (labelUsername.ForeColor == red)
            {
                labelProcessingResult.Text = "Please enter a correct username and press 'Process' again, in order to be able to access your account";
                labelProcessingResult.Visible = true;
            }
            if (labelPassword.ForeColor == red)
            {
                if (labelPassword.ForeColor == red)
                {
                    labelProcessingResult.Text = "Please enter a correct password and press 'Process' again, in order to be able to access your account";
                    labelProcessingResult.Visible = true;
                }
            }
            if (labelPassword.ForeColor == yellow && labelConfirmPassword.ForeColor == red)  //Only checks if the Confirm password field is correct, if the Password field is also correct in the first place.
            {
                labelProcessingResult.Text = "Please make sure you have confirmed your password and press 'Process' again, in order to be able to access your account";
                labelProcessingResult.Visible = true;
            }
            if (!restFieldsCorrect && usernameAndPasswordCorrect)       //The processing is completed normally, though with a warning message, if only the Username and Password fields are correct.
            {
                labelProcessingResult.Text = "Your personal info has been registered! In order to be able to enjoy our full range of services, make sure to fill the rest of your personal data " +
                                             "by accessing your account settings!";
                labelProcessingResult.Visible = true;
                buttonProcess.Enabled = false;
                buttonCancel.Enabled = false;
                processToolStripMenuItem.Enabled = false;
                cancelToolStripMenuItem.Enabled = false;
                foreach (TextBox textBox in textBoxList)
                    textBox.Enabled = false;
                foreach (ComboBox comboBox in comboBoxList)
                    comboBox.Enabled = false;
                foreach (CheckBox checkBox in checkBoxList)
                    checkBox.Enabled = false;
                trackBar1.Enabled = false;
                System.Media.SoundPlayer playSound = new System.Media.SoundPlayer("Success sound.wav");
                playSound.Play();
            }
        }
    }
}
