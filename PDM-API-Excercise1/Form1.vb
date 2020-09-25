Imports EPDM.Interop.epdm
Imports EPDM.Interop.EPDMResultCode


Public Class Exercise1

    Private Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        Dim vault As IEdmVault5 = New EdmVault5()

        Dim VaultMajorVersion As Integer
        Dim VaultMinorVersion As Integer

        MsgBox("Solidworks PDM Version: " + VaultMajorVersion + "." + VaultMinorVersion)

        vault.LoginAuto(VaultsComboBox.Text, Me.Handle.ToInt32())

        MsgBox("We are " + IIf(vault.IsLoggedIn, "", " not") + " logged in to " + VaultsComboBox.Text + ".")

    End Sub

    Private Sub Exercise1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim vault As IEdmVault8 = New EdmVault5
        Dim Views() As EdmViewInfo = {}
        vault.GetVaultViews(Views, False)
        VaultsComboBox.Items.Clear()
        For Each View As EdmViewInfo In Views
            VaultsComboBox.Items.Add(View.mbsVaultName)
        Next
        If VaultsComboBox.Items.Count > 0 Then
            VaultsComboBox.Text = VaultsComboBox.Items(0)
        End If
    End Sub
End Class
