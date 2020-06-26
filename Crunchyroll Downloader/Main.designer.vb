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
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.pictureBox3 = New System.Windows.Forms.PictureBox()
        Me.pictureBox2 = New System.Windows.Forms.PictureBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.FontLabel2 = New System.Windows.Forms.Label()
        Me.FontLabel = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Link = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictureBox4
        '
        Me.pictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox4.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_add
        Me.pictureBox4.Location = New System.Drawing.Point(23, 18)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(60, 24)
        Me.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox4.TabIndex = 24
        Me.pictureBox4.TabStop = False
        '
        'pictureBox3
        '
        Me.pictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox3.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.pictureBox3.Location = New System.Drawing.Point(789, 1)
        Me.pictureBox3.Name = "pictureBox3"
        Me.pictureBox3.Size = New System.Drawing.Size(50, 40)
        Me.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox3.TabIndex = 23
        Me.pictureBox3.TabStop = False
        '
        'pictureBox2
        '
        Me.pictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox2.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_settings
        Me.pictureBox2.Location = New System.Drawing.Point(702, 18)
        Me.pictureBox2.Name = "pictureBox2"
        Me.pictureBox2.Size = New System.Drawing.Size(60, 24)
        Me.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox2.TabIndex = 22
        Me.pictureBox2.TabStop = False
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox1.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_browser
        Me.pictureBox1.Location = New System.Drawing.Point(116, 18)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(60, 24)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pictureBox1.TabIndex = 21
        Me.pictureBox1.TabStop = False
        '
        'FontLabel2
        '
        Me.FontLabel2.AutoSize = True
        Me.FontLabel2.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FontLabel2.Location = New System.Drawing.Point(587, 1)
        Me.FontLabel2.Name = "FontLabel2"
        Me.FontLabel2.Size = New System.Drawing.Size(99, 19)
        Me.FontLabel2.TabIndex = 65
        Me.FontLabel2.Text = "Demo Label"
        Me.FontLabel2.Visible = False
        '
        'FontLabel
        '
        Me.FontLabel.AutoSize = True
        Me.FontLabel.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FontLabel.Location = New System.Drawing.Point(587, 31)
        Me.FontLabel.Name = "FontLabel"
        Me.FontLabel.Size = New System.Drawing.Size(99, 19)
        Me.FontLabel.TabIndex = 66
        Me.FontLabel.Text = "Demo Label"
        Me.FontLabel.Visible = False
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
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.main_background
        Me.ClientSize = New System.Drawing.Size(840, 552)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.FontLabel)
        Me.Controls.Add(Me.FontLabel2)
        Me.Controls.Add(Me.pictureBox4)
        Me.Controls.Add(Me.pictureBox3)
        Me.Controls.Add(Me.pictureBox2)
        Me.Controls.Add(Me.pictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "crunchyroll downloader"
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents pictureBox4 As PictureBox
    Private WithEvents pictureBox3 As PictureBox
    Private WithEvents pictureBox2 As PictureBox
    Private WithEvents pictureBox1 As PictureBox
    Friend WithEvents FontLabel2 As Label
    Friend WithEvents FontLabel As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Link As ColumnHeader
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Timer2 As Timer
End Class
