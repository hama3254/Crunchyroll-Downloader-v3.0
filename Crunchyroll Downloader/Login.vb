Imports System.IO
Imports System.Net
Imports System.Text

Public Class Login
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
#End Region

    Private Sub Reso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolTip1.SetToolTip(PictureBox9, My.Resources.US_ToolTip)
        Me.Icon = My.Resources.icon
        Me.Location = New Point(Main.Location.X + Main.Width / 2 - Me.Width / 2, Main.Location.Y + Main.Height / 2 - Me.Height / 2)
    End Sub




    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim Request0 As HttpWebRequest = CType(WebRequest.Create("https://api.crunchyroll.com/login.0.json"), HttpWebRequest)
        Request0.Method = "POST"
        Request0.ContentType = "application/x-www-form-urlencoded"
        If GeckoFX.keks = Nothing Then
            MsgBox("No active Cookie found!", MsgBoxStyle.Exclamation)
            Me.Close()
            Exit Sub

            'GeckoFX.WebBrowser1.Navigate("https://www.crunchyroll.com/")
            'Pause(2)
            'Dim c As String = GeckoFX.WebBrowser1.Document.Cookie.ToString
            'MsgBox(c)
            'Dim cookieGrapp7() As String = c.Split(New String() {"session_id="}, System.StringSplitOptions.RemoveEmptyEntries)
            'Dim cookieGrapp8() As String = cookieGrapp7(1).Split(New String() {";"}, System.StringSplitOptions.RemoveEmptyEntries)
            'MsgBox(cookieGrapp8(0))
            'GeckoFX.keks = cookieGrapp8(0)
            ''Console.WriteLine()
        End If
        Dim Post0 As String = "account=" + LoginID.Text + "&password=" + Password.Text + "&session_id=" + GeckoFX.keks
        Dim byteArray0() As Byte = Encoding.UTF8.GetBytes(Post0)
        Request0.ContentLength = byteArray0.Length
        Dim DataStream0 As Stream = Request0.GetRequestStream()
        DataStream0.Write(byteArray0, 0, byteArray0.Length)
        DataStream0.Close()
        Dim Response0 As HttpWebResponse = Request0.GetResponse()
        DataStream0 = Response0.GetResponseStream()
        Dim reader0 As New StreamReader(DataStream0)
        Dim ServerResponse0 As String = reader0.ReadToEnd()
        If InStr(ServerResponse0, My.Resources.LoginSuccess) Then
        Else
            MsgBox(Post0)
            MsgBox(ServerResponse0)
        End If
        reader0.Close()
        DataStream0.Close()
        Response0.Close()
        GeckoFX.WebBrowser1.Navigate("https://www.crunchyroll.com/")
        Me.Close()
        'WebBrowser1.LoadHtml(ServerResponse0)
    End Sub

    Private Sub PictureBox9_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox9.MouseEnter
        PictureBox9.Image = My.Resources.DialogNotFound_Submit_hover
    End Sub

    Private Sub PictureBox9_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox9.MouseLeave
        PictureBox9.Image = My.Resources.DialogNotFound_Submit
    End Sub

    Private Sub pictureBox3_Click(sender As Object, e As EventArgs) Handles pictureBox3.Click
        'Main.UserCloseDialog = True
        GeckoFX.WebBrowser1.Navigate("https://www.crunchyroll.com/")
        Me.Close()
    End Sub

    Private Sub pictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox3.MouseEnter
        pictureBox3.BackColor = SystemColors.Control
    End Sub

    Private Sub pictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox3.MouseLeave
        pictureBox3.BackColor = Color.Transparent
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        PictureBox1.Image = My.Resources.LoginSkipHover
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.LoginSkip
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        GeckoFX.WebBrowser1.Navigate("https://www.crunchyroll.com/")
        Me.Close()
    End Sub
End Class