﻿using FubuMVC.Navigation;

namespace FubuWorld.Website.Navigation
{
    public class FubuWorldMenu : NavigationRegistry
    {
        public FubuWorldMenu()
        {
            ForMenu(FubuWorldKeys.Main);
            Add += MenuNode.ForAction<HomeEndpoint>(FubuWorldKeys.Home, x => x.Index());
        }
    }
}