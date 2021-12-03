


Imports System.IO

Class MainWindow
    Property ladevorgangAbgeschlossen As Boolean = False
    Property appVerzeichnis As String = ""
    Sub New()
        InitializeComponent()
    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        e.Handled = True
        Dim dirr As New clsTools
        appVerzeichnis = IO.Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "klenk")
        appVerzeichnis = Directory.GetCurrentDirectory()
        dirr.setLogfile(appVerzeichnis) : l("Start " & Now) ': l("mgisversion:" & mgisVersion)

        initGemarkung()
        l("ladevorgang fertig")
        ladevorgangAbgeschlossen = True
    End Sub

    Private Sub initGemarkung()
        l("initgemarkung")
        Dim sql As String
        sql = "select distinct gemarkung,gemarkung from dbo_ALKIS_FS2EIGENTUEMER order by gemarkung;"
        Dim collGemarkung As New List(Of eigentuemerItem)
        Dim db As New clsDB
        collGemarkung = db.getGemarkungen(sql, "gemarkung")
        cmbgemarkung.DataContext = collGemarkung
        db = Nothing
    End Sub

    Private Sub btnsuche_Click(sender As Object, e As RoutedEventArgs)
        e.Handled = True
        suchen()
    End Sub

    Private Sub suchen()
        Dim temp As String = tbNenner.Text
        If temp = String.Empty Then temp = "0"
        selectAndShow(tbGemarkung.Text, tbflur.Text, tbZaehler.Text, temp)
        dg1.Width = Me.ActualWidth - 20
        dg1.Height = Me.ActualHeight - 90
    End Sub

    Private Sub selectAndShow(gem As String, flur As String, z As String, n As String)
        l("selectAndShow")
        Dim aktcoll As New List(Of eigentuemerItem)
        Dim sql As String
        sql = "select * from dbo_ALKIS_FS2EIGENTUEMER where gemarkung='" & gem & "'  and flur=" & flur & " and z=" & z & " and n=" & n & "  ;"
        aktcoll.Clear()
        Dim db As New clsDB
        aktcoll = db.getRecordCollection(sql)
        db = Nothing
        dg1.DataContext = aktcoll
    End Sub

    Sub l(tt As String)
        My.Log.WriteEntry(tt)
    End Sub

    Private Sub dg1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        e.Handled = True
        If dg1.SelectedItem Is Nothing Then Exit Sub
        If ladevorgangAbgeschlossen = False Then Exit Sub
        Dim item As New eigentuemerItem
        l("dg1_SelectionChanged")
        Try
            item = CType(dg1.SelectedItem, eigentuemerItem)
            Dim info As String
            info = clsTools.makeText(item)
            Clipboard.SetText(info)
            MessageBox.Show(info, "Die Info befindet sich nun in der Zwischenablage.")
        Catch ex As Exception
            l("fehler dg1-selectionch " & ex.ToString)
            Exit Sub
        End Try
    End Sub


    Private Sub BtnWeb_Click(sender As Object, e As RoutedEventArgs)
        e.Handled = True
        Process.Start("https://buergergis.kreis-offenbach.de")
        e.Handled = True
    End Sub

    Private Sub resize(sender As Object, e As SizeChangedEventArgs)
        e.Handled = True
        If Me.ActualWidth > 50 Then
            dg1.Width = Me.ActualWidth - 50
            scv.Width = Me.ActualWidth - 121
        End If
        If Me.ActualHeight > 50 Then
            dg1.Height = Me.ActualHeight - 90
            scv.Height = Me.ActualHeight - 121
        End If
    End Sub

    Private Sub BtnGM_Click(sender As Object, e As RoutedEventArgs)
        e.Handled = True
        Process.Start("https://maps.google.de") ' & tbGemarkung.Text)
        e.Handled = True

    End Sub


    'https://www.google.com/maps/@50.0173935,8.8843946,483a,35y,41.42t/data=!3m1!1e3

    Private Sub cmbgemarkung_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        e.Handled = True

        If ladevorgangAbgeschlossen = False Then Exit Sub
        l("cmbgemarkung_SelectionChanged")
        Dim item As New eigentuemerItem
        Dim coll As New List(Of eigentuemerItem)
        Try
            item = CType(cmbgemarkung.SelectedItem, eigentuemerItem)
            tbGemarkung.Text = item.adress
            item.gemarkung = item.adress
            'flurcmb init
            cmbFlur.DataContext = Nothing
            coll = flurinit(item)
            cmbFlur.DataContext = coll
            tbflur.Text = ""
            tbZaehler.Text = ""
            tbNenner.Text = ""
            cmbZaehler.DataContext = Nothing
            cmbNenner.DataContext = Nothing
            cmbFlur.IsDropDownOpen = True
        Catch ex As Exception
            nachricht("fehler in cmbgemarkung_selchan:" & ex.ToString)
            Exit Sub
        End Try
    End Sub

    Private Function flurinit(item As eigentuemerItem) As List(Of eigentuemerItem)
        Dim sql As String
        Dim coll As New List(Of eigentuemerItem)
        l("flurinit")
        Try
            sql = "select distinct flur from dbo_ALKIS_FS2EIGENTUEMER where gemarkung='" & item.gemarkung & "'  "

            Dim db As New clsDB
            coll = db.getGemarkungen(sql, "flur")
            db = Nothing
            l("flurinit ok " & coll.Count)
            Return coll
        Catch ex As Exception
            nachricht("Fehler in flurinit: " & ex.ToString)
            Return Nothing
        End Try
    End Function

    Private Sub btnsuche2_Click(sender As Object, e As RoutedEventArgs)
        e.Handled = True
        l("btnsuche2_Click")
        Dim aktcoll As New List(Of eigentuemerItem)
        If tbFS.Text = String.Empty Then
            MsgBox("Bitte FS eingeben!")
            Exit Sub
        End If
        Dim sql As String
        sql = "select * from dbo_ALKIS_FS2EIGENTUEMER where FS='" & tbFS.Text & "'   ;"
        aktcoll.Clear()
        Dim db As New clsDB
        aktcoll = db.getRecordCollection(sql)
        db = Nothing
        dg1.DataContext = aktcoll
        dg1.Width = Me.ActualWidth - 20
        dg1.Height = Me.ActualHeight - 90
        l("btnsuche2_Click ok")
    End Sub

    Private Sub Btn3d_Click(sender As Object, e As RoutedEventArgs)
        e.Handled = True
        Process.Start("https://www.google.com/maps/@50.0173935,8.8843946,483a,35y,41.42t/data=!3m1!1e3") ' & tbGemarkung.Text)
        e.Handled = True
    End Sub

    Private Sub cmbFlur_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        e.Handled = True
        If ladevorgangAbgeschlossen = False Then Exit Sub
        l("cmbFlur_SelectionChanged ")
        Dim item As New eigentuemerItem
        Dim collgemarkung As New List(Of eigentuemerItem)
        Try
            item = CType(cmbFlur.SelectedItem, eigentuemerItem)
            tbflur.Text = item.adress
            'flurcmb init
            item.gemarkung = tbGemarkung.Text
            item.flur = item.adress
            cmbZaehler.DataContext = Nothing
            collgemarkung = zaehlerinit(item)
            'tbZaehler.Text = ""
            'tbNenner.Text = ""
            cmbZaehler.DataContext = collgemarkung

            cmbNenner.DataContext = Nothing
            cmbZaehler.IsDropDownOpen = True
            l("cmbFlur_SelectionChanged ok")
        Catch ex As Exception
            nachricht("fehler in cmbFlur_SelectionChanged : " & ex.ToString)
            Exit Sub
        End Try
    End Sub
    Private Function zaehlerinit(item As eigentuemerItem) As List(Of eigentuemerItem)
        Dim sql As String
        l("zaehlerinit ")
        Try
            sql = "select distinct z from dbo_ALKIS_FS2EIGENTUEMER where gemarkung='" & item.gemarkung & "' and " &
                " flur=" & item.flur
            Dim collGemarkung As New List(Of eigentuemerItem)
            Dim db As New clsDB
            collGemarkung = db.getGemarkungen(sql, "z")
            db = Nothing
            l("zaehlerinit ok")
            Return collGemarkung
        Catch ex As Exception
            nachricht("fehler in zaehlerinit " & ex.ToString)
            Return Nothing
        End Try
    End Function

    Private Sub tbNenner_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tbNenner.TextChanged

    End Sub

    Private Sub cmbZaehler_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        e.Handled = True
        l("cmbZaehler_SelectionChanged ")
        If ladevorgangAbgeschlossen = False Then Exit Sub
        If cmbZaehler.SelectedItem Is Nothing Then
            tbZaehler.Text = ""
            Exit Sub
        End If
        Dim item As New eigentuemerItem
        Dim coll As New List(Of eigentuemerItem)
        Try
            item = CType(cmbZaehler.SelectedItem, eigentuemerItem)
            tbZaehler.Text = item.adress
            'flurcmb init
            item.gemarkung = tbGemarkung.Text
            item.flur = tbflur.Text
            item.zaehler = item.adress

            coll = nennerinit(item)
            cmbNenner.DataContext = coll
            cmbNenner.IsDropDownOpen = True
            l("cmbZaehler_SelectionChanged ok")
        Catch ex As Exception
            nachricht("fehler in cmbZaehler_SelectionChanged " & ex.ToString)
            Exit Sub
        End Try
    End Sub

    Private Function nennerinit(item As eigentuemerItem) As List(Of eigentuemerItem)
        Dim sql As String
        l("nennerinit ")
        Try
            sql = "select distinct n from dbo_ALKIS_FS2EIGENTUEMER where gemarkung='" & item.gemarkung & "' and " &
                " flur=" & item.flur & " and " &
                " z=" & item.zaehler
            Dim collGemarkung As New List(Of eigentuemerItem)
            Dim db As New clsDB
            collGemarkung = db.getGemarkungen(sql, "n")
            db = Nothing
            l("nennerinit ok")
            Return collGemarkung
        Catch ex As Exception
            nachricht("fehler in nennerinit" & ex.ToString)
            Return Nothing
        End Try
    End Function

    Private Sub cmbNenner_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        e.Handled = True
        l("cmbNenner_SelectionChanged")
        If ladevorgangAbgeschlossen = False Then Exit Sub
        If cmbNenner.SelectedItem Is Nothing Then
            tbNenner.Text = ""
            Exit Sub
        End If
        Dim item As New eigentuemerItem
        Try
            item = CType(cmbNenner.SelectedItem, eigentuemerItem)
            tbNenner.Text = item.adress

            suchen()
        Catch ex As Exception
            nachricht("fehler in cmbNenner_SelectionChanged " & ex.ToString)
            Exit Sub
        End Try
    End Sub

    Private Sub btnLogfilesdir_Click(sender As Object, e As RoutedEventArgs)
        e.Handled = True
        Process.Start(appVerzeichnis)
    End Sub

    Private Sub Btngp_Click(sender As Object, e As RoutedEventArgs)
        e.Handled = True
        Process.Start("https://www.geoportal.hessen.de/map?LAYER[visible]=1&LAYER[querylayer]=1&LAYER=1&LAYER=38415&LAYER=0&LAYER=0") ' & tbGemarkung.Text)
        e.Handled = True
    End Sub
    'https://www.google.com/maps/@50.0173935,8.8843946,483a,35y,41.42t/data=!3m1!1e3

End Class
