<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SoftSub
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
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SoftSubs = New System.Windows.Forms.GroupBox()
        Me.CBesES = New System.Windows.Forms.CheckBox()
        Me.CBitIT = New System.Windows.Forms.CheckBox()
        Me.CBruRU = New System.Windows.Forms.CheckBox()
        Me.CBarME = New System.Windows.Forms.CheckBox()
        Me.CBfrFR = New System.Windows.Forms.CheckBox()
        Me.CBesLA = New System.Windows.Forms.CheckBox()
        Me.CBptBR = New System.Windows.Forms.CheckBox()
        Me.CBdeDE = New System.Windows.Forms.CheckBox()
        Me.CBenUS = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SoftSubs.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox1.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.pictureBox1.Location = New System.Drawing.Point(479, 1)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(50, 40)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox1.TabIndex = 8
        Me.pictureBox1.TabStop = False
        '
        'pictureBox4
        '
        Me.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox4.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.crdSettings_Button_SafeExit
        Me.pictureBox4.Location = New System.Drawing.Point(76, 207)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(355, 30)
        Me.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox4.TabIndex = 9
        Me.pictureBox4.TabStop = False
        '
        'textBox1
        '
        Me.textBox1.BackColor = System.Drawing.Color.White
        Me.textBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.textBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.textBox1.Location = New System.Drawing.Point(34, 48)
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(417, 22)
        Me.textBox1.TabIndex = 43
        Me.textBox1.TabStop = False
        Me.textBox1.Text = "URL"
        Me.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox2.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.download_subs
        Me.PictureBox2.Location = New System.Drawing.Point(140, 85)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(199, 40)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 45
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(6, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(475, 29)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Status : idle"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SoftSubs
        '
        Me.SoftSubs.BackColor = System.Drawing.Color.Transparent
        Me.SoftSubs.Controls.Add(Me.CBesES)
        Me.SoftSubs.Controls.Add(Me.CBitIT)
        Me.SoftSubs.Controls.Add(Me.CBruRU)
        Me.SoftSubs.Controls.Add(Me.CBarME)
        Me.SoftSubs.Controls.Add(Me.CBfrFR)
        Me.SoftSubs.Controls.Add(Me.CBesLA)
        Me.SoftSubs.Controls.Add(Me.CBptBR)
        Me.SoftSubs.Controls.Add(Me.CBdeDE)
        Me.SoftSubs.Controls.Add(Me.CBenUS)
        Me.SoftSubs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SoftSubs.Location = New System.Drawing.Point(18, 64)
        Me.SoftSubs.Name = "SoftSubs"
        Me.SoftSubs.Size = New System.Drawing.Size(487, 137)
        Me.SoftSubs.TabIndex = 48
        Me.SoftSubs.TabStop = False
        Me.SoftSubs.Text = "SoftSubs"
        '
        'CBesES
        '
        Me.CBesES.AutoSize = True
        Me.CBesES.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBesES.Location = New System.Drawing.Point(162, 69)
        Me.CBesES.Name = "CBesES"
        Me.CBesES.Size = New System.Drawing.Size(151, 20)
        Me.CBesES.TabIndex = 5
        Me.CBesES.Text = "Español (España)"
        Me.CBesES.UseVisualStyleBackColor = True
        '
        'CBitIT
        '
        Me.CBitIT.AutoSize = True
        Me.CBitIT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBitIT.Location = New System.Drawing.Point(21, 106)
        Me.CBitIT.Name = "CBitIT"
        Me.CBitIT.Size = New System.Drawing.Size(134, 20)
        Me.CBitIT.TabIndex = 5
        Me.CBitIT.Text = "Italiano (Italian)"
        Me.CBitIT.UseVisualStyleBackColor = True
        '
        'CBruRU
        '
        Me.CBruRU.AutoSize = True
        Me.CBruRU.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBruRU.Location = New System.Drawing.Point(326, 31)
        Me.CBruRU.Name = "CBruRU"
        Me.CBruRU.Size = New System.Drawing.Size(158, 20)
        Me.CBruRU.TabIndex = 5
        Me.CBruRU.Text = "Русский (Russian)"
        Me.CBruRU.UseVisualStyleBackColor = True
        '
        'CBarME
        '
        Me.CBarME.AutoSize = True
        Me.CBarME.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBarME.Location = New System.Drawing.Point(162, 106)
        Me.CBarME.Name = "CBarME"
        Me.CBarME.Size = New System.Drawing.Size(124, 20)
        Me.CBarME.TabIndex = 5
        Me.CBarME.Text = "العربية (Arabic)"
        Me.CBarME.UseVisualStyleBackColor = True
        '
        'CBfrFR
        '
        Me.CBfrFR.AutoSize = True
        Me.CBfrFR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBfrFR.Location = New System.Drawing.Point(326, 106)
        Me.CBfrFR.Name = "CBfrFR"
        Me.CBfrFR.Size = New System.Drawing.Size(149, 20)
        Me.CBfrFR.TabIndex = 4
        Me.CBfrFR.Text = "Français (France)"
        Me.CBfrFR.UseVisualStyleBackColor = True
        '
        'CBesLA
        '
        Me.CBesLA.AutoSize = True
        Me.CBesLA.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBesLA.Location = New System.Drawing.Point(162, 31)
        Me.CBesLA.Name = "CBesLA"
        Me.CBesLA.Size = New System.Drawing.Size(116, 20)
        Me.CBesLA.TabIndex = 3
        Me.CBesLA.Text = "Español (LA)"
        Me.CBesLA.UseVisualStyleBackColor = True
        '
        'CBptBR
        '
        Me.CBptBR.AutoSize = True
        Me.CBptBR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBptBR.Location = New System.Drawing.Point(326, 69)
        Me.CBptBR.Name = "CBptBR"
        Me.CBptBR.Size = New System.Drawing.Size(151, 20)
        Me.CBptBR.TabIndex = 2
        Me.CBptBR.Text = "Português (Brasil)"
        Me.CBptBR.UseVisualStyleBackColor = True
        '
        'CBdeDE
        '
        Me.CBdeDE.AutoSize = True
        Me.CBdeDE.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBdeDE.Location = New System.Drawing.Point(21, 69)
        Me.CBdeDE.Name = "CBdeDE"
        Me.CBdeDE.Size = New System.Drawing.Size(83, 20)
        Me.CBdeDE.TabIndex = 1
        Me.CBdeDE.Text = "Deutsch"
        Me.CBdeDE.UseVisualStyleBackColor = True
        '
        'CBenUS
        '
        Me.CBenUS.AutoSize = True
        Me.CBenUS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CBenUS.Location = New System.Drawing.Point(21, 31)
        Me.CBenUS.Name = "CBenUS"
        Me.CBenUS.Size = New System.Drawing.Size(78, 20)
        Me.CBenUS.TabIndex = 0
        Me.CBenUS.Text = "English"
        Me.CBenUS.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.textBox1)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(18, 252)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(487, 135)
        Me.GroupBox1.TabIndex = 49
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Download only Subtitle"
        '
        'SoftSub
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.SoftSubs_Baclground
        Me.ClientSize = New System.Drawing.Size(530, 400)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.SoftSubs)
        Me.Controls.Add(Me.pictureBox4)
        Me.Controls.Add(Me.pictureBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SoftSub"
        Me.Text = "SoftSubs"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SoftSubs.ResumeLayout(False)
        Me.SoftSubs.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents pictureBox1 As PictureBox
    Private WithEvents pictureBox4 As PictureBox
    Public WithEvents textBox1 As TextBox
    Private WithEvents PictureBox2 As PictureBox
    Public WithEvents Label2 As Label
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
    Friend WithEvents GroupBox1 As GroupBox
End Class
