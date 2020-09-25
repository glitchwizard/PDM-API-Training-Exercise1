Imports EPDM.Interop.epdm
Imports EPDM.Interop.EPDMResultCode


Public Class Exercise1

    Private Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        Try

            Dim vault As IEdmVault5 = New EdmVault5()
            vault.VerifyVersion(28, 0)

            Dim VaultMajorVersion As Integer
            Dim VaultMinorVersion As Integer
            vault.GetVersion(VaultMajorVersion, VaultMinorVersion)
            MsgBox("Solidworks PDM Version: " + VaultMajorVersion.ToString() + "." + VaultMinorVersion.ToString())

            vault.LoginAuto(VaultsComboBox.Text, Me.Handle.ToInt32())

            MsgBox("We are " + IIf(vault.IsLoggedIn, "", " not") + " logged in to " + VaultsComboBox.Text + "." + vbCrLf + vbCrLf +
                   "The vault root path is """ + vault.RootFolderPath + """.")

        Catch ex As Runtime.InteropServices.COMException
            Select Case ex.ErrorCode
                Case EdmResultErrorCodes_e.E_EDM_UNSUPPORTED_PROGRAM_VERSION
                    MessageBox.Show("The installed program is at a version level that is lower than the specified version.")
                Case Else
                    MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + vbCrLf + ex.Message)
            End Select
        End Try

    End Sub

    Private Sub Exercise1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

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
        Catch ex As Runtime.InteropServices.COMException
            Select Case ex.ErrorCode
                Case EdmResultErrorCodes_e.E_EDM_UNSUPPORTED_PROGRAM_VERSION
                    MessageBox.Show("The installed program is at a version level that is lower than the specified version.")
                Case Else
                    MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + vbCrLf + ex.Message)
            End Select
        End Try
    End Sub
End Class
