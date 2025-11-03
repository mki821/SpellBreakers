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
            UIManager.Instance.PopupUI.AddPopup<WarningPopupUI>(PopupType.Warning).SetText(response.Message);
        }
    }

    private void CompleteLogin(PacketBase packet)
    {
        LoginResponsePacket response = (LoginResponsePacket)packet;

        if (response.Success)
        {
            NetworkManager.Instance.Token = ((LoginResponsePacket)packet).IssuedToken;

            UdpConnectPacket connect = new UdpConnectPacket { Token = NetworkManager.Instance.Token };
            NetworkManager.Instance.SendUdpAsync(connect);

            gameObject.SetActive(false);
            UIManager.Instance.GetUI(UIType.Room).gameObject.SetActive(true);
        }
        else
        {
            UIManager.Instance.PopupUI.AddPopup<WarningPopupUI>(PopupType.Warning).SetText(response.Message);
        }
    }
}
