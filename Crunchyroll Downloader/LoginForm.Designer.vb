<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginForm
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
        Me.Btn_Save = New System.Windows.Forms.Button()
        Me.Save = New MetroFramework.Controls.MetroCheckBox()
        Me.IssueLink = New MetroFramework.Controls.MetroLink()
        Me.PW = New MetroFramework.Controls.MetroTextBox()
        Me.Mail = New MetroFramework.Controls.MetroTextBox()
        Me.Delay = New System.Windows.Forms.Timer(Me.components)
        Me.DisplayTB = New MetroFramework.Controls.MetroTextBox()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.DisplayTB)
        Me.GroupBox3.Controls.Add(Me.Btn_Save)
        Me.GroupBox3.Controls.Add(Me.Save)
        Me.GroupBox3.Controls.Add(Me.IssueLink)
        Me.GroupBox3.Controls.Add(Me.PW)
        Me.GroupBox3.Controls.Add(Me.Mail)
        Me.GroupBox3.Location = New System.Drawing.Point(13, 60)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(480, 308)
        Me.GroupBox3.TabIndex = 38
        Me.GroupBox3.TabStop = False
        '
        'Btn_Save
        '
        Me.Btn_Save.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Btn_Save.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Save.FlatAppearance.BorderSize = 0
        Me.Btn_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Save.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.DialogNotFound_Submit
        Me.Btn_Save.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Btn_Save.Location = New System.Drawing.Point(165, 250)
        Me.Btn_Save.Name = "Btn_Save"
        Me.Btn_Save.Size = New System.Drawing.Size(150, 40)
        Me.Btn_Save.TabIndex = 3
        Me.Btn_Save.UseVisualStyleBackColor = False
        '
        'Save
        '
        Me.Save.AutoSize = True
        Me.Save.FontSize = MetroFramework.MetroCheckBoxSize.Medium
        Me.Save.Location = New System.Drawing.Point(177, 210)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(129, 19)
        Me.Save.TabIndex = 2
        Me.Save.Text = "Remember Login"
        Me.Save.UseSelectable = True
        '
        'IssueLink
        '
        Me.IssueLink.FontSize = MetroFramework.MetroLinkSize.Medium
        Me.IssueLink.Location = New System.Drawing.Point(166, 85)
        Me.IssueLink.Name = "IssueLink"
        Me.IssueLink.Size = New System.Drawing.Size(149, 23)
        Me.IssueLink.TabIndex = 41
        Me.IssueLink.Text = "See here: Github"
        Me.IssueLink.UseSelectable = True
        '
        'PW
        '
        '
        '
        '
        Me.PW.CustomButton.Image = Nothing
        Me.PW.CustomButton.Location = New System.Drawing.Point(138, 1)
        Me.PW.CustomButton.Name = ""
        Me.PW.CustomButton.Size = New System.Drawing.Size(21, 21)
        Me.PW.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.PW.CustomButton.TabIndex = 1
        Me.PW.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.PW.CustomButton.UseSelectable = True
        Me.PW.CustomButton.Visible = False
        Me.PW.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.PW.Lines = New String() {"Password"}
        Me.PW.Location = New System.Drawing.Point(165, 160)
        Me.PW.MaxLength = 32767
        Me.PW.Name = "PW"
        Me.PW.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PW.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.PW.SelectedText = ""
        Me.PW.SelectionLength = 0
        Me.PW.SelectionStart = 0
        Me.PW.ShortcutsEnabled = True
        Me.PW.Size = New System.Drawing.Size(160, 23)
        Me.PW.TabIndex = 1
        Me.PW.Text = "Password"
        Me.PW.UseSelectable = True
        Me.PW.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.PW.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'Mail
        '
        '
        '
        '
        Me.Mail.CustomButton.Image = Nothing
        Me.Mail.CustomButton.Location = New System.Drawing.Point(138, 1)
        Me.Mail.CustomButton.Name = ""
        Me.Mail.CustomButton.Size = New System.Drawing.Size(21, 21)
        Me.Mail.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.Mail.CustomButton.TabIndex = 1
        Me.Mail.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.Mail.CustomButton.UseSelectable = True
        Me.Mail.CustomButton.Visible = False
        Me.Mail.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.Mail.Lines = New String() {"E-Mail"}
        Me.Mail.Location = New System.Drawing.Point(165, 120)
        Me.Mail.MaxLength = 32767
        Me.Mail.Name = "Mail"
        Me.Mail.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Mail.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.Mail.SelectedText = ""
        Me.Mail.SelectionLength = 0
        Me.Mail.SelectionStart = 0
        Me.Mail.ShortcutsEnabled = True
        Me.Mail.Size = New System.Drawing.Size(160, 23)
        Me.Mail.TabIndex = 0
        Me.Mail.Text = "E-Mail"
        Me.Mail.UseSelectable = True
        Me.Mail.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.Mail.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'Delay
        '
        Me.Delay.Interval = 2000
        '
        'DisplayTB
        '
        '
        '
        '
        Me.DisplayTB.CustomButton.Image = Nothing
        Me.DisplayTB.CustomButton.Location = New System.Drawing.Point(416, 1)
        Me.DisplayTB.CustomButton.Name = ""
        Me.DisplayTB.CustomButton.Size = New System.Drawing.Size(51, 51)
        Me.DisplayTB.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.DisplayTB.CustomButton.TabIndex = 1
        Me.DisplayTB.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.DisplayTB.CustomButton.UseSelectable = True
        Me.DisplayTB.CustomButton.Visible = False
        Me.DisplayTB.Enabled = False
        Me.DisplayTB.FontSize = MetroFramework.MetroTextBoxSize.Medium
        Me.DisplayTB.Lines = New String() {"Due to Crunchyroll changes, we now need to fake a Nintendo Switch app login using" &
            " Crunchyroll account credentials"}
        Me.DisplayTB.Location = New System.Drawing.Point(10, 19)
        Me.DisplayTB.MaxLength = 32767
        Me.DisplayTB.Multiline = True
        Me.DisplayTB.Name = "DisplayTB"
        Me.DisplayTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.DisplayTB.ReadOnly = True
        Me.DisplayTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DisplayTB.SelectedText = ""
        Me.DisplayTB.SelectionLength = 0
        Me.DisplayTB.SelectionStart = 0
        Me.DisplayTB.ShortcutsEnabled = True
        Me.DisplayTB.Size = New System.Drawing.Size(462, 53)
        Me.DisplayTB.TabIndex = 39
        Me.DisplayTB.Text = "Due to Crunchyroll changes, we now need to fake a Nintendo Switch app login using" &
    " Crunchyroll account credentials"
        Me.DisplayTB.UseSelectable = True
        Me.DisplayTB.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.DisplayTB.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 379)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "LoginForm"
        Me.Text = "CRD-Login"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents GroupBox3 As GroupBox
    Friend WithEvents Delay As Timer
    Friend WithEvents PW As MetroFramework.Controls.MetroTextBox
    Friend WithEvents Mail As MetroFramework.Controls.MetroTextBox
    Friend WithEvents IssueLink As MetroFramework.Controls.MetroLink
    Friend WithEvents Save As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents Btn_Save As Button
    Friend WithEvents DisplayTB As MetroFramework.Controls.MetroTextBox
End Class
