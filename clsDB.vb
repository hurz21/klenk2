
Imports System.IO
Imports System.Data
Imports Microsoft.Data.Sqlite

Public Class clsDB
    'Private Property SQLitecnStr As String = "Data Source=D:\avs_lcube\alkis\klenk\klenkGmbh\klenk.sqlite ; "
    Private Property SQLitecnStr As String = "Data Source=klenk.sqlite ; "
    'Private Property SQLitecnStr As String = "Data Source=test.dll ; "
    Sub New()

    End Sub
    Sub l(tt As String)
        My.Log.WriteEntry(tt)
    End Sub
    Friend Function getRecordCollection(sql As String) As List(Of eigentuemerItem)
        l("getRecordCollection   ")
        Dim aktcoll As New List(Of eigentuemerItem)
        Try
            Dim SQLiteReader As SqliteDataReader
            Dim SQLiteConn As New SqliteConnection
            Dim SQLitecmd As New SqliteCommand
            SQLiteConn.ConnectionString = SQLitecnStr
            SQLiteConn.Open()
            SQLitecmd.Connection = SQLiteConn
            SQLitecmd.CommandText = sql
            SQLiteReader = SQLitecmd.ExecuteReader()
            While SQLiteReader.Read()
                Dim Lstv As New eigentuemerItem
                Lstv.buch = SQLiteReader("buch").ToString
                Lstv.adress = SQLiteReader("adress").ToString
                Lstv.gemarkung = SQLiteReader("gemarkung").ToString
                Lstv.flur = SQLiteReader("flur").ToString
                Lstv.zaehler = SQLiteReader("z").ToString
                Lstv.nenner = SQLiteReader("n").ToString
                aktcoll.Add(Lstv)
            End While
            SQLiteReader.Close()
            SQLiteConn.Close()
            l("getRecordCollection ok ")
            Return aktcoll
        Catch ex As Exception
            l("fehler  getRecordCollection " & ex.ToString)
            Return Nothing
        End Try
    End Function

    Friend Function getGemarkungen(sql As String, spalte As String) As List(Of eigentuemerItem)
        l("getGemarkungen")
        Dim colgem As New List(Of eigentuemerItem)
        Try
            Dim SQLiteReader As SqliteDataReader
            Dim SQLiteConn As New SqliteConnection
            Dim SQLitecmd As New SqliteCommand
            SQLiteConn.ConnectionString = SQLitecnStr
            SQLiteConn.Open()
            SQLitecmd.Connection = SQLiteConn
            SQLitecmd.CommandText = sql
            SQLiteReader = SQLitecmd.ExecuteReader()
            While SQLiteReader.Read()
                Dim Lstv As New eigentuemerItem
                Lstv.buch = SQLiteReader(spalte).ToString.Trim
                Lstv.adress = SQLiteReader(spalte).ToString.Trim

                colgem.Add(Lstv)
            End While
            SQLiteReader.Close()
            SQLiteConn.Close()
            l("getGemarkungen ok " & colgem.Count & " spalte: " & spalte)
            Return colgem
        Catch ex As Exception
            l("fehler  getGemarkungen fehler " & spalte & "  " & ex.ToString)
            Return Nothing
        End Try
    End Function
End Class
