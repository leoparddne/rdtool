using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace RDTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //注册开始和退出事件
            Startup += App_Startup;
            Exit += App_Exit;
        }


        private void App_Startup(object sender, StartupEventArgs e)
        {
            //UI线程未捕获异常处理事件
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            //程序退出时需要处理的业务
        }

        /// <summary>
        /// 提取异常信息
        /// </summary>
        private static string ExceptionToString(Exception ex, string info)
        {
            StringBuilder str = new StringBuilder($"{DateTime.Now}, {info}发生了一个错误！{Environment.NewLine}");
            if (ex.InnerException == null)
            {
                str.Append($"【对象名称】：{ex.Source}{Environment.NewLine}");
                str.Append($"【异常类型】：{ex.GetType().Name}{Environment.NewLine}");
                str.Append($"【详细信息】：{ex.Message}{Environment.NewLine}");
                str.Append($"【堆栈调用】：{ex.StackTrace}");
            }
            else
            {
                str.Append($"【对象名称】：{ex.InnerException.Source}{Environment.NewLine}");
                str.Append($"【异常类型】：{ex.InnerException.GetType().Name}{Environment.NewLine}");
                str.Append($"【详细信息】：{ex.InnerException.Message}{Environment.NewLine}");
                str.Append($"【堆栈调用】：{ex.InnerException.StackTrace}");
            }
            return str.ToString();
        }

        /// <summary>
        /// UI线程未捕获异常处理函数
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true;
                string msg = ExceptionToString(e.Exception, "UI线程");
                HandyControl.Controls.Growl.ErrorGlobal(msg);

            }
            catch (Exception ex)
            {
                string msg = ExceptionToString(ex, "UI线程 处理函数");
                HandyControl.Controls.Growl.ErrorGlobal(msg);

            }
        }

        /// <summary>
        /// Task线程内未捕获异常处理函数
        /// </summary>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                string msg = ExceptionToString(e.Exception, "Task线程");
                HandyControl.Controls.Growl.ErrorGlobal(msg);

                e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
            }
            catch (Exception ex)
            {
                string msg = ExceptionToString(ex, "Task线程 处理函数");
                HandyControl.Controls.Growl.ErrorGlobal(msg);
            }
        }

        /// <summary>
        /// 非UI线程未捕获异常处理函数
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                string msg;
                if (e.ExceptionObject is Exception ex)
                {
                    msg = ExceptionToString(ex, "非UI线程");
                }
                else
                {
                    msg = $"发生了一个错误！信息:{e.ExceptionObject}";
                }
                HandyControl.Controls.Growl.ErrorGlobal(msg);
            }
            catch (Exception ex)
            {
                string msg = ExceptionToString(ex, "非UI线程 处理函数");
                HandyControl.Controls.Growl.ErrorGlobal(msg);
            }
        }
    }
}
