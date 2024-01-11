<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CheckBoxComboBox
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
        Me.Animation = New System.Windows.Forms.Timer(Me.components)
        Me.BGP = New MetroFramework.Controls.MetroPanel()
        Me.SuspendLayout()
        '
        'Animation
        '
        Me.Animation.Interval = 10
        '
        'BGP
        '
        Me.BGP.AutoScroll = True
        Me.BGP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BGP.HorizontalScrollbar = True
        Me.BGP.HorizontalScrollbarBarColor = True
        Me.BGP.HorizontalScrollbarHighlightOnWheel = False
        Me.BGP.HorizontalScrollbarSize = 10
        Me.BGP.Location = New System.Drawing.Point(5, 5)
        Me.BGP.Name = "BGP"
        Me.BGP.Size = New System.Drawing.Size(310, 317)
        Me.BGP.TabIndex = 0
        Me.BGP.VerticalScrollbar = True
        Me.BGP.VerticalScrollbarBarColor = True
        Me.BGP.VerticalScrollbarHighlightOnWheel = False
        Me.BGP.VerticalScrollbarSize = 10
        '
        'CheckBoxComboBox
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle
        Me.ClientSize = New System.Drawing.Size(320, 327)
        Me.ControlBox = False
        Me.Controls.Add(Me.BGP)
        Me.DisplayHeader = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "CheckBoxComboBox"
        Me.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Animation As Timer
    Friend WithEvents BGP As MetroFramework.Controls.MetroPanel
End Class
