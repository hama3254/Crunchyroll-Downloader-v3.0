<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ffmpeg_options
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btn_cl = New System.Windows.Forms.Button()
        Me.btn_ok = New System.Windows.Forms.Button()
        Me.TB_cmd = New MetroFramework.Controls.MetroTextBox()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.btn_cl)
        Me.GroupBox3.Controls.Add(Me.btn_ok)
        Me.GroupBox3.Controls.Add(Me.TB_cmd)
        Me.GroupBox3.Location = New System.Drawing.Point(15, 65)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(720, 145)
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
        Me.btn_cl.Location = New System.Drawing.Point(395, 87)
        Me.btn_cl.Name = "btn_cl"
        Me.btn_cl.Size = New System.Drawing.Size(150, 40)
        Me.btn_cl.TabIndex = 76
        Me.btn_cl.Text = "Cancel"
        Me.btn_cl.UseVisualStyleBackColor = True
        '
        'btn_ok
        '
        Me.btn_ok.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.ffmpeg_OK_cL
        Me.btn_ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_ok.FlatAppearance.BorderSize = 0
        Me.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_ok.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_ok.ForeColor = System.Drawing.SystemColors.Control
        Me.btn_ok.Location = New System.Drawing.Point(175, 87)
        Me.btn_ok.Name = "btn_ok"
        Me.btn_ok.Size = New System.Drawing.Size(150, 40)
        Me.btn_ok.TabIndex = 76
        Me.btn_ok.Text = "Ok"
        Me.btn_ok.UseVisualStyleBackColor = True
        '
        'TB_cmd
        '
        '
        '
        '
        Me.TB_cmd.CustomButton.Image = Nothing
        Me.TB_cmd.CustomButton.Location = New System.Drawing.Point(680, 2)
        Me.TB_cmd.CustomButton.Name = ""
        Me.TB_cmd.CustomButton.Size = New System.Drawing.Size(27, 27)
        Me.TB_cmd.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.TB_cmd.CustomButton.TabIndex = 1
        Me.TB_cmd.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TB_cmd.CustomButton.UseSelectable = True
        Me.TB_cmd.CustomButton.Visible = False
        Me.TB_cmd.FontSize = MetroFramework.MetroTextBoxSize.Tall
        Me.TB_cmd.Lines = New String() {"https://www.crunchyroll.com/"}
        Me.TB_cmd.Location = New System.Drawing.Point(4, 40)
        Me.TB_cmd.MaxLength = 32767
        Me.TB_cmd.Name = "TB_cmd"
        Me.TB_cmd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TB_cmd.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TB_cmd.SelectedText = ""
        Me.TB_cmd.SelectionLength = 0
        Me.TB_cmd.SelectionStart = 0
        Me.TB_cmd.ShortcutsEnabled = True
        Me.TB_cmd.Size = New System.Drawing.Size(710, 32)
        Me.TB_cmd.TabIndex = 36
        Me.TB_cmd.Text = "https://www.crunchyroll.com/"
        Me.TB_cmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TB_cmd.UseSelectable = True
        Me.TB_cmd.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TB_cmd.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'ffmpeg_options
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(750, 225)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "ffmpeg_options"
        Me.Text = "ffmpeg command edit"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents GroupBox3 As GroupBox
    Friend WithEvents TB_cmd As MetroFramework.Controls.MetroTextBox
    Friend WithEvents btn_cl As Button
    Friend WithEvents btn_ok As Button
End Class
