<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
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
        Me.Btn_add = New System.Windows.Forms.PictureBox()
        Me.Btn_Close = New System.Windows.Forms.PictureBox()
        Me.Btn_Settings = New System.Windows.Forms.PictureBox()
        Me.Btn_Browser = New System.Windows.Forms.PictureBox()
        Me.StatusMainForm = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Link = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.TheTextBox = New System.Windows.Forms.RichTextBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        CType(Me.Btn_add, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btn_Settings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btn_Browser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Btn_add
        '
        Me.Btn_add.BackColor = System.Drawing.Color.Transparent
        Me.Btn_add.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_add.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_add
        Me.Btn_add.Location = New System.Drawing.Point(12, 5)
        Me.Btn_add.Name = "Btn_add"
        Me.Btn_add.Size = New System.Drawing.Size(100, 48)
        Me.Btn_add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Btn_add.TabIndex = 24
        Me.Btn_add.TabStop = False
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.Btn_Close.Location = New System.Drawing.Point(789, 1)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(50, 40)
        Me.Btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Btn_Close.TabIndex = 23
        Me.Btn_Close.TabStop = False
        '
        'Btn_Settings
        '
        Me.Btn_Settings.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Settings.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Settings.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_settings
        Me.Btn_Settings.Location = New System.Drawing.Point(656, 10)
        Me.Btn_Settings.Name = "Btn_Settings"
        Me.Btn_Settings.Size = New System.Drawing.Size(100, 40)
        Me.Btn_Settings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Btn_Settings.TabIndex = 22
        Me.Btn_Settings.TabStop = False
        '
        'Btn_Browser
        '
        Me.Btn_Browser.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Browser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Browser.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_browser
        Me.Btn_Browser.Location = New System.Drawing.Point(145, 10)
        Me.Btn_Browser.Name = "Btn_Browser"
        Me.Btn_Browser.Size = New System.Drawing.Size(100, 40)
        Me.Btn_Browser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Btn_Browser.TabIndex = 21
        Me.Btn_Browser.TabStop = False
        '
        'StatusMainForm
        '
        Me.StatusMainForm.BackColor = System.Drawing.Color.Transparent
        Me.StatusMainForm.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusMainForm.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.StatusMainForm.Location = New System.Drawing.Point(251, 17)
        Me.StatusMainForm.Name = "StatusMainForm"
        Me.StatusMainForm.Size = New System.Drawing.Size(370, 24)
        Me.StatusMainForm.TabIndex = 66
        Me.StatusMainForm.Text = "Crunchyroll Downloader"
        Me.StatusMainForm.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.SystemColors.Control
        Me.ListView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Link})
        Me.ListView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 93.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.ForeColor = System.Drawing.Color.Black
        Me.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(1, 64)
        Me.ListView1.MinimumSize = New System.Drawing.Size(800, 400)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(838, 487)
        Me.ListView1.TabIndex = 57
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'Link
        '
        Me.Link.Text = "Link"
        Me.Link.Width = 818
        '
        'PictureBox5
        '
        Me.PictureBox5.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.balken
        Me.PictureBox5.Location = New System.Drawing.Point(1, 56)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(838, 8)
        Me.PictureBox5.TabIndex = 67
        Me.PictureBox5.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 1000
        '
        'Timer3
        '
        Me.Timer3.Enabled = True
        Me.Timer3.Interval = 1000
        '
        'TheTextBox
        '
        Me.TheTextBox.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.TheTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TheTextBox.Location = New System.Drawing.Point(1, 558)
        Me.TheTextBox.Name = "TheTextBox"
        Me.TheTextBox.Size = New System.Drawing.Size(838, 111)
        Me.TheTextBox.TabIndex = 69
        Me.TheTextBox.Text = ""
        '
        'PictureBox6
        '
        Me.PictureBox6.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.balken
        Me.PictureBox6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox6.Location = New System.Drawing.Point(1, 549)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(838, 8)
        Me.PictureBox6.TabIndex = 68
        Me.PictureBox6.TabStop = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.main_background
        Me.ClientSize = New System.Drawing.Size(840, 558)
        Me.Controls.Add(Me.TheTextBox)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.StatusMainForm)
        Me.Controls.Add(Me.Btn_add)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.Btn_Settings)
        Me.Controls.Add(Me.Btn_Browser)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "crunchyroll downloader"
        CType(Me.Btn_add, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btn_Settings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btn_Browser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Btn_add As PictureBox
    Private WithEvents Btn_Close As PictureBox
    Private WithEvents Btn_Settings As PictureBox
    Private WithEvents Btn_Browser As PictureBox
    Friend WithEvents StatusMainForm As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Link As ColumnHeader
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Timer3 As Timer
    Friend WithEvents TheTextBox As RichTextBox
    Friend WithEvents PictureBox6 As PictureBox
End Class
