Module Module1
    Sub Main()
        ' Ordner und Anzahl der Differenztage (maximum)
        Dim ordner As String
        Dim differenzTage As Decimal

        Try
            ordner = My.Settings.path
            differenzTage = My.Settings.daysDiff
        Catch
            MsgBox("Fehler beim Einlesen der Konfiguration")
            Exit Sub
        End Try

        Dim zeiten As New List(Of Date)
        Dim count As Integer

        Try
            For Each items As String In IO.Directory.GetFiles(ordner)
                Dim datei = My.Computer.FileSystem.GetFileInfo(items)
                zeiten.Add(datei.CreationTime)
            Next

            ' Sortiere nach Zeit
            zeiten.Sort()
            ' Zeiten aufsteigend
            zeiten.Reverse()
            ' Für spätere Ausgabe
            count = zeiten.Count

            If count >= 2 Then
                ' Definiere neues sub(dateNeu, dateDavor)
                Dim dateNeu As Date
                Dim dateDavor As Date

                dateNeu = zeiten(0)
                dateDavor = zeiten(1)

                Console.WriteLine("Neue Datei: " & dateNeu.ToString)
                Console.WriteLine("Alte Datei: " & dateDavor.ToString)

                If (dateNeu - dateDavor).TotalDays >= Convert.ToDecimal(differenzTage) Then
                    MsgBox("Die Datensicherung ist fehlgeschlagen, Differenz von einem Tag überschritten")
                    Environment.Exit(0)
                ElseIf DateDiff(DateInterval.Day, dateNeu, Date.Now) >= differenzTage Then
                    MsgBox("Der Stand der neusten Datei hat die Tagesanzahl " & differenzTage.ToString() & " von Differenz(en) überschritten")
                End If
            Else
                Exit Sub
            End If
        Catch
            MsgBox("Fehler beim Einlesen der Dateien oder beim auslesen der Dateizeiten.")
        End Try

        ' Konsolenausgabe besser lesbar machen.
        Threading.Thread.Sleep(2.5 * 1000)

        Exit Sub
    End Sub
End Module