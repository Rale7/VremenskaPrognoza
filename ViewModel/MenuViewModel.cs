using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VremenskaPrognoza.MVVM;

namespace VremenskaPrognoza.ViewModel
{
    public class MenuViewModel
    {
        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand DragCommand {  get; }

        public MenuViewModel() {
            DragCommand = new RelayCommand(DragWindow);
            CloseCommand = new RelayCommand(CloseWindow);
            MinimizeCommand = new RelayCommand(MinimizeWindow);
            MaximizeCommand = new RelayCommand(MaximizeWindow);
        }
      
        private void DragWindow(object window)
        {
            if (window is Window wnd)
            {
                wnd.DragMove();
            }
        }

        private void CloseWindow(object window)
        {
            if (window is Window wnd)
            {
                wnd.Close();
            }
        }

        private void MinimizeWindow(object window)
        {
            if (window is Window wnd)
            {
                wnd.WindowState = WindowState.Minimized;
            }
        }
    
        private double prevLeft, prevTop, prevHeight, prevWidth;
        private bool isMaximized = false;

        private void MaximizeWindow(object window)
        {
            if (window is Window wnd)
            {
                var screen = SystemParameters.WorkArea;

                if (isMaximized)
                {
                    wnd.Left = prevLeft;
                    wnd.Top = prevTop;
                    wnd.Width = prevWidth;
                    wnd.Height = prevHeight;
                    isMaximized = false;
                }
                else
                {
                    prevLeft = wnd.Left;
                    prevTop = wnd.Top;
                    prevWidth = wnd.Width;
                    prevHeight = wnd.Height;

                    wnd.Left = screen.Left;
                    wnd.Top = screen.Top;
                    wnd.Width = screen.Width;
                    wnd.Height = screen.Height;

                    isMaximized = true;
                }
            }    
        }
    }
}
