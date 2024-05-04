Option Strict On
Imports Crunchyroll_Downloader.CRD_Classes
Imports MetroFramework.Components

Public Class ErrorDialog

    Dim Manager As MetroStyleManager = Main.Manager


    Private Sub Reso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.TopMost = True
        Manager.Owner = Me
        Me.StyleManager = Manager

        pictureBox3.Image = Main.CloseImg
        ComboBox1.Text = Nothing

        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        Me.Location = New Point(CInt(Main.Location.X + Main.Width / 2 - Me.Width / 2), CInt(Main.Location.Y + Main.Height / 2 - Me.Height / 2))
        ComboBox1.Items.Clear()
        If Main.DialogTaskString = "Language" Then
            'CheckBox1.Visible = False
            StatusLabel.Text = Main.LabelLangNotFoundText
            Dim lang_avalibe As String() = Main.ResoNotFoundString.Split(New String() {Chr(34) + "hardsub_lang" + Chr(34) + ":"}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 1 To lang_avalibe.Count - 1
                Dim langsplit As String() = lang_avalibe(i).Split(New String() {","}, System.StringSplitOptions.RemoveEmptyEntries)

                ComboBox1.Items.Add(langsplit(0))
            Next
            SurroundingSub()
        ElseIf Main.DialogTaskString = "Language_CR_Beta" Then
            'CheckBox1.Visible = False
            StatusLabel.Text = Main.LabelLangNotFoundText
            Debug.WriteLine("ResoNotFoundString")
            Debug.WriteLine(Main.ResoNotFoundString)
            Debug.WriteLine("ResoNotFoundString")

            Dim lang_avalibe As String() = Main.ResoNotFoundString.Split(New String() {";"}, System.StringSplitOptions.RemoveEmptyEntries)

            For i As Integer = 0 To lang_avalibe.Count - 1
                ComboBox1.Items.Add(Main.ConvertSubValue(lang_avalibe(i), ConvertSubsEnum.DisplayText))
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
        ElseIf Main.DialogTaskString = "AudioOnlyResolution" Then
            StatusLabel.Text = "" 'Main.LabelResoNotFoundText
            ComboBox1.Items.Add("x240,")
            ComboBox1.Items.Add("x360,")
            ComboBox1.Items.Add("x480,")
            ComboBox1.Items.Add("x720,")
            ComboBox1.Items.Add("x1080,")

        End If

        'check for the first option and error if there are none
        If ComboBox1.Items.Count > 0 Then
            ComboBox1.SelectedIndex = 0
        Else
            MsgBox("Error, no available options found!", MsgBoxStyle.Information)
        End If

        Delay.Enabled = True
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
                e.Graphics.DrawString(ComboBox1.Items(e.Index).ToString, e.Font, Brushes.Black, e.Bounds, st)

            End Using
        End If

    End Sub

    Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
        If ComboBox1.SelectedItem.ToString = Nothing Then
        Else
            If Main.DialogTaskString = "Language_CR_Beta" Then
                Main.ResoBackString = DisplayToHardSubValues(ComboBox1.SelectedItem.ToString)
                'MsgBox(Main.ResoBackString)
                Main.UserCloseDialog = False
                Me.Close()
            Else
                Main.ResoBackString = ComboBox1.SelectedItem.ToString
                Main.UserCloseDialog = False
                Me.Close()
            End If
        End If
    End Sub

    Public Function DisplayToHardSubValues(ByVal HardSub As String) As String
        For i As Integer = 0 To Main.LangValueEnum.Count - 1
            If Main.LangValueEnum.Item(i).DisplayText = HardSub Then ' does not find "No Hardsubs" but default is correct here anyway.
                Return Main.LangValueEnum.Item(i).CR_Value
            End If
        Next

        Return "null" ' null as text because CR does so...

    End Function

    Private Sub PictureBox9_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox9.MouseEnter
        PictureBox9.Image = My.Resources.DialogNotFound_Submit_hover
    End Sub

    Private Sub PictureBox9_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox9.MouseLeave
        PictureBox9.Image = My.Resources.DialogNotFound_Submit
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles pictureBox3.Click
        Main.UserCloseDialog = True
        Me.Close()
    End Sub

    Private Sub Btn_Close_MouseEnter(sender As Object, e As EventArgs) Handles pictureBox3.MouseEnter

        pictureBox3.Image = My.Resources.main_del
    End Sub

    Private Sub Btn_Close_MouseLeave(sender As Object, e As EventArgs) Handles pictureBox3.MouseLeave

        pictureBox3.Image = Main.CloseImg
    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        PictureBox9.Enabled = True
        PictureBox9.Cursor = Cursors.Hand
    End Sub

    Private Sub Delay_Tick(sender As Object, e As EventArgs) Handles Delay.Tick

        Try
            Dim hwnd As IntPtr = FindWindow(Nothing, Me.Text)
            SetForegroundWindow(hwnd)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        'MsgBox(Me.Text)
        Delay.Enabled = False
    End Sub
End Class