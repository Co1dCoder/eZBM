/*--------------------------------------------------------------------------------------+
|
|     $Source: MstnExamples/DgnEC/DgnECExample/Keyin.cs $
|
|  $Copyright: (c) 2015 Bentley Systems, Incorporated. All rights reserved. $
|
+--------------------------------------------------------------------------------------*/

namespace DgnECExample
{
    /*=================================================================================**//**
    * @bsiclass                                                               Bentley Systems
+===============+===============+===============+===============+===============+======*/
    public sealed class Keyin
    {

        /*---------------------------------------------------------------------------------**//**
        * Keyin to function mapping: Calls DgnECExample.FindSchemas().
        * @bsimethod                                                              Bentley Systems
+---------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdFindSchemas(string unparsed)
        {
            DgnECExample.Instance().FindSchemas();
        }

        /*---------------------------------------------------------------------------------**//**
        * Keyin to function mapping: Calls DgnECExample.FindAllInstances().
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdFindAllInstances(string unparsed)
        {
            DgnECExample.Instance().FindAllInstances();
        }

        /*---------------------------------------------------------------------------------**//**
        * Keyin to function mapping: Calls DgnECExample.ImportSchemas().
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdImportSchemas(string unparsed)
        {
            DgnECExample.Instance().ImportSchemas();
        }

        /*---------------------------------------------------------------------------------**//**
        * Keyin to function mapping: Starts write instance on element tool.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdWriteInstanceOnElement(string unparsed)
        {
            DgnECExample.Instance().WriteInstanceOnElement();
        }

        /*---------------------------------------------------------------------------------**//**
        * Keyin to function mapping: Starts find instances on element tool.
        * @bsimethod                                                              Bentley Systems
/*--------------+---------------+---------------+---------------+---------------+------*/
        public static void CmdFindAllInstancesOnElement(string unparsed)
        {
            DgnECExample.Instance().FindAllInstancesOnElement();
        }
    }
}
