Option Strict On

Imports System.IO
Imports System.IO.Compression
Module ProcessSleep
    Public Enum ThreadAccess As Integer
        TERMINATE = (&H1)
        SUSPEND_RESUME = (&H2)
        GET_CONTEXT = (&H8)
        SET_CONTEXT = (&H10)
        SET_INFORMATION = (&H20)
        QUERY_INFORMATION = (&H40)
        SET_THREAD_TOKEN = (&H80)
        IMPERSONATE = (&H100)
        DIRECT_IMPERSONATION = (&H200)
    End Enum

    Public Declare Function OpenThread Lib "kernel32.dll" (ByVal dwDesiredAccess As ThreadAccess, ByVal bInheritHandle As Boolean, ByVal dwThreadId As UInteger) As IntPtr
    Public Declare Function SuspendThread Lib "kernel32.dll" (ByVal hThread As IntPtr) As UInteger
    Public Declare Function ResumeThread Lib "kernel32.dll" (ByVal hThread As IntPtr) As UInteger
    Public Declare Function CloseHandle Lib "kernel32.dll" (ByVal hHandle As IntPtr) As Boolean

    Public Sub Pause(ByVal pau As Single)

        'Programmausführung verzögern *******************************************************

        Dim start, finish As Single
        start = CSng(Microsoft.VisualBasic.DateAndTime.Timer)

        finish = start + pau
        Do While Microsoft.VisualBasic.DateAndTime.Timer < finish
            Application.DoEvents()
        Loop

    End Sub

    Public Sub Pause_ms(ByVal ms As Integer)

        'Programmausführung verzögern *******************************************************
        Dim stopWatch As New Stopwatch()
        stopWatch.Start()

        Do Until stopWatch.Elapsed.TotalMilliseconds > ms
            Application.DoEvents()
        Loop

        stopWatch.Stop()
    End Sub

    Public Function DecompressString(ByVal bytes As Byte()) As String

        Using ms = New MemoryStream(bytes)
            Using ds = New GZipStream(ms, CompressionMode.Decompress)
                Using sr = New StreamReader(ds)

                    Return sr.ReadToEnd()

                End Using
            End Using
        End Using

    End Function


End Module
