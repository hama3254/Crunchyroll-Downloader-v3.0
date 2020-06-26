Public Class Startup

    Private Sub Startup_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Me.Icon = My.Resources.icon
        Me.Location = New Point(Main.Location.X + Main.Width / 2 - Me.Width / 2, Main.Location.Y + Main.Height / 2 - Me.Height / 2)

        'If Main.CreditsOnly = True Then
        '    PictureBox1.Visible = True
        'Else
        '    PictureBox1.Visible = False
        '    Timer1.Start()
        '    Opacity = 0
        '    Main.Opacity = 0
        'End If
        Label6.Text = "Version " + Application.ProductVersion.ToString

    End Sub

    Private Sub fadeIn()
        If Opacity >= 1 Then
            Timer1.[Stop]()
            Timer2.Start()
        Else
            Opacity += 0.09
        End If
    End Sub

    Private Sub fadeout()
        If Opacity <= 0 Then
            Timer2.[Stop]()
            Main.Opacity = 100
            Me.Close()
        Else
            Opacity -= 0.09
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        fadeIn()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        fadeout()
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)
        Process.Start("https://www.codeproject.com/Articles/60382/AsyncWorker-A-Typesafe-BackgroundWorker-and-about")
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Process.Start("https://www.ffmpeg.org/about.html")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.BackColor = SystemColors.Control
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.BackColor = Color.Transparent
    End Sub
#Region " Move Form "

    ' [ Move Form ]
    '
    ' // By Elektro 

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Process.Start("https://bitbucket.org/geckofx/geckofx-60.0/src/default/")
    End Sub


#End Region
End Class
