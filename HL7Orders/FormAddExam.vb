Public Class FormAddExam

    Public Sub LoadData(Data As List(Of Examination))
        For Each exm As Examination In Data
            Dim lvi As ListViewItem = Me.ListViewExams.Items.Add(exm.LoincCode)
            lvi.SubItems.Add(exm.Name)
            lvi.Tag = exm
            If exm.IsPanel Then
                lvi.ImageIndex = 1
            Else
                lvi.ImageIndex = 0
            End If
        Next
    End Sub


    Private Sub FormAddExam_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class