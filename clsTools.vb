Public Class clsTools
    Friend Shared Function makeText(item As eigentuemerItem) As String
        Dim text As String = ""
        Try
            text = text & "Flurstück: " & item.gemarkung & "  Flur: " & item.flur & ",  " & item.zaehler & "/" & item.nenner & Environment.NewLine
            text = text & "Eigentümer*in:" & Environment.NewLine
            text = text & item.adress & Environment.NewLine
            text = text & item.buch & Environment.NewLine
            Return text
        Catch ex As Exception
            l("fehler makeText " & ex.ToString)
            Return Nothing
        End Try
    End Function
    Shared Sub l(tt As String)
        My.Log.WriteEntry(tt)
    End Sub

    Sub setLogfile(dir As String)
        'MsgBox(strGlobals.localDocumentCacheRoot)
        With My.Log.DefaultFileLogWriter
#If DEBUG Then
            '.CustomLocation = mgisUserRoot & "logs\"
#Else
#End If
            '.CustomLocation = My.Computer.FileSystem.SpecialDirectories.Temp & "\mgis_logs\"
            '.CustomLocation = "c:\kreisoffenbach\klenk" & "\logs\"        
            .CustomLocation = dir
            .BaseFileName = Environment.UserName & "_" & Format(Now, "yyyyMMddhhmmss")
            '.BaseFileName = "klenk_" '& Format(Now, "yyyyMMddhhmmss")
            .AutoFlush = False
            .Append = False
        End With
    End Sub

End Class
