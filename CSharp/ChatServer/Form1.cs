using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ChatsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.chatsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.databaseDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Diese Codezeile lädt Daten in die Tabelle "databaseDataSet.Messages". Sie können sie bei Bedarf verschieben oder entfernen.
            this.messagesTableAdapter.Fill(this.databaseDataSet.Messages);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "databaseDataSet.Users". Sie können sie bei Bedarf verschieben oder entfernen.
            this.usersTableAdapter.Fill(this.databaseDataSet.Users);
            // TODO: Diese Codezeile lädt Daten in die Tabelle "databaseDataSet.Chats". Sie können sie bei Bedarf verschieben oder entfernen.
            this.chatsTableAdapter.Fill(this.databaseDataSet.Chats);

        }
    }
}
