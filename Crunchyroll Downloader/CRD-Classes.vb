Public Class CRD_Classes

    Public Enum DownloadScopeEnum
        OldDefault = 0
        MergeAudio = 1
        SubsOnly = 2
        AudioOnly = 3
    End Enum
    Public Enum UpdateScope
        Skip = 0
        Info = 1
        InfoPre = 2
        Auto = 3
        Pre = 4
    End Enum

    Public Enum ConvertSubsEnum
        DisplayText = 0
        MP4CC_ISO_639_2 = 1
        Both = 2
    End Enum

End Class


#Region "CR"
Public Class CR_Beta_Stream
    'Public audioLanguage As String
    Public Url As String
    Public subLang As String
    Public Format As String
    'ByVal audioLanguage As String, 
    Public Sub New(ByVal subLang As String, ByVal Format As String, ByVal Url As String)
        Me.subLang = subLang
        Me.Url = Url.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/").Replace("\u0026", "&")
        Me.Format = Format
    End Sub
    'Me.audioLanguage,
    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.subLang, Me.Format, Me.Url)
    End Function

End Class


Public Class CR_Tokens

    Public access_token As String
    Public refresh_token As String
    Public expires_Unix As Integer

    Public Sub New(ByVal access_token As String, ByVal refresh_token As String, ByVal expires_Unix As Integer)
        Me.access_token = access_token
        Me.refresh_token = refresh_token
        Me.expires_Unix = expires_Unix
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.access_token, Me.refresh_token, Me.expires_Unix.ToString)
    End Function

End Class

Public Class CR_MediaVersion

    Public AudioLang As String
    Public guid As String

    Public Sub New(ByVal AudioLang As String, ByVal guid As String)
        Me.AudioLang = AudioLang
        Me.guid = guid
    End Sub
    'Me.audioLanguage,
    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.AudioLang, Me.guid)
    End Function

End Class

Public Class CR_Subtiles
    Public Url As String
    Public SubLangValue As String
    Public SubLangName As String
    Public DefaultSub As Boolean
    Public Index As String
    Public Sub New(ByVal SubLangValue As String, ByVal SubLangName As String, ByVal Url As String, ByVal Index As String, ByVal DefaultSub As Boolean)
        Me.SubLangValue = SubLangValue
        Me.SubLangName = SubLangName
        Me.Url = Url
        Me.Index = Index
        Me.DefaultSub = DefaultSub
    End Sub
    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}, {2}", Me.SubLangValue, Me.SubLangName, Me.Url, Me.Index, Me.DefaultSub.ToString)
    End Function

End Class
Public Class UrlJson

    Public Url As String
    Public Content As String
    Public Sub New(ByVal Url As String, ByVal Content As String)
        Me.Url = Url
        Me.Content = Content

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.Url, Me.Content)
    End Function
End Class

Public Class CR_Seasons

    Public Season As String
    Public guid As String
    Public audio_locale As String
    Public Auth As String
    Public Sub New(ByVal guid As String, ByVal audio_locale As String, ByVal Auth As String, ByVal Season As String)
        Me.Season = Season
        Me.guid = guid
        Me.audio_locale = audio_locale
        Me.Auth = Auth

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.guid, Me.audio_locale, Me.Season)
    End Function
End Class


#End Region


Public Class ServerResponse

    Public Type As String
    Public Content As String
    Public Sub New(ByVal Content As String, ByVal Type As String)
        Me.Content = Content
        Me.Type = Type

    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.Content, Me.Type)
    End Function
End Class


Public Class NameValuePair

    Public DisplayText As String
    Public MP4CC As String
    Public CR_Value As String
    Public FM_Value As String
    Public Sub New(ByVal DisplayText As String, ByVal MP4CC As String, ByVal CR_Value As String, ByVal FM_Value As String)
        Me.MP4CC = MP4CC
        Me.DisplayText = DisplayText
        Me.CR_Value = CR_Value
        Me.FM_Value = FM_Value
    End Sub

    Public Overrides Function ToString() As String
        Return String.Format("{0}, {1}", Me.DisplayText, Me.MP4CC, Me.CR_Value, Me.FM_Value)
    End Function
End Class