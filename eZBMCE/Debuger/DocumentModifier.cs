using System;
using System.IO;
using System.Text;
using System.Windows;
using Bentley.DgnPlatformNET;
using Bentley.Interop.MicroStationDGN;
using Bentley.MstnPlatformNET;
using MessageCenter = Bentley.MstnPlatformNET.MessageCenter;

namespace eZBMCE.AddinManager
{
    /// <summary> 对文档进行配置，以启动文档的改写模式 </summary>
    public class DocumentModifier : IDisposable
    {
        #region ---   fields

        /// <summary> 当前活动的进程 </summary>
        public Application Application { get; }
        public ModelReference ModelRef { get; }

        //
        public Session Session { get; }
        public DgnFile DgnFile { get; }
        public DgnModel DgnModel { get; }
        /// <summary> 当前活动的视口 </summary>
        public Viewport ActiveViewport { get; }
        /// <summary> 消息中心 </summary>
        public readonly MessageCenter _messageCenter;

        //
        private readonly bool _openDebugerText;
        private readonly StringBuilder _debugerSb;
        private readonly TransactionPosition _transactionPosition;

        #endregion

        #region ---   构造函数
        /// <summary> 对文档进行配置，以启动文档的改写模式 </summary>
        /// <param name="openDebugerText">是否要打开一个文本调试器</param>
        public DocumentModifier(bool openDebugerText)
        {
            _openDebugerText = openDebugerText;

            Application = Bentley.MstnPlatformNET.InteropServices.Utilities.ComApp;
            ModelRef = Application.ActiveModelReference;
            _messageCenter = MessageCenter.Instance;
            //
            this.Session = Session.Instance;
            this.DgnModel = Session.GetActiveDgnModel();
            this.DgnFile = Session.GetActiveDgnFile();
            this.ActiveViewport = Session.GetActiveViewport();

            // 获得当前文档和数据库   Get the current document and database
            //acDataBase = acActiveDocument.Database;
            //acEditor = acActiveDocument.Editor;
            _transactionPosition = TransactionManager.CurrentTransactionPosition;
            ////
            //acLock = acActiveDocument.LockDocument();
            //acTransaction = acDataBase.TransactionManager.StartTransaction();
            if (openDebugerText)
            {
                _debugerSb = new StringBuilder();
            }
        }
        #endregion

        #region Transaction

        /// <summary> 回滚事务 </summary>
        public void TransactionRollBack()
        {
            TransactionManager.ReverseToPosition(_transactionPosition);
        }

        #endregion

        #region IDisposable Support

        private bool valuesDisposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!valuesDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    //acTransaction.Dispose();
                    //acLock.Dispose();

                    // 写入调试信息 并 关闭文本调试器
                    if (_openDebugerText && _debugerSb.Length > 0)
                    {
                        ShowDebugerInfo();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                valuesDisposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~DocumentModifier()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion

        #region --- Debuger Info

        /// <summary> 向文本调试器中写入数据 </summary>
        /// <param name="value"></param>
        public void WriteLineIntoDebuger(params object[] value)
        {
            if (_openDebugerText)
            {
                _debugerSb.Append(value[0]);
                for (int i = 1; i < value.Length; i++)
                {
                    _debugerSb.Append($", {value[i]}");
                }
                _debugerSb.AppendLine();
            }
        }

        /// <summary> 向文本调试器中写入多行数据 </summary>
        /// <param name="lines"></param>
        public void WriteLinesIntoDebuger(params object[] lines)
        {
            if (_openDebugerText)
            {
                foreach (var s in lines)
                {
                    _debugerSb.AppendLine(s.ToString());
                }
            }
        }

        /// <summary> 实时显示调试信息 </summary>
        /// <param name="value"></param>
        public void WriteMessageLineNow(params object[] value)
        {
            WriteMessageLineNow(false, value);
        }

        /// <summary> 在弹出的对话框中，实时显示调试信息 </summary>
        /// <param name="value"></param>
        public void WriteAlterMessageNow(params object[] value)
        {
            WriteMessageLineNow(true, value);
        }

        /// <summary> 实时显示当前正在执行的方法的名称 </summary>
        public void WriteCurrentMethodName()
        {
            System.Diagnostics.StackTrace ss = new System.Diagnostics.StackTrace(true);
            /*
               1.(new StackTrace()).GetFrame(1) // 0为本身的方法；1为调用方法
               2.(new StackTrace()).GetFrame(1).GetMethod().Name; // 方法名
               3.(new StackTrace()).GetFrame(1).GetMethod().ReflectedType.Name; // 类名
             */
            System.Reflection.MethodBase mb = ss.GetFrame(1).GetMethod();

            WriteMessageLineNow(false, mb.Name);
        }


        /// <summary> 实时显示调试信息 </summary>
        /// <param name="openAlertBox">是否要弹出一个警告对话框</param>
        /// <param name="value"></param>
        private void WriteMessageLineNow(bool openAlertBox, params object[] value)
        {
            string str;
            var sb = new StringBuilder();

            str = value[0] == null ? "TNull" : value[0].ToString();
            sb.Append(str);
            for (int i = 1; i < value.Length; i++)
            {
                str = value[i] == null ? "TNull" : value[i].ToString();
                sb.Append($", {str}");
            }

            // _messageCenter.AddMessage(sb.ToString());
            _messageCenter.ShowDebugMessage(sb.ToString(), null, openAlertBox: openAlertBox);
        }

        private void ShowDebugerInfo()
        {
            if (_openDebugerText)
            {
                _messageCenter.ShowDebugMessage("AddinManager 调试信息",
                    detailedMessage: _debugerSb.ToString(), openAlertBox: false);

                _debugerSb.Clear();
            }
        }

        #endregion

    }
}