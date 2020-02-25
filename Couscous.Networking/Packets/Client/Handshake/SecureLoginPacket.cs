using System.Threading.Tasks;
using Couscous.Game.Players;

namespace Couscous.Networking.Packets.Client.Handshake
{
    public class SecureLoginPacket : IClientPacket
    {
        private readonly PlayerHandler _playerHandler;
        
        public SecureLoginPacket(PlayerHandler playerHandler)
        {
            _playerHandler = playerHandler;
        }
        
        public async Task HandleAsync(NetworkClient client, ClientPacketReader reader)
        {
            client.Player = await _playerHandler.GetPlayerBySsoTicketAsync(reader.ReadString());

            if (client.Player == null)
            {
                return;
            }

            _playerHandler.TryRegisterPlayer(client.Player);
        }
    }
}