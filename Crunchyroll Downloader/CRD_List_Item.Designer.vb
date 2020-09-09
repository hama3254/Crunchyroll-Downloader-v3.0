<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CRD_List_Item
    Inherits System.Windows.Forms.UserControl

    'UserControl1 überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.PB_Thumbnail = New System.Windows.Forms.PictureBox()
        Me.bt_pause = New System.Windows.Forms.PictureBox()
        Me.bt_del = New System.Windows.Forms.PictureBox()
        Me.Label_Anime = New System.Windows.Forms.Label()
        Me.Label_website = New System.Windows.Forms.Label()
        Me.Label_Reso = New System.Windows.Forms.Label()
        Me.Label_Hardsub = New System.Windows.Forms.Label()
        Me.Label_percent = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PB_Thumbnail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bt_pause, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bt_del, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PB_Thumbnail
        '
        Me.PB_Thumbnail.BackColor = System.Drawing.SystemColors.Desktop
        Me.PB_Thumbnail.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.main_del
        Me.PB_Thumbnail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PB_Thumbnail.Location = New System.Drawing.Point(11, 20)
        Me.PB_Thumbnail.Name = "PB_Thumbnail"
        Me.PB_Thumbnail.Size = New System.Drawing.Size(168, 95)
        Me.PB_Thumbnail.TabIndex = 0
        Me.PB_Thumbnail.TabStop = False
        '
        'bt_pause
        '
        Me.bt_pause.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.main_pause
        Me.bt_pause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bt_pause.Location = New System.Drawing.Point(740, 15)
        Me.bt_pause.Name = "bt_pause"
        Me.bt_pause.Size = New System.Drawing.Size(25, 20)
        Me.bt_pause.TabIndex = 1
        Me.bt_pause.TabStop = False
        '
        'bt_del
        '
        Me.bt_del.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.main_del
        Me.bt_del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.bt_del.Location = New System.Drawing.Point(775, 10)
        Me.bt_del.Name = "bt_del"
        Me.bt_del.Size = New System.Drawing.Size(35, 29)
        Me.bt_del.TabIndex = 2
        Me.bt_del.TabStop = False
        '
        'Label_Anime
        '
        Me.Label_Anime.AutoSize = True
        Me.Label_Anime.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Anime.Location = New System.Drawing.Point(195, 42)
        Me.Label_Anime.Name = "Label_Anime"
        Me.Label_Anime.Size = New System.Drawing.Size(270, 19)
        Me.Label_Anime.TabIndex = 3
        Me.Label_Anime.Text = "Anime Titel, Season, Episode "
        '
        'Label_website
        '
        Me.Label_website.AutoSize = True
        Me.Label_website.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label_website.Location = New System.Drawing.Point(195, 15)
        Me.Label_website.Name = "Label_website"
        Me.Label_website.Size = New System.Drawing.Size(72, 19)
        Me.Label_website.TabIndex = 4
        Me.Label_website.Text = "website"
        '
        'Label_Reso
        '
        Me.Label_Reso.AutoSize = True
        Me.Label_Reso.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Reso.Location = New System.Drawing.Point(195, 101)
        Me.Label_Reso.Name = "Label_Reso"
        Me.Label_Reso.Size = New System.Drawing.Size(99, 19)
        Me.Label_Reso.TabIndex = 5
        Me.Label_Reso.Text = "Resolution"
        '
        'Label_Hardsub
        '
        Me.Label_Hardsub.AutoSize = True
        Me.Label_Hardsub.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Hardsub.Location = New System.Drawing.Point(300, 101)
        Me.Label_Hardsub.Name = "Label_Hardsub"
        Me.Label_Hardsub.Size = New System.Drawing.Size(126, 19)
        Me.Label_Hardsub.TabIndex = 6
        Me.Label_Hardsub.Text = "Hardsub Label"
        '
        'Label_percent
        '
        Me.Label_percent.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label_percent.Font = New System.Drawing.Font("Consolas", 12.0!)
        Me.Label_percent.Location = New System.Drawing.Point(455, 101)
        Me.Label_percent.Name = "Label_percent"
        Me.Label_percent.Size = New System.Drawing.Size(355, 19)
        Me.Label_percent.TabIndex = 7
        Me.Label_percent.Text = "Status Label : speed, size and percent"
        Me.Label_percent.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(195, 70)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(615, 20)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 8
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'CRD_List_Item
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Crunchyroll_Downloader.My.Resources.Resources.backgroud
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label_percent)
        Me.Controls.Add(Me.Label_Hardsub)
        Me.Controls.Add(Me.Label_Reso)
        Me.Controls.Add(Me.Label_website)
        Me.Controls.Add(Me.Label_Anime)
        Me.Controls.Add(Me.bt_del)
        Me.Controls.Add(Me.bt_pause)
        Me.Controls.Add(Me.PB_Thumbnail)
        Me.Name = "CRD_List_Item"
        Me.Size = New System.Drawing.Size(838, 142)
        CType(Me.PB_Thumbnail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bt_pause, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bt_del, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PB_Thumbnail As PictureBox
    Friend WithEvents bt_pause As PictureBox
    Friend WithEvents bt_del As PictureBox
    Friend WithEvents Label_Anime As Label
    Friend WithEvents Label_website As Label
    Friend WithEvents Label_Reso As Label
    Friend WithEvents Label_Hardsub As Label
    Friend WithEvents Label_percent As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Timer1 As Timer
End Class
