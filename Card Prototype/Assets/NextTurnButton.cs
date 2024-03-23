using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NextTurnButton : NetworkBehaviour
{
    public Button button;
    public TurnManager turnManager;
    public HandManager handManager;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        CmdEndTurn();

    }

    [Command(requiresAuthority = false)]
    public void CmdEndTurn(NetworkConnectionToClient connection = null)
    {
        EndTurn(connection);
    }

    [Server]
    private void EndTurn(NetworkConnectionToClient connection)
    {
        if (!turnManager.IsCurrentTurn(connection)) return;
        //code that handles your turn end
        turnManager.NextPlayer();

        RpcSetupInitialHand();
    }

    [ClientRpc]
    private void RpcSetupInitialHand()
    {
        // Execute SetupInitialHand() on the client
        handManager.SetupInitialHand();
    }
}
