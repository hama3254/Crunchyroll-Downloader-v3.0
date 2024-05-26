<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Error_msg
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btn_cl = New System.Windows.Forms.Button()
        Me.btn_ign = New System.Windows.Forms.Button()
        Me.btn_ok = New System.Windows.Forms.Button()
        Me.ErrorLabel = New MetroFramework.Controls.MetroLabel()
        Me.Delay = New System.Windows.Forms.Timer(Me.components)
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ErrorBox = New System.Windows.Forms.RichTextBox()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.btn_cl)
        Me.GroupBox3.Controls.Add(Me.btn_ign)
        Me.GroupBox3.Controls.Add(Me.btn_ok)
        Me.GroupBox3.Controls.Add(Me.ErrorLabel)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 60)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(613, 203)
        Me.GroupBox3.TabIndex = 38
        Me.GroupBox3.TabStop = False
        '
        'btn_cl
        '
        Me.btn_cl.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.ffmpeg_OK_cL
        Me.btn_cl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_cl.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_cl.FlatAppearance.BorderSize = 0
        Me.btn_cl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_cl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cl.ForeColor = System.Drawing.SystemColors.Control
        Me.btn_cl.Location = New System.Drawing.Point(28, 121)
        Me.btn_cl.Name = "btn_cl"
        Me.btn_cl.Size = New System.Drawing.Size(150, 40)
        Me.btn_cl.TabIndex = 77
        Me.btn_cl.Text = "Details"
        Me.btn_cl.UseVisualStyleBackColor = True
        '
        'btn_ign
        '
        Me.btn_ign.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.ffmpeg_OK_cL
        Me.btn_ign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ign.FlatAppearance.BorderSize = 0
        Me.btn_ign.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ign.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ign.ForeColor = System.Drawing.SystemColors.Control
        Me.btn_ign.Location = New System.Drawing.Point(235, 121)
        Me.btn_ign.Name = "btn_ign"
        Me.btn_ign.Size = New System.Drawing.Size(150, 40)
        Me.btn_ign.TabIndex = 78
        Me.btn_ign.Text = "Ignore"
        Me.btn_ign.UseVisualStyleBackColor = True
        '
        'btn_ok
        '
        Me.btn_ok.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.ffmpeg_OK_cL
        Me.btn_ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ok.FlatAppearance.BorderSize = 0
        Me.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ok.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ok.ForeColor = System.Drawing.SystemColors.Control
        Me.btn_ok.Location = New System.Drawing.Point(424, 121)
        Me.btn_ok.Name = "btn_ok"
        Me.btn_ok.Size = New System.Drawing.Size(150, 40)
        Me.btn_ok.TabIndex = 78
        Me.btn_ok.Text = "Ok"
        Me.btn_ok.UseVisualStyleBackColor = True
        '
        'ErrorLabel
        '
        Me.ErrorLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ErrorLabel.BackColor = System.Drawing.Color.Transparent
        Me.ErrorLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.ErrorLabel.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.ErrorLabel.ForeColor = System.Drawing.Color.Black
        Me.ErrorLabel.Location = New System.Drawing.Point(6, 16)
        Me.ErrorLabel.Name = "ErrorLabel"
        Me.ErrorLabel.Size = New System.Drawing.Size(601, 53)
        Me.ErrorLabel.TabIndex = 34
        Me.ErrorLabel.Text = "Status: idle"
        Me.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Delay
        '
        Me.Delay.Interval = 2000
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.FlatAppearance.BorderSize = 0
        Me.Btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Close.ForeColor = System.Drawing.Color.Transparent
        Me.Btn_Close.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.Btn_Close.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Btn_Close.Location = New System.Drawing.Point(581, 8)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(35, 35)
        Me.Btn_Close.TabIndex = 39
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.ErrorBox)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 286)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(613, 203)
        Me.GroupBox1.TabIndex = 79
        Me.GroupBox1.TabStop = False
        '
        'ErrorBox
        '
        Me.ErrorBox.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ErrorBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ErrorBox.Location = New System.Drawing.Point(10, 19)
        Me.ErrorBox.Name = "ErrorBox"
        Me.ErrorBox.ReadOnly = True
        Me.ErrorBox.Size = New System.Drawing.Size(593, 170)
        Me.ErrorBox.TabIndex = 0
        Me.ErrorBox.Text = ""
        '
        'Error_msg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 275)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.GroupBox3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Error_msg"
        Me.Text = "CRD-Error"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents GroupBox3 As GroupBox
    Public WithEvents ErrorLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents Delay As Timer
    Friend WithEvents Btn_Close As Button
    Friend WithEvents btn_cl As Button
    Friend WithEvents btn_ok As Button
    Private WithEvents GroupBox1 As GroupBox
    Friend WithEvents ErrorBox As RichTextBox
    Friend WithEvents btn_ign As Button
End Class
