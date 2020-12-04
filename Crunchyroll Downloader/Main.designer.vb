<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
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
        Me.Btn_add = New System.Windows.Forms.PictureBox()
        Me.Btn_Close = New System.Windows.Forms.PictureBox()
        Me.Btn_Settings = New System.Windows.Forms.PictureBox()
        Me.Btn_Browser = New System.Windows.Forms.PictureBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Link = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.TheTextBox = New System.Windows.Forms.RichTextBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.MetroStyleExtender1 = New MetroFramework.Components.MetroStyleExtender(Me.components)
        Me.MetroStyleManager1 = New MetroFramework.Components.MetroStyleManager(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.Btn_add, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btn_Settings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btn_Browser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Btn_add
        '
        Me.Btn_add.BackColor = System.Drawing.Color.Transparent
        Me.Btn_add.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_add.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_add
        Me.Btn_add.Location = New System.Drawing.Point(20, 17)
        Me.Btn_add.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_add.Name = "Btn_add"
        Me.Btn_add.Size = New System.Drawing.Size(80, 35)
        Me.Btn_add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Btn_add.TabIndex = 24
        Me.Btn_add.TabStop = False
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.Btn_Close.Location = New System.Drawing.Point(790, 1)
        Me.Btn_Close.Margin = New System.Windows.Forms.Padding(0)
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
        Me.Btn_Settings.Location = New System.Drawing.Point(676, 17)
        Me.Btn_Settings.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_Settings.Name = "Btn_Settings"
        Me.Btn_Settings.Size = New System.Drawing.Size(80, 35)
        Me.Btn_Settings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Btn_Settings.TabIndex = 22
        Me.Btn_Settings.TabStop = False
        '
        'Btn_Browser
        '
        Me.Btn_Browser.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Browser.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Browser.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_browser
        Me.Btn_Browser.Location = New System.Drawing.Point(114, 18)
        Me.Btn_Browser.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_Browser.Name = "Btn_Browser"
        Me.Btn_Browser.Size = New System.Drawing.Size(80, 35)
        Me.Btn_Browser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Btn_Browser.TabIndex = 21
        Me.Btn_Browser.TabStop = False
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
        Me.ListView1.Location = New System.Drawing.Point(1, 71)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(0)
        Me.ListView1.MinimumSize = New System.Drawing.Size(798, 403)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(840, 546)
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
        Me.PictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox5.Location = New System.Drawing.Point(1, 65)
        Me.PictureBox5.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(840, 6)
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
        Me.TheTextBox.Location = New System.Drawing.Point(0, 652)
        Me.TheTextBox.Margin = New System.Windows.Forms.Padding(0)
        Me.TheTextBox.Name = "TheTextBox"
        Me.TheTextBox.Size = New System.Drawing.Size(840, 96)
        Me.TheTextBox.TabIndex = 69
        Me.TheTextBox.Text = ""
        Me.TheTextBox.Visible = False
        '
        'PictureBox6
        '
        Me.PictureBox6.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.balken
        Me.PictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox6.Location = New System.Drawing.Point(0, 618)
        Me.PictureBox6.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(840, 6)
        Me.PictureBox6.TabIndex = 68
        Me.PictureBox6.TabStop = False
        '
        'MetroStyleManager1
        '
        Me.MetroStyleManager1.Owner = Nothing
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.Main_top
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(1, 0)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(840, 1)
        Me.PictureBox1.TabIndex = 70
        Me.PictureBox1.TabStop = False
        '
        'Main
        '
        Me.ApplyImageInvert = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(842, 630)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TheTextBox)
        Me.Controls.Add(Me.PictureBox6)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Btn_add)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.Btn_Settings)
        Me.Controls.Add(Me.Btn_Browser)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(840, 630)
        Me.Name = "Main"
        Me.Resizable = True
        Me.Style = MetroFramework.MetroColorStyle.Orange
        Me.Text = "Crunchyroll Downloader"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        CType(Me.Btn_add, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btn_Settings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btn_Browser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MetroStyleManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents Btn_add As PictureBox
    Private WithEvents Btn_Close As PictureBox
    Private WithEvents Btn_Settings As PictureBox
    Private WithEvents Btn_Browser As PictureBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Link As ColumnHeader
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
    Friend WithEvents Timer3 As Timer
    Friend WithEvents TheTextBox As RichTextBox
    Friend WithEvents PictureBox6 As PictureBox
    Friend WithEvents MetroStyleExtender1 As MetroFramework.Components.MetroStyleExtender
    Friend WithEvents MetroStyleManager1 As MetroFramework.Components.MetroStyleManager
    Friend WithEvents PictureBox1 As PictureBox
End Class
