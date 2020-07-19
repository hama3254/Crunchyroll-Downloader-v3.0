Public Class Reso
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
        'ButtonResoNotFound.Text = Main.ButtonResoNotFoundText
        Me.Icon = My.Resources.icon
        Me.Location = New Point(Main.Location.X + Main.Width / 2 - Me.Width / 2, Main.Location.Y + Main.Height / 2 - Me.Height / 2)
        ComboBox1.Items.Clear()
        If Main.DialogTaskString = "Language" Then
            'CheckBox1.Visible = False
            StatusLabel.Text = Main.LabelLangNotFoundText
            Dim lang_avalibe As String() = Main.ResoNotFoundString.Split(New String() {Chr(34) + "hardsub_lang" + Chr(34) + ":"}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 1 To lang_avalibe.Count - 1
                Dim langsplit As String() = lang_avalibe(i).Split(New [Char]() {","})
                ComboBox1.Items.Add(langsplit(0))
            Next
            SurroundingSub()
        ElseIf Main.DialogTaskString = "Resolution" Then
            StatusLabel.Text = Main.LabelResoNotFoundText
            Dim Reso_avaible1 As String() = Main.ResoNotFoundString.Split(New String() {"RESOLUTION="}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 1 To Reso_avaible1.Count - 1
                Dim Reso_avaible2 As String() = Reso_avaible1(i).Split(New [Char]() {Chr(44)})
                ComboBox1.Items.Add(Reso_avaible2(0))
            Next
            SurroundingSub()
            Try
                ComboBox1.SelectedIndex = 0
            Catch ex As Exception
            End Try

        ElseIf Main.DialogTaskString = "Funimation_Resolution" Then
            StatusLabel.Text = Main.LabelResoNotFoundText
            Dim ResoList As New List(Of String)
            Dim m3u8_split As String() = Main.ResoNotFoundString.Split(New String() {vbLf}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To m3u8_split.Count - 1
                If InStr(m3u8_split(i), "RESOLUTION=") Then
                    ResoList.Add(m3u8_split(i))
                End If
            Next

            Dim Reso_avaible1 As String() = Main.ResoNotFoundString.Split(New String() {"RESOLUTION="}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To ResoList.Count - 1
                Dim Reso_avaible As String() = ResoList.Item(i).Split(New String() {"RESOLUTION="}, System.StringSplitOptions.RemoveEmptyEntries)
                ComboBox1.Items.Add(Reso_avaible(1))
            Next
            SurroundingSub()
            Try
                ComboBox1.SelectedIndex = 0
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub SurroundingSub()
        Dim list As List(Of Object) = New List(Of Object)()

        For Each o As Object In ComboBox1.Items

            If Not list.Contains(o) Then
                list.Add(o)
            End If
        Next

        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange(list.ToArray())
    End Sub


    Private Sub ComboBox1_DrawItem(sender As Object, e As DrawItemEventArgs) Handles ComboBox1.DrawItem

        ComboBox1.BackColor = Color.White
        If e.Index >= 0 Then
            Using st As New StringFormat With {.Alignment = StringAlignment.Center}
                ' e.DrawBackground()
                ' e.DrawFocusRectangle()
                e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds)
                e.Graphics.DrawString(sender.Items(e.Index).ToString, e.Font, Brushes.Black, e.Bounds, st)

            End Using
        End If

    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        If ComboBox1.SelectedItem.ToString = Nothing Then
        Else
            Main.ResoBackString = ComboBox1.SelectedItem.ToString
            Main.UserCloseDialog = False
            Me.Close()
        End If
    End Sub

    Private Sub PictureBox9_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox9.MouseEnter
        PictureBox9.Image = My.Resources.DialogNotFound_Submit_hover
    End Sub

    Private Sub PictureBox9_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox9.MouseLeave
        PictureBox9.Image = My.Resources.DialogNotFound_Submit
    End Sub

    Private Sub pictureBox3_Click(sender As Object, e As EventArgs) Handles pictureBox3.Click
        Main.UserCloseDialog = True
        Me.Close()
    End Sub

    Private Sub pictureBox3_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox3.MouseEnter
        pictureBox3.BackColor = SystemColors.Control
    End Sub

    Private Sub pictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox3.MouseLeave
        pictureBox3.BackColor = Color.Transparent
    End Sub

End Class