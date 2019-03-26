using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bentley.Interop.MicroStationDGN;
using eZBMCE.AddinManager;

namespace eZBMCE.LocatePrimitive
{
    class LocateSelection : ILocateCommandEvents
    {
        private readonly DocumentModifier _docMdf;
        private LocateCriteria _locateCriteria;
        private Element _selElement;
        private readonly CommandState _commandState;

        public LocateSelection(DocumentModifier docMdf)
        {
            _docMdf = docMdf;
            _commandState = docMdf.Application.CommandState;
        }

        #region ---   ILocateCommandEvents 接口

        public void Accept(Element Element, ref Point3d Point, View View)
        {
            _docMdf.WriteMessageLineNow(false, "Accept", Element.Type, Point, View.Index);
        }

        public void Cleanup()
        {
            _docMdf.WriteMessageLineNow( "Cleanup");

        }

        public void Dynamics(ref Point3d Point, View View, MsdDrawingMode DrawMode)
        {
            _docMdf.WriteMessageLineNow( "Dynamics", Point.ToString(), View.Index, DrawMode);
        }

        public void LocateFailed()
        {
            _docMdf.WriteMessageLineNow( "LocateFailed");

        }

        public void LocateFilter(Element Element, ref Point3d Point, ref bool Accepted)
        {
            _docMdf.WriteMessageLineNow( "LocateFilter", Element.Type, Point, Accepted);
        }

        public void LocateReset()
        {
            _docMdf.WriteMessageLineNow( "LocateReset");
        }

        public void Start()
        {
            _docMdf.WriteMessageLineNow( "Start");
        }
        #endregion

    }
}
