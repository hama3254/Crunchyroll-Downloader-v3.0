<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Reso
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
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.pictureBox3 = New System.Windows.Forms.PictureBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.StatusLabel = New System.Windows.Forms.Label()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.ComboBox1.Location = New System.Drawing.Point(34, 82)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(281, 22)
        Me.ComboBox1.TabIndex = 33
        '
        'pictureBox3
        '
        Me.pictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox3.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.pictureBox3.Location = New System.Drawing.Point(329, 1)
        Me.pictureBox3.Name = "pictureBox3"
        Me.pictureBox3.Size = New System.Drawing.Size(50, 40)
        Me.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox3.TabIndex = 36
        Me.pictureBox3.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.PictureBox9)
        Me.GroupBox3.Controls.Add(Me.StatusLabel)
        Me.GroupBox3.Controls.Add(Me.ComboBox1)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 43)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(354, 203)
        Me.GroupBox3.TabIndex = 38
        Me.GroupBox3.TabStop = False
        '
        'PictureBox9
        '
        Me.PictureBox9.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox9.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.DialogNotFound_Submit
        Me.PictureBox9.Location = New System.Drawing.Point(100, 146)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(150, 40)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox9.TabIndex = 39
        Me.PictureBox9.TabStop = False
        '
        'StatusLabel
        '
        Me.StatusLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusLabel.BackColor = System.Drawing.Color.Transparent
        Me.StatusLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusLabel.ForeColor = System.Drawing.Color.Black
        Me.StatusLabel.Location = New System.Drawing.Point(6, 16)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(342, 53)
        Me.StatusLabel.TabIndex = 34
        Me.StatusLabel.Text = "Status: idle"
        Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Reso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.DialogNotFound_Background
        Me.ClientSize = New System.Drawing.Size(380, 280)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.pictureBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Reso"
        Me.Text = "crunchyroll downloader"
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ComboBox1 As ComboBox
    Private WithEvents pictureBox3 As PictureBox
    Private WithEvents GroupBox3 As GroupBox
    Public WithEvents StatusLabel As Label
    Private WithEvents PictureBox9 As PictureBox
End Class
