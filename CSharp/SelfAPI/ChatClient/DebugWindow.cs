using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient {
    public partial class DebugWindow : Form {
        private static readonly TextWriter Original = Console.Out;

        public DebugWindow() {
            InitializeComponent();

            TextBoxWriter writer = new TextBoxWriter( this.richTextBox1, this );
            Console.SetOut( writer );
        }

        private void DebugWindow_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            Hide();
        }

        private void PullToolStripMenuItem_Click(object sender, EventArgs e) { Console.WriteLine( "Up To Date: " + DateTime.Now ); }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e) { this.richTextBox1.Text = ""; }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e) {
            if ( this.fontDialog1.ShowDialog( this ) != DialogResult.OK ) return;

            this.Font             = this.fontDialog1.Font;
            this.fontDialog1.Font = this.fontDialog1.Font;
            this.menuStrip1.Font  = this.fontDialog1.Font;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) { Environment.Exit( 0 ); }

        private void FontToolStripMenuItem1_Click(object sender, EventArgs e) {
            if ( this.colorDialog1.ShowDialog( this ) != DialogResult.OK ) return;

            this.ForeColor              = this.colorDialog1.Color;
            this.richTextBox1.ForeColor = this.colorDialog1.Color;
            this.menuStrip1.ForeColor   = this.colorDialog1.Color;
        }

        private void BackToolStripMenuItem_Click(object sender, EventArgs e) {
            if ( this.colorDialog2.ShowDialog( this ) != DialogResult.OK ) return;

            this.BackColor              = this.colorDialog2.Color;
            this.richTextBox1.BackColor = this.colorDialog2.Color;
            this.menuStrip1.BackColor   = this.colorDialog2.Color;
        }
    }

    public class TextBoxWriter : TextWriter {
        // The control where we will write text.
        private readonly RichTextBox _textBox;
        private readonly Form        _owner;
        private          string      text;

        public TextBoxWriter(RichTextBox control, Form owner) {
            this._textBox = control;
            this._owner   = owner;
        }

        public override void Write(char value) { Write( value.ToString() ); }

        public override void Write(string value) {
            this.text += value;

            if ( this._owner.Visible ) {
                this._owner.Invoke( new Action( () => {
                    this._textBox.AppendText( this.text );
                } ) );
                this.text = "";
            }
        }

        public override Encoding Encoding => Encoding.Unicode;
    }
}
