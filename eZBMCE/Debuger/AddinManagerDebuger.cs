using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using Bentley.DgnPlatformNET;
using Application = Bentley.Interop.MicroStationDGN.Application;

namespace eZBMCE.AddinManager
{
    public class AddinManagerDebuger
    {
        #region ---   执行外部命令

        public enum ExternalCmdResult
        {
            Commit,
            Cancel,
        }

        /// <param name="docMdf"></param>
        /// <returns>如果要取消操作（即将事务 Abort 掉），则返回 false，如果要提交事务，则返回 true </returns>
        public delegate ExternalCmdResult ExternalCommand(DocumentModifier docMdf);

        /// <summary> 执行外部命令，并且在执行命令之前，自动将 事务打开</summary>
        /// <param name="cmd">要执行的命令</param>
        public static void ExecuteCommand(ExternalCommand cmd)
        {
            using (DocumentModifier docMdf = new DocumentModifier(openDebugerText: true))
            {
                try
                {
                    //var impliedSelection = docMdf.acEditor.SelectImplied().Value;
                    // cmd(docMdf, impliedSelection);
                    var res = cmd(docMdf);
                    switch (res)
                    {
                        case ExternalCmdResult.Commit:
                            //docMdf.acTransaction.Commit();
                            // return ExternalCommandResult.Succeeded;
                            break;
                        case ExternalCmdResult.Cancel:
                            docMdf.TransactionRollBack();
                            break;
                        default:
                            //docMdf.acTransaction.Abort();
                            // return ExternalCommandResult.Cancelled;
                            break;
                    }
                }
                catch (System.Exception ex)
                {
                    string errorMessage = ex.Message + "\r\n\r\n" + ex.StackTrace;
                    MessageBox.Show(errorMessage, @"出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //
                }
            }
        }
        #endregion


        public static ExternalCommandResult DebugInAddinManager(ExternalCommand cmd,ref string errorMessage, ref IList<ElementId> elementSet)
        {
            var dat = new DllActivator_eZBM();
            dat.ActivateReferences();

            using (var docMdf = new DocumentModifier(true))
            {
                try
                {
                    var canCommit = cmd(docMdf);
                    //
                    switch (canCommit)
                    {
                        case ExternalCmdResult.Commit:
                            //docMdf.acTransaction.Commit();
                            return ExternalCommandResult.Succeeded;
                            break;
                        case ExternalCmdResult.Cancel:
                            docMdf.TransactionRollBack();
                            return ExternalCommandResult.Cancelled;
                            break;
                        default:
                            //docMdf.acTransaction.Abort();
                            return ExternalCommandResult.Cancelled;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    //docMdf.acTransaction.Abort(); // Abort the transaction and rollback to the previous state
                    errorMessage = ex.Message + "\r\n\r\n" + ex.StackTrace;
                    return ExternalCommandResult.Failed;
                }
            }
        }

        /// <summary>
        /// 具体的调试操作的代码模板
        /// </summary>
        /// <param name="docMdf"></param>
        /// <param name="impliedSelection"></param>
        private void DoSomethingTemplate(DocumentModifier docMdf)
        {
            //var obj = AddinManagerDebuger.PickObject<Entity>(docMdf.acEditor);
            //if (obj != null)
            //{
            //    var blkTb =
            //        docMdf.acTransaction.GetObject(docMdf.acDataBase.BlockTableId, OpenMode.ForRead) as BlockTable;
            //    var btr =
            //        docMdf.acTransaction.GetObject(blkTb[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as
            //            BlockTableRecord;

            //    var ent = new DBText();

            //    // 将新对象添加到块表记录和事务
            //    btr.AppendEntity(ent);
            //    docMdf.acTransaction.AddNewlyCreatedDBObject(ent, true);
            //}
        }
    }
}