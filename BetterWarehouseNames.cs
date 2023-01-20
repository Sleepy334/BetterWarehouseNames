using ICities;

namespace BetterWarehouseNames
{
    public class TransferManagerMain : IUserMod
	{
		private static string Version = "v0.1";

#if DEBUG
        private static string Config => " [DEBUG]";
#else
		private static string Config => "";
#endif
        public static string ModName => $"Better Warehouse Names {Version}{Config}"; 
		public static string Title => $"Better Warehouse Names {Version}{Config}";

		public static bool IsEnabled = false;

		public string Name
		{
			get { return ModName; }
		}

		public string Description
		{
			get { return "Better names for warehouse storage resources."; }
		}

		public void OnEnabled()
		{
			IsEnabled = true;

			Patcher.PatchAll();
		}

		public void OnDisabled()
		{
			IsEnabled = false;
            Patcher.UnpatchAll();
        }

		// Sets up a settings user interface
		public void OnSettingsUI(UIHelper helper)
		{
			// TODO
		}
    }
}