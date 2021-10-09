Imports CefSharp

Public Class ErrorBrowser


    Private Sub ChromiumWebBrowser1_FrameLoadEnd(sender As Object, e As FrameLoadEndEventArgs) Handles ChromiumWebBrowser1.FrameLoadEnd
        If e.Frame.IsMain Then




            GetHTML()

        End If
    End Sub

    Async Sub GetHTML()
        Try

            Dim HTML As String = Await ChromiumWebBrowser1.GetSourceAsync
            Debug.WriteLine("got error browser html")
            Me.Invoke(New Action(Function() As Object

                                     ProcessFallbackHTML(HTML, ChromiumWebBrowser1.Address)
                                     'If Main.UserBowser = False Then
                                     '    Me.Close()
                                     'End If
                                     Return Nothing
                                 End Function))

        Catch ex As Exception

        End Try
    End Sub

    Public Sub ProcessFallbackHTML(ByVal document As String, ByVal Address As String)
        Dim localHTML As String = document
        Debug.WriteLine(Date.Now.ToString + "." + Date.Now.Millisecond.ToString)
        Debug.WriteLine(Address)

        If CBool(InStr(Address, "https://www.funimation.com/api/showexperience/")) Then

            Main.ErrorBrowserBackString = localHTML.Replace("<body>", "").Replace("</body>", "").Replace("<pre>", "").Replace("</pre>", "").Replace("</html>", "").Replace("<html><head></head><pre style=" + Chr(34) + "word-wrap: break-word; white-space: pre-wrap;" + Chr(34) + ">", "") '

            Me.Close()

        End If

    End Sub

    Private Sub ErrorBrowser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(-10000, -10000)
        If Main.ErrorBrowserString = "Funimation_showexperience" Then
            ChromiumWebBrowser1.Load(Main.ErrorBrowserUrl)
        End If
    End Sub
End Class