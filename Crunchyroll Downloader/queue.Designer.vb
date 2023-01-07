<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Queue
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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.UpdateListTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Btn_Close = New System.Windows.Forms.Button()
        Me.Btn_min = New System.Windows.Forms.Button()
        Me.RunQueue = New MetroFramework.Controls.MetroToggle()
        Me.Label1 = New MetroFramework.Controls.MetroLabel()
        Me.RunQueueTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 20
        Me.ListBox1.Location = New System.Drawing.Point(25, 65)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(700, 304)
        Me.ListBox1.TabIndex = 0
        '
        'UpdateListTimer
        '
        Me.UpdateListTimer.Enabled = True
        Me.UpdateListTimer.Interval = 1000
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
        Me.Btn_Close.Location = New System.Drawing.Point(695, 4)
        Me.Btn_Close.Name = "Btn_Close"
        Me.Btn_Close.Size = New System.Drawing.Size(35, 35)
        Me.Btn_Close.TabIndex = 49
        Me.Btn_Close.UseVisualStyleBackColor = False
        '
        'Btn_min
        '
        Me.Btn_min.BackColor = System.Drawing.Color.Transparent
        Me.Btn_min.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Btn_min.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_min.FlatAppearance.BorderSize = 0
        Me.Btn_min.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Btn_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_min.ForeColor = System.Drawing.Color.Transparent
        Me.Btn_min.Image = Global.Crunchyroll_Downloader.My.Resources.Resources.main_mini
        Me.Btn_min.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Btn_min.Location = New System.Drawing.Point(652, 4)
        Me.Btn_min.Name = "Btn_min"
        Me.Btn_min.Size = New System.Drawing.Size(30, 30)
        Me.Btn_min.TabIndex = 48
        Me.Btn_min.UseVisualStyleBackColor = False
        '
        'RunQueue
        '
        Me.RunQueue.Location = New System.Drawing.Point(325, 415)
        Me.RunQueue.Name = "RunQueue"
        Me.RunQueue.Size = New System.Drawing.Size(96, 20)
        Me.RunQueue.TabIndex = 50
        Me.RunQueue.Text = "Aus"
        Me.RunQueue.UseSelectable = True
        '
        'Label1
        '
        Me.Label1.FontWeight = MetroFramework.MetroLabelWeight.Regular
        Me.Label1.Location = New System.Drawing.Point(25, 385)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(700, 22)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Process Queue"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RunQueueTimer
        '
        Me.RunQueueTimer.Interval = 2500
        '
        'Queue
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(750, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RunQueue)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Btn_Close)
        Me.Controls.Add(Me.Btn_min)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Queue"
        Me.Text = "Queue"
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents ListBox1 As ListBox
    Friend WithEvents UpdateListTimer As Timer
    Friend WithEvents Btn_Close As Button
    Friend WithEvents Btn_min As Button
    Friend WithEvents RunQueue As MetroFramework.Controls.MetroToggle
    Friend WithEvents Label1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents RunQueueTimer As Timer
End Class
