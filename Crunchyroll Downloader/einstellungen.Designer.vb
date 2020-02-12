<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class einstellungen
    Inherits System.Windows.Forms.Form

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
        Me.GB_Sub_Path = New System.Windows.Forms.GroupBox()
        Me.RBStaffel = New System.Windows.Forms.RadioButton()
        Me.RBAnime = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DL_Count_simultaneous = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Firefox_True = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.FFMPEG_Command = New System.Windows.Forms.ComboBox()
        Me.SoftSubs = New System.Windows.Forms.GroupBox()
        Me.MergeMP4 = New System.Windows.Forms.CheckBox()
        Me.CBesES = New System.Windows.Forms.CheckBox()
        Me.CBitIT = New System.Windows.Forms.CheckBox()
        Me.CBruRU = New System.Windows.Forms.CheckBox()
        Me.CBarME = New System.Windows.Forms.CheckBox()
        Me.CBfrFR = New System.Windows.Forms.CheckBox()
        Me.CBesLA = New System.Windows.Forms.CheckBox()
        Me.CBptBR = New System.Windows.Forms.CheckBox()
        Me.CBdeDE = New System.Windows.Forms.CheckBox()
        Me.CBenUS = New System.Windows.Forms.CheckBox()
        Me.GB_SubLanguage = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.GB_Resolution = New System.Windows.Forms.GroupBox()
        Me.AAuto = New System.Windows.Forms.RadioButton()
        Me.A480p = New System.Windows.Forms.RadioButton()
        Me.A360p = New System.Windows.Forms.RadioButton()
        Me.A720p = New System.Windows.Forms.RadioButton()
        Me.A1080p = New System.Windows.Forms.RadioButton()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.MultiDLSoftSubs = New System.Windows.Forms.GroupBox()
        Me.SoftSubsMass = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.comboBox4 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.comboBox3 = New System.Windows.Forms.ComboBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.GB_Sub_Path.SuspendLayout()
        Me.DL_Count_simultaneous.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SoftSubs.SuspendLayout()
        Me.GB_SubLanguage.SuspendLayout()
        Me.GB_Resolution.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MultiDLSoftSubs.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GB_Sub_Path
        '
        Me.GB_Sub_Path.BackColor = System.Drawing.Color.Transparent
        Me.GB_Sub_Path.Controls.Add(Me.RBStaffel)
        Me.GB_Sub_Path.Controls.Add(Me.RBAnime)
        Me.GB_Sub_Path.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GB_Sub_Path.ForeColor = System.Drawing.Color.Black
        Me.GB_Sub_Path.Location = New System.Drawing.Point(6, 85)
        Me.GB_Sub_Path.Name = "GB_Sub_Path"
        Me.GB_Sub_Path.Size = New System.Drawing.Size(456, 51)
        Me.GB_Sub_Path.TabIndex = 3
        Me.GB_Sub_Path.TabStop = False
        Me.GB_Sub_Path.Text = "Unterordner "
        '
        'RBStaffel
        '
        Me.RBStaffel.AutoSize = True
        Me.RBStaffel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBStaffel.ForeColor = System.Drawing.Color.Black
        Me.RBStaffel.Location = New System.Drawing.Point(251, 21)
        Me.RBStaffel.Name = "RBStaffel"
        Me.RBStaffel.Size = New System.Drawing.Size(174, 22)
        Me.RBStaffel.TabIndex = 1
        Me.RBStaffel.TabStop = True
        Me.RBStaffel.Text = "Anime Serie + Staffel"
        Me.ToolTip1.SetToolTip(Me.RBStaffel, "Erstelle je einen Ordner für den Anime und darin einen für die Staffel")
        Me.RBStaffel.UseVisualStyleBackColor = True
        '
        'RBAnime
        '
        Me.RBAnime.AutoSize = True
        Me.RBAnime.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBAnime.ForeColor = System.Drawing.Color.Black
        Me.RBAnime.Location = New System.Drawing.Point(30, 21)
        Me.RBAnime.Name = "RBAnime"
        Me.RBAnime.Size = New System.Drawing.Size(113, 22)
        Me.RBAnime.TabIndex = 1
        Me.RBAnime.TabStop = True
        Me.RBAnime.Text = "Anime Serie"
        Me.ToolTip1.SetToolTip(Me.RBAnime, "Erstelle einen Ordner für den Anime, unabhänig der Staffeln")
        Me.RBAnime.UseVisualStyleBackColor = True
        '
        'DL_Count_simultaneous
        '
        Me.DL_Count_simultaneous.BackColor = System.Drawing.Color.Transparent
        Me.DL_Count_simultaneous.Controls.Add(Me.NumericUpDown1)
        Me.DL_Count_simultaneous.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.DL_Count_simultaneous.ForeColor = System.Drawing.Color.Black
        Me.DL_Count_simultaneous.Location = New System.Drawing.Point(6, 11)
        Me.DL_Count_simultaneous.Name = "DL_Count_simultaneous"
        Me.DL_Count_simultaneous.Size = New System.Drawing.Size(456, 68)
        Me.DL_Count_simultaneous.TabIndex = 5
        Me.DL_Count_simultaneous.TabStop = False
        Me.DL_Count_simultaneous.Text = "simultaneous downloads"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(98, 30)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {12, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(265, 22)
        Me.NumericUpDown1.TabIndex = 0
        Me.NumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox1.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.pictureBox1.Location = New System.Drawing.Point(449, 1)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(50, 40)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox1.TabIndex = 7
        Me.pictureBox1.TabStop = False
        '
        'pictureBox4
        '
        Me.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox4.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.crdSettings_Button_SafeExit
        Me.pictureBox4.Location = New System.Drawing.Point(67, 505)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(355, 30)
        Me.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox4.TabIndex = 8
        Me.pictureBox4.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 47)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(476, 441)
        Me.TabControl1.TabIndex = 38
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.PictureBox6)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.DL_Count_simultaneous)
        Me.TabPage1.Controls.Add(Me.GB_Sub_Path)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(468, 415)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Settings"
        '
        'PictureBox6
        '
        Me.PictureBox6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox6.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_credits_default
        Me.PictureBox6.Location = New System.Drawing.Point(195, 359)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(76, 39)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox6.TabIndex = 41
        Me.PictureBox6.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Firefox_True)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(6, 215)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(456, 138)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Browser Settings"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(186, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Default Website"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 57)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(444, 22)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "https://www.crunchyroll.com/"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Firefox_True
        '
        Me.Firefox_True.AutoSize = True
        Me.Firefox_True.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Firefox_True.ForeColor = System.Drawing.Color.Black
        Me.Firefox_True.Location = New System.Drawing.Point(141, 99)
        Me.Firefox_True.Name = "Firefox_True"
        Me.Firefox_True.Size = New System.Drawing.Size(166, 20)
        Me.Firefox_True.TabIndex = 0
        Me.Firefox_True.Text = "Use Firefox Profil Folder"
        Me.Firefox_True.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.PictureBox2)
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(6, 142)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(456, 67)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Crunchyroll US"
        '
        'PictureBox2
        '
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox2.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.crdsettings_setUScookie_button
        Me.PictureBox2.Location = New System.Drawing.Point(154, 21)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(150, 30)
        Me.PictureBox2.TabIndex = 4
        Me.PictureBox2.TabStop = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.CheckBox1)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.SoftSubs)
        Me.TabPage2.Controls.Add(Me.GB_SubLanguage)
        Me.TabPage2.Controls.Add(Me.GB_Resolution)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(468, 415)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Output Settings"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.FFMPEG_Command)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(6, 346)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(449, 63)
        Me.GroupBox2.TabIndex = 40
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ffmpeg command"
        '
        'FFMPEG_Command
        '
        Me.FFMPEG_Command.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.FFMPEG_Command.DropDownHeight = 250
        Me.FFMPEG_Command.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FFMPEG_Command.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FFMPEG_Command.FormattingEnabled = True
        Me.FFMPEG_Command.IntegralHeight = False
        Me.FFMPEG_Command.ItemHeight = 16
        Me.FFMPEG_Command.Items.AddRange(New Object() {" -c copy -bsf:a aac_adtstoasc ", " -c:v hevc_nvenc -preset fast -b:v 6M -bsf:a aac_adtstoasc ", " -c:v libx265 -preset fast -b:v 6M -bsf:a aac_adtstoasc "})
        Me.FFMPEG_Command.Location = New System.Drawing.Point(6, 25)
        Me.FFMPEG_Command.Name = "FFMPEG_Command"
        Me.FFMPEG_Command.Size = New System.Drawing.Size(437, 22)
        Me.FFMPEG_Command.Sorted = True
        Me.FFMPEG_Command.TabIndex = 33
        '
        'SoftSubs
        '
        Me.SoftSubs.BackColor = System.Drawing.Color.Transparent
        Me.SoftSubs.Controls.Add(Me.MergeMP4)
        Me.SoftSubs.Controls.Add(Me.CBesES)
        Me.SoftSubs.Controls.Add(Me.CBitIT)
        Me.SoftSubs.Controls.Add(Me.CBruRU)
        Me.SoftSubs.Controls.Add(Me.CBarME)
        Me.SoftSubs.Controls.Add(Me.CBfrFR)
        Me.SoftSubs.Controls.Add(Me.CBesLA)
        Me.SoftSubs.Controls.Add(Me.CBptBR)
        Me.SoftSubs.Controls.Add(Me.CBdeDE)
        Me.SoftSubs.Controls.Add(Me.CBenUS)
        Me.SoftSubs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SoftSubs.Location = New System.Drawing.Point(6, 134)
        Me.SoftSubs.Name = "SoftSubs"
        Me.SoftSubs.Size = New System.Drawing.Size(449, 186)
        Me.SoftSubs.TabIndex = 49
        Me.SoftSubs.TabStop = False
        Me.SoftSubs.Text = "SoftSubs"
        '
        'MergeMP4
        '
        Me.MergeMP4.AutoSize = True
        Me.MergeMP4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MergeMP4.Location = New System.Drawing.Point(128, 155)
        Me.MergeMP4.Name = "MergeMP4"
        Me.MergeMP4.Size = New System.Drawing.Size(194, 20)
        Me.MergeMP4.TabIndex = 6
        Me.MergeMP4.Text = "Merge softubs with video file"
        Me.MergeMP4.UseVisualStyleBackColor = True
        '
        'CBesES
        '
        Me.CBesES.AutoSize = True
        Me.CBesES.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBesES.Location = New System.Drawing.Point(166, 74)
        Me.CBesES.Name = "CBesES"
        Me.CBesES.Size = New System.Drawing.Size(135, 20)
        Me.CBesES.TabIndex = 5
        Me.CBesES.Text = "Español (España)"
        Me.CBesES.UseVisualStyleBackColor = True
        '
        'CBitIT
        '
        Me.CBitIT.AutoSize = True
        Me.CBitIT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBitIT.Location = New System.Drawing.Point(25, 111)
        Me.CBitIT.Name = "CBitIT"
        Me.CBitIT.Size = New System.Drawing.Size(116, 20)
        Me.CBitIT.TabIndex = 5
        Me.CBitIT.Text = "Italiano (Italian)"
        Me.CBitIT.UseVisualStyleBackColor = True
        '
        'CBruRU
        '
        Me.CBruRU.AutoSize = True
        Me.CBruRU.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBruRU.Location = New System.Drawing.Point(306, 111)
        Me.CBruRU.Name = "CBruRU"
        Me.CBruRU.Size = New System.Drawing.Size(141, 20)
        Me.CBruRU.TabIndex = 5
        Me.CBruRU.Text = "Русский (Russian)"
        Me.CBruRU.UseVisualStyleBackColor = True
        '
        'CBarME
        '
        Me.CBarME.AutoSize = True
        Me.CBarME.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBarME.Location = New System.Drawing.Point(166, 111)
        Me.CBarME.Name = "CBarME"
        Me.CBarME.Size = New System.Drawing.Size(108, 20)
        Me.CBarME.TabIndex = 5
        Me.CBarME.Text = "العربية (Arabic)"
        Me.CBarME.UseVisualStyleBackColor = True
        '
        'CBfrFR
        '
        Me.CBfrFR.AutoSize = True
        Me.CBfrFR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBfrFR.Location = New System.Drawing.Point(306, 36)
        Me.CBfrFR.Name = "CBfrFR"
        Me.CBfrFR.Size = New System.Drawing.Size(132, 20)
        Me.CBfrFR.TabIndex = 4
        Me.CBfrFR.Text = "Français (France)"
        Me.CBfrFR.UseVisualStyleBackColor = True
        '
        'CBesLA
        '
        Me.CBesLA.AutoSize = True
        Me.CBesLA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBesLA.Location = New System.Drawing.Point(166, 36)
        Me.CBesLA.Name = "CBesLA"
        Me.CBesLA.Size = New System.Drawing.Size(104, 20)
        Me.CBesLA.TabIndex = 3
        Me.CBesLA.Text = "Español (LA)"
        Me.CBesLA.UseVisualStyleBackColor = True
        '
        'CBptBR
        '
        Me.CBptBR.AutoSize = True
        Me.CBptBR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBptBR.Location = New System.Drawing.Point(306, 74)
        Me.CBptBR.Name = "CBptBR"
        Me.CBptBR.Size = New System.Drawing.Size(133, 20)
        Me.CBptBR.TabIndex = 2
        Me.CBptBR.Text = "Português (Brasil)"
        Me.CBptBR.UseVisualStyleBackColor = True
        '
        'CBdeDE
        '
        Me.CBdeDE.AutoSize = True
        Me.CBdeDE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBdeDE.Location = New System.Drawing.Point(25, 74)
        Me.CBdeDE.Name = "CBdeDE"
        Me.CBdeDE.Size = New System.Drawing.Size(76, 20)
        Me.CBdeDE.TabIndex = 1
        Me.CBdeDE.Text = "Deutsch"
        Me.CBdeDE.UseVisualStyleBackColor = True
        '
        'CBenUS
        '
        Me.CBenUS.AutoSize = True
        Me.CBenUS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBenUS.Location = New System.Drawing.Point(25, 36)
        Me.CBenUS.Name = "CBenUS"
        Me.CBenUS.Size = New System.Drawing.Size(71, 20)
        Me.CBenUS.TabIndex = 0
        Me.CBenUS.Text = "English"
        Me.CBenUS.UseVisualStyleBackColor = True
        '
        'GB_SubLanguage
        '
        Me.GB_SubLanguage.BackColor = System.Drawing.Color.Transparent
        Me.GB_SubLanguage.Controls.Add(Me.ComboBox1)
        Me.GB_SubLanguage.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GB_SubLanguage.ForeColor = System.Drawing.Color.Black
        Me.GB_SubLanguage.Location = New System.Drawing.Point(6, 65)
        Me.GB_SubLanguage.Name = "GB_SubLanguage"
        Me.GB_SubLanguage.Size = New System.Drawing.Size(449, 63)
        Me.GB_SubLanguage.TabIndex = 39
        Me.GB_SubLanguage.TabStop = False
        Me.GB_SubLanguage.Text = "Sub Sprache"
        '
        'ComboBox1
        '
        Me.ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBox1.DropDownHeight = 250
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.IntegralHeight = False
        Me.ComboBox1.ItemHeight = 16
        Me.ComboBox1.Items.AddRange(New Object() {"Deutsch", "English", "Español (España)", "Español (LA)", "Français (France)", "Italiano (Italian)", "Português (Brasil)", "Русский (Russian)", "العربية (Arabic)"})
        Me.ComboBox1.Location = New System.Drawing.Point(60, 25)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(326, 22)
        Me.ComboBox1.Sorted = True
        Me.ComboBox1.TabIndex = 33
        '
        'GB_Resolution
        '
        Me.GB_Resolution.BackColor = System.Drawing.Color.Transparent
        Me.GB_Resolution.Controls.Add(Me.AAuto)
        Me.GB_Resolution.Controls.Add(Me.A480p)
        Me.GB_Resolution.Controls.Add(Me.A360p)
        Me.GB_Resolution.Controls.Add(Me.A720p)
        Me.GB_Resolution.Controls.Add(Me.A1080p)
        Me.GB_Resolution.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GB_Resolution.ForeColor = System.Drawing.Color.Black
        Me.GB_Resolution.Location = New System.Drawing.Point(6, 6)
        Me.GB_Resolution.Name = "GB_Resolution"
        Me.GB_Resolution.Size = New System.Drawing.Size(449, 53)
        Me.GB_Resolution.TabIndex = 38
        Me.GB_Resolution.TabStop = False
        Me.GB_Resolution.Text = "Auflösung"
        '
        'AAuto
        '
        Me.AAuto.AutoSize = True
        Me.AAuto.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AAuto.ForeColor = System.Drawing.Color.Black
        Me.AAuto.Location = New System.Drawing.Point(377, 21)
        Me.AAuto.Name = "AAuto"
        Me.AAuto.Size = New System.Drawing.Size(66, 22)
        Me.AAuto.TabIndex = 3
        Me.AAuto.TabStop = True
        Me.AAuto.Text = "[Auto]"
        Me.AAuto.UseVisualStyleBackColor = True
        '
        'A480p
        '
        Me.A480p.AutoSize = True
        Me.A480p.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.A480p.ForeColor = System.Drawing.Color.Black
        Me.A480p.Location = New System.Drawing.Point(198, 21)
        Me.A480p.Name = "A480p"
        Me.A480p.Size = New System.Drawing.Size(62, 22)
        Me.A480p.TabIndex = 2
        Me.A480p.TabStop = True
        Me.A480p.Text = "480p"
        Me.A480p.UseVisualStyleBackColor = True
        '
        'A360p
        '
        Me.A360p.AutoSize = True
        Me.A360p.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.A360p.ForeColor = System.Drawing.Color.Black
        Me.A360p.Location = New System.Drawing.Point(286, 21)
        Me.A360p.Name = "A360p"
        Me.A360p.Size = New System.Drawing.Size(62, 22)
        Me.A360p.TabIndex = 2
        Me.A360p.TabStop = True
        Me.A360p.Text = "360p"
        Me.A360p.UseVisualStyleBackColor = True
        '
        'A720p
        '
        Me.A720p.AutoSize = True
        Me.A720p.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.A720p.ForeColor = System.Drawing.Color.Black
        Me.A720p.Location = New System.Drawing.Point(119, 21)
        Me.A720p.Name = "A720p"
        Me.A720p.Size = New System.Drawing.Size(62, 22)
        Me.A720p.TabIndex = 1
        Me.A720p.TabStop = True
        Me.A720p.Text = "720p"
        Me.A720p.UseVisualStyleBackColor = True
        '
        'A1080p
        '
        Me.A1080p.AutoSize = True
        Me.A1080p.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.A1080p.ForeColor = System.Drawing.Color.Black
        Me.A1080p.Location = New System.Drawing.Point(25, 21)
        Me.A1080p.Name = "A1080p"
        Me.A1080p.Size = New System.Drawing.Size(71, 22)
        Me.A1080p.TabIndex = 0
        Me.A1080p.TabStop = True
        Me.A1080p.Text = "1080p"
        Me.A1080p.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Controls.Add(Me.MultiDLSoftSubs)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(468, 415)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Subtile Download"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.PictureBox5)
        Me.GroupBox3.Controls.Add(Me.StatusLabel)
        Me.GroupBox3.Controls.Add(Me.TextBox2)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(9, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(450, 162)
        Me.GroupBox3.TabIndex = 46
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Single Download "
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox5.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.softsubs_download
        Me.PictureBox5.Location = New System.Drawing.Point(24, 116)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(403, 36)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 46
        Me.PictureBox5.TabStop = False
        '
        'StatusLabel
        '
        Me.StatusLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusLabel.BackColor = System.Drawing.Color.Transparent
        Me.StatusLabel.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.StatusLabel.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.StatusLabel.Location = New System.Drawing.Point(8, 71)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(433, 29)
        Me.StatusLabel.TabIndex = 38
        Me.StatusLabel.Text = "Status: idle"
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.TextBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBox2.Location = New System.Drawing.Point(6, 35)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(435, 22)
        Me.TextBox2.TabIndex = 4
        Me.TextBox2.TabStop = False
        Me.TextBox2.Text = "URL"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'MultiDLSoftSubs
        '
        Me.MultiDLSoftSubs.BackColor = System.Drawing.Color.Transparent
        Me.MultiDLSoftSubs.Controls.Add(Me.SoftSubsMass)
        Me.MultiDLSoftSubs.Controls.Add(Me.PictureBox3)
        Me.MultiDLSoftSubs.Controls.Add(Me.comboBox4)
        Me.MultiDLSoftSubs.Controls.Add(Me.ComboBox2)
        Me.MultiDLSoftSubs.Controls.Add(Me.comboBox3)
        Me.MultiDLSoftSubs.Enabled = False
        Me.MultiDLSoftSubs.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MultiDLSoftSubs.Location = New System.Drawing.Point(9, 174)
        Me.MultiDLSoftSubs.Name = "MultiDLSoftSubs"
        Me.MultiDLSoftSubs.Size = New System.Drawing.Size(450, 223)
        Me.MultiDLSoftSubs.TabIndex = 45
        Me.MultiDLSoftSubs.TabStop = False
        Me.MultiDLSoftSubs.Text = "Multi Download"
        '
        'SoftSubsMass
        '
        Me.SoftSubsMass.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SoftSubsMass.BackColor = System.Drawing.Color.Transparent
        Me.SoftSubsMass.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SoftSubsMass.ForeColor = System.Drawing.Color.Black
        Me.SoftSubsMass.Location = New System.Drawing.Point(6, 130)
        Me.SoftSubsMass.Name = "SoftSubsMass"
        Me.SoftSubsMass.Size = New System.Drawing.Size(438, 26)
        Me.SoftSubsMass.TabIndex = 46
        Me.SoftSubsMass.Text = "Status: idle"
        Me.SoftSubsMass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox3.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.softsubs_download_gray
        Me.PictureBox3.Location = New System.Drawing.Point(24, 169)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(403, 36)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 45
        Me.PictureBox3.TabStop = False
        '
        'comboBox4
        '
        Me.comboBox4.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBox4.FormattingEnabled = True
        Me.comboBox4.Location = New System.Drawing.Point(6, 97)
        Me.comboBox4.Name = "comboBox4"
        Me.comboBox4.Size = New System.Drawing.Size(441, 23)
        Me.comboBox4.TabIndex = 2
        '
        'ComboBox2
        '
        Me.ComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(6, 25)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(441, 23)
        Me.ComboBox2.TabIndex = 1
        '
        'comboBox3
        '
        Me.comboBox3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBox3.FormattingEnabled = True
        Me.comboBox3.Location = New System.Drawing.Point(6, 62)
        Me.comboBox3.Name = "comboBox3"
        Me.comboBox3.Size = New System.Drawing.Size(441, 23)
        Me.comboBox3.TabIndex = 1
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(67, 326)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(351, 20)
        Me.CheckBox1.TabIndex = 6
        Me.CheckBox1.Text = "i know that re-encoding the video takes time and power"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'einstellungen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.crdSettings_Background
        Me.ClientSize = New System.Drawing.Size(500, 550)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pictureBox4)
        Me.Controls.Add(Me.pictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "einstellungen"
        Me.Text = "crunchyroll downloader"
        Me.GB_Sub_Path.ResumeLayout(False)
        Me.GB_Sub_Path.PerformLayout()
        Me.DL_Count_simultaneous.ResumeLayout(False)
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.SoftSubs.ResumeLayout(False)
        Me.SoftSubs.PerformLayout()
        Me.GB_SubLanguage.ResumeLayout(False)
        Me.GB_Resolution.ResumeLayout(False)
        Me.GB_Resolution.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MultiDLSoftSubs.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GB_Sub_Path As GroupBox
    Friend WithEvents RBStaffel As RadioButton
    Friend WithEvents RBAnime As RadioButton
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents DL_Count_simultaneous As GroupBox
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Private WithEvents pictureBox1 As PictureBox
    Private WithEvents pictureBox4 As PictureBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Firefox_True As CheckBox
    Friend WithEvents GroupBox4 As GroupBox
    Private WithEvents PictureBox2 As PictureBox
    Friend WithEvents GB_SubLanguage As GroupBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents GB_Resolution As GroupBox
    Friend WithEvents AAuto As RadioButton
    Friend WithEvents A480p As RadioButton
    Friend WithEvents A360p As RadioButton
    Friend WithEvents A720p As RadioButton
    Friend WithEvents A1080p As RadioButton
    Friend WithEvents SoftSubs As GroupBox
    Friend WithEvents CBesES As CheckBox
    Friend WithEvents CBitIT As CheckBox
    Friend WithEvents CBruRU As CheckBox
    Friend WithEvents CBarME As CheckBox
    Friend WithEvents CBfrFR As CheckBox
    Friend WithEvents CBesLA As CheckBox
    Friend WithEvents CBptBR As CheckBox
    Friend WithEvents CBdeDE As CheckBox
    Friend WithEvents CBenUS As CheckBox
    Private WithEvents PictureBox6 As PictureBox
    Friend WithEvents MergeMP4 As CheckBox
    Friend WithEvents TabPage3 As TabPage
    Public WithEvents GroupBox3 As GroupBox
    Public WithEvents PictureBox5 As PictureBox
    Public WithEvents StatusLabel As Label
    Public WithEvents TextBox2 As TextBox
    Public WithEvents MultiDLSoftSubs As GroupBox
    Public WithEvents SoftSubsMass As Label
    Public WithEvents PictureBox3 As PictureBox
    Public WithEvents comboBox4 As ComboBox
    Public WithEvents ComboBox2 As ComboBox
    Public WithEvents comboBox3 As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents FFMPEG_Command As ComboBox
    Friend WithEvents CheckBox1 As CheckBox
End Class
