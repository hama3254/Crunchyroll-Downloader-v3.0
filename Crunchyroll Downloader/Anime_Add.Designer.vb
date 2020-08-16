<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Anime_Add
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.textBox2 = New System.Windows.Forms.TextBox()
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.pictureBox3 = New System.Windows.Forms.PictureBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Add_Display = New System.Windows.Forms.Label()
        Me.comboBox4 = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.comboBox3 = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.groupBox1.SuspendLayout()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox1
        '
        Me.groupBox1.BackColor = System.Drawing.Color.Transparent
        Me.groupBox1.Controls.Add(Me.StatusLabel)
        Me.groupBox1.Controls.Add(Me.ComboBox2)
        Me.groupBox1.Controls.Add(Me.TextBox4)
        Me.groupBox1.Controls.Add(Me.textBox1)
        Me.groupBox1.Controls.Add(Me.textBox2)
        Me.groupBox1.Location = New System.Drawing.Point(5, 45)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(620, 162)
        Me.groupBox1.TabIndex = 33
        Me.groupBox1.TabStop = False
        '
        'StatusLabel
        '
        Me.StatusLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusLabel.BackColor = System.Drawing.Color.Transparent
        Me.StatusLabel.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusLabel.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.StatusLabel.Location = New System.Drawing.Point(18, 127)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(593, 29)
        Me.StatusLabel.TabIndex = 38
        Me.StatusLabel.Text = "Status: idle"
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBox2
        '
        Me.ComboBox2.BackColor = System.Drawing.Color.White
        Me.ComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(18, 98)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(585, 23)
        Me.ComboBox2.Sorted = True
        Me.ComboBox2.TabIndex = 37
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TextBox4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBox4.Location = New System.Drawing.Point(18, 70)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(585, 22)
        Me.TextBox4.TabIndex = 36
        Me.TextBox4.TabStop = False
        Me.TextBox4.Text = "Main Directory"
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'textBox1
        '
        Me.textBox1.BackColor = System.Drawing.Color.White
        Me.textBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.textBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.textBox1.Location = New System.Drawing.Point(18, 14)
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(585, 22)
        Me.textBox1.TabIndex = 4
        Me.textBox1.TabStop = False
        Me.textBox1.Text = "URL"
        Me.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'textBox2
        '
        Me.textBox2.BackColor = System.Drawing.Color.White
        Me.textBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.textBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.textBox2.Location = New System.Drawing.Point(18, 42)
        Me.textBox2.Name = "textBox2"
        Me.textBox2.Size = New System.Drawing.Size(585, 22)
        Me.textBox2.TabIndex = 5
        Me.textBox2.TabStop = False
        Me.textBox2.Text = "Name of the Anime"
        Me.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pictureBox4
        '
        Me.pictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox4.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_button_download_default
        Me.pictureBox4.Location = New System.Drawing.Point(44, 213)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(537, 50)
        Me.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox4.TabIndex = 42
        Me.pictureBox4.TabStop = False
        '
        'pictureBox3
        '
        Me.pictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox3.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.pictureBox3.Location = New System.Drawing.Point(579, 1)
        Me.pictureBox3.Name = "pictureBox3"
        Me.pictureBox3.Size = New System.Drawing.Size(50, 40)
        Me.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox3.TabIndex = 43
        Me.pictureBox3.TabStop = False
        '
        'groupBox2
        '
        Me.groupBox2.BackColor = System.Drawing.Color.Transparent
        Me.groupBox2.Controls.Add(Me.PictureBox1)
        Me.groupBox2.Controls.Add(Me.Add_Display)
        Me.groupBox2.Controls.Add(Me.comboBox4)
        Me.groupBox2.Controls.Add(Me.ComboBox1)
        Me.groupBox2.Controls.Add(Me.comboBox3)
        Me.groupBox2.Location = New System.Drawing.Point(5, 45)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(620, 162)
        Me.groupBox2.TabIndex = 44
        Me.groupBox2.TabStop = False
        Me.groupBox2.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.add_mass_cancel
        Me.PictureBox1.Location = New System.Drawing.Point(113, 117)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(403, 36)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 45
        Me.PictureBox1.TabStop = False
        '
        'Add_Display
        '
        Me.Add_Display.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add_Display.BackColor = System.Drawing.Color.Transparent
        Me.Add_Display.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Add_Display.ForeColor = System.Drawing.Color.Black
        Me.Add_Display.Location = New System.Drawing.Point(20, 114)
        Me.Add_Display.Name = "Add_Display"
        Me.Add_Display.Size = New System.Drawing.Size(591, 38)
        Me.Add_Display.TabIndex = 36
        Me.Add_Display.Text = "..."
        Me.Add_Display.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'comboBox4
        '
        Me.comboBox4.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBox4.FormattingEnabled = True
        Me.comboBox4.Location = New System.Drawing.Point(24, 83)
        Me.comboBox4.Name = "comboBox4"
        Me.comboBox4.Size = New System.Drawing.Size(585, 23)
        Me.comboBox4.TabIndex = 2
        '
        'ComboBox1
        '
        Me.ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(24, 19)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(585, 23)
        Me.ComboBox1.TabIndex = 1
        '
        'comboBox3
        '
        Me.comboBox3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBox3.FormattingEnabled = True
        Me.comboBox3.Location = New System.Drawing.Point(24, 51)
        Me.comboBox3.Name = "comboBox3"
        Me.comboBox3.Size = New System.Drawing.Size(585, 23)
        Me.comboBox3.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.ListBox1)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 45)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(620, 162)
        Me.GroupBox3.TabIndex = 46
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Visible = False
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(3, 16)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(614, 143)
        Me.ListBox1.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 2500
        '
        'Anime_Add
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.add_background
        Me.ClientSize = New System.Drawing.Size(630, 275)
        Me.Controls.Add(Me.pictureBox3)
        Me.Controls.Add(Me.pictureBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.groupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Anime_Add"
        Me.Text = "Add"
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents textBox1 As TextBox
    Public WithEvents ComboBox2 As ComboBox
    Public WithEvents TextBox4 As TextBox
    Public WithEvents textBox2 As TextBox
    Private WithEvents pictureBox3 As PictureBox
    Public WithEvents StatusLabel As Label
    Public WithEvents Add_Display As Label
    Public WithEvents comboBox4 As ComboBox
    Public WithEvents ComboBox1 As ComboBox
    Public WithEvents comboBox3 As ComboBox
    Public WithEvents pictureBox4 As PictureBox
    Public WithEvents groupBox2 As GroupBox
    Public WithEvents PictureBox1 As PictureBox
    Public WithEvents groupBox1 As GroupBox
    Public WithEvents GroupBox3 As GroupBox
    Friend WithEvents Timer1 As Timer
    Private WithEvents Timer2 As Timer
    Public WithEvents ListBox1 As ListBox
End Class
