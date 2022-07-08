<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Anime_Add
    Inherits MetroFramework.Forms.MetroForm


    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Anime_Add))
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.SubTitlesOnlyCB = New MetroFramework.Controls.MetroComboBox()
        Me.StatusLabel = New MetroFramework.Controls.MetroLabel()
        Me.ComboBox2 = New MetroFramework.Controls.MetroComboBox()
        Me.TextBox4 = New MetroFramework.Controls.MetroTextBox()
        Me.textBox1 = New MetroFramework.Controls.MetroTextBox()
        Me.TextBox2 = New MetroFramework.Controls.MetroTextBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.bt_Cancel_mass = New System.Windows.Forms.Button()
        Me.comboBox4 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox1 = New MetroFramework.Controls.MetroComboBox()
        Me.comboBox3 = New MetroFramework.Controls.MetroComboBox()
        Me.Add_Display = New MetroFramework.Controls.MetroLabel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Btn_min = New System.Windows.Forms.PictureBox()
        Me.Btn_Close = New System.Windows.Forms.PictureBox()
        Me.btn_dl = New System.Windows.Forms.Button()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.groupBox1.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.Btn_min, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'groupBox1
        '
        Me.groupBox1.BackColor = System.Drawing.Color.Transparent
        Me.groupBox1.Controls.Add(Me.SubTitlesOnlyCB)
        Me.groupBox1.Controls.Add(Me.StatusLabel)
        Me.groupBox1.Controls.Add(Me.ComboBox2)
        Me.groupBox1.Controls.Add(Me.TextBox4)
        Me.groupBox1.Controls.Add(Me.textBox1)
        Me.groupBox1.Controls.Add(Me.TextBox2)
        Me.groupBox1.Location = New System.Drawing.Point(15, 70)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(720, 280)
        Me.groupBox1.TabIndex = 33
        Me.groupBox1.TabStop = False
        '
        'SubTitlesOnlyCB
        '
        Me.SubTitlesOnlyCB.BackColor = System.Drawing.Color.White
        Me.SubTitlesOnlyCB.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubTitlesOnlyCB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.SubTitlesOnlyCB.FormattingEnabled = True
        Me.SubTitlesOnlyCB.ItemHeight = 23
        Me.SubTitlesOnlyCB.Items.AddRange(New Object() {"[Default]", "[Subtitles only]"})
        Me.SubTitlesOnlyCB.Location = New System.Drawing.Point(18, 190)
        Me.SubTitlesOnlyCB.Name = "SubTitlesOnlyCB"
        Me.SubTitlesOnlyCB.Size = New System.Drawing.Size(693, 29)
        Me.SubTitlesOnlyCB.Sorted = True
        Me.SubTitlesOnlyCB.TabIndex = 39
        Me.SubTitlesOnlyCB.UseSelectable = True
        '
        'StatusLabel
        '
        Me.StatusLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusLabel.BackColor = System.Drawing.Color.Transparent
        Me.StatusLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.StatusLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.StatusLabel.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.StatusLabel.Location = New System.Drawing.Point(18, 228)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(693, 46)
        Me.StatusLabel.TabIndex = 38
        Me.StatusLabel.Text = "Status: idle"
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBox2
        '
        Me.ComboBox2.BackColor = System.Drawing.Color.White
        Me.ComboBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.ItemHeight = 23
        Me.ComboBox2.Location = New System.Drawing.Point(18, 148)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(693, 29)
        Me.ComboBox2.Sorted = True
        Me.ComboBox2.TabIndex = 37
        Me.ComboBox2.UseSelectable = True
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.Cursor = System.Windows.Forms.Cursors.Hand
        '
        '
        '
        Me.TextBox4.CustomButton.Image = Nothing
        Me.TextBox4.CustomButton.Location = New System.Drawing.Point(665, 1)
        Me.TextBox4.CustomButton.Name = ""
        Me.TextBox4.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.TextBox4.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.TextBox4.CustomButton.TabIndex = 1
        Me.TextBox4.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TextBox4.CustomButton.UseSelectable = True
        Me.TextBox4.CustomButton.Visible = False
        Me.TextBox4.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.TextBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBox4.Lines = New String() {"Main Directory"}
        Me.TextBox4.Location = New System.Drawing.Point(18, 106)
        Me.TextBox4.MaxLength = 32767
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TextBox4.SelectedText = ""
        Me.TextBox4.SelectionLength = 0
        Me.TextBox4.SelectionStart = 0
        Me.TextBox4.ShortcutsEnabled = True
        Me.TextBox4.Size = New System.Drawing.Size(693, 29)
        Me.TextBox4.TabIndex = 36
        Me.TextBox4.TabStop = False
        Me.TextBox4.Text = "Main Directory"
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextBox4.UseSelectable = True
        Me.TextBox4.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TextBox4.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'textBox1
        '
        Me.textBox1.BackColor = System.Drawing.Color.White
        Me.textBox1.Cursor = System.Windows.Forms.Cursors.Hand
        '
        '
        '
        Me.textBox1.CustomButton.Image = Nothing
        Me.textBox1.CustomButton.Location = New System.Drawing.Point(665, 1)
        Me.textBox1.CustomButton.Name = ""
        Me.textBox1.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.textBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.textBox1.CustomButton.TabIndex = 1
        Me.textBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.textBox1.CustomButton.UseSelectable = True
        Me.textBox1.CustomButton.Visible = False
        Me.textBox1.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.textBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.textBox1.Lines = New String() {"URL"}
        Me.textBox1.Location = New System.Drawing.Point(18, 22)
        Me.textBox1.MaxLength = 32767
        Me.textBox1.Name = "textBox1"
        Me.textBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.textBox1.SelectedText = ""
        Me.textBox1.SelectionLength = 0
        Me.textBox1.SelectionStart = 0
        Me.textBox1.ShortcutsEnabled = True
        Me.textBox1.Size = New System.Drawing.Size(693, 29)
        Me.textBox1.TabIndex = 4
        Me.textBox1.TabStop = False
        Me.textBox1.Text = "URL"
        Me.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.textBox1.UseSelectable = True
        Me.textBox1.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.textBox1.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.Cursor = System.Windows.Forms.Cursors.Hand
        '
        '
        '
        Me.TextBox2.CustomButton.Image = Nothing
        Me.TextBox2.CustomButton.Location = New System.Drawing.Point(665, 1)
        Me.TextBox2.CustomButton.Name = ""
        Me.TextBox2.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.TextBox2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.TextBox2.CustomButton.TabIndex = 1
        Me.TextBox2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TextBox2.CustomButton.UseSelectable = True
        Me.TextBox2.CustomButton.Visible = False
        Me.TextBox2.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.TextBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBox2.Lines = New String() {"Use Custom Name"}
        Me.TextBox2.Location = New System.Drawing.Point(18, 64)
        Me.TextBox2.MaxLength = 32767
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TextBox2.SelectedText = ""
        Me.TextBox2.SelectionLength = 0
        Me.TextBox2.SelectionStart = 0
        Me.TextBox2.ShortcutsEnabled = True
        Me.TextBox2.Size = New System.Drawing.Size(693, 29)
        Me.TextBox2.TabIndex = 5
        Me.TextBox2.TabStop = False
        Me.TextBox2.Text = "Use Custom Name"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextBox2.UseSelectable = True
        Me.TextBox2.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TextBox2.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'groupBox2
        '
        Me.groupBox2.BackColor = System.Drawing.Color.Transparent
        Me.groupBox2.Controls.Add(Me.bt_Cancel_mass)
        Me.groupBox2.Controls.Add(Me.comboBox4)
        Me.groupBox2.Controls.Add(Me.ComboBox1)
        Me.groupBox2.Controls.Add(Me.comboBox3)
        Me.groupBox2.Controls.Add(Me.Add_Display)
        Me.groupBox2.Location = New System.Drawing.Point(15, 70)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(720, 280)
        Me.groupBox2.TabIndex = 44
        Me.groupBox2.TabStop = False
        Me.groupBox2.Visible = False
        '
        'bt_Cancel_mass
        '
        Me.bt_Cancel_mass.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.add_mass_cancel
        Me.bt_Cancel_mass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.bt_Cancel_mass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bt_Cancel_mass.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bt_Cancel_mass.ForeColor = System.Drawing.SystemColors.Control
        Me.bt_Cancel_mass.Location = New System.Drawing.Point(159, 231)
        Me.bt_Cancel_mass.Name = "bt_Cancel_mass"
        Me.bt_Cancel_mass.Size = New System.Drawing.Size(403, 36)
        Me.bt_Cancel_mass.TabIndex = 37
        Me.bt_Cancel_mass.Text = "Cancel"
        Me.bt_Cancel_mass.UseVisualStyleBackColor = True
        '
        'comboBox4
        '
        Me.comboBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBox4.FormattingEnabled = True
        Me.comboBox4.ItemHeight = 23
        Me.comboBox4.Location = New System.Drawing.Point(13, 154)
        Me.comboBox4.Name = "comboBox4"
        Me.comboBox4.Size = New System.Drawing.Size(693, 29)
        Me.comboBox4.TabIndex = 2
        Me.comboBox4.UseSelectable = True
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.ItemHeight = 23
        Me.ComboBox1.Location = New System.Drawing.Point(13, 50)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(693, 29)
        Me.ComboBox1.TabIndex = 1
        Me.ComboBox1.UseSelectable = True
        '
        'comboBox3
        '
        Me.comboBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBox3.FormattingEnabled = True
        Me.comboBox3.ItemHeight = 23
        Me.comboBox3.Location = New System.Drawing.Point(13, 102)
        Me.comboBox3.Name = "comboBox3"
        Me.comboBox3.Size = New System.Drawing.Size(693, 29)
        Me.comboBox3.TabIndex = 1
        Me.comboBox3.UseSelectable = True
        '
        'Add_Display
        '
        Me.Add_Display.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Add_Display.BackColor = System.Drawing.Color.Transparent
        Me.Add_Display.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.Add_Display.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.Add_Display.ForeColor = System.Drawing.Color.Black
        Me.Add_Display.Location = New System.Drawing.Point(20, 228)
        Me.Add_Display.Name = "Add_Display"
        Me.Add_Display.Size = New System.Drawing.Size(691, 42)
        Me.Add_Display.TabIndex = 36
        Me.Add_Display.Text = "..."
        Me.Add_Display.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.ListBox1)
        Me.GroupBox3.Location = New System.Drawing.Point(15, 70)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(720, 280)
        Me.GroupBox3.TabIndex = 46
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Visible = False
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(13, 18)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(693, 244)
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
        'Btn_min
        '
        Me.Btn_min.BackColor = System.Drawing.Color.Transparent
        Me.Btn_min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Btn_min.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_min.Image = CType(resources.GetObject("Btn_min.Image"), System.Drawing.Image)
        Me.Btn_min.Location = New System.Drawing.Point(567, 1)
        Me.Btn_min.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_min.Name = "Btn_min"
        Me.Btn_min.Size = New System.Drawing.Size(25, 25)
        Me.Btn_min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Btn_min.TabIndex = 73
        Me.Btn_min.TabStop = False
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.Btn_Close.Location = New System.Drawing.Point(592, 1)
        Me.Btn_Close.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(40, 40)
        Me.Btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Btn_Close.TabIndex = 72
        Me.Btn_Close.TabStop = False
        '
        'btn_dl
        '
        Me.btn_dl.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.main_button_download_default
        Me.btn_dl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_dl.FlatAppearance.BorderSize = 0
        Me.btn_dl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_dl.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_dl.ForeColor = System.Drawing.SystemColors.Control
        Me.btn_dl.Location = New System.Drawing.Point(106, 377)
        Me.btn_dl.Name = "btn_dl"
        Me.btn_dl.Size = New System.Drawing.Size(538, 50)
        Me.btn_dl.TabIndex = 75
        Me.btn_dl.Text = "Download"
        Me.btn_dl.UseVisualStyleBackColor = True
        '
        'Timer3
        '
        Me.Timer3.Enabled = True
        '
        'Anime_Add
        '
        Me.ApplyImageInvert = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(750, 450)
        Me.Controls.Add(Me.btn_dl)
        Me.Controls.Add(Me.Btn_min)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Anime_Add"
        Me.Padding = New System.Windows.Forms.Padding(10, 60, 20, 20)
        Me.Text = "Add Video"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.Btn_min, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents groupBox2 As GroupBox
    Public WithEvents groupBox1 As GroupBox
    Public WithEvents GroupBox3 As GroupBox
    Friend WithEvents Timer1 As Timer
    Private WithEvents Timer2 As Timer
    Public WithEvents ListBox1 As ListBox
    Public WithEvents StatusLabel As MetroFramework.Controls.MetroLabel
    Public WithEvents Add_Display As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroTextBox1 As MetroFramework.Controls.MetroTextBox
    Public WithEvents textBox1 As MetroFramework.Controls.MetroTextBox
    Public WithEvents TextBox4 As MetroFramework.Controls.MetroTextBox
    Public WithEvents TextBox2 As MetroFramework.Controls.MetroTextBox
    Public WithEvents ComboBox2 As MetroFramework.Controls.MetroComboBox
    Public WithEvents comboBox4 As MetroFramework.Controls.MetroComboBox
    Public WithEvents ComboBox1 As MetroFramework.Controls.MetroComboBox
    Public WithEvents comboBox3 As MetroFramework.Controls.MetroComboBox
    Private WithEvents Btn_min As PictureBox
    Private WithEvents Btn_Close As PictureBox
    Public WithEvents SubTitlesOnlyCB As MetroFramework.Controls.MetroComboBox
    Friend WithEvents btn_dl As Button
    Friend WithEvents Timer3 As Timer
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents bt_Cancel_mass As Button
End Class
