<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class network_scan
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(network_scan))
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.ComboBox3 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox1 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox2 = New MetroFramework.Controls.MetroComboBox()
        Me.NetworkStatusLabel = New MetroFramework.Controls.MetroLabel()
        Me.Btn_min = New System.Windows.Forms.PictureBox()
        Me.Btn_Close = New System.Windows.Forms.PictureBox()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox2.SuspendLayout()
        CType(Me.Btn_min, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictureBox4
        '
        Me.pictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.pictureBox4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox4.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_button_download_default
        Me.pictureBox4.Location = New System.Drawing.Point(85, 304)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(530, 50)
        Me.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox4.TabIndex = 42
        Me.pictureBox4.TabStop = False
        '
        'groupBox2
        '
        Me.groupBox2.BackColor = System.Drawing.Color.Transparent
        Me.groupBox2.Controls.Add(Me.ComboBox3)
        Me.groupBox2.Controls.Add(Me.ComboBox1)
        Me.groupBox2.Controls.Add(Me.ComboBox2)
        Me.groupBox2.Controls.Add(Me.NetworkStatusLabel)
        Me.groupBox2.Location = New System.Drawing.Point(10, 63)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(680, 220)
        Me.groupBox2.TabIndex = 44
        Me.groupBox2.TabStop = False
        '
        'ComboBox3
        '
        Me.ComboBox3.Enabled = False
        Me.ComboBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.ItemHeight = 23
        Me.ComboBox3.Location = New System.Drawing.Point(15, 112)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(650, 29)
        Me.ComboBox3.TabIndex = 37
        Me.ComboBox3.UseSelectable = True
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.ItemHeight = 23
        Me.ComboBox1.Items.AddRange(New Object() {"Video Stream", "Audio Stream", "Subtile"})
        Me.ComboBox1.Location = New System.Drawing.Point(15, 20)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(650, 29)
        Me.ComboBox1.TabIndex = 1
        Me.ComboBox1.UseSelectable = True
        '
        'ComboBox2
        '
        Me.ComboBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.ItemHeight = 23
        Me.ComboBox2.Location = New System.Drawing.Point(15, 65)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(650, 29)
        Me.ComboBox2.TabIndex = 1
        Me.ComboBox2.UseSelectable = True
        '
        'NetworkStatusLabel
        '
        Me.NetworkStatusLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NetworkStatusLabel.BackColor = System.Drawing.Color.Transparent
        Me.NetworkStatusLabel.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.NetworkStatusLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.NetworkStatusLabel.ForeColor = System.Drawing.Color.Black
        Me.NetworkStatusLabel.Location = New System.Drawing.Point(15, 160)
        Me.NetworkStatusLabel.Name = "NetworkStatusLabel"
        Me.NetworkStatusLabel.Size = New System.Drawing.Size(651, 50)
        Me.NetworkStatusLabel.TabIndex = 36
        Me.NetworkStatusLabel.Text = "..."
        Me.NetworkStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Btn_min
        '
        Me.Btn_min.BackColor = System.Drawing.Color.Transparent
        Me.Btn_min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Btn_min.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_min.Image = CType(resources.GetObject("Btn_min.Image"), System.Drawing.Image)
        Me.Btn_min.Location = New System.Drawing.Point(567, 1)
        Me.Btn_min.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_min.Name = "Btn_min"
        Me.Btn_min.Size = New System.Drawing.Size(25, 25)
        Me.Btn_min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Btn_min.TabIndex = 73
        Me.Btn_min.TabStop = False
        '
        'Btn_Close
        '
        Me.Btn_Close.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Btn_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Close.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_close
        Me.Btn_Close.Location = New System.Drawing.Point(592, 1)
        Me.Btn_Close.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(40, 40)
        Me.Btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Btn_Close.TabIndex = 72
        Me.Btn_Close.TabStop = False
        '
        'network_scan
        '
        Me.ApplyImageInvert = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(700, 377)
        Me.Controls.Add(Me.Btn_min)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.pictureBox4)
        Me.Controls.Add(Me.groupBox2)
        Me.Name = "network_scan"
        Me.Padding = New System.Windows.Forms.Padding(10, 60, 20, 20)
        Me.Text = "Select a stream or subtitle"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox2.ResumeLayout(False)
        CType(Me.Btn_min, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Btn_Close, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents pictureBox4 As PictureBox
    Public WithEvents groupBox2 As GroupBox
    Public WithEvents NetworkStatusLabel As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroTextBox1 As MetroFramework.Controls.MetroTextBox
    Public WithEvents ComboBox1 As MetroFramework.Controls.MetroComboBox
    Public WithEvents ComboBox2 As MetroFramework.Controls.MetroComboBox
    Private WithEvents Btn_min As PictureBox
    Private WithEvents Btn_Close As PictureBox
    Public WithEvents ComboBox3 As MetroFramework.Controls.MetroComboBox
End Class
