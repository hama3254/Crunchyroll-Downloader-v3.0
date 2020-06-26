<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Startup
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
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 75
        '
        'Timer2
        '
        Me.Timer2.Interval = 75
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.Black
        Me.label1.Location = New System.Drawing.Point(38, 228)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(140, 22)
        Me.label1.TabIndex = 26
        Me.label1.Text = "libraries: "
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(379, 228)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 22)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "ffmpeg"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(5, Byte), Integer))
        Me.PictureBox1.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.PictureBox1.Location = New System.Drawing.Point(549, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 39)
        Me.PictureBox1.TabIndex = 29
        Me.PictureBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(187, 154)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(368, 45)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Created by hama3254"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(175, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(184, 228)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(140, 22)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "GeckoFX"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(183, 109)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(372, 45)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Created by hama3254"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(182, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(373, 45)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Crunchyroll Downloader"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Startup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.credits_background
        Me.ClientSize = New System.Drawing.Size(600, 259)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Startup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Startup"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(205, Byte), Integer), CType(CType(205, Byte), Integer))
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Public WithEvents label1 As Label
    Public WithEvents Label3 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Public WithEvents Label5 As Label
    Public WithEvents Label7 As Label
    Public WithEvents Label6 As Label
    Public WithEvents Label4 As Label
End Class
