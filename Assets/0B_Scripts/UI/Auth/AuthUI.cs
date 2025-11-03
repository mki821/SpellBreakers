public class AuthUI : UIBase
{
    private void Awake()
    {
        PacketHandler.Register(PacketId.RegisterResponse, CompleteRegister);
        PacketHandler.Register(PacketId.LoginResponse, CompleteLogin);

        if(!string.IsNullOrEmpty(NetworkManager.Instance.Token))
        {
            AutoLoginPacket login = new AutoLoginPacket
            {
                Token = NetworkManager.Instance.Token
            };

            NetworkManager.Instance.SendAsync(login);
        }
    }

    private void CompleteRegister(PacketBase packet)
    {
        RegisterResponsePacket response = (RegisterResponsePacket)packet;

        if (response.Success)
        {
            UIManager.Instance.GetUI(UIType.Register).gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIType.Login).gameObject.SetActive(true);
        }
        else
        {
            WarningPopupUI warning = UIManager.Instance.PopupUI.AddPopup(PopupType.Warning) as WarningPopupUI;
            warning.SetText(response.Message);
        }
    }

    private void CompleteLogin(PacketBase packet)
    {
        LoginResponsePacket response = (LoginResponsePacket)packet;

        if (response.Success)
        {
            ListRoomPacket list = new ListRoomPacket();
            NetworkManager.Instance.SendAsync(list);

            NetworkManager.Instance.Token = ((LoginResponsePacket)packet).IssuedToken;

            UdpConnectPacket connect = new UdpConnectPacket { Token = NetworkManager.Instance.Token };
            NetworkManager.Instance.SendUdpAsync(connect);
            
            UIManager.Instance.GetUI(UIType.Login).gameObject.SetActive(false);
        }
        else
        {
            WarningPopupUI warning = UIManager.Instance.PopupUI.AddPopup(PopupType.Warning) as WarningPopupUI;
            warning.SetText(response.Message);
        }
    }
}
