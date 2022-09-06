using WindowsFolderPicker = Windows.Storage.Pickers.FolderPicker;
using SharedUI.Models;

namespace DoryRiggerMaui.Windows
{
    public class FolderPicker : IFolderPicker
    {
        public async Task<string> PickFolder()
        {
            var folderPicker = new WindowsFolderPicker();

            folderPicker.FileTypeFilter.Add("*");

            // 通过传入窗口对象获取当前窗口的HWND
            var hwnd = ((MauiWinUIWindow)Application.Current.Windows[0].Handler.PlatformView).WindowHandle;

            //将HWND与文件选择器关联
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

            var result = await folderPicker.PickSingleFolderAsync();

            return result?.Path;
        }
    }
}
