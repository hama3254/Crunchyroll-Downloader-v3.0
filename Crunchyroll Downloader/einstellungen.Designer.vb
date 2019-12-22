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
        Me.GB_Resolution = New System.Windows.Forms.GroupBox()
        Me.A480p = New System.Windows.Forms.RadioButton()
        Me.A360p = New System.Windows.Forms.RadioButton()
        Me.A720p = New System.Windows.Forms.RadioButton()
        Me.A1080p = New System.Windows.Forms.RadioButton()
        Me.GB_SubLanguage = New System.Windows.Forms.GroupBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.GB_Sub_Path = New System.Windows.Forms.GroupBox()
        Me.RBStaffel = New System.Windows.Forms.RadioButton()
        Me.RBAnime = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.pictureBox3 = New System.Windows.Forms.PictureBox()
        Me.DL_Count_simultaneous = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Firefox_True = New System.Windows.Forms.CheckBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GB_Resolution.SuspendLayout()
        Me.GB_SubLanguage.SuspendLayout()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_Sub_Path.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DL_Count_simultaneous.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GB_Resolution
        '
        Me.GB_Resolution.BackColor = System.Drawing.Color.Transparent
        Me.GB_Resolution.Controls.Add(Me.A480p)
        Me.GB_Resolution.Controls.Add(Me.A360p)
        Me.GB_Resolution.Controls.Add(Me.A720p)
        Me.GB_Resolution.Controls.Add(Me.A1080p)
        Me.GB_Resolution.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GB_Resolution.ForeColor = System.Drawing.Color.Black
        Me.GB_Resolution.Location = New System.Drawing.Point(6, 172)
        Me.GB_Resolution.Name = "GB_Resolution"
        Me.GB_Resolution.Size = New System.Drawing.Size(339, 54)
        Me.GB_Resolution.TabIndex = 0
        Me.GB_Resolution.TabStop = False
        Me.GB_Resolution.Text = "Auflösung"
        '
        'A480p
        '
        Me.A480p.AutoSize = True
        Me.A480p.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.A480p.ForeColor = System.Drawing.Color.Black
        Me.A480p.Location = New System.Drawing.Point(162, 21)
        Me.A480p.Name = "A480p"
        Me.A480p.Size = New System.Drawing.Size(64, 23)
        Me.A480p.TabIndex = 2
        Me.A480p.TabStop = True
        Me.A480p.Text = "480p"
        Me.A480p.UseVisualStyleBackColor = True
        '
        'A360p
        '
        Me.A360p.AutoSize = True
        Me.A360p.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.A360p.ForeColor = System.Drawing.Color.Black
        Me.A360p.Location = New System.Drawing.Point(242, 21)
        Me.A360p.Name = "A360p"
        Me.A360p.Size = New System.Drawing.Size(64, 23)
        Me.A360p.TabIndex = 2
        Me.A360p.TabStop = True
        Me.A360p.Text = "360p"
        Me.A360p.UseVisualStyleBackColor = True
        '
        'A720p
        '
        Me.A720p.AutoSize = True
        Me.A720p.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.A720p.ForeColor = System.Drawing.Color.Black
        Me.A720p.Location = New System.Drawing.Point(88, 21)
        Me.A720p.Name = "A720p"
        Me.A720p.Size = New System.Drawing.Size(64, 23)
        Me.A720p.TabIndex = 1
        Me.A720p.TabStop = True
        Me.A720p.Text = "720p"
        Me.A720p.UseVisualStyleBackColor = True
        '
        'A1080p
        '
        Me.A1080p.AutoSize = True
        Me.A1080p.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.A1080p.ForeColor = System.Drawing.Color.Black
        Me.A1080p.Location = New System.Drawing.Point(9, 21)
        Me.A1080p.Name = "A1080p"
        Me.A1080p.Size = New System.Drawing.Size(73, 23)
        Me.A1080p.TabIndex = 0
        Me.A1080p.TabStop = True
        Me.A1080p.Text = "1080p"
        Me.A1080p.UseVisualStyleBackColor = True
        '
        'GB_SubLanguage
        '
        Me.GB_SubLanguage.BackColor = System.Drawing.Color.Transparent
        Me.GB_SubLanguage.Controls.Add(Me.PictureBox5)
        Me.GB_SubLanguage.Controls.Add(Me.ComboBox1)
        Me.GB_SubLanguage.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GB_SubLanguage.ForeColor = System.Drawing.Color.Black
        Me.GB_SubLanguage.Location = New System.Drawing.Point(3, 232)
        Me.GB_SubLanguage.Name = "GB_SubLanguage"
        Me.GB_SubLanguage.Size = New System.Drawing.Size(342, 95)
        Me.GB_SubLanguage.TabIndex = 1
        Me.GB_SubLanguage.TabStop = False
        Me.GB_SubLanguage.Text = "Sub Sprache"
        '
        'PictureBox5
        '
        Me.PictureBox5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox5.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.settings_add_softsubs
        Me.PictureBox5.Location = New System.Drawing.Point(102, 59)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(127, 30)
        Me.PictureBox5.TabIndex = 34
        Me.PictureBox5.TabStop = False
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
        Me.ComboBox1.Location = New System.Drawing.Point(44, 25)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(265, 22)
        Me.ComboBox1.Sorted = True
        Me.ComboBox1.TabIndex = 33
        '
        'GB_Sub_Path
        '
        Me.GB_Sub_Path.BackColor = System.Drawing.Color.Transparent
        Me.GB_Sub_Path.Controls.Add(Me.RBStaffel)
        Me.GB_Sub_Path.Controls.Add(Me.RBAnime)
        Me.GB_Sub_Path.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GB_Sub_Path.ForeColor = System.Drawing.Color.Black
        Me.GB_Sub_Path.Location = New System.Drawing.Point(5, 96)
        Me.GB_Sub_Path.Name = "GB_Sub_Path"
        Me.GB_Sub_Path.Size = New System.Drawing.Size(342, 70)
        Me.GB_Sub_Path.TabIndex = 3
        Me.GB_Sub_Path.TabStop = False
        Me.GB_Sub_Path.Text = "Unterordner "
        '
        'RBStaffel
        '
        Me.RBStaffel.AutoSize = True
        Me.RBStaffel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.RBStaffel.ForeColor = System.Drawing.Color.Black
        Me.RBStaffel.Location = New System.Drawing.Point(153, 24)
        Me.RBStaffel.Name = "RBStaffel"
        Me.RBStaffel.Size = New System.Drawing.Size(183, 23)
        Me.RBStaffel.TabIndex = 1
        Me.RBStaffel.TabStop = True
        Me.RBStaffel.Text = "Anime Serie + Staffel"
        Me.ToolTip1.SetToolTip(Me.RBStaffel, "Erstelle je einen Ordner für den Anime und darin einen für die Staffel")
        Me.RBStaffel.UseVisualStyleBackColor = True
        '
        'RBAnime
        '
        Me.RBAnime.AutoSize = True
        Me.RBAnime.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.RBAnime.ForeColor = System.Drawing.Color.Black
        Me.RBAnime.Location = New System.Drawing.Point(9, 24)
        Me.RBAnime.Name = "RBAnime"
        Me.RBAnime.Size = New System.Drawing.Size(118, 23)
        Me.RBAnime.TabIndex = 1
        Me.RBAnime.TabStop = True
        Me.RBAnime.Text = "Anime Serie"
        Me.ToolTip1.SetToolTip(Me.RBAnime, "Erstelle einen Ordner für den Anime, unabhänig der Staffeln")
        Me.RBAnime.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.PictureBox2)
        Me.GroupBox4.Controls.Add(Me.pictureBox3)
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(6, 173)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(339, 81)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Crunchyroll US"
        '
        'PictureBox2
        '
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox2.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.crdsettings_setUScookie_button
        Me.PictureBox2.Location = New System.Drawing.Point(189, 35)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(150, 30)
        Me.PictureBox2.TabIndex = 4
        Me.PictureBox2.TabStop = False
        '
        'pictureBox3
        '
        Me.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox3.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.crdsettings_setowncookie_button
        Me.pictureBox3.Location = New System.Drawing.Point(9, 35)
        Me.pictureBox3.Name = "pictureBox3"
        Me.pictureBox3.Size = New System.Drawing.Size(150, 30)
        Me.pictureBox3.TabIndex = 3
        Me.pictureBox3.TabStop = False
        '
        'DL_Count_simultaneous
        '
        Me.DL_Count_simultaneous.BackColor = System.Drawing.Color.Transparent
        Me.DL_Count_simultaneous.Controls.Add(Me.NumericUpDown1)
        Me.DL_Count_simultaneous.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.DL_Count_simultaneous.ForeColor = System.Drawing.Color.Black
        Me.DL_Count_simultaneous.Location = New System.Drawing.Point(12, 11)
        Me.DL_Count_simultaneous.Name = "DL_Count_simultaneous"
        Me.DL_Count_simultaneous.Size = New System.Drawing.Size(333, 79)
        Me.DL_Count_simultaneous.TabIndex = 5
        Me.DL_Count_simultaneous.TabStop = False
        Me.DL_Count_simultaneous.Text = "simultaneous downloads"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(35, 35)
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
        Me.pictureBox1.Location = New System.Drawing.Point(328, 1)
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
        Me.pictureBox4.Location = New System.Drawing.Point(10, 429)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(355, 30)
        Me.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox4.TabIndex = 8
        Me.pictureBox4.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Firefox_True)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(6, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(339, 138)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Browser Settings"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(120, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Default Website"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 57)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(327, 22)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Text = "https://www.crunchyroll.com/"
        '
        'Firefox_True
        '
        Me.Firefox_True.AutoSize = True
        Me.Firefox_True.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Firefox_True.ForeColor = System.Drawing.Color.Black
        Me.Firefox_True.Location = New System.Drawing.Point(88, 99)
        Me.Firefox_True.Name = "Firefox_True"
        Me.Firefox_True.Size = New System.Drawing.Size(166, 20)
        Me.Firefox_True.TabIndex = 0
        Me.Firefox_True.Text = "Use Firefox Profil Folder"
        Me.Firefox_True.UseVisualStyleBackColor = True
        '
        'PictureBox6
        '
        Me.PictureBox6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox6.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_credits_default
        Me.PictureBox6.Location = New System.Drawing.Point(131, 276)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(76, 39)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox6.TabIndex = 37
        Me.PictureBox6.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(10, 46)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(359, 364)
        Me.TabControl1.TabIndex = 38
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.DL_Count_simultaneous)
        Me.TabPage1.Controls.Add(Me.GB_SubLanguage)
        Me.TabPage1.Controls.Add(Me.GB_Resolution)
        Me.TabPage1.Controls.Add(Me.GB_Sub_Path)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(351, 338)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Download Settings"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.PictureBox6)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(351, 338)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Additional Settings"
        '
        'einstellungen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.crdSettings_Background
        Me.ClientSize = New System.Drawing.Size(379, 470)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.pictureBox4)
        Me.Controls.Add(Me.pictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "einstellungen"
        Me.Text = "crunchyroll downloader"
        Me.GB_Resolution.ResumeLayout(False)
        Me.GB_Resolution.PerformLayout()
        Me.GB_SubLanguage.ResumeLayout(False)
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GB_Sub_Path.ResumeLayout(False)
        Me.GB_Sub_Path.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DL_Count_simultaneous.ResumeLayout(False)
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GB_Resolution As GroupBox
    Friend WithEvents GB_SubLanguage As GroupBox
    Friend WithEvents A360p As RadioButton
    Friend WithEvents A720p As RadioButton
    Friend WithEvents A1080p As RadioButton
    Friend WithEvents GB_Sub_Path As GroupBox
    Friend WithEvents RBStaffel As RadioButton
    Friend WithEvents RBAnime As RadioButton
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents A480p As RadioButton
    Friend WithEvents DL_Count_simultaneous As GroupBox
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents ComboBox1 As ComboBox
    Private WithEvents pictureBox1 As PictureBox
    Private WithEvents pictureBox3 As PictureBox
    Private WithEvents pictureBox4 As PictureBox
    Private WithEvents PictureBox2 As PictureBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Firefox_True As CheckBox
    Private WithEvents PictureBox5 As PictureBox
    Private WithEvents PictureBox6 As PictureBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
End Class
