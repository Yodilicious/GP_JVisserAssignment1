/* 
 * ReservationForm.cs
 * 
 * Program that provides a reservation system for airline flight seating.
 * 
 * Revision History
 *     Jodi Visser, 2016.09.25: Created
 */
 

namespace JVisserAssignment1
{
    using System;
    using System.Windows.Forms;

    public partial class ReservationForm : Form
    {
        //Class Constructors
        private string[,] reservations;
        private string[] waitingList;
        private const int ROWS = 5;
        private const int COLUMNS = 3;
        private const int WAITING_LIST_MAX = 10;

        /// <summary>
        /// Initialize fields for the class.
        /// </summary>
        public ReservationForm()
        {
            InitializeComponent();

            reservations = new string[ROWS, COLUMNS];
            waitingList = new string[WAITING_LIST_MAX];

            InitializeGridListBoxes();
        }

        /// <summary>
        /// Initialize grid for the list boxes.
        /// </summary>
        private void InitializeGridListBoxes()
        {
            InitializeListBox(COLUMNS, lstCols);
            InitializeListBox(ROWS, lstRows);
        }

        /// <summary>
        /// Method that adds items to a List box.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="list"></param>
        private void InitializeListBox(int count, ListBox list)
        {
            for (int item = 0; item < count; item++)
            {
                list.Items.Add(item.ToString());
            }
        }

        /// <summary>
        /// Method that when Status button is clicked, checks each row and column 
        /// to determine if seats are available or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatus_Click(object sender, EventArgs e)
        {
            if(ValidateGridListBoxes())
            {
                int row = lstRows.SelectedIndex;
                int col = lstCols.SelectedIndex;

                txtStatus.Text = string.IsNullOrEmpty(reservations[row, col]) ? "Available" : "Not Available";
            }
        }

        /// <summary>
        /// Method that iterates through the columns and rows of the seats, checks if a seat is occupied or not,
        /// if a name has been entered then books the seat. If seats are all booked, the name is added to the waiting list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBook_Click(object sender, EventArgs e)
        {
            if (IsReservationListNotFull())
            {
                if (ValidateGridListBoxes())
                {
                    int row = lstRows.SelectedIndex;
                    int col = lstCols.SelectedIndex;

                    if (string.IsNullOrEmpty(reservations[row, col]))
                    {
                        if (string.IsNullOrEmpty(txtName.Text))
                        {
                            MessageBox.Show("Please input a valid name.");
                        }
                        else
                        {
                            reservations[row, col] = txtName.Text;
                            MessageBox.Show($"Seat [{row}, {col}] is now booked.");
                        }
                    }
                    else
                    {
                        // Show Validation Message Seat already booked
                        MessageBox.Show("Seat is occupied, please choose another.");
                    }
                }
            }
            else
            {
                AddToWaitingList(txtName.Text);
            }
        }

        /// <summary>
        /// Method that returns true if user does not choose a column and row.
        /// </summary>
        /// <returns></returns>
        private bool ValidateGridListBoxes()
        {
            string validationMessage = string.Empty;
                       
            validationMessage += ValidateSelectedListBox(lstRows, "Please select a row!\n");
            validationMessage += ValidateSelectedListBox(lstCols, "Please select a column!\n");
            
            if(string.IsNullOrEmpty(validationMessage))
            {
                return true;
            }

            MessageBox.Show(validationMessage);
            return false;
        }

        /// <summary>
        /// Method that 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        private string ValidateSelectedListBox(ListBox list, string validationMessage)
        {
            if (list.SelectedIndex == -1)
            {
                return validationMessage;
            }

            return string.Empty;
        }

        /// <summary>
        /// Method that cancels a seat and if there are customers on the waiting list the first person 
        /// from the waiting list is moved to the newly cancelled seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ValidateGridListBoxes())
            {
                int row = lstRows.SelectedIndex;
                int col = lstCols.SelectedIndex;

                if (!string.IsNullOrEmpty(reservations[row, col]))
                {
                    if(string.IsNullOrEmpty(waitingList[0]))
                    {
                        reservations[row, col] = string.Empty;
                        MessageBox.Show("Seat is successfully cancelled.");
                    }
                    else
                    {
                        reservations[row, col] = waitingList[0];

                        for (int i = 1; i < waitingList.Length; i++)
                        {
                            waitingList[i - 1] = waitingList[i];
                        }
                        waitingList[waitingList.Length - 1] = string.Empty;
                       
                        MessageBox.Show("Moved the first person from Waiting List to the cancelled seat");
                    }
                }
                else
                {
                    MessageBox.Show("Seat is empty, cannot cancel an empty seat.");
                }
            }
        }
                
        /// <summary>
        /// Method that informs user if seats are still available. If there aren't anymore seats to be reserved, 
        /// customer will be added to the waiting list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToWaitingList_Click(object sender, EventArgs e)
        {
            if(IsReservationListNotFull())
            {
                MessageBox.Show("There are still seats available, use Book to book seats.");
            }
            else
            {
                AddToWaitingList(txtName.Text);
            }
        }

        /// <summary>
        /// Method that adds customers to the waiting list when all the reservations are booked. 
        /// When both are booked, the user will be informed.
        /// </summary>
        /// <param name="name"></param>
        private void AddToWaitingList(string name)
        {
            if (IsWaitListNotFull())
            {
                for (int i = 0; i < waitingList.Length; i++)
                {
                    if (string.IsNullOrEmpty(waitingList[i]))
                    {
                        waitingList[i] = name;
                        break;
                    }
                }

                MessageBox.Show("Successfully added to Waiting List.");
            }
            else
            {
                MessageBox.Show("Waiting List is full and so is Reservation List!");
            }
        }

        /// <summary>
        /// Method that checks if Waiting List is not full. 
        /// </summary>
        /// <returns></returns>
        private bool IsWaitListNotFull()
        {
            for (int i = 0; i < waitingList.Length; i++)
            {
                if (string.IsNullOrEmpty(waitingList[i]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method that checks if the Reservation List is full and updates accordingly
        /// </summary>
        /// <returns></returns>
        private bool IsReservationListNotFull()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    if (string.IsNullOrEmpty(reservations[row, col]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Method that shows the reserved seats when Show All button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            rtxShowAll.Text = string.Empty;

            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    rtxShowAll.AppendText($"[{row}, {col}] -- {reservations[row, col]}\n");
                }
            }
        }


        /// <summary>
        /// Method that shows the seat listing for the Waiting List when button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowWaitingList_Click(object sender, EventArgs e)
        {
            rtxShowWaitingList.Text = string.Empty;

            for (int row = 0; row < waitingList.Length; row++)
            {
                rtxShowWaitingList.AppendText($"[{row}] -- {waitingList[row]}\n");
            }
        }

        /// <summary>
        /// Method that books all the seats when the Fill All button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFillAll_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    reservations[row, col] = RandomNameGenerator.GenerateName();
                }
            }
        }

        private void btnGetName_Click(object sender, EventArgs e)
        {
            txtName.Text = RandomNameGenerator.GenerateName();
        }

        private void lblSecretDebug_Click(object sender, EventArgs e)
        {
            btnGetName.Visible = !btnGetName.Visible;
            btnFillWaitingList.Visible = !btnFillWaitingList.Visible;
        }

        private void btnFillWaitingList_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < waitingList.Length; i++)
            {
                waitingList[i] = RandomNameGenerator.GenerateName();
            }
        }
    }
}
